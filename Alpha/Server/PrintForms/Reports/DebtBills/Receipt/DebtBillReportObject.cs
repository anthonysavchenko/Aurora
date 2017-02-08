using DevExpress.XtraReports.UI;
using System.Data;

namespace Taumis.Alpha.Server.PrintForms.Reports.DebtBills.Receipt
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