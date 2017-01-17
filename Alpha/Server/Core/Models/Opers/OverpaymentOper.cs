using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class OverpaymentOper : Entity
    {
        public OverpaymentOper()
        {
            this.OverpaymentOperPoses = new List<OverpaymentOperPos>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime PaymentPeriod { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public Nullable<int> OverpaymentCorrectionOperID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual OverpaymentCorrectionOper OverpaymentCorrectionOper { get; set; }
        public virtual ICollection<OverpaymentOperPos> OverpaymentOperPoses { get; set; }
    }
}