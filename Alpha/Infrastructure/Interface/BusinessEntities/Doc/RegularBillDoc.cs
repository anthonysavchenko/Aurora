using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Ежемесячная квитанция
    /// </summary>
    public class RegularBillDoc : Bill
    {
        public RegularBillDoc()
            : base()
        {
            _regularBillDocSeviceTypePoses = new Dictionary<string, RegularBillDocSeviceTypePos>();
            _regularBillDocCounterPoses = new Dictionary<string, RegularBillDocCounterPos>();
            _regularBillDocSharedCounterPoses = new Dictionary<string, RegularBillDocSharedCounterPos>();
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

        private DateTime _payBeforeDateTime;
        /// <summary>
        /// Оплатить до
        /// </summary>
        public DateTime PayBeforeDateTime
        {
            get
            {
                Load();
                return _payBeforeDateTime;
            }
            set
            {
                Load();
                _payBeforeDateTime = value;
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

        private decimal _overpaymentValue;
        /// <summary>
        /// Переплата
        /// </summary>
        public decimal OverpaymentValue
        {
            get
            {
                Load();
                return _overpaymentValue;
            }
            set
            {
                Load();
                _overpaymentValue = value;
            }
        }

        private decimal _monthChargeValue;
        /// <summary>
        /// Месячные начисления
        /// </summary>
        public decimal MonthChargeValue
        {
            get
            {
                Load();
                return _monthChargeValue;
            }
            set
            {
                Load();
                _monthChargeValue = value;
            }
        }

        private string _contractorContactInfo;
        /// <summary>
        /// Контактная информация подрядчика
        /// </summary>
        public string ContractorContactInfo
        {
            get
            {
                Load();
                return _contractorContactInfo;
            }
            set
            {
                Load();
                _contractorContactInfo = value;
            }
        }

        private decimal _buildingArea;
        /// <summary>
        /// Месячные начисления
        /// </summary>
        public decimal BuildingArea
        {
            get
            {
                Load();
                return _buildingArea;
            }
            set
            {
                Load();
                _buildingArea = value;
            }
        }

        private Dictionary<string, RegularBillDocSeviceTypePos> _regularBillDocSeviceTypePoses;
        /// <summary>
        /// Данные по начислениям
        /// </summary>        
        public Dictionary<string, RegularBillDocSeviceTypePos> RegularBillDocSeviceTypePoses
        {
            get
            {
                Load();
                return _regularBillDocSeviceTypePoses;
            }
        }

        private Dictionary<string, RegularBillDocCounterPos> _regularBillDocCounterPoses;
        /// <summary>
        /// Данные счетчиков
        /// </summary>        
        public Dictionary<string, RegularBillDocCounterPos> RegularBillDocCounterPoses
        {
            get
            {
                Load();
                return _regularBillDocCounterPoses;
            }
        }

        private Dictionary<string, RegularBillDocSharedCounterPos> _regularBillDocSharedCounterPoses;
        /// <summary>
        /// Данные общих счетчиков
        /// </summary>        
        public Dictionary<string, RegularBillDocSharedCounterPos> RegularBillDocSharedCounterPoses
        {
            get
            {
                Load();
                return _regularBillDocSharedCounterPoses;
            }
        }
    }
}
