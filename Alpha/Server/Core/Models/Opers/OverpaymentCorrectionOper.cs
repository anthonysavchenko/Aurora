using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class OverpaymentCorrectionOper : Entity
    {
        public OverpaymentCorrectionOper()
        {
            this.OverpaymentCorrectionOperPoses = new List<OverpaymentCorrectionOperPos>();
            this.OverpaymentOpers = new List<OverpaymentOper>();
        }

        public decimal Value { get; set; }
        public int ChargeOperID { get; set; }
        public DateTime Period { get; set; }
        public virtual ChargeOper ChargeOper { get; set; }
        public virtual ICollection<OverpaymentCorrectionOperPos> OverpaymentCorrectionOperPoses { get; set; }
        public virtual ICollection<OverpaymentOper> OverpaymentOpers { get; set; }
    }
}