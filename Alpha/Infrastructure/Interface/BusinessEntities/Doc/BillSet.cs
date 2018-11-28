using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Набор квитанций
    /// </summary>
    public class BillSet : DomainObject
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

        private BillType _billType;
        /// <summary>
        /// Вид квитанций в наборе
        /// </summary>
        public BillType BillType
        {
            get
            {
                Load();
                return _billType;
            }
            set
            {
                Load();
                _billType = value;
            }
        }

        private short _quantity;
        /// <summary>
        /// Количество квитанций
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
        /// Общая сумма
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
    }
}
