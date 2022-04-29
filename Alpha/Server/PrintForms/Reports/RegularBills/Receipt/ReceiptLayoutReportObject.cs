using DevExpress.XtraReports.UI;
using System.Data;
using Taumis.Alpha.Server.PrintForms.Reports.RegularBills.CountReport;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt
{
    public partial class ReceiptLayoutReportObject : XtraReport, IReceiptLayoutReportObject
    {
        public ReceiptLayoutReportObject()
        {
            InitializeComponent();
        }

        public int CustomerId
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).CustomerId = value;
                ((BuildingConsumptionReportObject)BuildingConsumptionSubreport.ReportSource).CustId.Value = value;
                ((TableCountReportObject)CountersSubreport.ReportSource).CustId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                ((TopReceiptReportObject)TopReceiptSubreport.ReportSource).ReportDataSource = value;
                ((BuildingConsumptionReportObject)BuildingConsumptionSubreport.ReportSource).DataSource = value;
                ((TableCountReportObject)CountersSubreport.ReportSource).DataSource = value;
            }
        }

        public bool ReportVisible
        {
            set
            {
                Detail.Visible = value;
            }
        }

        public bool ShowBuildingConsumptionData { set => BuildingConsumptionSubreport.Visible = value; }

        public bool ShowCountersData { set => CountersSubreport.Visible = value; }
    }
}