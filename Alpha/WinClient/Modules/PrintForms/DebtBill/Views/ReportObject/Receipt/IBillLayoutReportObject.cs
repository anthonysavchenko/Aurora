namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.ReportObject.Receipt
{
    public interface IBillLayoutReportObject : IReceiptReportObject
    {
        /// <summary>
        /// Видимость отчета на странице
        /// </summary>
        bool ReportVisible { set; }
    }
}