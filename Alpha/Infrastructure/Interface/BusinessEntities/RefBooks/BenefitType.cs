using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class BenefitType : DomainObject
    {
        /// <summary>
        /// Тип правила начисления льготы
        /// </summary>
        public enum BenefitRuleType : byte
        {
            /// <summary>
            /// 50% по норме площади в зависимости от количества жильцов
            /// </summary>
            FiftyPercentBySquare,

            /// <summary>
            /// Фиксированный процент
            /// </summary>
            FixedPercent
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