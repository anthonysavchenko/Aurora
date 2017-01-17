using System.Data;

namespace CustomerWebSite.PrintForms.MutualSettlement.ServiceTypes
{
    /// <summary>
    /// Интерфейс отчета деталей типам услуг
    /// </summary>
    public interface IServiceTypesReportObject
    {
        /// <summary>
        /// Порядковый номер отчета
        /// </summary>
        int ReportNumber { set; }

        /// <summary>
        /// Источник данных
        /// </summary>
        DataSet ReportDataSource { set; }
    }
}