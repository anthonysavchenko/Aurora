using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class ApplyPercentCorrectionCommand : ICommand
    {
        public DateTime Period { get; set; }
        public ServicePercentCorrection ServicePercentCorrection { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public Dictionary<int, decimal> ChargesByPos { get; set; }
    }
}
