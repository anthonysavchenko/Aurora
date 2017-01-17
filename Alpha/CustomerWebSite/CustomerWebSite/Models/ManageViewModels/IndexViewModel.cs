using System.Collections.Generic;

namespace CustomerWebSite.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public class AccountSetting
        {
            public string Account { get; set; }
            public bool SendBill { get; set; }
        }

        public bool HasPassword { get; set; }
        public bool BrowserRemembered { get; set; }
        public List<AccountSetting> Accounts { get; set; }
    }
}