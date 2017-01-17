using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class PaymentCorrectionOper : Entity
    {
        public PaymentCorrectionOper()
        {
            this.PaymentCorrectionOperPoses = new List<PaymentCorrectionOperPos>();
            this.PaymentOpers = new List<PaymentOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public int PaymentOperID { get; set; }
        public virtual ICollection<PaymentCorrectionOperPos> PaymentCorrectionOperPoses { get; set; }
        public virtual PaymentOper PaymentOper { get; set; }
        public virtual ICollection<PaymentOper> PaymentOpers { get; set; }
    }
}