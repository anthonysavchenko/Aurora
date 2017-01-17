using System.Data;
using CustomerWebSite.PrintForms.RegularBill.CountReport;
using DevExpress.XtraReports.UI;

namespace CustomerWebSite.PrintForms.RegularBill.Receipt
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