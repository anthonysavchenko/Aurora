using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Частный счетчик
    /// </summary>
    public class PrivateCounter : DomainObject
    {
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

        private Service _service;
        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service
        {
            get
            {
                Load();
                return _service;
            }
            set
            {
                Load();
                _service = value;
            }
        }

        private readonly Dictionary<string, PrivateCounterValue> _values = new Dictionary<string, PrivateCounterValue>();
        /// <summary>
        /// Значения/показания
        /// </summary>
        public Dictionary<string, PrivateCounterValue> Values
        {
            get
            {
                return _values;
            }
        }
    }
}