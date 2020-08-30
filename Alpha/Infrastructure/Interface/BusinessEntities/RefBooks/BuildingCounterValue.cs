using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class BuildingCounterValue : DomainObject
    {
        private DateTime _month;
        /// <summary>
        /// Период
        /// </summary>
        public DateTime Month
        {
            get
            {
                Load();
                return _month;
            }
            set
            {
                Load();
                _month = value;
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

        private decimal? _currentValue;
        /// <summary>
        /// Значение, показание
        /// </summary>
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

        private DateTime? _currentDate;
        /// <summary>
        /// Значение, показание
        /// </summary>
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

        private BuildingCounter _buildingCounter;
        /// <summary>
        /// Счетчик
        /// </summary>
        public BuildingCounter BuildingCounter
        {
            get
            {
                Load();
                return _buildingCounter;
            }
            set
            {
                Load();
                _buildingCounter = value;
            }
        }

        private BuildingValuesUploadPos _buildingValuesUploadPos;

        public BuildingValuesUploadPos BuildingValuesUploadPos
        {
            get
            {
                Load();
                return _buildingValuesUploadPos;
            }
            set
            {
                Load();
                _buildingValuesUploadPos = value;
            }
        }
    }
}
