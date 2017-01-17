
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class Contractor : DomainObject
    {
        private string _name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get 
            { 
                Load();
                return _name; 
            }

            set 
            { 
                Load();
                _name = value; 
            }
        }

        private string _code;

        /// <summary>
        /// Код
        /// </summary>
        public string Code
        {
            get 
            { 
                Load(); 
                return _code; 
            }

            set 
            { 
                Load(); 
                _code = value; 
            }
        }

        private string _contactInfo;

        /// <summary>
        /// Контактная информация
        /// </summary>
        public string ContactInfo
        {
            get
            {
                Load();
                return _contactInfo;
            }
            set
            {
                Load();
                _contactInfo = value;
            }
        }
    }
}