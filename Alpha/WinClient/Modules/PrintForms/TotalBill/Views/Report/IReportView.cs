using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.Report
{
    /// <summary>
    /// »нтерфейс вида с отчетом
    /// </summary>
    public interface IReportView : IBaseReportForReportObjectView
    {
        /// <summary>
        /// »сточник данных
        /// </summary>
        TotalBillDataSet DataSource { set; }

        /// <summary>
        /// ѕечатает отчет
        /// </summary>
        void PrintReport();
    }
}