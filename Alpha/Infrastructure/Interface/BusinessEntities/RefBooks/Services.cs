using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class Service : DomainObject
    {
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