using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Resident : Entity
    {
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string ResidentDocument { get; set; }
        public byte OwnerRelationship { get; set; }
        public Nullable<int> BenefitTypeID { get; set; }
        public int CustomerID { get; set; }
        public virtual BenefitType BenefitType { get; set; }
        public virtual Customer Customer { get; set; }
    }
}