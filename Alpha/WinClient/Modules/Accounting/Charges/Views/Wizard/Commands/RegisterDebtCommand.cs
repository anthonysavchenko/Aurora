using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterDebtCommand : ICommand
    {
        public string File { get; set; }

        public Action<int> ResetProgressBar { get; set; }
    }
}
