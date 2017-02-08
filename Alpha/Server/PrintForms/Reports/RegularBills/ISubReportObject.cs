using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills
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