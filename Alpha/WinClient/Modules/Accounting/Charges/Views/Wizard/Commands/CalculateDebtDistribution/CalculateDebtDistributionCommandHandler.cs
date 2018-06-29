using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateDebtDistributionCommandHandler : ICommandHandler<CalculateDebtDistributionCommand>
    {
        private readonly ICommandDispatcher _dispatcher;

        public CalculateDebtDistributionCommandHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Execute(CalculateDebtDistributionCommand command)
        {
            var _calculateChargeCommand =
                    new CalculateChargeCommand
                    {
                        CustomerInfo = command.CustomerInfo,
                        Cache = command.Cache
                    };
            _dispatcher.Execute(_calculateChargeCommand);

            decimal _distributionSum = _calculateChargeCommand.Result.Sum(x => x.Value);

            command.Result = new Dictionary<int, decimal>();

            if (_distributionSum > 0)
            {
                decimal _coefficient = command.DebtValue / _distributionSum;

                _distributionSum = 0;

                foreach (var _posValuePair in _calculateChargeCommand.Result)
                {
                    decimal _value = _coefficient * _posValuePair.Value;

                    _distributionSum += _value;

                    if (_distributionSum > command.DebtValue)
                    {
                        _value -= _distributionSum - command.DebtValue;
                        _distributionSum = command.DebtValue;
                    }

                    command.Result.Add(_posValuePair.Key, Math.Round(_value, 2, MidpointRounding.AwayFromZero));
                }

                if (_distributionSum < command.DebtValue)
                {
                    command.Result[command.Result.First().Key] += command.DebtValue - _distributionSum;
                    _distributionSum = command.DebtValue;
                }
            }
        }
    }
}
