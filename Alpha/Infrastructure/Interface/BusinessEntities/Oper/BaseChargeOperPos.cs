using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper
{
    /// <summary>
    /// Базовая позиция операций начисления
    /// </summary>
    public abstract class BaseChargeOperPos : DomainObject
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

        private BaseChargeOper _chargeOper;
        /// <summary>
        /// Операция начисления
        /// </summary>
        public BaseChargeOper ChargeOper
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
