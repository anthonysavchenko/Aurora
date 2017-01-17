using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject.Receipt
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
                ((PublicPlaceTableReportObject)publicPlaceTableSubreport.ReportSource).CustId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((PublicPlaceTableReportObject)publicPlaceTableSubreport.ReportSource).DataSource = value;
            }
        }

        #endregion
    }
}