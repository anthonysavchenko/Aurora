using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Набор начислений
    /// </summary>
    public class ChargeSet : DomainObject
    {
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

        private int _number;
        /// <summary>
        /// Номер
        /// </summary>
        public int Number
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

        private int _quantity;
        /// <summary>
        /// Количество платежей
        /// </summary>
        public int Quantity
        {
            get
            {
                Load();
                return _quantity;
            }
            set
            {
                Load();
                _quantity = value;
            }
        }

        private decimal _valueSum;
        /// <summary>
        /// Сумма платежей
        /// </summary>
        public decimal ValueSum
        {
            get
            {
                Load();
                return _valueSum;
            }
            set
            {
                Load();
                _valueSum = value;
            }
        }

        private User _author;
        /// <summary>
        /// Автор
        /// </summary>
        public User Author
        {
            get
            {
                Load();
                return _author;
            }
            set
            {
                Load();
                _author = value;
            }
        }
    }
}