using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.DebtBills.Receipt
{
    public partial class BillLayoutReportObject : XtraReport, IBillLayoutReportObject
    {
        public BillLayoutReportObject()
        {
            InitializeComponent();
        }

        public int RecId
        {
            set
            {
                ((DebtBillReportObject)topBillSubreport.ReportSource).BillId.Value = value;
                ((DebtBillReportObject)bottomBillSubreport.ReportSource).BillId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((DebtBillReportObject)topBillSubreport.ReportSource).ReportDataSource = value;
                ((DebtBillReportObject)bottomBillSubreport.ReportSource).ReportDataSource = value;
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