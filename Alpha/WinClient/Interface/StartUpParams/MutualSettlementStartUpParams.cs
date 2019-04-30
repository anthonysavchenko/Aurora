using System;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams
{
    public class MutualSettlementStartUpParams : AnyStartUpParams
    {
        public DateTime Since { get; private set; }
        public DateTime Till { get; private set; }
        public string CustomerId { get; private set; } 

        public MutualSettlementStartUpParams(DateTime since, DateTime till, string customerId)
        {
            Since = since;
            Till = till;
            CustomerId = customerId;
        }
    }
}
