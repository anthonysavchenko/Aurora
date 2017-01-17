using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class TotalBillDocPos : Entity
    {
        public int TotalBillDocID { get; set; }
        public string ServiceTypeName { get; set; }
        public decimal Value { get; set; }
        public decimal TotalCharged { get; set; }
        public decimal TotalPaid { get; set; }
        public virtual TotalBillDoc TotalBillDoc { get; set; }
    }
}