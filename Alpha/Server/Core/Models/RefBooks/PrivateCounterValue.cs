using System;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class PrivateCounterValue : Entity
    {
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public int PrivateCounterID { get; set; }
        public virtual PrivateCounter PrivateCounter { get; set; }
    }
}