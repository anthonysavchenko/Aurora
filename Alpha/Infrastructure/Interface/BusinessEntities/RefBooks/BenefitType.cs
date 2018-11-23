using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class BenefitType : DomainObject
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

        private BenefitRuleType _benefitRule;

        /// <summary>
        /// Правило начисления льготы
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

        private byte? _fixedPercent;

        /// <summary>
        /// Фиксированный процент льготы
        /// </summary>
        public byte? FixedPercent
        {
            get
            {
                Load();
                return _fixedPercent;
            }
            set
            {
                Load();
                _fixedPercent = value;
            }
        }
    }
}