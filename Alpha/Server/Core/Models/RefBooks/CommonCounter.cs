using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class CommonCounter
    {
        public CommonCounter()
        {
            this.CommonCounterCoefficients = new List<CommonCounterCoefficient>();
            this.CommonCounterValues = new List<CommonCounterValue>();
        }

        public int ID { get; set; }
        public string Number { get; set; }
        public int BuildingID { get; set; }
        public int ServiceID { get; set; }
        public virtual Building Building { get; set; }
        public virtual ICollection<CommonCounterCoefficient> CommonCounterCoefficients { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<CommonCounterValue> CommonCounterValues { get; set; }
    }
}