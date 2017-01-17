using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class RebenefitOper : Entity
    {
        public RebenefitOper()
        {
            this.RebenefitOperPoses = new List<RebenefitOperPos>();
        }

        public decimal Value { get; set; }
        public int RechargeOperID { get; set; }
        public Nullable<int> BenefitCorrectionOperID { get; set; }
        public virtual BenefitCorrectionOper BenefitCorrectionOper { get; set; }
        public virtual ICollection<RebenefitOperPos> RebenefitOperPoses { get; set; }
        public virtual RechargeOper RechargeOper { get; set; }
    }
}
