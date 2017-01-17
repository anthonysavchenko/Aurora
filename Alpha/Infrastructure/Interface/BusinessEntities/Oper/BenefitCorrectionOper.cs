using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция корректировки льготы
    /// </summary>
    public class BenefitCorrectionOper : DomainObject
    {
        private decimal _value;
        /// <summary>
        /// Сумма операции
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

        private ChargeCorrectionOper _chargeCorrectionOper;
        /// <summary>
        /// Операция корректировки начисления
        /// </summary>
        public ChargeCorrectionOper ChargeCorrectionOper
        {
            get
            {
                Load();
                return _chargeCorrectionOper;
            }
            set
            {
                Load();
                _chargeCorrectionOper = value;
            }
        }
    }
}