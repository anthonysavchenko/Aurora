using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseMultipleListView
    {
        public ItemView()
        {
            InitializeComponent();
            Initialize(GridControlOfListView, GridViewOfListView, "ID");
        }

        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }

            get
            {
                return (ItemViewPresenter)base.Presenter;
            }
        }

        public override DataTable ElemList
        {
            set
            {
                if (value != null)
                {
                    value.PrimaryKey = new DataColumn[] { value.Columns["ID"] };
                }
                GridControlOfListView.DataSource = value;
                GridViewOfListView.BestFitColumns();
                SubGridViewOfListView.BestFitColumns();
                GridControlOfListView.LevelTree.Nodes.Add("ID_Email", SubGridViewOfListView);
            }
        }
    }
}
