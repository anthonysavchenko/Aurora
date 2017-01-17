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

        private decimal _rate;
        /// <summary>
        /// Тариф
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

        private CustomerPos _customerPos;
        /// <summary>
        /// Привязка услуги к абоненту
        /// </summary>
        public CustomerPos CustomerPos
        {
            get
            {
                Load();
                return _customerPos;
            }
            set
            {
                Load();
                _customerPos = value;
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