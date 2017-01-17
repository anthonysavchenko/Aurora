using System.Collections.Generic;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Общедомовой счетчик
    /// </summary>
    public class CommonCounter : DomainObject
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

        private Building _building;
        /// <summary>
        /// Дом
        /// </summary>
        public Building Building
        {
            get
            {
                Load();
                return _building;
            }
            set
            {
                Load();
                _building = value;
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

        private readonly Dictionary<string, CommonCounterValue> _values = new Dictionary<string, CommonCounterValue>();
        /// <summary>
        /// Показания счетчика
        /// </summary>
        public Dictionary<string, CommonCounterValue> Values
        {
            get
            {
                return _values;
            }
        }
    }
}