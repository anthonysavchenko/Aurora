using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.PaymentsAndCharges
{
    public interface IPaymentsAndChargesView : IBaseSimpleListView
    {
        DateTime Since { get; set; }
        DateTime Till { get; set; }
    }
}
