using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt
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