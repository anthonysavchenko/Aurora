using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Операция начисления
    /// </summary>
    public class ChargeOper : BaseChargeOper
    {
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

        private RegularBillDoc _regularBillDoc;
        /// <summary>
        /// Квитанция ежемесячного начисления
        /// </summary>
        public RegularBillDoc RegularBillDoc
        {
            get
            {
                Load();
                return _regularBillDoc;
            }
            set
            {
                Load();
                _regularBillDoc = value;
            }
        }

        private BenefitOper _benefitOper;
        /// <summary>
        /// Операция начисления льготы
        /// </summary>
        public BenefitOper BenefitOper
        {
            get
            {
                Load();
                return _benefitOper;
            }
            set
            {
                Load();
                _benefitOper = value;
            }
        }
    }
}