using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;
namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Building : Entity
    {
        public Building()
        {
            CommonCounters = new List<CommonCounter>();
            Customers = new List<Customer>();
        }
        public int StreetID { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public short FloorCount { get; set; }
        public byte EntranceCount { get; set; }
        public string Note { get; set; }
        public virtual Street Street { get; set; }
        public virtual ICollection<CommonCounter> CommonCounters { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}