using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class FinePos : DomainObject
    {
        private decimal _value;
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

        private Customer _customer;
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

        private FineDoc _doc;
        public FineDoc Doc
        {
            get
            {
                Load();
                return _doc;
            }
            set
            {
                Load();
                _doc = value;
            }
        }
    }
}
