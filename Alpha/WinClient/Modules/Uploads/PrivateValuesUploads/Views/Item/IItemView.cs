using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Views.Item
{
    public interface IItemView : IBaseMultipleListView
    {
        string Month { set;  }

        string Directory { set; }

        string Note { set; }

        string Description { set; }

        void ShowDomainOnView();
    }
}
