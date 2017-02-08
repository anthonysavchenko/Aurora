using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.CountReport
{
    public partial class CountReportObject : XtraReport, ISubReportObject
    {
        public CountReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of ISubReportObject

        public int CustomerId
        {
            set
            {
                CustId.Value = value;
                ((TableCountReportObject)countTableSubreport.ReportSource).CustId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((TableCountReportObject)countTableSubreport.ReportSource).DataSource = value;
            }
        }

        #endregion
    }
}