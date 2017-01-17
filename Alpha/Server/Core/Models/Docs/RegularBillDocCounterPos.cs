using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class RegularBillDocCounterPos : Entity
    {
        public string Number { get; set; }
        public decimal PrevValue { get; set; }
        public decimal CurValue { get; set; }
        public decimal Consumption { get; set; }
        public decimal Rate { get; set; }
        public int RegularBillDocID { get; set; }
        public virtual RegularBillDoc RegularBillDoc { get; set; }
    }
}
