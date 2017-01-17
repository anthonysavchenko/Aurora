using System.Data;
using DevExpress.XtraReports.UI;

namespace CustomerWebSite.PrintForms.RegularBill.Receipt
{
    public partial class BottomReceiptReportObject : XtraReport, ISubReportObject
    {
        public BottomReceiptReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of ISubReportObject

        public int CustomerId
        {
            set
            {
                CustId.Value = value;
                ((ServiceTableReportObject)serviceTableSubreport.ReportSource).CustId.Value = value;
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