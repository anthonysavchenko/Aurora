using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills
{
    /// <summary>
    /// Интерфейс отчета для каждой квитанции
    /// </summary>
    public interface IReceiptLayoutReportObject : ISubReportObject
    {
        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        bool ReportVisible { set; }

        bool ShowBuildingConsumptionData { set; }
    }
}