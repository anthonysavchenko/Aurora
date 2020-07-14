using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class DecFormsDownload : DomainObject
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

        private string _directory;

        public string Directory
        {
            get
            {
                Load();
                return _directory;
            }
            set
            {
                Load();
                _directory = value;
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
