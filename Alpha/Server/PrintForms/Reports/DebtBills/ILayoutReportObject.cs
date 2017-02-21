namespace Taumis.Alpha.Server.PrintForms.Reports.DebtBills
{
    /// <summary>
    /// Интерфейс отчета, содержащего другие отчеты
    /// </summary>
    public interface ILayoutReportObject
    {
        /// <summary>
        /// Переносить каждую квитанцию на отдельную страницу
        /// </summary>
        bool PageBreakAfterBill { set; }

        /// <summary>
        /// Отображать отчет
        /// </summary>
        bool ReportVisible { set; }

        /// <summary>
        /// Отображать линию отреза между квитанциями
        /// </summary>
        bool ShowLineBetweenBills { set; get; }
    }
}