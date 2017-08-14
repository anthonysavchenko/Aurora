using System;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams
{
    public class RechargeStartUpParams : AnyStartUpParams
    {
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
        public int CustomerID { get; set; }
    }
}