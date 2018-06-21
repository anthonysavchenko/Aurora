using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.View.Wizard.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterDebtCommandHandler : ICommandHandler<RegisterDebtCommand>
    {
        private readonly Cache _cache = new Cache();
        private readonly ICommandDispatcher _dispatcher;

        public RegisterDebtCommandHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Execute(RegisterDebtCommand command)
        {
            command.Result = new RegisterCommandResult();

            var _parseDebtFileCommand = new ParseDebtFileCommand { DebtFile = command.File };
            _dispatcher.Execute(_parseDebtFileCommand);

            if (_parseDebtFileCommand.Result.Count == 0)
                return;

            int _rechargeSetId;
            using (var _db = new Entities())
            {
                var _user = new Users { ID = int.Parse(UserHolder.User.ID) };
                _db.Users.Attach(_user);

                RechargeSets _rechargeSet =
                    new RechargeSets
                    {
                        CreationDateTime = command.Now,
                        Period = command.Period,
                        Number = _db.RechargeSets.Any() ? _db.RechargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };
                _db.AddToRechargeSets(_rechargeSet);
                _db.SaveChanges();
                _rechargeSetId = _rechargeSet.ID;
            }

            command.ResetProgressBar(_parseDebtFileCommand.Result.Keys.Count);

            foreach (KeyValuePair<int, decimal> _pair in _parseDebtFileCommand.Result)
            {
                using (var _db = new Entities())
                {
                    _db.CommandTimeout = 3600;

                    try
                    {
                        var _contractors = _db.Contractors.ToDictionary(x => x.ID);
                        var _services = _db.Services.ToDictionary(x => x.ID);
                        var _rechargeSet = _db.RechargeSets.First(x => x.ID == _rechargeSetId);


                        int _customerID = _pair.Key;
                        decimal _debt = _pair.Value;

                        CustomerInfo _customerInfo = _db.GetCustomerInfo(_customerID, command.Period);

                        var _calculateDebtDistrCommand =
                            new CalculateDebtDistributionCommand
                            {
                                CustomerInfo = _customerInfo,
                                Cache = _cache,
                                DebtValue = _debt
                            };
                        _dispatcher.Execute(_calculateDebtDistrCommand);

                        var _createRechargeOperCommand =
                            new CreateRechargeOperCommand
                            {
                                BenefitsByPos = new Dictionary<int, decimal>(),
                                ChargeCorrectionOper = null,
                                ChargeOper = null,
                                ChargesByPos = _calculateDebtDistrCommand.Result,
                                CustomerInfo = _customerInfo,
                                RechargeSet = _rechargeSet,
                                Contractors = _contractors,
                                Services = _services,
                                Now = command.Now,
                                DbCustomer = _db.Customers.First(c => c.ID == _customerID),
                                Db = _db
                            };
                        _dispatcher.Execute(_createRechargeOperCommand);

                        _rechargeSet.Quantity++;
                        _rechargeSet.ValueSum += _calculateDebtDistrCommand.Result.Values.Sum(x => x);

                        _db.SaveChanges();

                        command.Result.Total = _rechargeSet.ValueSum;
                        command.Result.Processed = _rechargeSet.Quantity;

                    }
                    catch (Exception ex)
                    {
                        Logger.SimpleWrite($"RegisterRechargeCommand. CustomerId: {_pair.Key}. Exception: {ex}");
                    }
                }
            }
        }
    }
}
