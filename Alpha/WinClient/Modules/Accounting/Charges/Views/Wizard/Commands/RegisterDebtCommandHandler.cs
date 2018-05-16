using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterDebtCommandHandler : ICommandHandler<RegisterDebtCommand>
    {
        private readonly IServerTimeService _sts;

        public RegisterDebtCommandHandler(IServerTimeService sts)
        {
            _sts = sts;
        }

        public void Execute(RegisterDebtCommand command)
        {
            int _failCount = 0;
            int _processed = 0;
            decimal _totalValue = 0;

            DateTime _now = _sts.GetDateTimeInfo().Now;
            PeriodInfo _periodInfo = _sts.GetPeriodInfo();
            int _rechargeId

            ICommandDispatcher _dispatcher;

            var _parseDebtFileCommand = new ParseDebtFileCommand { DebtFile = command.File };
            _dispatcher.Execute(_parseDebtFileCommand);

            using (Entities _db = new Entities())
            {
                if (_parseDebtFileCommand.Result.Count > 0)
                {
                    var _user = new Users { ID = int.Parse(UserHolder.User.ID) };
                    _db.Users.Attach(_user);

                    RechargeSets _rechargeSet =
                        new RechargeSets
                        {
                            CreationDateTime = _now,
                            Period = _periodInfo.FirstUncharged,
                            Number = _db.RechargeSets.Any() ? _db.RechargeSets.Max(c => c.Number) + 1 : 1,
                            Author = _user
                        };
                    _db.AddToRechargeSets(_rechargeSet);
                    _db.SaveChanges();
                }
            }

            command.ResetProgressBar(_parseDebtFileCommand.Result.Keys.Count);

            try
            {
                foreach (KeyValuePair<int, decimal> _pair in _customerDebt)
                {
                    int _customerID = _pair.Key;
                    decimal _debt = _pair.Value;

                    using (Entities _db = new Entities())
                    {
                        CustomerInfo customerInfo = _db.GetCustomerInfo(_customerID, );

                        var _calculateChargeCommand =
                            new CalculateChargeCommand
                            {
                                CustomerInfo
                            };

                        try
                        {
                            Customers _dbCustomer = new Customers { ID = _customerID };
                            _db.Customers.Attach(_dbCustomer);

                            var _customer =
                                _db.Customers
                                    .Where(c => c.ID == _customerID)
                                    .Select(c =>
                                        new
                                        {
                                            c.ID,
                                            c.Square,
                                            c.Account,
                                            BuildingID = c.Buildings.ID,
                                            BuildingNonResidentialPlaceArea = c.Buildings.NonResidentialPlaceArea,
                                            ResidentsCount = c.Residents.Count(),
                                            FederalBenefitResidentsCount = c.Residents
                                                    .Count(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule == 0),
                                            LocalBenefitCoefficient = c.Residents
                                                    .Where(resident => resident.BenefitTypes != null && resident.BenefitTypes.BenefitRule != 0)
                                                    .Max(resident => resident.BenefitTypes.FixedPercent) ?? 0,
                                        })
                                    .First();

                            IList<CustomerPosValue> _distribution = new List<CustomerPosValue>();

                            var _customerPoses =
                                 _db.CustomerPoses
                                     .Where(p =>
                                         p.Customers.ID == _customer.ID &&
                                         p.Since <= _rechargeSet.Period &&
                                         p.Till >= _rechargeSet.Period)
                                     .Select(p =>
                                         new CustomerPosInfo
                                         {
                                             ID = p.ID,
                                             ServiceID = p.Services.ID,
                                             ContractorID = p.Contractors.ID,
                                             ChargeRule = p.Services.ChargeRule,
                                             Rate = p.Rate
                                         })
                                     // Необходимо для вычисления банковской комиссии расходов по сод. общ. им. после вычисления суммы начисления самих расходов
                                     .OrderBy(p => p.ChargeRule)
                                     .ToList();

                            Dictionary<int, Services> _services =
                                _db.Services
                                    .Include(s => s.ServiceTypes)
                                    .ToDictionary(s => s.ID, s => s);

                            Dictionary<int, Contractors> _contractors = _db.Contractors
                                    .ToDictionary(
                                        contractor => contractor.ID,
                                        contractor => contractor);

                            if (_customerPoses.Any())
                            {
                                foreach (var _customerPos in _customerPoses)
                                {
                                    decimal _value = 0;

                                    //Перенести правило начисления по услуге в тип услуги
                                    switch ((Service.ChargeRuleType)_customerPos.ChargeRule)
                                    {
                                        case Service.ChargeRuleType.SquareRate:
                                            _value = _customerPos.Rate * _customer.Square;
                                            break;
                                        case Service.ChargeRuleType.ResidentsRate:
                                            _value = _customerPos.Rate * _customer.ResidentsCount;
                                            break;
                                        case Service.ChargeRuleType.CounterRate:
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceAreaRate:
                                            {
                                                decimal _livingArea =
                                                    _db.Customers
                                                        .Where(c =>
                                                            c.Buildings.ID == _customer.BuildingID &&
                                                            c.CustomerPoses.Any(p => p.Till >= _period))
                                                        .Sum(c => (decimal?)c.Square) ?? 0;

                                                decimal _area = _livingArea + _customer.BuildingNonResidentialPlaceArea;

                                                PublicPlaces _pp = _db.PublicPlaces
                                                    .FirstOrDefault(pp =>
                                                        pp.ServiceID == _customerPos.ServiceID && pp.BuildingID == _customer.BuildingID);

                                                decimal? _norm = _services[_customerPos.ServiceID].Norm;

                                                if (_pp != null && _norm.HasValue && _area > 0)
                                                {
                                                    decimal _rate = Math.Round(_norm.Value * _pp.Area / _area * _customerPos.Rate, 2, MidpointRounding.AwayFromZero);
                                                    _value = _customer.Square * _rate;
                                                }
                                            }
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceVolumeAreaRate:
                                            {
                                                decimal _livingArea =
                                                    _db.Customers
                                                        .Where(c =>
                                                            c.Buildings.ID == _customer.BuildingID &&
                                                            c.CustomerPoses.Any(p => p.Till >= _period))
                                                        .Sum(c => (decimal?)c.Square) ?? 0;
                                                decimal _area = _livingArea + _customer.BuildingNonResidentialPlaceArea;

                                                decimal _volume = _db.PublicPlaceServiceVolumes
                                                    .Where(x => x.BuildingID == _customer.BuildingID && x.ServiceID == _customerPos.ServiceID && x.Period == _period)
                                                    .Select(x => x.Volume)
                                                    .FirstOrDefault();

                                                decimal _rate =
                                                    Math.Round(_volume / _area * _customerPos.Rate, 2, MidpointRounding.AwayFromZero);
                                                _value = _customer.Square * _rate;
                                            }
                                            break;

                                        case Service.ChargeRuleType.PublicPlaceBankCommission:
                                            {
                                                decimal _publicPlaceAreaRateSum = _customerPoses
                                                    .Where(p => p.ChargeRule == (byte)Service.ChargeRuleType.PublicPlaceAreaRate)
                                                    .Sum(p => p.Rate);
                                                decimal _rate = Math.Round(_publicPlaceAreaRateSum * _customerPos.Rate / 100, 2, MidpointRounding.AwayFromZero);
                                                _value = _rate * _customer.Square;
                                                // Заменяем тариф для внесения в квитанцию 
                                                _customerPos.Rate = _rate;
                                            }
                                            break;
                                        case Service.ChargeRuleType.FixedRate:
                                        default:
                                            _value = _customerPos.Rate;
                                            break;
                                    }

                                    if (_value > 0)
                                    {
                                        _distribution.Add(
                                            new CustomerPosValue
                                            {
                                                CustomerPos = _customerPos,
                                                Value = _value
                                            });
                                    }
                                }

                                decimal _distributionSum = _distribution.Sum(p => p.Value);

                                if (_distributionSum > 0)
                                {
                                    decimal _coefficient = _debt / _distributionSum;

                                    _distributionSum = 0;

                                    foreach (CustomerPosValue _customerPosValue in _distribution)
                                    {
                                        _customerPosValue.Value = _coefficient * _customerPosValue.Value;

                                        _distributionSum += _customerPosValue.Value;

                                        if (_distributionSum > _debt)
                                        {
                                            _customerPosValue.Value -= _distributionSum - _debt;
                                            _distributionSum = _debt;
                                        }
                                    }

                                    if (_distributionSum < _debt)
                                    {
                                        _distribution.First().Value += _debt - _distributionSum;
                                        _distributionSum = _debt;
                                    }

                                    #region Дополнительные начисления

                                    _rechargeSet = (RechargeSets)_db.GetObjectByKey(new EntityKey("Entities.RechargeSets", "ID", _rechargeSet.ID));

                                    RechargeOpers _rechargeOper =
                                        new RechargeOpers
                                        {
                                            RechargeSets = _rechargeSet,
                                            CreationDateTime = _now,
                                            Customers = _dbCustomer,
                                            Value = Math.Round(_distributionSum, 2, MidpointRounding.AwayFromZero)
                                        };
                                    _db.AddToRechargeOpers(_rechargeOper);

                                    foreach (CustomerPosValue _customerPosValue in _distribution)
                                    {
                                        RechargeOperPoses _pos = new RechargeOperPoses
                                        {
                                            Services = _services[_customerPosValue.CustomerPos.ServiceID],
                                            Contractors = _contractors[_customerPosValue.CustomerPos.ContractorID],
                                            RechargeOpers = _rechargeOper,
                                            Value = Math.Round(_customerPosValue.Value, 2, MidpointRounding.AwayFromZero)
                                        };
                                        _db.AddToRechargeOperPoses(_pos);
                                        _posesToRegister.Add(_pos);
                                    }

                                    _db.SaveChanges();

                                    _rechargeSet.Quantity++;
                                    _rechargeSet.ValueSum += _rechargeOper.Value;

                                    _db.SaveChanges();

                                    _totalValue = _rechargeSet.ValueSum;

                                    _posesToRegister.Clear();

                                    #endregion
                                }

                                _processed++;
                            }
                            else
                            {
                                Logger.SimpleWrite($"Отсутствуют услуги у абонента с л/с {_customer.Account}");
                                _failCount++;
                            }
                        }
                        catch (Exception _ex)
                        {
                            Logger.SimpleWrite($"Абонент {_customerID}. Exception: {_ex}");
                            _failCount++;
                        }
                    }

                    View.AddProgress(1);
                    Application.DoEvents();
                }

                View.ResultCount = _processed;
                View.ResultValue = _totalValue;
                View.ResultErrorCount = _failCount;
            }
            catch (Exception _ex)
            {
                View.ShowMessage("Импорт долгов не выполнен", "Ошибка операции");
                Logger.SimpleWrite($"Debts import error: {_ex}");
            }
        }
    }
}
