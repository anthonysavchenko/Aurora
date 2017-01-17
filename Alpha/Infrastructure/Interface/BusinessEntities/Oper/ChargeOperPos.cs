namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции начисления
    /// </summary>
    public class ChargeOperPos : BaseChargeOperPos
    {
        private ChargeOper _chargeOper;
        /// <summary>
        /// Операция начисления
        /// </summary>
        public new ChargeOper ChargeOper
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