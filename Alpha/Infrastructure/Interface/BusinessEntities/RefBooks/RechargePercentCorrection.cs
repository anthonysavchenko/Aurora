using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class RechargePercentCorrection : DomainObject
    {
        private DateTime _period;
        public DateTime Period
        {
            get
            {
                Load();
                return _period;
            }
            set
            {
                Load();
                _period = value;
            }
        }

        private int _days;
        public int Days
        {
            get
            {
                Load();
                return _days;
            }
            set
            {
                Load();
                _days = value;
            }
        }

        private int _percent;
        public int Percent
        {
            get
            {
                Load();
                return _percent;
            }
            set
            {
                Load();
                _percent = value;
            }
        }

        private CustomerPos _customerPos;
        public CustomerPos CustomerPos
        {
            get
            {
                Load();
                return _customerPos;
            }
            set
            {
                Load();
                _customerPos = value;
            }
        }
    }
}
