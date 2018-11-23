using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция корректировки льготного платежа
    /// </summary>
    public class BenefitCorrectionOperPos : DomainObject
    {
        private BenefitRuleType _benefitRule;
        /// <summary>
        /// Benefit rule
        /// </summary>
        public BenefitRuleType BenefitRule
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

        private BenefitCorrectionOper _benefitCorrectionOper;
        /// <summary>
        /// Операция льготного платежа
        /// </summary>
        public BenefitCorrectionOper BenefitCorrectionOper
        {
            get
            {
                Load();
                return _benefitCorrectionOper;
            }
            set
            {
                Load();
                _benefitCorrectionOper = value;
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