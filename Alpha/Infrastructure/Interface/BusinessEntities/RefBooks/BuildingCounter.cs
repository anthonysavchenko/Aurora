using System;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class BuildingCounter : DomainObject
    {
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

        private UtilityService _utilityService;

        public UtilityService UtilityService
        {
            get
            {
                Load();
                return _utilityService;
            }
            set
            {
                Load();
                _utilityService = value;
            }
        }

        private byte _coefficient;

        public byte Coefficient
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

        private Building _building;

        public Building Building
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

        private DateTime? _checkedSince;

        public DateTime? CheckedSince
        {
            get
            {
                Load();
                return _checkedSince;
            }
            set
            {
                Load();
                _checkedSince = value;
            }
        }

        private DateTime? _checkedTill;

        public DateTime? CheckedTill
        {
            get
            {
                Load();
                return _checkedTill;
            }
            set
            {
                Load();
                _checkedTill = value;
            }
        }
    }
}
