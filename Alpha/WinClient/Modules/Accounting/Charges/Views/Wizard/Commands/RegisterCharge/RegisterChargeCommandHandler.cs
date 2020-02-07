using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.Alpha.Infrastructure.Interface.ExtensionMethods;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterChargeCommandHandler : ICommandHandler<RegisterChargeCommand>
    {
        private readonly ICache _cache = new Cache();
        private readonly PaymentDistributionService _pds;
        private ICommandDispatcher _dispatcher;

        public RegisterChargeCommandHandler(PaymentDistributionService pds, ICommandDispatcher dispatcher)
        {
            _pds = pds;
            _dispatcher = dispatcher;
        }

        public void Execute(RegisterChargeCommand cmd)
        {
            cmd.Result = new RegisterCommandResult();

            _cache.Init(cmd.Period);

            int _chargeSetId = 2056;

            int[] _customers;
            int[] _currentCustomers = { 34439, 34440, 34441, 34442, 34443, 34444, 34445, 34446, 34447, 34448, 34449, 34450, 34451, 34452, 34453, 34454, 34455, 34456, 34457, 34458, 34459, 34460, 34461, 34462, 34463, 34464, 34465, 34466, 34467, 34468, 34469, 34470, 34471, 34472, 34473, 34474, 34475, 34476, 34477, 34478, 34479, 34480, 34481, 34482, 34483, 34484, 34485, 34486, 34487, 34488, 34489, 34490, 34491, 34492, 34493, 34494, 34495, 34496, 34497, 34498, 34499, 34500, 34501, 34502, 34503, 34504, 34505, 34506, 34507, 34508, 34509, 34510, 34511, 34512, 34513, 34514, 34515, 34516, 34517, 34518, 34519, 34520, 34521, 49618 };
            using (Entities _db = new Entities())
            {
                _customers = _db.Customers.Where(c => _currentCustomers.Contains(c.ID)).Select(c => c.ID).ToArray();
            }

            cmd.ResetProgressBar(_customers.Length);

            for (int i = 0; i < _customers.Length; i++)
            {
                int _customerId = _customers[i];

                using (var _db = new Entities())
                {
                    _db.CommandTimeout = 3600;
                    try
                    {
                        var _chargeSet = _db.ChargeSets.First(x => x.ID == _chargeSetId);

                        var _periodBalance = _db
                            .GetCustomerBalancesGroupedByPeriod(_customerId, beforeGroupFilter: x => x.Period == cmd.Period)
                            .Values
                            .FirstOrDefault() ?? new Dictionary<int, Balance>();
                        var _services = _db.Services.ToDictionary(x => x.ID);
                        var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                        var _dbCustomerStub = new Customers { ID = _customerId };
                        _db.Customers.Attach(_dbCustomerStub);
                        CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerId, cmd.Period);

                        var _calculateChargesCommand =
                            new CalculateChargeCommand
                            {
                                Cache = _cache,
                                CustomerInfo = _customerInfo
                            };
                        _dispatcher.Execute(_calculateChargesCommand);
                        _periodBalance.AddCharges(
                            _calculateChargesCommand.Result
                                .Join(_customerInfo.Poses,
                                    x => x.Key,
                                    y => y.Id,
                                    (x, y) =>
                                    new
                                    {
                                        y.ServiceId,
                                        x.Value
                                    })
                                .ToDictionary(x => x.ServiceId, x => x.Value));

                        var _calculateBenefitsCommand =
                            new CalculateBenefitsCommand
                            {
                                CustomerInfo = _customerInfo
                            };
                        _dispatcher.Execute(_calculateBenefitsCommand);
                        _periodBalance.AddBenefits(
                            _calculateBenefitsCommand.Result
                                .Join(_customerInfo.Poses,
                                    x => x.Key,
                                    y => y.Id,
                                    (x, y) =>
                                    new
                                    {
                                        y.ServiceId,
                                        x.Value
                                    })
                                .ToDictionary(x => x.ServiceId, x => x.Value));

                        var _createChargeOperCommand =
                            new CreateChargeCommand
                            {
                                BenefitsByPos = _calculateBenefitsCommand.Result,
                                ChargesByPos = _calculateChargesCommand.Result,
                                ChargeSet = _chargeSet,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Now = cmd.Now,
                                Period = cmd.Period,
                                Services = _services,
                                Contractors = _contractors,
                                Db = _db
                            };
                        _dispatcher.Execute(_createChargeOperCommand);

                        _dispatcher.Execute(
                            new CreateOverpaymentCommand
                            {
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                DbCustomerStub = _dbCustomerStub,
                                Period = cmd.Period,
                                LastChargedPeriod = cmd.LastChargedPeriod,
                                Services = _services,
                                Db = _db
                            });

                        _dispatcher.Execute(
                            new CreateBillCommand
                            {
                                BillSetId = 28493,
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                Contractors = _contractors,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Db = _db
                            });

                        _db.SaveChanges();
                        cmd.Result.Processed++;
                        cmd.Result.Total += _createChargeOperCommand.Result.Value;

                        cmd.ProgressAction(1);
                    }
                    catch (Exception ex)
                    {
                        cmd.Result.Errors++;
                        Logger.SimpleWrite($"RegisterChargeCommand. CustomerId: {_customerId}\r\nException: {ex}");
                    }
                }
            }
        }

        private int CreateChargeSet(DateTime now, DateTime period, int authorId)
        {
            int _chargeSetId;

            using (Entities _db = new Entities())
            {
                Users _user = new Users { ID = authorId };
                _db.Users.Attach(_user);

                var _chargeSet =
                    new ChargeSets
                    {
                        CreationDateTime = now,
                        Period = period,
                        Number = _db.ChargeSets.Any() ? _db.ChargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };

                _db.ChargeSets.AddObject(_chargeSet);
                _db.SaveChanges();
                _chargeSetId = _chargeSet.ID;
            }

            return _chargeSetId;
        }

        private Dictionary<int, int> CreateBillSets(DateTime now)
        {
            var _result = new Dictionary<int, int>();

            using (Entities _db = new Entities())
            {
                var _zipCodes = _db.Buildings
                    .GroupBy(x => x.ZipCode)
                    .Select(g =>
                        new
                        {
                            ZipCode = g.Key,
                            BuildingIds = g.Select(x => x.ID)
                        })
                    .ToList();

                foreach (var _zc in _zipCodes)
                {
                    BillSets _billSet =
                        new BillSets()
                        {
                            CreationDateTime = now,
                            Number = _db.BillSets.Any() ? _db.BillSets.Max(c => c.Number) + 1 : 1,
                            BillType = (byte)BillType.Regular,
                        };
                    _db.AddToBillSets(_billSet);
                    _db.SaveChanges();

                    foreach (int _id in _zc.BuildingIds)
                    {
                        _result.Add(_id, _billSet.ID);
                    }
                }
            }

            return _result;
        }
    }
}
