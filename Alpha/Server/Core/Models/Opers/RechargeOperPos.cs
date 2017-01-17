﻿using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class RechargeOperPos : Entity
    {
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int ContractorID { get; set; }
        public int RechargeOperID { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual RechargeOper RechargeOper { get; set; }
        public virtual Service Service { get; set; }
    }
}