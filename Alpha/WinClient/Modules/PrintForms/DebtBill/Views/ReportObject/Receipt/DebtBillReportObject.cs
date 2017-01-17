using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.ReportObject.Receipt
{
    public partial class DebtBillReportObject : XtraReport, IReceiptReportObject
    {
        public DebtBillReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of ILayoutReportObject
        
        /// <summary>
        /// ID квитанции
        /// </summary>
        public int RecId
        {
            set
            {
                BillId.Value = value;
            }
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
            }
        }

        #endregion
    }
}