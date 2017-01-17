namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции дополнительного начисления
    /// </summary>
    public class RechargeOperPos : BaseChargeOperPos
    {
        private RechargeOper _chargeOper;
        /// <summary>
        /// Операция дополнительного начисления
        /// </summary>
        public new RechargeOper ChargeOper
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
    }
}