using System.Data;
using Taumis.Alpha.Server.PrintForms.DataSets;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.Report
{
    /// <summary>
    /// Интерфейс вида с отчетом
    /// </summary>
    public interface IReportView : IBaseReportForReportObjectView
    {
        /// <summary>
        /// Источник данных
        /// </summary>
        DebtBillDataSet DataSource { set; }

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
    }
}