using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Индивидуальный прибор учета
    /// </summary>
    public class PrivateCounter : DomainObject
    {
        private string _model;
        /// <summary>
        /// Модель прибора учета
        /// </summary>
        public string Model
        {
            get
            {
                Load();
                return _model;
            }
            set
            {
                Load();
                _model = value;
            }
        }

        private string _number;
        /// <summary>
        /// Номер
        /// </summary>
        public string Number
        {
            get
            {
                Load();
                return _number;
            }
            set
            {
                Load();
                _number = value;
            }
        }

        private Customer _customer;
        /// <summary>
        /// Привязка услуги к абоненту
        /// </summary>
        public Customer Customer
        {
            get
            {
                Load();
                return _customer;
            }
            set
            {
                Load();
                _customer = value;
            }
        }
    }
}
