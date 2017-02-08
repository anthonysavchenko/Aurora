using System;
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using Taumis.Alpha.Server.PrintForms.Constants;
using Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt;

//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills
{
    public partial class LayoutReportObject : XtraReport, ILayoutReportObject
    {
        /// <summary>
        /// Количество распечатаных квитанций по абоненту
        /// </summary>
        private int _printedCount;

        public LayoutReportObject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Тип квитанции
        /// </summary>
        public ReceiptTypes ReceiptType
        {
           set
           {
                switch (value)
                {
                    case ReceiptTypes.Standart:
                        ReceiptSubreport.ReportSource = new ReceiptLayoutReportObject();
                        cutLine.BeforePrint += cutLine_BeforePrint;
                        break;

                    case ReceiptTypes.WithCountsData:
                        ReceiptSubreport.ReportSource = new ReceiptWithCountLayoutReportObject();
                        cutLine.BeforePrint -= cutLine_BeforePrint;
                        break;
                }
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
                ((IReceiptLayoutReportObject)ReceiptSubreport.ReportSource).ReportDataSource = value;
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
                ((IReceiptLayoutReportObject)ReceiptSubreport.ReportSource).ReportVisible = value;
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

        private void Receipt1Subreport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((IReceiptLayoutReportObject)ReceiptSubreport.ReportSource).CustomerId = Convert.ToInt32(GetCurrentColumnValue("CustomerId"));
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