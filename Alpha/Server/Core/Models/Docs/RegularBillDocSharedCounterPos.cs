using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class RegularBillDocSharedCounterPos : Entity
    {
        public int RegularBillDocID { get; set; }
        public decimal SharedCounterValue { get; set; }
        public decimal SharedCharge { get; set; }
        public virtual RegularBillDoc RegularBillDoc { get; set; }
    }
}