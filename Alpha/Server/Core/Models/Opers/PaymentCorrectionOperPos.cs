using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class PaymentCorrectionOperPos : Entity
    {
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int PaymentCorrectionOperID { get; set; }
        public virtual PaymentCorrectionOper PaymentCorrectionOper { get; set; }
        public virtual Service Service { get; set; }
    }
}