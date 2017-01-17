using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class CustomerPos : Entity
    {
        public CustomerPos()
        {
            this.PrivateCounters = new List<PrivateCounter>();
        }

        public int ServiceID { get; set; }
        public int ContractorID { get; set; }
        public decimal Rate { get; set; }
        public int CustomerID { get; set; }
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
        public virtual Contractor Contractor { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<PrivateCounter> PrivateCounters { get; set; }
    }
}