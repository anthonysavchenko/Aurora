using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class PaymentSet : DomainObject
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

        private DateTime _paymentDate;
        /// <summary>
        /// Дата внесения платежа
        /// </summary>
        public DateTime PaymentDate
        {
            get
            {
                Load();
                return _paymentDate;
            }
            set
            {
                Load();
                _paymentDate = value;
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

        private bool _isFile;
        /// <summary>
        /// Загружено ли из файла
        /// </summary>
        public bool IsFile
        {
            get
            {
                Load();
                return _isFile;
            }
            set
            {
                Load();
                _isFile = value;
            }
        }

        private short _quantity;
        /// <summary>
        /// Количество платежей
        /// </summary>
        public short Quantity
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

        private string _comment;
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get
            {
                Load();
                return _comment;
            }
            set
            {
                Load();
                _comment = value;
            }
        }

        private Intermediary _intermediary;
        /// <summary>
        /// Посредник
        /// </summary>
        public Intermediary Intermediary
        {
            get
            {
                Load();
                return _intermediary;
            }
            set
            {
                Load();
                _intermediary = value;
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