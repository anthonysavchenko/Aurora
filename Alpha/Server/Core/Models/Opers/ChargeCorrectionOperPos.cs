using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class ChargeCorrectionOperPos : Entity
    {
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int ContractorID { get; set; }
        public int ChargeCorrectionOperID { get; set; }
        public virtual ChargeCorrectionOper ChargeCorrectionOper { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Service Service { get; set; }
    }
}