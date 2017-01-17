using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.ReportObject.Receipt
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