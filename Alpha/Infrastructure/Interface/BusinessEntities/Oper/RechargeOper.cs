using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция дополнительного начисления
    /// </summary>
    public class RechargeOper : BaseChargeOper
    {
        private RechargeSet _chargeSet;
        /// <summary>
        /// Набор дополнительных начислений
        /// </summary>
        public new RechargeSet ChargeSet
        {
            get
            {
                Load();
                return _chargeSet;
            }
            set
            {
                Load();
                _chargeSet = value;
            }
        }

        private ChargeOper _chargeOper;
        /// <summary>
        /// Операция начисления, в соответствии с которой делается перерасчет (доп. начисление)
        /// </summary>
        /// <remarks>Может быть null</remarks>
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

        private ChargeCorrectionOper _chargeCorrectionOper;
        /// <summary>
        /// Операция корректировки начисления
        /// </summary>
        /// <remarks>Может быть null</remarks>
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

        private RebenefitOper _rebenefitOper;
        /// <summary>
        /// Операция дополнительного начисления льготы
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
    }
}