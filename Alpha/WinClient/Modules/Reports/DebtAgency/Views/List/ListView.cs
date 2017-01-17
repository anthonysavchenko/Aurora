using System;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAgency.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportConponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        #region Implementation of IListView

        public DateTime Period
        {
            get
            {
                return periodDateEdit.DateTime;
            }
            set
            {
                periodDateEdit.DateTime = value;
            }
        }

        #endregion
    }
}