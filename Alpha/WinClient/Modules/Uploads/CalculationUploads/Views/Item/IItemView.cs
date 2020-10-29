using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.Item
{
    public interface IItemView : IBaseMultipleListView
    {
        string Month { set;  }

        string DirectoryPath { set; }

        string Note { set; }

        string Description { set; }

        void ShowDomainOnView();
    }
}
