using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.Receipt
{
    public partial class TopReceiptReportObject : XtraReport, ISubReportObject
    {
        public TopReceiptReportObject()
        {
            InitializeComponent();
        }

        public int CustomerId
        {
            set
            {
                CustId.Value = value;
                ((ServiceTableReportObject)serviceTableSubreport.ReportSource).CustId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((ServiceTableReportObject)serviceTableSubreport.ReportSource).DataSource = value;
            }
        }
    }
}