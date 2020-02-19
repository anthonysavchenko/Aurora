using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.Views.ReportObject
{
    public partial class LayoutReportObject : BaseReportObject
    {
        public LayoutReportObject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Источник данных
        /// </summary>
        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
            }
        }
    }
}