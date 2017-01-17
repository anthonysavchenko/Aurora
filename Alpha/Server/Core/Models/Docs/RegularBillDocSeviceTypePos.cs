using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class RegularBillDocSeviceTypePos : Entity
    {
        public int RegularBillDocID { get; set; }
        public string ServiceTypeName { get; set; }
        public decimal PayRate { get; set; }
        public decimal Charge { get; set; }
        public decimal Benefit { get; set; }
        public decimal Recalculation { get; set; }
        public decimal Payable { get; set; }
        public virtual RegularBillDoc RegularBillDoc { get; set; }
    }
}