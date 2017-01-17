using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class TotalBillDoc : Entity
    {
        public TotalBillDoc()
        {
            this.TotalBillDocPoses = new List<TotalBillDocPos>();
        }

        public int CustomerID { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Account { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public string Square { get; set; }
        public int ResidentsCount { get; set; }
        public decimal Value { get; set; }
        public int BillSetID { get; set; }
        public DateTime Period { get; set; }
        public Nullable<DateTime> StartPeriod { get; set; }
        public virtual BillSet BillSet { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<TotalBillDocPos> TotalBillDocPoses { get; set; }
    }
}