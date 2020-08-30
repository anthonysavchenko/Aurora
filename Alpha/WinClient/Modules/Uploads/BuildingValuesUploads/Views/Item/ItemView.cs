using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseMultipleListView, IItemView
    {
        public ItemView()
        {
            InitializeComponent();
            Initialize(_gridControlOfListView, _gridViewOfListView, "ID");
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

        public string Month
        {
            set
            {
                MonthTextBox.Text = value;
            }
        }

        public string FilePath
        {
            set
            {
                FilePathTextBox.Text = value;
            }
        }

        public string Note
        {
            set
            {
                NoteTextBox.Text = value;
            }
        }

        public string Description
        {
            set
            {
                DescriptionTextBox.Text = value;
            }
        }

        public void ShowDomainOnView()
        {
            Presenter.ShowDomainOnView();
        }

        private void _gridViewOfListView_FocusedRowChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var description = GridListView.GetFocusedRowCellDisplayText("Description");
            PosDescriptionTextBox.Text = description;
        }
    }
}
