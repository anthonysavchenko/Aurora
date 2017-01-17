using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class Intermediary : DomainObject
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

        private decimal _rate;
        /// <summary>
        /// Процент, взимаемый посредником
        /// </summary>
        public decimal Rate
        {
            get
            {
                Load();
                return _rate;
            }
            set
            {
                Load();
                _rate = value;
            }
        }
    }
}