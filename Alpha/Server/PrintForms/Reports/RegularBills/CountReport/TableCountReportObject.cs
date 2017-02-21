using System.Data;
using DevExpress.XtraReports.UI;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.CountReport
{
    public partial class TableCountReportObject : XtraReport, ISubReportObject
    {
        public TableCountReportObject()
        {
            InitializeComponent();
        }

        public int CustomerId
        {
            set
            {
                CustId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
            }
        }
    }
}