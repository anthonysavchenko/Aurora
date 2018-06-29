using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterChargeCommand : ResultCommand<RegisterCommandResult>
    {
        public Action<int> ProgressAction { get; set; }
        public Action<int> ResetProgressBar { get; set; }
        public DateTime Now { get; set; }
        public DateTime Period { get; set; }
        public DateTime LastChargedPeriod { get; set; }
        public int AuthorId { get; set; }
    }
}
