﻿using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Данные из счетчиков ежемесячной квитанции 
    /// </summary>
    public class RegularBillDocCounterPos : DomainObject
    {
        private RegularBillDoc _regularBillDoc;
        /// <summary>
        /// Ежемесячная квитанция
        /// </summary>
        public RegularBillDoc RegularBillDoc
        {
            get
            {
                Load();
                return _regularBillDoc;
            }
            set
            {
                Load();
                _regularBillDoc = value;
            }
        }

        private string _number;
        /// <summary>
        /// Номер счетчика
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

        private decimal _prevValue;
        /// <summary>
        /// Предыдущее показание
        /// </summary>
        public decimal PrevValue
        {
            get
            {
                Load();
                return _prevValue;
            }
            set
            {
                Load();
                _prevValue = value;
            }
        }

        private decimal? _curValue;
        /// <summary>
        /// Текущее показание
        /// </summary>
        public decimal? CurValue
        {
            get
            {
                Load();
                return _curValue;
            }
            set
            {
                Load();
                _curValue = value;
            }
        }

        private decimal _consumption;
        /// <summary>
        /// Потребление
        /// </summary>
        public decimal Consumption
        {
            get
            {
                Load();
                return _consumption;
            }
            set
            {
                Load();
                _consumption = value;
            }
        }

        private decimal _rate;
        /// <summary>
        /// Ставка
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

        private string _ServiceName;

        /// <summary>
        /// Наименование услуги
        /// </summary>
        public string ServiceName
        {
            get
            {
                Load();
                return _ServiceName;
            }
            set
            {
                Load();
                _ServiceName = value;
            }
        }

        private string _measure;

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Measure
        {
            get
            {
                Load();
                return _measure;
            }
            set
            {
                Load();
                _measure = value;
            }
        }

        private decimal? _norm;
        
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal? Norm
        {
            get
            {
                Load();
                return _norm;
            }
            set
            {
                Load();
                _norm = value;
            }
        }
    }
}
