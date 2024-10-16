﻿using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeCorrectionCommand : ResultCommand<ChargeCorrectionOpers>
    {
        public ChargeOpers ChargeOper { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public DateTime Period { get; set; }
        public DateTime Now { get; set; }
        public Dictionary<int, DataBase.Services> Services { get; set; }
        public Dictionary<int, Contractors> Contractors { get; set; }

        public Entities Db { get; set; }
    }
}
