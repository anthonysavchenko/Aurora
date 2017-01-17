using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class BenefitCorrectionOper : Entity
    {
        public BenefitCorrectionOper()
        {
            this.BenefitCorrectionOperPoses = new List<BenefitCorrectionOperPos>();
            this.BenefitOpers = new List<BenefitOper>();
            this.RebenefitOpers = new List<RebenefitOper>();
        }

        public decimal Value { get; set; }
        public int ChargeCorrectionOperID { get; set; }
        public virtual ICollection<BenefitCorrectionOperPos> BenefitCorrectionOperPoses { get; set; }
        public virtual ChargeCorrectionOper ChargeCorrectionOper { get; set; }
        public virtual ICollection<BenefitOper> BenefitOpers { get; set; }
        public virtual ICollection<RebenefitOper> RebenefitOpers { get; set; }
    }
}