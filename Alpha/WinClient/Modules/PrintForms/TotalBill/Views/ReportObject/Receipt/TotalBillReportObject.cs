using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.ReportObject.Receipt
{
    public partial class TotalBillReportObject : XtraReport, ILayoutReportObject
    {
        public TotalBillReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of ILayoutReportObject

        public int TotalBillId
        {
            set
            {
                BillId.Value = value;
                ((ServiceTableReportObject)serviceTableSubreport.ReportSource).BillId.Value = value;
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

        #endregion
    }
}