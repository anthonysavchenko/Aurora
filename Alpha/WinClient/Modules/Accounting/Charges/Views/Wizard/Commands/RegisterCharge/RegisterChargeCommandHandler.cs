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

            DateTime _lastChargedPeriod = new DateTime(2019, 7, 1, 0, 0, 0);
            int[] _chargeSets = { 1050, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1065, 1066, 1067, 1068, 1069,
                1070, 1071, 1072, 1073, 1074, 1075, 1076, 1077, 1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086 };
            int[] _billSets   = { 7369, 7461, 7545, 7662, 7824, 7935, 8054, 8169, 8242, 8367, 8497, 8610, 8698, 8770, 8885, 8982, 9117, 9236, 9389, 9472,
                9564, 9668, 9797, 9880, 10030, 10191, 10333, 10475, 10652, 10812, 10916, 11001, 11122, 11206, 11291, 11362, 11425 };
            int _count = 0;

            cmd.ResetProgressBar(_chargeSets.Length);

            for (DateTime _currentPeriod = new DateTime(2016, 7, 1, 0, 0, 0); _currentPeriod <= _lastChargedPeriod; _currentPeriod.AddMonths(1))
            {
                _cache.Init(_currentPeriod);

                int _chargeSetId = _chargeSets[_count];
                int _billSetId = _billSets[_count];
                int _customerId = 48720;

                using (var _db = new Entities())
                {
                    _db.CommandTimeout = 3600;
                    try
                    {
                        var _chargeSet = _db.ChargeSets.First(x => x.ID == _chargeSetId);

                        var _periodBalance = _db
                            .GetCustomerBalancesGroupedByPeriod(_customerId, beforeGroupFilter: x => x.Period == _currentPeriod)
                            .Values
                            .FirstOrDefault() ?? new Dictionary<int, Balance>();
                        var _services = _db.Services.ToDictionary(x => x.ID);
                        var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                        var _dbCustomerStub = new Customers { ID = _customerId };
                        _db.Customers.Attach(_dbCustomerStub);
                        CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerId, _currentPeriod);

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
                                Period = _currentPeriod,
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
                                Period = _currentPeriod,
                                LastChargedPeriod = _currentPeriod.AddMonths(-1),
                                Services = _services,
                                Db = _db
                            });

                        _dispatcher.Execute(
                            new CreateBillCommand
                            {
                                BillSetId = _billSetId,
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

                _count++;
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
