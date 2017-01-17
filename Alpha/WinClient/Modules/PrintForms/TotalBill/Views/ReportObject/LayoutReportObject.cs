using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.ReportObject
{
    public partial class LayoutReportObject : BaseReportObject
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