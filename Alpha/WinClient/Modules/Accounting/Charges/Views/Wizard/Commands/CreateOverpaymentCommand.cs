using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateOverpaymentCommand : ICommand
    {
        public ChargeOpers ChargeOper { get; set; }
        public Customers DbCustomerStub { get; set; }
        public PeriodInfo PeriodInfo { get; set; }
        public Dictionary<int, DataBase.Services> Services { get; set; }
        public Dictionary<int, Balance> ChargePeriodBalance { get; set; }
    }
}
