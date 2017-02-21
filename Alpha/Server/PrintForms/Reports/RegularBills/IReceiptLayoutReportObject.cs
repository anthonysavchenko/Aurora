using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills
{
    /// <summary>
    /// Интерфейс отчета для каждой квитанции
    /// </summary>
    public interface IReceiptLayoutReportObject
    {
        /// <summary>
        /// ID абонента
        /// </summary>
        int CustomerId { set; }

        /// <summary>
        /// Источник данных
        /// </summary>
        DataSet ReportDataSource { set; }

        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        bool ReportVisible { set; }
    }
}