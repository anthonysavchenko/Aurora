using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report.Models
{
    public class ServiceBalances
    {
        public IDictionary<ServiceBalanceKey, Balance> Balances { get; set; }

        public ServiceBalances()
        {
            Balances = new Dictionary<ServiceBalanceKey, Balance>();
        }

        public void Add(ServiceBalanceKey key, Balance value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].Add(value);
        }
    }
}
