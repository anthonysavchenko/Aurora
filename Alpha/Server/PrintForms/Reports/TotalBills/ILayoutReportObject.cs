using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.TotalBills
{
    /// <summary>
    /// Интерфейс отчета, содержащего другие отчеты
    /// </summary>
    public interface ILayoutReportObject
    {
        /// <summary>
        /// ID квитанции
        /// </summary>
        int TotalBillId { set; }

        /// <summary>
        /// Источник данных
        /// </summary>
        DataSet ReportDataSource { set; }
    }
}