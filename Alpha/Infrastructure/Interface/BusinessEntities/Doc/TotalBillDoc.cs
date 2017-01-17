using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Квитанция за период
    /// </summary>
    public class TotalBillDoc : Bill
    {
        public TotalBillDoc()
            : base()
        {
            _totalBillDocPoses = new Dictionary<string, TotalBillDocPos>();
        }

        private Customer _customer;
        /// <summary>
        /// Плательщик
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

        private string _square;
        /// <summary>
        /// Площадь
        /// </summary>
        public string Square
        {
            get
            {
                Load();
                return _square;
            }
            set
            {
                Load();
                _square = value;
            }
        }

        private int _residentsCount;
        /// <summary>
        /// Количество проживающих
        /// </summary>
        public int ResidentsCount
        {
            get
            {
                Load();
                return _residentsCount;
            }
            set
            {
                Load();
                _residentsCount = value;
            }
        }

        private DateTime? _startPeriod;
        /// <summary>
        /// Период начала всех начислений
        /// </summary>
        public DateTime? StartPeriod
        {
            get
            {
                Load();
                return _startPeriod;
            }
            set
            {
                Load();
                _startPeriod = value;
            }
        }

        private Dictionary<string, TotalBillDocPos> _totalBillDocPoses;
        /// <summary>
        /// Данные по начислениям
        /// </summary>        
        public Dictionary<string, TotalBillDocPos> TotalBillDocPoses
        {
            get
            {
                Load();
                return _totalBillDocPoses;
            }
        }
    }
}
