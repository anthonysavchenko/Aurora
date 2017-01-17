using System.Data;

namespace CustomerWebSite.PrintForms.RegularBill
{
    /// <summary>
    /// Интерфейс отчета, находящего внутри другого отчета
    /// </summary>
    public interface ISubReportObject
    {
        /// <summary>
        /// ID абонента
        /// </summary>
        int CustomerId { set; }

        /// <summary>
        /// Источник данных
        /// </summary>
        DataSet ReportDataSource { set; }
    }
}