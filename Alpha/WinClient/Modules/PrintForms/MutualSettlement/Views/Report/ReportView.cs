using DevExpress.XtraPrinting.Control;
using DevExpress.XtraReports.UI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Reports.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report
{
    /// <summary>
    /// Вид с отчетом
    /// </summary>
    [SmartPart]
    public partial class ReportView : /*System.Windows.Forms.UserControl//*/BaseReportForReportObjectView<LayoutReportObject>, IReportView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ReportView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Control для отображения отчета
        /// </summary>
        protected override PrintControl ReportPrintControl
        {
            get
            {
                return printControl1;
            }
        }

        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new ReportViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ReportViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IReportView

        /// <summary>
        /// Источник данных
        /// </summary>
        public MutualSettlementDataSet DataSource
        {
            set
            {
                Report.ReportDataSource = value;
            }
        }

        /// <summary>
        /// Печатает отчет
        /// </summary>
        public void PrintReport()
        {
            ReportPrintTool _printTool = new ReportPrintTool(Report);
            _printTool.Print();
        }
        #endregion
    }
}