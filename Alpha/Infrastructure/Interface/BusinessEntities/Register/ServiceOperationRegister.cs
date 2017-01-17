using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Register
{
    /// <summary>
    /// Service operation register
    /// </summary>
    public class ServiceOperationRegister : DomainObject
    {
        public enum OperationTypes
        {
            Charge = 0,
            Payment = 1,
            PaymentCorrection = 2,
            Benefit = 3,
            OverpaymentCorrection = 4,
            Overpayment = 5,
            Recharge = 6,
            Rebenefit = 7,
            ChargeCorrection = 8,
            BenefitCorrection = 9,
            RebenefitCorrection = 10,
            RechargeCorrection = 11
        }

        private DateTime _operationDateTime;

        /// <summary>
        /// Operation register date and time
        /// </summary>
        public DateTime OperationDateTime
        {
            get
            {
                Load();
                return _operationDateTime;
            }
            set
            {
                Load();
                _operationDateTime = value;
            }
        }

        private int _operationID;

        /// <summary>
        /// Operation ID
        /// </summary>
        public int OperationID
        {
            get
            {
                Load();
                return _operationID;
            }
            set
            {
                Load();
                _operationID = value;
            }
        }

        private OperationTypes _operationType;

        /// <summary>
        /// Operation type
        /// </summary>
        public OperationTypes OperationType
        {
            get
            {
                Load();
                return _operationType;
            }
            set
            {
                Load();
                _operationType = value;
            }
        }

        private Service _service;

        /// <summary>
        /// Service
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

        private DateTime _servicePeriod;

        /// <summary>
        /// Period when service was delivered
        /// </summary>
        public DateTime ServicePeriod
        {
            get
            {
                Load();
                return _servicePeriod;
            }
            set
            {
                Load();
                _servicePeriod = value;
            }
        }

        private Customer _customer;

        /// <summary>
        /// Customer
        /// </summary>
        public Customer Customer
        {
            get
            {
                Load();
                return _customer;
            }
            set
            {
                Load();
                _customer = value;
            }
        }

        private decimal _value;

        /// <summary>
        /// Value
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
    }
}