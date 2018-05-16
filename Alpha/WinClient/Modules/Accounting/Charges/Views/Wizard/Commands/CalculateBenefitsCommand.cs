using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateBenefitsCommand : ResultCommand<Dictionary<int, decimal>>
    {
        public CustomerInfo CustomerInfo { get; set; }
    }
}
