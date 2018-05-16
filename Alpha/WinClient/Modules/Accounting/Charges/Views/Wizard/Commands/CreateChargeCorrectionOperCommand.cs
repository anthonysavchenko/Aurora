using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCorrectionOperCommand : ResultCommand<ChargeCorrectionOpers>
    {
        public ChargeOpers ChargeOper { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public DateTime CurrentPeriod { get; set; }
        public DateTime CorrectionPeriod { get; set; }
        public DateTime Now { get; set; }
        public Dictionary<int, DataBase.Services> Services { get; set; }
        public Dictionary<int, Contractors> Contractors { get; set; }
    }
}
