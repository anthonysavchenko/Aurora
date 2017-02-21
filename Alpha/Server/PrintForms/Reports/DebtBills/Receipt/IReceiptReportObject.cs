using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.DebtBills.Receipt
{
    public interface IReceiptReportObject
    {
        /// <summary>
        /// ID квитанции
        /// </summary>
        int RecId { set; }

        /// <summary>
        /// Источник данных
        /// </summary>
        DataSet ReportDataSource { set; }
    }
}