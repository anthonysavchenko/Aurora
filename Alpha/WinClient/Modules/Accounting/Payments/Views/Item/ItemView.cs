using DevExpress.Data;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseMultipleListView
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

        private void _gridViewOfListView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // Initialization 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                e.TotalValueReady = true;
                e.TotalValue = Presenter.CorrectionsCount();
            }
        }
    }
}