using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class BenefitType : Entity
    {
        public BenefitType()
        {
            Residents = new List<Resident>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public byte BenefitRule { get; set; }
        public Nullable<byte> FixedPercent { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
    }
}