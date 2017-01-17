using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Street : Entity
    {
        public Street()
        {
            Buildings = new List<Building>();
        }

        public string Name { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }
}