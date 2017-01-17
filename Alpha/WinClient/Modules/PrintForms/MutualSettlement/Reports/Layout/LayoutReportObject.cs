using System;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Reports.ServiceTypes;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Reports.Layout
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
                ((IServiceTypesReportObject)ServiceTypesXRSubreport.ReportSource).ReportDataSource = value;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((IServiceTypesReportObject)ServiceTypesXRSubreport.ReportSource).ReportNumber = Convert.ToInt32(GetCurrentColumnValue("ReportNumber"));
        }
    }
}