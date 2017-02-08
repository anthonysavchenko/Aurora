using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class BankDetail : Entity
    {
        /// <summary>
        /// Наименование банка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// БИК
        /// </summary>
        public string BIK { get; set; }
        
        /// <summary>
        /// КПП
        /// </summary>
        public string KPP { get; set; }

        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        public string CorrAccount { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string INN { get; set; }

        public virtual ICollection<Building> Buildings { get; set; } 
    }
}
