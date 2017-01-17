using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция корректировки переплаты
    /// </summary>
    public class OverpaymentCorrectionOper : DomainObject
    {
        private DateTime _period;
        /// <summary>
        /// Период
        /// </summary>
        public DateTime Period
        {
            get
            {
                Load();
                return _period;
            }
            set
            {
                Load();
                _period = value;
            }
        }

        private decimal _value;
        /// <summary>
        /// Сумма корректировки
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
    }
}