using System.Data;
using System.IO;
using Taumis.Alpha.Server.PrintForms.Constants;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.Report
{
    /// <summary>
    /// Интерфейс вида с отчетом
    /// </summary>
    public interface IReportView : IBaseReportForReportObjectView
    {
        /// <summary>
        /// Источник данных
        /// </summary>
        RegularBillDataSet DataSource { set; }

        /// <summary>
        /// Печатает отчет
        /// </summary>
        void PrintReport();

        /// <summary>
        /// Тип квитанции
        /// </summary>
        ReceiptTypes ReceiptType { set; }

        /// <summary>
        /// Принтеры
        /// </summary>
        DataTable Printers { set; }

        /// <summary>
        /// Выбранный принтер
        /// </summary>
        string SelectedPrinter { set; get; }

        /// <summary>
        /// Убрать квитанции с нулевыми начислениями
        /// </summary>
        bool RemoveEmptyBills { set; get; }

        /// <summary>
        /// Доступность удаления квитанций с нулевыми начислениями
        /// </summary>
        bool RemoveEmptyBillsEnabled { set; }

        /// <summary>
        /// Печатать одной квитанции на листе
        /// </summary>
        bool OneBillOnSheet { set; get; }

        /// <summary>
        /// Доступность печатати одной квитанции на листе
        /// </summary>
        bool OneBillOnSheetEnabled { set; }

        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        bool ReportVisible { set; }

        /// <summary>
        /// Переносить каждую квитанцию на отдельную страницу
        /// </summary>
        bool PageBreakAfterBill { set; }

        /// <summary>
        /// Отображать линию отреза между квитанциями
        /// </summary>
        bool ShowLineBetweenBills { set; }

        MemoryStream GeneratePdf(RegularBillDataSet dataSet);
        MemoryStream GeneratePdf();
    }
}