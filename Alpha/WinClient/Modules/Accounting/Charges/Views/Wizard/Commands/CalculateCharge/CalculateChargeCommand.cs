using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateChargeCommand : ResultCommand<Dictionary<int, decimal>>
    {
        public CustomerInfo CustomerInfo { get; set; }
        public ICache Cache { get; set; }
    }
}
