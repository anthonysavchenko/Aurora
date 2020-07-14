using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class Email : DomainObject
    {
        private DecFormsDownload _decFormsDownload;

        public DecFormsDownload DecFormsDownload
        {
            get
            {
                Load();
                return _decFormsDownload;
            }
            set
            {
                Load();
                _decFormsDownload = value;
            }
        }

        private string _subject;

        public string Subject
        {
            get
            {
                Load();
                return _subject;
            }
            set
            {
                Load();
                _subject = value;
            }
        }

        private string _fromAddress;

        public string FromAddress
        {
            get
            {
                Load();
                return _fromAddress;
            }
            set
            {
                Load();
                _fromAddress = value;
            }
        }

        private DateTime? _received;

        public DateTime? Received
        {
            get
            {
                Load();
                return _received;
            }
            set
            {
                Load();
                _received = value;
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
