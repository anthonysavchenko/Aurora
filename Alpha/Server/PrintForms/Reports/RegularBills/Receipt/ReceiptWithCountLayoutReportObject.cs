using DevExpress.XtraReports.UI;
using System.Data;
using Taumis.Alpha.Server.PrintForms.Reports.RegularBills.CountReport;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt
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
                ((CountReportObject)countSubreport.ReportSource).CustomerId = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).ReportDataSource = value;
                ((CountReportObject)countSubreport.ReportSource).ReportDataSource = value;
            }
        }
    }
}