using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class Service : DomainObject
    {
        /// <summary>
        /// Тип правила начисления сборов за услугу
        /// </summary>
        public enum ChargeRuleType : byte
        {
            /// <summary>
            /// Фиксированное начиление
            /// </summary>
            FixedRate = 0,

            /// <summary>
            /// Начисление за квадратный метр площади
            /// </summary>
            SquareRate = 1,

            /// <summary>
            /// Начисление по тарифу за количество жильцов
            /// </summary>
            ResidentsRate = 2,

            /// <summary>
            /// Начисление по счетчику
            /// </summary>
            CounterRate = 3,

            /// <summary>
            /// Начиление за квадратный метр только за содержание общедомового имещества (СОД)
            /// </summary>
            PublicPlaceAreaRate = 4,

            /// <summary>
            /// Начисление банковской коммисси в виде процента от расходов СОД
            /// </summary>
            PublicPlaceBankCommission = 5,

            /// <summary>
            /// Начисление по общему счетчику пропорционально площади помещения
            /// </summary>
            CommonCounterByAreaRate = 6,

            /// <summary>
            /// Начисление по общему счетчику пропорционально отапливаемой площади помещения
            /// </summary>
            CommonCounterByHeatedAreaRate = 7,

            /// <summary>
            /// Начисление по общему счетчику пропорционально сумме площадей абонентов, которым назначена услуга
            /// </summary>
            CommonCounterByAssignedCustomerAreaRate = 8
        }

        private string _name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get
            {
                Load();
                return _name;
            }

            set
            {
                Load();
                _name = value;
            }
        }

        private string _code;

        /// <summary>
        /// Код
        /// </summary>
        public string Code
        {
            get
            {
                Load();
                return _code;
            }

            set
            {
                Load();
                _code = value;
            }
        }

        private ServiceType _serviceType;

        /// <summary>
        /// Код
        /// </summary>
        public ServiceType ServiceType
        {
            get
            {
                Load();
                return _serviceType;
            }

            set
            {
                Load();
                _serviceType = value;
            }
        }

        private ChargeRuleType _chargeRule;

        /// <summary>
        /// Правило начисления сборов за услугу
        /// </summary>
        public ChargeRuleType ChargeRule
        {
            get
            {
                Load();
                return _chargeRule;
            }
            set
            {
                Load();
                _chargeRule = value;
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

        private string _measure;
        /// <summary>
        /// Единица измерения норматива
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
    }
}