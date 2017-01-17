using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class BenefitCorrectionOperPos : Entity
    {
        public BenefitRuleTypes BenefitRule { get; set; }
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int BenefitCorrectionOperID { get; set; }
        public int ContractorID { get; set; }
        public virtual BenefitCorrectionOper BenefitCorrectionOper { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Service Service { get; set; }
    }
}