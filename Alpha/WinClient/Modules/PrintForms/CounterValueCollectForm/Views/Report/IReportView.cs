using System.Data;
using System.IO;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.DataSets;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Report
{
    /// <summary>
    /// Интерфейс вида с отчетом
    /// </summary>
    public interface IReportView : IBaseReportForReportObjectView
    {
        /// <summary>
        /// Источник данных
        /// </summary>
        CollectFormDataSet DataSource { set; }

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

        MemoryStream GeneratePdf(CollectFormDataSet dataSet);
        MemoryStream GeneratePdf();
    }
}