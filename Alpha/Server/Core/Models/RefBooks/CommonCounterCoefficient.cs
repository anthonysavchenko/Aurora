using System;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class CommonCounterCoefficient : Entity
    {
        public DateTime Period { get; set; }
        public decimal Coefficient { get; set; }
        public int CommonCounterID { get; set; }
        public virtual CommonCounter CommonCounter { get; set; }
    }
}