using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    public class RouteForm : DomainObject
    {
        private DecFormsUploadPos _decFormsUploadPos;

        public DecFormsUploadPos DecFormsUploadPos
        {
            get
            {
                Load();
                return _decFormsUploadPos;
            }
            set
            {
                Load();
                _decFormsUploadPos = value;
            }
        }

        private string _street;

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

        private string _building;

        public string Building
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
    }
}
