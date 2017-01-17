using System;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class PaymentOperPos : Entity
    {
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int PaymentOperID { get; set; }
        public virtual PaymentOper PaymentOper { get; set; }
        public virtual Service Service { get; set; }
    }
}