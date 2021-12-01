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
            foreach (var c in command.CustomerInfo.Poses)
            {
                if (c.ServiceId == 52)
                {
                    if (command.Result == null)
                    {
                        command.Result = new Dictionary<int, decimal>();
                    }
                    command.Result.Add(c.Id, Math.Round(command.DebtValue, 2, MidpointRounding.AwayFromZero));
                }
            }
        }
    }
}
