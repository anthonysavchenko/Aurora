using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class PaymentSet : Entity
    {
        public PaymentSet()
        {
            this.PaymentOpers = new List<PaymentOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public int Number { get; set; }
        public bool IsFile { get; set; }
        public short Quantity { get; set; }
        public decimal ValueSum { get; set; }
        public string Comment { get; set; }
        public Nullable<int> IntermediaryID { get; set; }
        public int Author { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual Intermediary Intermediary { get; set; }
        public virtual ICollection<PaymentOper> PaymentOpers { get; set; }
        public virtual User User { get; set; }
    }
}