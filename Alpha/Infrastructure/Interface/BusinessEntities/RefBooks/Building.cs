using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Дом
    /// </summary>
    public class Building : DomainObject
    {
        private string _street;
        /// <summary>
        /// Улица
        /// </summary>
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

        private string _number;
        /// <summary>
        /// Номер дома
        /// </summary>
        public string Number
        {
            get
            {
                Load();
                return _number;
            }
            set
            {
                Load();
                _number = value;
            }
        }

        private BuildingContract _buildingContract;

        public BuildingContract BuildingContract
        {
            get
            {
                Load();
                return _buildingContract;
            }
            set
            {
                Load();
                _buildingContract = value;
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

        /// <summary>
        /// ОДПУ
        /// </summary>
        public Dictionary<string, BuildingCounter> Counters { get; } = new Dictionary<string, BuildingCounter>();
    }
}
