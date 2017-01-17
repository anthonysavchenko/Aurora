using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class User : Entity
    {
        public User()
        {
            ChargeSets = new List<ChargeSet>();
            RechargeSets = new List<RechargeSet>();
            PaymentSets = new List<PaymentSet>();
            Customers = new List<Customer>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Aka { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public virtual ICollection<ChargeSet> ChargeSets { get; set; }
        public virtual ICollection<RechargeSet> RechargeSets { get; set; }
        public virtual ICollection<PaymentSet> PaymentSets { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}