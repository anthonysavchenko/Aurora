using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция льготного платежа
    /// </summary>
    public class BenefitOperPos : DomainObject
    {
        private BenefitType.BenefitRuleType _benefitRule;
        /// <summary>
        /// Benefit rule
        /// </summary>
        public BenefitType.BenefitRuleType BenefitRule
        {
            get
            {
                Load();
                return _benefitRule;
            }
            set
            {
                Load();
                _benefitRule = value;
            }
        }

        private decimal _value;
        /// <summary>
        /// Value
        /// </summary>
        public decimal Value
        {
            get
            {
                Load();
                return _value;
            }
            set
            {
                Load();
                _value = value;
            }
        }

        private Service _service;
        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service
        {
            get
            {
                Load();
                return _service;
            }
            set
            {
                Load();
                _service = value;
            }
        }

        private BenefitOper _benefitOper;
        /// <summary>
        /// Операция льготного платежа
        /// </summary>
        public BenefitOper BenefitOper
        {
            get
            {
                Load();
                return _benefitOper;
            }
            set
            {
                Load();
                _benefitOper = value;
            }
        }

        private Contractor _contractor;
        /// <summary>
        /// Подрядчик
        /// </summary>
        public Contractor Contractor
        {
            get
            {
                Load();
                return _contractor;
            }
            set
            {
                Load();
                _contractor = value;
            }
        }
    }
}