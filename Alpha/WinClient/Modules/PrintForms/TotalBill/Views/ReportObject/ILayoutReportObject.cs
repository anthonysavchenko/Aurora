using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.ReportObject
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