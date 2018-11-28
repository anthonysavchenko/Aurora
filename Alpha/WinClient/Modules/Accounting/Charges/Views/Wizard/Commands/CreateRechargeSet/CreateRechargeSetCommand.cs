using System;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateRechargeSetCommand : ResultCommand<int>
    {
        public DateTime Now { get; set; }
        public DateTime Period { get; set; }
        public int AuthorId { get; set; }
    }
}
