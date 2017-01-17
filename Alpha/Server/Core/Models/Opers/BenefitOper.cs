using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class BenefitOper : Entity
    {
        public BenefitOper()
        {
            this.BenefitOperPoses = new List<BenefitOperPos>();
        }

        public decimal Value { get; set; }
        public int ChargeOperID { get; set; }
        public Nullable<int> BenefitCorrectionOperID { get; set; }
        public virtual BenefitCorrectionOper BenefitCorrectionOper { get; set; }
        public virtual ICollection<BenefitOperPos> BenefitOperPoses { get; set; }
        public virtual ChargeOper ChargeOper { get; set; }
    }
}