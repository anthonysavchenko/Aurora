using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeSetCommand : ResultCommand<int>
    {
        public DateTime Now { get; set; }
        public DateTime Period { get; set; }
    }
}
