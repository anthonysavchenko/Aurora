using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class DecFormsUpload : DomainObject
    {
        private DateTime _created;

        public DateTime Created
        {
            get
            {
                Load();
                return _created;
            }
            set
            {
                Load();
                _created = value;
            }
        }

        private DateTime _month;

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

        private User _author;

        public User Author
        {
            get
            {
                Load();
                return _author;
            }
            set
            {
                Load();
                _author = value;
            }
        }
    }
}
