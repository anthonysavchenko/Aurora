﻿using System;
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using Taumis.Alpha.Server.PrintForms.Reports.RegularBills;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportObject = DevExpress.XtraReports.UI.XtraReport;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.RegularBill.Views.ReportObject
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
            int _customerId = Convert.ToInt32(GetCurrentColumnValue("CustomerId"));
            var _subreport = ((IReceiptLayoutReportObject)ReceiptSubreport.ReportSource);
            _subreport.CustomerId = _customerId;

            DataRow[] _rows = ((DataSet)DataSource).Tables["BuildingConsumptionData"].Select($"CustomerId = {_customerId}");
            _subreport.ShowBuildingConsumptionData = _rows.Length > 0;

            _rows = ((DataSet)DataSource).Tables["CounterData"].Select($"CustomerId = {_customerId}");
            _subreport.ShowCountersData = _rows.Length > 0;
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