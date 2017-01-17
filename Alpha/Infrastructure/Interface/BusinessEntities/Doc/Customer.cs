using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Domain.Doc;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Абонент
    /// </summary>
    public class Customer : TransDoc
    {
        /// <summary>
        /// Тип собственника
        /// </summary>
        public enum OwnerTypes
        {
            /// <summary>
            /// Собственнк неизвестен
            /// </summary>
            Unknown = 0,

            /// <summary>
            /// Физическое лицо
            /// </summary>
            PhysicalPerson,

            /// <summary>
            /// Юридицеское лицо
            /// </summary>
            JuridicalPerson
        }

        public Customer()
        {
            _customerPoses = new Dictionary<string, CustomerPos>();
            _residents = new Dictionary<string, Resident>();
        }

        private OwnerTypes _ownerType;

        /// <summary>
        /// Тип собственника
        /// </summary>
        public OwnerTypes OwnerType
        {
            get
            {
                Load();
                return _ownerType;
            }
            set
            {
                Load();
                _ownerType = value;
            }
        }

        private string _physicalPersonFullName;

        /// <summary>
        /// Полное имя физического лица
        /// </summary>
        public string PhysicalPersonFullName
        {
            get
            {
                Load();
                return _physicalPersonFullName;
            }
            set
            {
                Load();
                _physicalPersonFullName = value;
            }
        }

        private string _physicalPersonShortName;

        /// <summary>
        /// Краткое имя физического лица
        /// </summary>
        public string PhysicalPersonShortName
        {
            get
            {
                Load();
                return _physicalPersonShortName;
            }
            set
            {
                Load();
                _physicalPersonShortName = value;
            }
        }

        private string _juridicalPersonFullName;

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        public string JuridicalPersonFullName
        {
            get
            {
                Load();
                return _juridicalPersonFullName;
            }
            set
            {
                Load();
                _juridicalPersonFullName = value;
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

        private readonly Dictionary<string, CustomerPos> _customerPoses;

        /// <summary>
        /// Строки документа.
        /// </summary>        
        public Dictionary<string, CustomerPos> CustomerPoses
        {
            get
            {
                Load();
                return _customerPoses;
            }
        }

        private bool _isPivate;

        /// <summary>
        /// Приватизированная квартира
        /// </summary>
        public bool IsPrivate
        {
            get
            {
                Load();
                return _isPivate;
            }
            set
            {
                Load();
                _isPivate = value;
            }
        }

        private int _roomsCount;

        /// <summary>
        /// Количество комнат
        /// </summary>
        public int RoomsCount
        {
            get
            {
                Load();
                return _roomsCount;
            }
            set
            {
                Load();
                _roomsCount = value;
            }
        }

        private string _apartment;
        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment
        {
            get
            {
                Load();
                return _apartment;
            }
            set
            {
                Load();
                _apartment = value;
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

        private decimal _square;
        /// <summary>
        /// Площадь квартиры
        /// </summary>
        public decimal Square
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

        private string _comment;
        /// <summary>
        /// Примечание
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

        private readonly Dictionary<string, Resident> _residents;
        /// <summary>
        /// Строки документа.
        /// </summary>        
        public Dictionary<string, Resident> Residents
        {
            get
            {
                Load();
                return _residents;
            }
        }

        private short _floor;
        /// <summary>
        /// Этаж
        /// </summary>
        public short Floor
        {
            get
            {
                Load();
                return _floor;
            }
            set
            {
                Load();
                _floor = value;
            }
        }

        private byte _entrance;
        /// <summary>
        /// Подъезд
        /// </summary>
        public byte Entrance
        {
            get
            {
                Load();
                return _entrance;
            }
            set
            {
                Load();
                _entrance = value;
            }
        }

        private bool _liftPresence;
        /// <summary>
        /// Наличие лифта
        /// </summary>
        public bool LiftPresence
        {
            get
            {
                Load();
                return _liftPresence;
            }
            set
            {
                Load();
                _liftPresence = value;
            }
        }

        private bool _rubbishChutePresence;
        /// <summary>
        /// Наличие мусоропровода
        /// </summary>
        public bool RubbishChutePresence
        {
            get
            {
                Load();
                return _rubbishChutePresence;
            }
            set
            {
                Load();
                _rubbishChutePresence = value;
            }
        }

        private bool _billSendingSubscription;
        public bool BillSendingSubscription
        {
            get
            {
                Load();
                return _billSendingSubscription;
            }
            set
            {
                Load();
                _billSendingSubscription = value;
            }
        }

        private User _user;
        /// <summary>
        /// Web пользователь
        /// </summary>
        public User User
        {
            get
            {
                Load();
                return _user;
            }
            set
            {
                Load();
                _user = value;
            }
        }

        private bool _debtsRepayment;
        /// <summary>
        /// Флаг реструктуризации долга
        /// </summary>
        public bool DebtsRepayment
        {
            get
            {
                Load();
                return _debtsRepayment;
            }
            set
            {
                Load();
                _debtsRepayment = value;
            }
        }
    }
}