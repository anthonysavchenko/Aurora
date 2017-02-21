namespace Taumis.Alpha.Server.PrintForms.Reports.DebtBills.Receipt
{
    public interface IBillLayoutReportObject : IReceiptReportObject
    {
        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        bool ReportVisible { set; }
    }
}