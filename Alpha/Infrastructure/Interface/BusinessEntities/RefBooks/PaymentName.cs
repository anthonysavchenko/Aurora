using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.RefBook
{

    /// <summary>
    /// Наименование платежа. 
    /// </summary>
    public class PaymentName : DomainObject
    {
        private string aka;

        public string Aka
        {
            get { Load(); return aka; }
            set { Load(); aka = value; }
        }

        private string shortName;

        public string ShortName
        {
            get { Load(); return shortName; }
            set { Load(); shortName = value; }
        }
    }
}