using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterDebtCommand : ResultCommand<RegisterCommandResult>
    {
        public DateTime Now { get; set; }
        public DateTime Period { get; set; }
        public string File { get; set; }

        public Action<int> ProgressAction { get; set; }
        public Action<int> ResetProgressBar { get; set; }
    }
}
