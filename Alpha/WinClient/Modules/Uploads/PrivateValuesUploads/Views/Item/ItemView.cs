using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.PrivateValuesUploads.Views.Item
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

        public string Directory
        {
            set
            {
                DirectoryTextBox.Text = value;
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
    }
}
