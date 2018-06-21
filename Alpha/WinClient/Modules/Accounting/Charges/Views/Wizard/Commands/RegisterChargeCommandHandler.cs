using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterChargeCommandHandler : ICommandHandler<RegisterChargeCommand>
    {
        private readonly Cache _cache = new Cache();
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

            var _createChargeSetCommand =
                new CreateChargeSetCommand
                {
                    Now = cmd.Now,
                    Period = cmd.Period
                };
            _dispatcher.Execute(_createChargeSetCommand);
            int _chargeSetId = _createChargeSetCommand.Result;

            var _createBillSetsCommand =
                new CreateBillSetsCommand
                {
                    CreationDateTime = cmd.Now,
                };
            _dispatcher.Execute(_createBillSetsCommand);
            Dictionary<int, int> _billSetByBuilding = _createBillSetsCommand.Result;

            int[] _customers;
            using (Entities _db = new Entities())
            {
                _customers = _db.Customers.Select(c => c.ID).ToArray();
            }

            for (int i = 0; i < _customers.Length; i++)
            {
                int _customerId = _customers[i];

                using (Entities _db = new Entities())
                {
                    _db.CommandTimeout = 3600;
                    try
                    {
                        var _chargeSet = _db.ChargeSets.First(x => x.ID == _chargeSetId);

                        var _periodBalance = _db
                            .GetCustomerBalancesGroupedByPeriod(_customerId, x => x.Period == cmd.Period)
                            .Values
                            .FirstOrDefault() ?? new Dictionary<int, Balance>();
                        var _services = _db.Services.Include(x => x.ServiceTypes).ToDictionary(x => x.ID);
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
                        _periodBalance.AddCharges(_calculateChargesCommand.Result);

                        var _calculateBenefitsCommand =
                            new CalculateBenefitsCommand
                            {
                                CustomerInfo = _customerInfo
                            };
                        _dispatcher.Execute(_calculateBenefitsCommand);
                        _periodBalance.AddBenefits(_calculateBenefitsCommand.Result);

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
                                BillSetId = _billSetByBuilding[_customerInfo.BuildingId],
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                Contractors = _contractors,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Services = _services,
                                Db = _db
                            });

                        _db.SaveChanges();
                        cmd.Result.Processed++;
                        cmd.Result.Total += _createChargeOperCommand.Result.Value;

                        cmd.ProgressAction(1);
                    }
                    catch (Exception ex)
                    {
                        Logger.SimpleWrite($"RegisterChargeCommand. Exception: {ex}");
                    }
                }
            }
        }
    }
}
