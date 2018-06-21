using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CalculateDebtDistributionCommand : ResultCommand<Dictionary<int, decimal>>
    {
        public CustomerInfo CustomerInfo { get; set; }
        public Cache Cache { get; set; }
        public decimal DebtValue { get; set; }
    }
}
