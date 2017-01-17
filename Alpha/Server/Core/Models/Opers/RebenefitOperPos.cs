using Taumis.Alpha.Server.Core.Models.Enums;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class RebenefitOperPos : Entity
    {
        public BenefitRuleTypes BenefitRule { get; set; }
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int RebenefitOperID { get; set; }
        public int ContractorID { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual RebenefitOper RebenefitOper { get; set; }
        public virtual Service Service { get; set; }
    }
}