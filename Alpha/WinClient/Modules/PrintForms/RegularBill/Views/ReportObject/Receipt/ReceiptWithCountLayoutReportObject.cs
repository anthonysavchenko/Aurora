using DevExpress.XtraReports.UI;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.CountReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.Receipt
{
    public partial class ReceiptWithCountLayoutReportObject : XtraReport, ISubReportObject
    {
        public ReceiptWithCountLayoutReportObject()
        {
            InitializeComponent();
        }

        public int CustomerId
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).CustomerId = value;
                ((BottomReceiptReportObject)ButtomReceiptSubreport.ReportSource).CustomerId = value;
                ((CountReportObject)countSubreport.ReportSource).CustomerId = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).ReportDataSource = value;
                ((BottomReceiptReportObject)ButtomReceiptSubreport.ReportSource).ReportDataSource = value;
                ((CountReportObject)countSubreport.ReportSource).ReportDataSource = value;
            }
        }
    }
}