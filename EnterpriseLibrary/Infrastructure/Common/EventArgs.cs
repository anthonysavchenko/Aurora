using System;

namespace Taumis.EnterpriseLibrary.Win
{
    public class EventArgs<T> : EventArgs
    {
        private T _data;

        public EventArgs(T data)
        {
            _data = data;
        }

        public T Data
        {
            get { return _data; }
        }
    }
}
