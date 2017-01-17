using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Domain.DocLine;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Житель
    /// </summary>
    public class Resident : DomainObject, IDocLine
    {
        public enum Relationship : byte
        {
            Unknown,
            Father,
            Mother,
            Son,
            Daughter,
            Grandson,
            Granddaughter,
            Relative,
            Owner
        }

        private string _firstName;
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get
            {
                Load();
                return _firstName;
            }
            set
            {
                Load();
                _firstName = value;
            }
        }

        private string _patronymic;
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic
        {
            get
            {
                Load();
                return _patronymic;
            }
            set
            {
                Load();
                _patronymic = value;
            }
        }

        private string _surname;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname
        {
            get
            {
                Load();
                return _surname;
            }
            set
            {
                Load();
                _surname = value;
            }
        }

        private BenefitType _benefitType;
        /// <summary>
        /// Тип льготы
        /// </summary>
        public BenefitType BenefitType
        {
            get
            {
                Load();
                return _benefitType;
            }
            set
            {
                Load();
                _benefitType = value;
            }
        }

        private Customer _customer;
        /// <summary>
        /// Абонент, к которому относится житель
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

        private Relationship _ownerRelationship;
        /// <summary>
        /// Связь с собственником
        /// </summary>
        public Relationship OwnerRelationship
        {
            get
            {
                Load();
                return _ownerRelationship;
            }
            set
            {
                Load();
                _ownerRelationship = value;
            }
        }

        private string _residentDocument;
        /// <summary>
        /// Документ, подтверждающий льготу
        /// </summary>
        public string ResidentDocument
        {
            get
            {
                Load();
                return _residentDocument;
            }
            set
            {
                Load();
                _residentDocument = value;
            }
        }

        #region Implementation of IDocLine

        /// <summary>
        /// Документ к которому принадлежит позиция.
        /// </summary>
        public object Doc
        {
            get
            {
                return Customer;
            }
            set
            {
                Customer = (Customer)value;
            }
        }

        #endregion
    }
}