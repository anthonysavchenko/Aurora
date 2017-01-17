using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class ChargeOper : Entity
    {
        public ChargeOper()
        {
            this.BenefitOpers = new List<BenefitOper>();
            this.ChargeOperPoses = new List<ChargeOperPos>();
            this.OverpaymentCorrectionOpers = new List<OverpaymentCorrectionOper>();
            this.RechargeOpers = new List<RechargeOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public int ChargeSetID { get; set; }
        public Nullable<int> ChargeCorrectionOperID { get; set; }
        public Nullable<int> RegularBillDocID { get; set; }
        public virtual ICollection<BenefitOper> BenefitOpers { get; set; }
        public virtual ChargeCorrectionOper ChargeCorrectionOper { get; set; }
        public virtual ICollection<ChargeOperPos> ChargeOperPoses { get; set; }
        public virtual RegularBillDoc RegularBillDoc { get; set; }
        public virtual ChargeSet ChargeSet { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OverpaymentCorrectionOper> OverpaymentCorrectionOpers { get; set; }
        public virtual ICollection<RechargeOper> RechargeOpers { get; set; }
    }
}