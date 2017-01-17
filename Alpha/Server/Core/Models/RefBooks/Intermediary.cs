using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;
namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Intermediary : Entity
    {
        public Intermediary()
        {
            PaymentSets = new List<PaymentSet>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Rate { get; set; }
        public virtual ICollection<PaymentSet> PaymentSets { get; set; }
    }
}