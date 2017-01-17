using System.Data;
using DevExpress.XtraReports.UI;

namespace CustomerWebSite.PrintForms.RegularBill.Receipt
{
    public partial class ReceiptLayoutReportObject : XtraReport, IReceiptLayoutReportObject
    {
        public ReceiptLayoutReportObject()
        {
            InitializeComponent();
        }

        public int CustomerId
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).CustomerId = value;
                ((BottomReceiptReportObject)ButtomReceiptSubreport.ReportSource).CustomerId = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).ReportDataSource = value;
                ((BottomReceiptReportObject)ButtomReceiptSubreport.ReportSource).ReportDataSource = value;
            }
        }

        public bool ReportVisible
        {
            set
            {
                Detail.Visible = value;
            }
        }
    }
}