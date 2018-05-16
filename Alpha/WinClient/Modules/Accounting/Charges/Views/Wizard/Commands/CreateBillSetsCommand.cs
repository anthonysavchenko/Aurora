using System;
using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateBillSetsCommand : ResultCommand<Dictionary<int, int>>
    {
        public DateTime CreationDateTime { get; set; }
    }
}
