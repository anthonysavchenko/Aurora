using System;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class CommonCounterValue :Entity
    {
        public DateTime Period { get; set; }
        public decimal Value { get; set; }
        public int CommonCounterID { get; set; }
        public virtual CommonCounter CommonCounter { get; set; }
    }
}