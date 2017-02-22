using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.MutualSettlementBills.ServiceTypes
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