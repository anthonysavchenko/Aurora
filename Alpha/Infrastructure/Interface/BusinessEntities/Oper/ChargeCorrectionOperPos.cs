using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Позиция операции корректировки начисления
    /// </summary>
    public class ChargeCorrectionOperPos : DomainObject
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

        private Service _service;
        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service
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

        private Contractor _contractor;
        /// <summary>
        /// Подрядчик
        /// </summary>
        public Contractor Contractor
        {
            get
            {
                Load();
                return _contractor;
            }
            set
            {
                Load();
                _contractor = value;
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