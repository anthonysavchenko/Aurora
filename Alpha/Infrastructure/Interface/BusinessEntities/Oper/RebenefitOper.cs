using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция дополнительного льготного платежа
    /// </summary>
    public class RebenefitOper : DomainObject
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

        private RechargeOper _rechargeOper;
        /// <summary>
        /// Операция дополнительного начисления
        /// </summary>
        public RechargeOper RechargeOper
        {
            get
            {
                Load();
                return _rechargeOper;
            }
            set
            {
                Load();
                _rechargeOper = value;
            }
        }
    }
}