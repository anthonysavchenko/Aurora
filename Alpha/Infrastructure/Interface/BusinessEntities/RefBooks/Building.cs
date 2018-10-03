using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Дом
    /// </summary>
    public class Building : DomainObject
    {
        private string _number;
        /// <summary>
        /// Номер дома
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

        private string _zipCode;
        /// <summary>
        /// Индекс
        /// </summary>
        public string ZipCode
        {
            get
            {
                Load();
                return _zipCode;
            }
            set
            {
                Load();
                _zipCode = value;
            }
        }

        private Street _street;
        /// <summary>
        /// Улица
        /// </summary>
        public Street Street
        {
            get
            {
                Load();
                return _street;
            }
            set
            {
                Load();
                _street = value;
            }
        }

        /// <summary>
        /// Общедомовые счетчики
        /// </summary>
        public Dictionary<string, CommonCounter> CommonCounters { get; } = new Dictionary<string, CommonCounter>();

        private short _floorCount;
        /// <summary>
        /// Количество этажей
        /// </summary>
        public short FloorCount
        {
            get
            {
                Load();
                return _floorCount;
            }
            set
            {
                Load();
                _floorCount = value;
            }
        }

        private byte _entranceCount;
        /// <summary>
        /// Количество подъездов
        /// </summary>
        public byte EntranceCount
        {
            get
            {
                Load();
                return _entranceCount;
            }
            set
            {
                Load();
                _entranceCount = value;
            }
        }

        private string _note;
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note
        {
            get
            {
                Load();
                return _note;
            }
            set
            {
                Load();
                _note = value;
            }
        }

        private string _fiasID;
        /// <summary>
        /// Код ФИАС
        /// </summary>
        public string FiasID
        {
            get
            {
                Load();
                return _fiasID;
            }
            set
            {
                Load();
                _fiasID = value;
            }
        }

        private decimal _nonResidentialPlaceArea;
        /// <summary>
        /// Площадь нежилых помещений
        /// </summary>
        public decimal NonResidentialPlaceArea
        {
            get
            {
                Load();
                return _nonResidentialPlaceArea;
            }
            set
            {
                Load();
                _nonResidentialPlaceArea = value;
            }
        }

        /// <summary>
        /// Места общего пользования
        /// </summary>
        public List<PublicPlace> PublicPlaces { get; } = new List<PublicPlace>();

        private BankDetail _bankDetail;
        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public BankDetail BankDetail
        {
            get
            {
                Load();
                return _bankDetail;
            }
            set
            {
                Load();
                _bankDetail = value;
            }
        }

        private CounterValueCollectDistrict _counterValueCollectDistrict;
        /// <summary>
        /// Участок сбора показаний приборов учета
        /// </summary>
        public CounterValueCollectDistrict CounterValueCollectDistrict
        {
            get
            {
                Load();
                return _counterValueCollectDistrict;
            }
            set
            {
                Load();
                _counterValueCollectDistrict = value;
            }
        }
    }
}