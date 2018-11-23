using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Views.List
{
    public interface IListView : IBaseSimpleListView
    {
        string DistrictName { get; }
    }
}