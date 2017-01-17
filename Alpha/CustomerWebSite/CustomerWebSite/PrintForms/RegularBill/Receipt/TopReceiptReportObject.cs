using System.Data;
using DevExpress.XtraReports.UI;

namespace CustomerWebSite.PrintForms.RegularBill.Receipt
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