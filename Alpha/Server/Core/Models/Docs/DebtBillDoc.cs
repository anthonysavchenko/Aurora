using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class DebtBillDoc : Entity
    {
        public DateTime CreationDateTime { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public decimal Value { get; set; }
        public int BillSetID { get; set; }
        public DateTime Period { get; set; }
        public virtual BillSet BillSet { get; set; }
    }
}