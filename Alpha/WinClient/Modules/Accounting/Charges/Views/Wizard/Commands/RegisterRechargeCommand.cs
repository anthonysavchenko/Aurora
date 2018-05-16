using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class RegisterRechargeCommand : ResultCommand<RegisterCommandResult>
    {
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
        public int[] CustomerIds { get; set; }
        public Dictionary<int, ServicePercentCorrection> ServicePercentCorrectionByCustomer { get; set; }

        public Action<int> ProgressAction { get; set; }
        public Action<int> ResetProgressBar { get; set; }
    }
}
