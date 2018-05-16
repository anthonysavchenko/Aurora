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
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterChargeCommandHandler : ICommandHandler<RegisterChargeCommand>
    {
        private readonly Cache _cache = new Cache();
        private readonly IServerTimeService _sts;
        private readonly PaymentDistributionService _pds;
        private DateTime _now;
        private PeriodInfo _periodInfo;
        private int _chargeSetId;
        private Dictionary<int, int> _billSetByBuilding;
        private int[] _customers;
        private ICommandDispatcher _dispatcher;

        public RegisterChargeCommandHandler(IServerTimeService sts, PaymentDistributionService pds)
        {
            _sts = sts;
            _pds = pds;
        }

        public void Execute(RegisterChargeCommand command)
        {
            command.Result = new RegisterCommandResult();

            using (Entities _db = new Entities())
            {
                Init(_db);

                var _createBillCommand = new CreateBillCommand();

                for (int i = 0; i < _customers.Length; i++)
                {
                    int _customerId = _customers[i];

                    try
                    {
                        Dictionary<int, Balance> _periodBalance = _db
                            .GetCustomerBalancesGroupedByPeriod(_customerId, x => x.Period == _periodInfo.FirstUncharged)
                            .Values
                            .FirstOrDefault() ?? new Dictionary<int, Balance>();
                        Dictionary<int, DataBase.Services> _services = _db.Services.Include(x => x.ServiceTypes).ToDictionary(x => x.ID);
                        Dictionary<int, Contractors> _contractors = _db.Contractors.ToDictionary(x => x.ID);
                        Customers _dbCustomerStub = new Customers { ID = _customerId };
                        _db.Customers.Attach(_dbCustomerStub);
                        CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerId, _periodInfo.FirstUncharged);

                        var _calculateChargesCommand =
                            new CalculateChargeCommand
                            {
                                Cache = _cache,
                                CustomerInfo = _customerInfo,
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
                                ChargeSetId = _chargeSetId,
                                CustomerInfo = _customerInfo,
                                DbCustomerStub = _dbCustomerStub,
                                Now = _now,
                                Period = _periodInfo.FirstUncharged,
                                Services = _services,
                                Contractors = _contractors
                            };
                        _dispatcher.Execute(_createChargeOperCommand);

                        _dispatcher.Execute(
                            new CreateOverpaymentCommand
                            {
                                ChargeOper = _createChargeOperCommand.Result,
                                ChargePeriodBalance = _periodBalance,
                                DbCustomerStub = _dbCustomerStub,
                                PeriodInfo = _periodInfo,
                                Services = _services
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
                                Services = _services
                            });

                        _db.SaveChanges();
                        command.Result.Processed++;
                        command.Result.Total += _createChargeOperCommand.Result.Value;

                        command.ProgressAction(1);
                    }
                    catch(Exception _ex)
                    {
                        // TODO: Log
                    }
                }
            }
        }

        private void Init(Entities db)
        {
            _dispatcher = new CommandDispatcher(
                new CommandHandlerAdapter<CalculateChargeCommand>(new CalculateChargeCommandHandler()),
                new CommandHandlerAdapter<CalculateBenefitsCommand>(new CalculateBenefitsCommandHandler()),
                new CommandHandlerAdapter<CreateChargeCommand>(new CreateChargeCommandHandler(db)),
                new CommandHandlerAdapter<CreateOverpaymentCommand>(new CreateOverpaymentCommandHandler(_pds, db)),
                new CommandHandlerAdapter<CreateBillCommand>(new CreateBillCommandHandler(db)));

            DateTimeInfo _dateTimeInfo = _sts.GetDateTimeInfo();
            _periodInfo = _sts.GetPeriodInfo();
            _now = _dateTimeInfo.Now;

            var _createChargeSetCommand = 
                new CreateChargeSetCommand
                {
                    Now = _now,
                    Period = _periodInfo.FirstUncharged
                };
            _dispatcher.Execute(_createChargeSetCommand);
            _chargeSetId = _createChargeSetCommand.Result;

            var _createBillSetsCommand = 
                new CreateBillSetsCommand
                { 
                    CreationDateTime = _now
                };
            _dispatcher.Execute(_createBillSetsCommand);
            _billSetByBuilding = _createBillSetsCommand.Result;

            _customers = db.Customers.Select(c => c.ID).ToArray();
        }
    }
}
