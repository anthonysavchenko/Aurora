using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Позиция квитанции с информацией о начислениях за содержание общедомового имущества
    /// </summary>
    public class RegularBillDocPublicPlacePos : DomainObject
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

        private string _service;
        /// <summary>
        /// Услуга
        /// </summary>
        public string Service
        {
            get
            {
                Load();
                return _service;
            }
            set
            {
                Load();
                _service = value;
            }
        }

        private decimal _norm;
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Norm
        {
            get
            {
                Load();
                return _norm;
            }
            set
            {
                Load();
                _norm = value;
            }
        }

        private string _normMeasure;
        /// <summary>
        /// Еденица измерения норматива
        /// </summary>
        public string NormMeasure
        {
            get
            {
                Load();
                return _normMeasure;
            }
            set
            {
                Load();
                _normMeasure = value;
            }
        }

        private decimal _rate;
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Rate
        {
            get
            {
                Load();
                return _rate;
            }
            set
            {
                Load();
                _rate = value;
            }
        }

        private decimal _area;
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Area
        {
            get
            {
                Load();
                return _area;
            }
            set
            {
                Load();
                _area = value;
            }
        }

        private decimal _serviceVolume;
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal ServiceVolume
        {
            get
            {
                Load();
                return _serviceVolume;
            }
            set
            {
                Load();
                _serviceVolume = value;
            }
        }

        private decimal _total;
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Total
        {
            get
            {
                Load();
                return _total;
            }
            set
            {
                Load();
                _total = value;
            }
        }
    }
}