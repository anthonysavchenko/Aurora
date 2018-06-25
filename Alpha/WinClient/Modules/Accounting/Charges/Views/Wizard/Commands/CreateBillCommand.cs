using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateBillCommand : ICommand
    {
        public ChargeOpers ChargeOper { get; set; }
        public Dictionary<int, Balance> ChargePeriodBalance { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public Customers DbCustomerStub { get; set; }
        public Dictionary<int, Contractors> Contractors { get; set; }
        public int BillSetId { get; set; }

        public Entities Db { get; set; }
    }
}
