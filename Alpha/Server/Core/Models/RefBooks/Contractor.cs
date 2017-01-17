using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Contractor : Entity
    {
        public Contractor()
        {
            RechargeOperPoses = new List<RechargeOperPos>();
            RebenefitOperPoses = new List<RebenefitOperPos>();
            ChargeOperPoses = new List<ChargeOperPos>();
            CustomerPoses = new List<CustomerPos>();
            ChargeCorrectionOperPoses = new List<ChargeCorrectionOperPos>();
            BenefitOperPoses = new List<BenefitOperPos>();
            BenefitCorrectionOperPoses = new List<BenefitCorrectionOperPos>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<RechargeOperPos> RechargeOperPoses { get; set; }
        public virtual ICollection<RebenefitOperPos> RebenefitOperPoses { get; set; }
        public virtual ICollection<ChargeOperPos> ChargeOperPoses { get; set; }
        public virtual ICollection<CustomerPos> CustomerPoses { get; set; }
        public virtual ICollection<ChargeCorrectionOperPos> ChargeCorrectionOperPoses { get; set; }
        public virtual ICollection<BenefitOperPos> BenefitOperPoses { get; set; }
        public virtual ICollection<BenefitCorrectionOperPos> BenefitCorrectionOperPoses { get; set; }
    }
}