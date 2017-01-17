using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class RechargeOper : Entity
    {
        public RechargeOper()
        {
            this.ChargeCorrectionOpers = new List<ChargeCorrectionOper>();
            this.RebenefitOpers = new List<RebenefitOper>();
            this.RechargeOperPoses = new List<RechargeOperPos>();
        }

        public DateTime CreationDateTime { get; set; }
        public decimal Value { get; set; }
        public int CustomerID { get; set; }
        public int RechargeSetID { get; set; }
        public Nullable<int> ChargeOperID { get; set; }
        public Nullable<int> ChargeCorrectionOperID { get; set; }
        public virtual ICollection<ChargeCorrectionOper> ChargeCorrectionOpers { get; set; }
        public virtual ChargeCorrectionOper ChargeCorrectionOper { get; set; }
        public virtual ChargeOper ChargeOper { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<RebenefitOper> RebenefitOpers { get; set; }
        public virtual ICollection<RechargeOperPos> RechargeOperPoses { get; set; }
        public virtual RechargeSet RechargeSet { get; set; }
    }
}