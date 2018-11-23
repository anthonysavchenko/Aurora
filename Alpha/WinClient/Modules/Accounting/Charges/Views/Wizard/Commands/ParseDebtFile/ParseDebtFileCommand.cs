using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class ParseDebtFileCommand : ResultCommand<Dictionary<int, decimal>>
    {
        public string DebtFile { get; set; }
    }
}
