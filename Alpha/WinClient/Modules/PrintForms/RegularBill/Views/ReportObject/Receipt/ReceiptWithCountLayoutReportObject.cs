using DevExpress.XtraReports.UI;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.CountReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.Receipt
{
    public partial class ReceiptWithCountLayoutReportObject : XtraReport, ISubReportObject, IReceiptLayoutReportObject
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
                ((TableCountReportObject)countSubreport.ReportSource).CustomerId = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).ReportDataSource = value;
                ((BottomReceiptReportObject)ButtomReceiptSubreport.ReportSource).ReportDataSource = value;
                ((TableCountReportObject)countSubreport.ReportSource).ReportDataSource = value;
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