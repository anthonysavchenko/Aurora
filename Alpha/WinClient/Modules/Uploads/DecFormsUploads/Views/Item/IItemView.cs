using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Item
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
