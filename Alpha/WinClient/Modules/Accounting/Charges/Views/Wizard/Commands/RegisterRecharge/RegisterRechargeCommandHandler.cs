using System;
using System.Linq;
using System.Data.Entity;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterRechargeCommandHandler : ICommandHandler<RegisterRechargeCommand>
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly ICache _cache = new Cache();

        public RegisterRechargeCommandHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Execute(RegisterRechargeCommand cmd)
        {
            cmd.Result = new RegisterCommandResult();

            DateTime _period = cmd.Since;

            int _monthCount = (_period.Year == cmd.Till.Year
                    ? cmd.Till.Month - _period.Month
                    : 12 - _period.Month + 12 * (cmd.Till.Year - _period.Year - 1) + cmd.Till.Month) + 1; 

            cmd.ResetProgressBar(cmd.CustomerIds.Length * _monthCount);

            var _createRechargeSetCommand =
                new CreateRechargeSetCommand
                {
                    AuthorId = cmd.AuthorId,
                    Now = cmd.Now,
                    Period = cmd.Period
                };
            _dispatcher.Execute(_createRechargeSetCommand);
            int _rechargeSetId = _createRechargeSetCommand.Result;

            while (_period <= cmd.Till)
            {
                _cache.Init(_period);

                for (int i = 0; i < cmd.CustomerIds.Length; i++)
                {
                    using (var _db = new Entities())
                    {
                        _db.CommandTimeout = 3600;
                        int _customerID = cmd.CustomerIds[i];

                        try
                        {
                            var _rechargeSet = _db.RechargeSets.First(x => x.ID == _rechargeSetId);

                            var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                            var _services = _db.Services.ToDictionary(x => x.ID);

                            CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerID, _period);

                            Customers _dbCustomer = new Customers { ID = _customerID };
                            _db.Customers.Attach(_dbCustomer);

                            var _calculateChargesCommand =
                                new CalculateChargeCommand
                                {
                                    Cache = _cache,
                                    CustomerInfo = _customerInfo,
                                };
                            _dispatcher.Execute(_calculateChargesCommand);

                            if (cmd.ServicePercentCorrectionByCustomer != null)
                            {
                                _dispatcher.Execute(
                                    new ApplyPercentCorrectionCommand
                                    {
                                        Period = _period,
                                        ChargesByPos = _calculateChargesCommand.Result,
                                        CustomerInfo = _customerInfo,
                                        ServicePercentCorrection = cmd.ServicePercentCorrectionByCustomer.ContainsKey(_customerID)
                                            ? cmd.ServicePercentCorrectionByCustomer[_customerID]
                                            : null,
                                        Db = _db
                                    });
                            }

                            var _calculateBenefitsCommand =
                                new CalculateBenefitsCommand
                                {
                                    CustomerInfo = _customerInfo
                                };
                            _dispatcher.Execute(_calculateBenefitsCommand);

                            ChargeOpers _chargeOper =
                                _db.ChargeOpers
                                    .Include(c => c.ChargeCorrectionOpers)
                                    .FirstOrDefault(c =>
                                        c.Customers.ID == _customerInfo.Id &&
                                        c.ChargeSets.Period == _period);

                            ChargeCorrectionOpers _chargeCorrectionOper = null;
                            if (_chargeOper != null)
                            {
                                var _correctionCommand =
                                    new CreateChargeCorrectionCommand
                                    {
                                        Contractors = _contractors,
                                        Services = _services,
                                        ChargeOper = _chargeOper,
                                        Period = cmd.Period,
                                        CustomerInfo = _customerInfo,
                                        Now = cmd.Now,
                                        Db = _db
                                    };
                                _dispatcher.Execute(_correctionCommand);
                                _chargeCorrectionOper = _correctionCommand.Result;
                            }

                            _dispatcher.Execute(
                                new CreateRechargeCommand
                                {
                                    BenefitsByPos = _calculateBenefitsCommand.Result,
                                    ChargesByPos = _calculateChargesCommand.Result,
                                    ChargeCorrectionOper = _chargeCorrectionOper,
                                    ChargeOper = _chargeOper,
                                    Contractors = _contractors,
                                    Services = _services,
                                    CustomerInfo = _customerInfo,
                                    DbCustomer = _dbCustomer,
                                    Now = cmd.Now,
                                    RechargeSet = _rechargeSet,
                                    Db = _db
                                });

                            _db.SaveChanges();

                            cmd.Result.Total = _rechargeSet.ValueSum;
                            cmd.Result.Processed = _rechargeSet.Quantity;

                        }
                        catch (Exception ex)
                        {
                            Logger.SimpleWrite($"RegisterRechargeCommand.CustomerId: {_customerID}\r\nException: {ex}");
                        }
                    }
                    cmd.ProgressAction(1);
                }

                _period = _period.AddMonths(1);
            }
        }
    }
}
