using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.ReportObject.Receipt
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