using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Данные из общих счетчиков ежемесячной квитанции
    /// </summary>
    public class RegularBillDocSharedCounterPos : DomainObject
    {
        private RegularBillDoc _regularBillDoc;
        /// <summary>
        /// Ежемесячная квитанция
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

        private decimal _sharedCounterValue;
        /// <summary>
        /// Показание счетчика
        /// </summary>
        public decimal SharedCounterValue
        {
            get
            {
                Load();
                return _sharedCounterValue;
            }
            set
            {
                Load();
                _sharedCounterValue = value;
            }
        }

        private decimal _sharedCharge;
        /// <summary>
        /// Начисления по счетчику
        /// </summary>
        public decimal SharedCharge
        {
            get
            {
                Load();
                return _sharedCharge;
            }
            set
            {
                Load();
                _sharedCharge = value;
            }
        }
    }
}