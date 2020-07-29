using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class DecFormsUploadPos : DomainObject
    {
        private DecFormsUpload _decFormsUpload;

        public DecFormsUpload DecFormsUpload
        {
            get
            {
                Load();
                return _decFormsUpload;
            }
            set
            {
                Load();
                _decFormsUpload = value;
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

        private DecFormsType _formType;

        public DecFormsType FormType
        {
            get
            {
                Load();
                return _formType;
            }
            set
            {
                _formType = value;
            }
        }

        private RouteForm _routeForm;

        public RouteForm RouteForm
        {
            get
            {
                Load();
                return _routeForm;
            }
            set
            {
                Load();
                _routeForm = value;
            }
        }

        private FillForm _fillForm;

        public FillForm FillForm
        {
            get
            {
                Load();
                return _fillForm;
            }
            set
            {
                Load();
                _fillForm = value;
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
