using System.Collections.Generic;
using Taumis.Alpha.Server.Core.Models.Docs;
namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Building : Entity
    {
        public int StreetID { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public short FloorCount { get; set; }
        public byte EntranceCount { get; set; }
        public string Note { get; set; }
        
        /// <summary>
        /// Код ФИАС
        /// </summary>
        public string FiasID { get; set; }
        
        /// <summary>
        /// Площадь нежилых помещений
        /// </summary>
        public decimal NonResidentialPlaceArea { get; set; }

        /// <summary>
        /// ID банковских реквизитов
        /// </summary>
        public int BankDetailID { get; set; }

        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public BankDetail BankDetail { get; set; }
        public virtual Street Street { get; set; }
        public virtual ICollection<CommonCounter> CommonCounters { get; set; } = new List<CommonCounter>();
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}