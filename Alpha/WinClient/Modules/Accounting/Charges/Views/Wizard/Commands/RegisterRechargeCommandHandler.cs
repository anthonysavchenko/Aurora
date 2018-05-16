using System;
using System.Linq;
using System.Data.Entity;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Dispatchers;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterRechargeCommandHandler : ICommandHandler<RegisterRechargeCommand>
    {
        private readonly IServerTimeService _sts;
        private readonly Cache _cache = new Cache();

        public RegisterRechargeCommandHandler(IServerTimeService sts)
        {
            _sts = sts;
        }

        public void Execute(RegisterRechargeCommand command)
        {
            command.Result = new RegisterCommandResult();

            DateTime _now = _sts.GetDateTimeInfo().Now;
            DateTime _currentPeriod = _sts.GetPeriodInfo().FirstUncharged;
            DateTime _period = command.Since;

            int _monthCount = (_period.Year == command.Till.Year
                    ? command.Till.Month - _period.Month
                    : 12 - _period.Month + 12 * (command.Till.Year - _period.Year - 1) + command.Till.Month) + 1; 

            //View.ResetProgressBar(_customerIDs.Length * _monthCount);

            int _rechargeSetId;

            using (Entities _entities = new Entities())
            {
                Users _user = new Users { ID = int.Parse(UserHolder.User.ID) };

                RechargeSets _rechargeSet =
                    new RechargeSets
                    {
                        CreationDateTime = _now,
                        Period = _currentPeriod,
                        Number = _entities.RechargeSets.Any() ? _entities.RechargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };
                _entities.AddToRechargeSets(_rechargeSet);
                _entities.SaveChanges();
                _rechargeSetId = _rechargeSet.ID;
            }

            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                RechargeSets _rechargeSet = _db.RechargeSets.First(x => x.ID == _rechargeSetId);

                var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                var _services = _db.Services.ToDictionary(x => x.ID);

                var _dispatcher = CreateDispatcher(_db);

                while (_period <= command.Till)
                {
                    _cache.Init(_period);

                    for (int i = 0; i < command.CustomerIds.Length; i++)
                    {
                        try
                        {
                            int _customerID = command.CustomerIds[i];

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

                            _dispatcher.Execute(
                                new ApplyPercentCorrectionCommand
                                {
                                    Period = _period,
                                    ChargesByPos = _calculateChargesCommand.Result,
                                    CustomerInfo = _customerInfo,
                                    ServicePercentCorrection = command.ServicePercentCorrectionByCustomer.ContainsKey(_customerID)
                                        ? command.ServicePercentCorrectionByCustomer[_customerID]
                                        : null
                                });

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
                                    new CreateChargeCorrectionOperCommand
                                    {
                                        Contractors = _contractors,
                                        Services = _services
                                    };
                                _dispatcher.Execute(_correctionCommand);
                                _chargeCorrectionOper = _correctionCommand.Result;
                            }

                            _dispatcher.Execute(
                                new CreateRechargeOperCommand
                                {
                                    BenefitsByPos = _calculateBenefitsCommand.Result,
                                    ChargesByPos = _calculateChargesCommand.Result,
                                    ChargeCorrectionOper = _chargeCorrectionOper,
                                    ChargeOper = _chargeOper,
                                    Contractors = _contractors,
                                    Services = _services,
                                    CustomerInfo = _customerInfo,
                                    DbCustomer = _dbCustomer,
                                    Now = _now,
                                    Period = _period,
                                    RechargeSet = _rechargeSet
                                });

                            _rechargeSet.Quantity++;
                            _rechargeSet.ValueSum += _calculateChargesCommand.Result.Values.Sum(x => x);

                            _db.SaveChanges();
                            
                        }
                        catch (Exception _ex)
                        {
                           // TODO: Log
                        }

                        command.ProgressAction(1);
                    }

                    _period = _period.AddMonths(1);
                }

                command.Result.Total = _rechargeSet.ValueSum;
                command.Result.Processed = _rechargeSet.Quantity;
            }
        }

        private ICommandDispatcher CreateDispatcher(Entities db)
        {
            return new CommandDispatcher(
                new CommandHandlerAdapter<CalculateChargeCommand>(new CalculateChargeCommandHandler()),
                new CommandHandlerAdapter<ApplyPercentCorrectionCommand>(new ApplyPercentCorrectionCommandHandler(db)),
                new CommandHandlerAdapter<CalculateBenefitsCommand>(new CalculateBenefitsCommandHandler()),
                new CommandHandlerAdapter<CreateChargeCorrectionOperCommand>(new CreateChargeCorrectionOperCommandHandler(db)),
                new CommandHandlerAdapter<CreateRechargeOperCommand>(new CreateRechargeOperCommandHandler(db)));
        }
    }
}
