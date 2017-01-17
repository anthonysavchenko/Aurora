using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class ChargeSet : Entity
    {
        public ChargeSet()
        {
            this.ChargeOpers = new List<ChargeOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime Period { get; set; }
        public int Number { get; set; }
        public int Quantity { get; set; }
        public decimal ValueSum { get; set; }
        public int Author { get; set; }
        public virtual ICollection<ChargeOper> ChargeOpers { get; set; }
        public virtual User User { get; set; }
    }
}