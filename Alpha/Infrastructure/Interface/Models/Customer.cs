using System;

namespace Taumis.Alpha.Infrastructure.Interface.Models
{
    public class Customer
    {
        public Address Address;
        public bool IsNorm;
        public DateTime? PrevDate;
        public Counter Counter;
    }
}
