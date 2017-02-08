using System;
using System.Data;
using DevExpress.XtraReports.UI;
using Taumis.Alpha.Server.PrintForms.Reports.MutualSettlementBills.ServiceTypes;

namespace Taumis.Alpha.Server.PrintForms.Reports.MutualSettlementBills.Layout
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
                ((IServiceTypesReportObject)ServiceTypesXRSubreport.ReportSource).ReportDataSource = value;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((IServiceTypesReportObject)ServiceTypesXRSubreport.ReportSource).ReportNumber = Convert.ToInt32(GetCurrentColumnValue("ReportNumber"));
        }
    }
}