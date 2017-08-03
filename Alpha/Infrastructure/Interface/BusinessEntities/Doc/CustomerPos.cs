using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Domain.DocLine;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Абонент
    /// </summary>
    public class CustomerPos : BaseDocPos
    {
        /// <summary>
        /// Вспомогательный класс для хранения списка уникальных позиций абонента при множественном редактировании
        /// </summary>
        public class ServiceSinceTill
        {
            public int ServiceID
            {
                set;
                get;
            }

            public DateTime Since
            {
                set;
                get;
            }

            public DateTime Till
            {
                set;
                get;
            }
        }

        private DomService _service;

        /// <summary>
        /// Service
        /// </summary>
        public DomService Service
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

        private DateTime _since;

        /// <summary>
        /// Since
        /// </summary>
        public DateTime Since
        {
            get
            {
                Load();
                return _since;
            }
            set
            {
                Load();
                _since = value;
            }
        }

        private DateTime _till;

        /// <summary>
        /// Till
        /// </summary>
        public DateTime Till
        {
            get
            {
                Load();
                return _till;
            }
            set
            {
                Load();
                _till = value;
            }
        }

        private DomContractor _contractor;

        /// <summary>
        /// Contractor
        /// </summary>
        public DomContractor Contractor
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

        private decimal _rate;

        /// <summary>
        /// Rate
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

        private PrivateCounter _privateCounter;
        /// <summary>
        /// Прибор учета
        /// </summary>
        public PrivateCounter PrivateCounter
        {
            get
            {
                Load();
                return _privateCounter;
            }
            set
            {
                Load();
                _privateCounter = value;
            }
        }
    }
}
