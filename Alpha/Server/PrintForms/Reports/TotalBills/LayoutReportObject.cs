using System;
using System.Data;
using DevExpress.XtraReports.UI;

namespace Taumis.Alpha.Server.PrintForms.Reports.TotalBills
{
    public partial class LayoutReportObject : XtraReport
    {
        public LayoutReportObject()
        {
            InitializeComponent();
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((ILayoutReportObject)ReceiptSubreport.ReportSource).ReportDataSource = value;
            }
        }

        private void Receipt1Subreport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((ILayoutReportObject)ReceiptSubreport.ReportSource).TotalBillId = Convert.ToInt32(GetCurrentColumnValue("TotalBillId"));
        }
    }
}