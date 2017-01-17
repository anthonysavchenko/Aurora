using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebSite.Models
{
    public class LoginMenuViewModel
    {
        public class AccountInfo
        {
            public string Account { get; set; }
            public string CustomerName { get; set; }
            public string Street { get; set; }
            public string Building { get; set; }
            public string Apartment { get; set; }

            [DisplayFormat(DataFormatString = "{0:N} кв. м.")]
            public decimal Square { get; set; }
        }

        public bool IsAuthenticated { get; set; }
        public string Login { get; set; }
        public List<AccountInfo> Accounts { get; set; }
        public string Account { get; set; }
        public bool IsPaymentsAndChargesPageSelected { get; set; }
        public bool IsSettigsPageSelected { get; set; }
    }
}