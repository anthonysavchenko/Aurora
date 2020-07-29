using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    /// <summary>
    /// Показания индивидуальных приборов учета
    /// </summary>
    public class RouteFormValue : DomainObject
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

        private PrivateCounterValueType _valueType;

        public PrivateCounterValueType ValueType
        {
            get
            {
                Load();
                return _valueType;
            }
            set
            {
                Load();
                _valueType = value;
            }
        }


        private int? _value;
        /// <summary>
        /// Значение, показание
        /// </summary>
        public int? Value
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

        private PrivateCounter _privateCounter;
        /// <summary>
        /// Счетчик
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

        private RouteFormPos _routeFormPos;

        public RouteFormPos RouteFormPos
        {
            get
            {
                Load();
                return _routeFormPos;
            }
            set
            {
                Load();
                _routeFormPos = value;
            }
        }
    }
}