using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.ReportObject.Receipt
{
    public partial class ReceiptLayoutReportObject : XtraReport, ILayoutReportObject
    {
        public ReceiptLayoutReportObject()
        {
            InitializeComponent();
        }

        public int TotalBillId
        {
            set
            {
                ((TotalBillReportObject)topReceiptSubreport.ReportSource).TotalBillId = value;
                ((TotalBillReportObject)bottomReceiptSubreport.ReportSource).TotalBillId = value;
                ((StatementReportObject)statementSubreport.ReportSource).TotalBillId = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TotalBillReportObject)topReceiptSubreport.ReportSource).ReportDataSource = value;
                ((TotalBillReportObject)bottomReceiptSubreport.ReportSource).ReportDataSource = value;
                ((StatementReportObject)statementSubreport.ReportSource).ReportDataSource = value;
            }
        }
    }
}