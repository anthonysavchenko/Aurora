using System;
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.ReportObject.Receipt;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.DebtBill.Views.ReportObject
{
    public partial class LayoutReportObject : BaseReportObject, ILayoutReportObject
    {
        /// <summary>
        /// Количество распечатаных квитанций по абоненту
        /// </summary>
        private int _printedCount;

        public LayoutReportObject()
        {
            InitializeComponent();
        }

        public DataSet ReportDataSource
        {
            set
            {
                DataSource = value;
                ((IReceiptReportObject)BillSubreport.ReportSource).ReportDataSource = value;
            }
        }

        /// <summary>
        /// Переносить каждую квитанцию на отдельную страницу
        /// </summary>
        public bool PageBreakAfterBill
        {
            set
            {
                Detail.PageBreak = value ? PageBreak.AfterBand : PageBreak.None;
            }
        }

        /// <summary>
        /// Отображать отчет
        /// </summary>
        public bool ReportVisible
        {
            set
            {
                ((IBillLayoutReportObject)BillSubreport.ReportSource).ReportVisible = value;
                cutLine.ForeColor = value ? Color.Black : Color.White;
                cutLine.Visible = true;
            }
        }

        /// <summary>
        /// Отображать линию отреза между квитанциями
        /// </summary>
        public bool ShowLineBetweenBills
        {
            set;
            get;
        }

        private void Bill1Subreport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((IReceiptReportObject)BillSubreport.ReportSource).RecId = Convert.ToInt32(GetCurrentColumnValue("BillId"));
        }

        private void cutLine_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ShowLineBetweenBills)
            {
                cutLine.ForeColor = Color.Black;
                cutLine.Visible = ++_printedCount % 2 != 0;
            }
            else
            {
                cutLine.ForeColor = Color.White;
                cutLine.Visible = true;
            }
        }
    }
}