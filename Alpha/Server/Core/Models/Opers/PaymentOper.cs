using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class PaymentOper : Entity
    {
        public PaymentOper()
        {
            this.PaymentCorrectionOpers = new List<PaymentCorrectionOper>();
            this.PaymentOperPoses = new List<PaymentOperPos>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime PaymentPeriod { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public int PaymentSetID { get; set; }
        public Nullable<int> PaymentCorrectionOperID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<PaymentCorrectionOper> PaymentCorrectionOpers { get; set; }
        public virtual PaymentCorrectionOper PaymentCorrectionOper { get; set; }
        public virtual ICollection<PaymentOperPos> PaymentOperPoses { get; set; }
        public virtual PaymentSet PaymentSet { get; set; }
    }
}
