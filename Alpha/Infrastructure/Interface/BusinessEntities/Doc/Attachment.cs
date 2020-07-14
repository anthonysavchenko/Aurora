using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class Attachment : DomainObject
    {
        private Email _email;

        public Email Email
        {
            get
            {
                Load();
                return _email;
            }
            set
            {
                Load();
                _email = value;
            }
        }

        private string _fileName;

        public string FileName
        {
            get
            {
                Load();
                return _fileName;
            }
            set
            {
                Load();
                _fileName = value;
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
