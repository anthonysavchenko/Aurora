using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Item
{
    public interface IItemView : IBaseMultipleListView
    {
        string Month { set;  }

        string FilePath { set; }

        string Note { set; }

        string Description { set; }

        void ShowDomainOnView();
    }
}
