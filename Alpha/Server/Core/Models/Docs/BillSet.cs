using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Enums;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class BillSet : Entity
    {
        public BillSet()
        {
            this.DebtBillDocs = new List<DebtBillDoc>();
            this.RegularBillDocs = new List<RegularBillDoc>();
            this.TotalBillDocs = new List<TotalBillDoc>();
        }

        public System.DateTime CreationDateTime { get; set; }
        public int Number { get; set; }
        public BillTypes BillType { get; set; }
        public short Quantity { get; set; }
        public decimal ValueSum { get; set; }
        public virtual ICollection<DebtBillDoc> DebtBillDocs { get; set; }
        public virtual ICollection<RegularBillDoc> RegularBillDocs { get; set; }
        public virtual ICollection<TotalBillDoc> TotalBillDocs { get; set; }
    }
}