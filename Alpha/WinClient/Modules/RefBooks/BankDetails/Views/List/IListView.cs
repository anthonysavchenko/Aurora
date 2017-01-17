using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BankDetails.Views.List
{
    public interface IListView : IBaseSimpleListView
    {
        string BankName { get; }
        string BIK { get; }
        string KPP { get; }
        string CorrAccount { get; }
        string Account { get; }
        string INN { get; }
    }
}