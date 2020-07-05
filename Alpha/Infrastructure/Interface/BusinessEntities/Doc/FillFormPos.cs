using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class FillFormPos : DomainObject
    {
        private FillForm _fillForm;

        public FillForm FillForm
        {
            get
            {
                Load();
                return _fillForm;
            }
            set
            {
                Load();
                _fillForm = value;
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

        private FillFormCounterType _counterType;

        public FillFormCounterType CounterType
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

        private string _counterModel;

        public string CounterModel
        {
            get
            {
                Load();
                return _counterModel;
            }
            set
            {
                Load();
                _counterModel = value;
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
    }
}
