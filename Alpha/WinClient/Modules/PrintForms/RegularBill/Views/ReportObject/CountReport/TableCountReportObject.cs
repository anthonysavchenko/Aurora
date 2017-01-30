using System.Data;
using DevExpress.XtraReports.UI;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.CountReport
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