using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция дополнительного льготного платежа
    /// </summary>
    public class RebenefitOperPos : DomainObject
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

        private RebenefitOper _rebenefitOper;
        /// <summary>
        /// Операция дополнительного льготного платежа
        /// </summary>
        public RebenefitOper RebenefitOper
        {
            get
            {
                Load();
                return _rebenefitOper;
            }
            set
            {
                Load();
                _rebenefitOper = value;
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