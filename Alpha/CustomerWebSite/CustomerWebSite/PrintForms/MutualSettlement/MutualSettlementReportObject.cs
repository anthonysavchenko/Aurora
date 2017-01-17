using System;
using System.Data;
using CustomerWebSite.PrintForms.MutualSettlement.ServiceTypes;
using DevExpress.XtraReports.UI;

namespace CustomerWebSite.PrintForms.MutualSettlement
{
    public partial class MutualSettlementReportObject : XtraReport
    {
        public MutualSettlementReportObject()
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