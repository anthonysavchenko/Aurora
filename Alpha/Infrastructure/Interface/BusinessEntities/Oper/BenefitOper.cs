using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция льготного платежа
    /// </summary>
    public class BenefitOper : DomainObject
    {
        private decimal _value;
        /// <summary>
        /// Сумма
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

        private ChargeOper _chargeOper;
        /// <summary>
        /// Операция начисления
        /// </summary>
        public ChargeOper ChargeOper
        {
            get
            {
                Load();
                return _chargeOper;
            }
            set
            {
                Load();
                _chargeOper = value;
            }
        }

        private BenefitCorrectionOper _benefitCorrectionOper;
        /// <summary>
        /// Операция корректировки льготы
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
    }
}