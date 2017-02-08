using System;
using System.Data;
using DevExpress.XtraReports.UI;

namespace Taumis.Alpha.Server.PrintForms.Reports.MutualSettlementBills.ServiceTypes
{
    public partial class ServiceTypesReportObject : XtraReport, IServiceTypesReportObject
    {
        private string groupHeader;

        public ServiceTypesReportObject()
        {
            InitializeComponent();
        }

        #region Implementation of IPeriodsReportObject

        /// <summary>
        /// Порядковый номер отчета
        /// </summary>
        public int ReportNumber
        {
            set
            {
                ReportNumberParam.Value = value;
            }
        }

        /// <summary>
        /// Источник данных
        /// </summary>
        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
            }
        }

        #endregion

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = groupHeader == GetCurrentColumnValue("GroupHeader").ToString();
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            groupHeader = GetCurrentColumnValue("GroupHeader").ToString();
            ((XRLabel)sender).Text = groupHeader;
        }
    }
}