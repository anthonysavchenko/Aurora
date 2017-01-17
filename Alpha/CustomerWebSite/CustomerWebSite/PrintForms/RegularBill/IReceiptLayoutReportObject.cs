using System.Data;

namespace CustomerWebSite.PrintForms.RegularBill
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