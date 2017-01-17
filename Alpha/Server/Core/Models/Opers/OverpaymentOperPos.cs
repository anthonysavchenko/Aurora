using System;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Opers
{
    public class OverpaymentOperPos : Entity
    {
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public int ServiceID { get; set; }
        public int OverpaymentOperID { get; set; }
        public virtual OverpaymentOper OverpaymentOper { get; set; }
        public virtual Service Service { get; set; }
    }
}