using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.TotalBills.Receipt
{
    public partial class StatementReportObject : XtraReport, ILayoutReportObject
    {
        public StatementReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of ILayoutReportObject

        public int TotalBillId
        {
            set
            {
                BillId.Value = value;
                ((StatementServiceTableReportObject)statementServiceTableSubreport.ReportSource).BillId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((StatementServiceTableReportObject)statementServiceTableSubreport.ReportSource).DataSource = value;
            }
        }

        #endregion
    }
}