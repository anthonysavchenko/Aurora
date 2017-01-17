﻿using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Набор квитанций
    /// </summary>
    public class BillSet : DomainObject
    {
        /// <summary>
        /// Виды квитанций
        /// </summary>
        public enum BillTypes
        {
            /// <summary>
            /// Ежемесячные
            /// </summary>
            Regular,

            /// <summary>
            /// Долговые
            /// </summary>
            Debt,

            /// <summary>
            /// За период
            /// </summary>
            Total
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

        private BillTypes _billType;
        /// <summary>
        /// Вид квитанций в наборе
        /// </summary>
        public BillTypes BillType
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
