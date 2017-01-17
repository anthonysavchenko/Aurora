using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Квитанция
    /// </summary>
    public class Bill : DomainObject
    {
        private BillSet _BillSet;
        /// <summary>
        /// Набор квитнаций
        /// </summary>
        public BillSet BillSet
        {
            get
            {
                Load();
                return _BillSet;
            }
            set
            {
                Load();
                _BillSet = value;
            }
        }

        private DateTime _creationDateTime;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDateTime
        {
            get
            {
                Load();
                return _creationDateTime;
            }
            set
            {
                Load();
                _creationDateTime = value;
            }
        }

        private DateTime _period;
        /// <summary>
        /// Период
        /// </summary>
        public DateTime Period
        {
            get
            {
                Load();
                return _period;
            }
            set
            {
                Load();
                _period = value;
            }
        }
 
        private decimal _value;
        /// <summary>
        /// Сумма долга
        /// </summary>
        public decimal Value
        {
            get
            {
                Load();
                return _value;
            }
            set
            {
                Load();
                _value = value;
            }
        }

        private string _account;
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account
        {
            get
            {
                Load();
                return _account;
            }
            set
            {
                Load();
                _account = value;
            }
        }

        private string _address;
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address
        {
            get
            {
                Load();
                return _address;
            }
            set
            {
                Load();
                _address = value;
            }
        }

        private string _owner;
        /// <summary>
        /// Плательщик
        /// </summary>
        public string Owner
        {
            get
            {
                Load();
                return _owner;
            }
            set
            {
                Load();
                _owner = value;
            }
        }
    }
}
