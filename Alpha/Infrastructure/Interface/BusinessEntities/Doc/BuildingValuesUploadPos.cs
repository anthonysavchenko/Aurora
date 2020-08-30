using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class BuildingValuesUploadPos : DomainObject
    {
        private BuildingValuesUpload _buildingValuesUpload;

        public BuildingValuesUpload BuildingValuesUpload
        {
            get
            {
                Load();
                return _buildingValuesUpload;
            }
            set
            {
                Load();
                _buildingValuesUpload = value;
            }
        }

        private string _street;

        public string Street
        {
            get
            {
                Load();
                return _street;
            }
            set
            {
                Load();
                _street = value;
            }
        }

        private string _building;

        public string Building
        {
            get
            {
                Load();
                return _building;
            }
            set
            {
                Load();
                _building = value;
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

        private byte? _coefficient;

        public byte? Coefficient
        {
            get
            {
                Load();
                return _coefficient;
            }
            set
            {
                Load();
                _coefficient = value;
            }
        }

        private decimal? _currentValue;

        public decimal? CurrentValue
        {
            get
            {
                Load();
                return _currentValue;
            }
            set
            {
                Load();
                _currentValue = value;
            }
        }

        private decimal? _prevValue;

        public decimal? PrevValue
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

        private DateTime? _currentDate;

        public DateTime? CurrentDate
        {
            get
            {
                Load();
                return _currentDate;
            }
            set
            {
                Load();
                _currentDate = value;
            }
        }

        private string _errorDescription;

        public string ErrorDescription
        {
            get
            {
                Load();
                return _errorDescription;
            }
            set
            {
                Load();
                _errorDescription = value;
            }
        }

        private string _exceptionMessage;

        public string ExceptionMessage
        {
            get
            {
                Load();
                return _exceptionMessage;
            }
            set
            {
                Load();
                _exceptionMessage = value;
            }
        }
    }
}
