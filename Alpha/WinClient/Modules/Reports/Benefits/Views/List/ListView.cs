using System;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Benefits.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportComponents(gridControlOfListView, gridViewOfListView);
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

        /// <summary>
        /// Период отчета
        /// </summary>
        public DateTime Period
        {
            get
            {
                DateTime _temp = periodDateEdit.DateTime;
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                periodDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Показывать только федеральные льготы (по норме площади)
        /// </summary>
        public bool ShowOnlyFederalBenefits
        {
            get
            {
                return onlyFederalBenefitsCheckBox.Checked;
            }
        }

        #endregion
    }
}