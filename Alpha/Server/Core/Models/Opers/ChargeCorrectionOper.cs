using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class ChargeCorrectionOper : Entity
    {
        public ChargeCorrectionOper()
        {
            this.BenefitCorrectionOpers = new List<BenefitCorrectionOper>();
            this.ChargeCorrectionOperPoses = new List<ChargeCorrectionOperPos>();
            this.ChargeOpers = new List<ChargeOper>();
            this.RechargeOpers = new List<RechargeOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public Nullable<int> RechargeOperID { get; set; }
        public virtual ICollection<BenefitCorrectionOper> BenefitCorrectionOpers { get; set; }
        public virtual ICollection<ChargeCorrectionOperPos> ChargeCorrectionOperPoses { get; set; }
        public virtual ICollection<ChargeOper> ChargeOpers { get; set; }
        public virtual RechargeOper RechargeOper { get; set; }
        public virtual ICollection<RechargeOper> RechargeOpers { get; set; }
    }
}
