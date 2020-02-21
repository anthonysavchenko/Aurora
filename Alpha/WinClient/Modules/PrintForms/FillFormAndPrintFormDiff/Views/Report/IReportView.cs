using System.Data;
using System.IO;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.DataSets;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.FillFormAndPrintFormDiff.Views.Report
{
    /// <summary>
    /// Интерфейс вида с отчетом
    /// </summary>
    public interface IReportView : IBaseReportForReportObjectView
    {
        /// <summary>
        /// Источник данных
        /// </summary>
        PrintFormAndFillFormDiffsDataSet DataSource { set; }

        /// <summary>
        /// Печатает отчет
        /// </summary>
        void PrintReport();

        /// <summary>
        /// Принтеры
        /// </summary>
        DataTable Printers { set; }

        /// <summary>
        /// Выбранный принтер
        /// </summary>
        string SelectedPrinter { set; get; }

        MemoryStream GeneratePdf(PrintFormAndFillFormDiffsDataSet dataSet);
        MemoryStream GeneratePdf();
    }
}