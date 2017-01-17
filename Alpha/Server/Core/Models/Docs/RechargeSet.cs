using System;
using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Opers;
using Taumis.Alpha.Server.Core.Models.RefBooks;

namespace Taumis.Alpha.Server.Core.Models.Docs
{
    public class RechargeSet : Entity
    {
        public RechargeSet()
        {
            RechargeOpers = new List<RechargeOper>();
        }

        public DateTime CreationDateTime { get; set; }
        public DateTime Period { get; set; }
        public int Number { get; set; }
        public int Quantity { get; set; }
        public decimal ValueSum { get; set; }
        public string Comment { get; set; }
        public int Author { get; set; }
        public virtual ICollection<RechargeOper> RechargeOpers { get; set; }
        public virtual User User { get; set; }
    }
}