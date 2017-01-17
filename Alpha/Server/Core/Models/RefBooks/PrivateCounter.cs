using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class PrivateCounter : Entity
    {
        public string Number { get; set; }
        public decimal Rate { get; set; }
        public int CustomerPosID { get; set; }
        public virtual CustomerPos CustomerPos { get; set; }
        public ICollection<PrivateCounterValue> PrivateCounterValues { get; set; }
    }
}