using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;
using Taumis.Alpha.Server.Core.Models.Opers;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Service : Entity
    {
        public Service()
        {
            PaymentCorrectionOperPoses = new List<PaymentCorrectionOperPos>();
            OverpaymentCorrectionOperPoses = new List<OverpaymentCorrectionOperPos>();
            PaymentOperPoses = new List<PaymentOperPos>();
            OverpaymentOperPoses = new List<OverpaymentOperPos>();
            CustomerPoses = new List<CustomerPos>();
            RechargeOperPoses = new List<RechargeOperPos>();
            CommonCounters = new List<CommonCounter>();
            ChargeOperPoses = new List<ChargeOperPos>();
            ChargeCorrectionOperPoses = new List<ChargeCorrectionOperPos>();
            RebenefitOperPoses = new List<RebenefitOperPos>();
            BenefitOperPoses = new List<BenefitOperPos>();
            BenefitCorrectionOperPoses = new List<BenefitCorrectionOperPos>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public byte ChargeRule { get; set; }
        public int ServiceTypeID { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<PaymentCorrectionOperPos> PaymentCorrectionOperPoses { get; set; }
        public virtual ICollection<OverpaymentCorrectionOperPos> OverpaymentCorrectionOperPoses { get; set; }
        public virtual ICollection<PaymentOperPos> PaymentOperPoses { get; set; }
        public virtual ICollection<OverpaymentOperPos> OverpaymentOperPoses { get; set; }
        public virtual ICollection<CustomerPos> CustomerPoses { get; set; }
        public virtual ICollection<RechargeOperPos> RechargeOperPoses { get; set; }
        public virtual ICollection<CommonCounter> CommonCounters { get; set; }
        public virtual ICollection<ChargeOperPos> ChargeOperPoses { get; set; }
        public virtual ICollection<ChargeCorrectionOperPos> ChargeCorrectionOperPoses { get; set; }
        public virtual ICollection<RebenefitOperPos> RebenefitOperPoses { get; set; }
        public virtual ICollection<BenefitOperPos> BenefitOperPoses { get; set; }
        public virtual ICollection<BenefitCorrectionOperPos> BenefitCorrectionOperPoses { get; set; }
    }
}