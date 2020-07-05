using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class RouteFormPos : DomainObject
    {
        private RouteForm _routeForm;

        public RouteForm RouteForm
        {
            get
            {
                Load();
                return _routeForm;
            }
            set
            {
                Load();
                _routeForm = value;
            }
        }

        private string _apartment;

        public string Apartment
        {
            get
            {
                Load();
                return _apartment;
            }
            set
            {
                Load();
                _apartment = value;
            }
        }

        private RouteFormCounterType _counterType;

        public RouteFormCounterType CounterType
        {
            get
            {
                Load();
                return _counterType;
            }
            set
            {
                Load();
                _counterType = value;
            }
        }

        private string _counterNumber;

        public string CounterNumber
        {
            get
            {
                Load();
                return _counterNumber;
            }
            set
            {
                Load();
                _counterNumber = value;
            }
        }

        private DateTime? _prevDate;

        public DateTime? PrevDate
        {
            get
            {
                Load();
                return _prevDate;
            }
            set
            {
                Load();
                _prevDate = value;
            }
        }

        private int? _prevValue;

        public int? PrevValue
        {
            get
            {
                Load();
                return _prevValue;
            }
            set
            {
                Load();
                _prevValue = value;
            }
        }

        private int? _prevDayValue;

        public int? PrevDayValue
        {
            get
            {
                Load();
                return _prevDayValue;
            }
            set
            {
                Load();
                _prevDayValue = value;
            }
        }

        private int? _prevNightValue;

        public int? PrevNightValue
        {
            get
            {
                Load();
                return _prevNightValue;
            }
            set
            {
                Load();
                _prevNightValue = value;
            }
        }

        private string _account;

        public string Account
        {
            get
            {
                Load();
                return _account;
            }
            set
            {
                Load();
                _account = value;
            }
        }

        private string _owner;

        public string Owner
        {
            get
            {
                Load();
                return _owner;
            }
            set
            {
                Load();
                _owner = value;
            }
        }

        private string _counterCapacity;

        public string CounterCapacity
        {
            get
            {
                Load();
                return _counterCapacity;
            }
            set
            {
                Load();
                _counterCapacity = value;
            }
        }

        private string _debt;

        public string Debt
        {
            get
            {
                Load();
                return _debt;
            }
            set
            {
                Load();
                _debt = value;
            }
        }

        private string _payed;

        public string Payed
        {
            get
            {
                Load();
                return _payed;
            }
            set
            {
                Load();
                _payed = value;
            }
        }

        private string _phone;

        public string Phone
        {
            get
            {
                Load();
                return _phone;
            }
            set
            {
                Load();
                _phone = value;
            }
        }

        private string _note;

        public string Note
        {
            get
            {
                Load();
                return _note;
            }
            set
            {
                Load();
                _note = value;
            }
        }
    }
}
