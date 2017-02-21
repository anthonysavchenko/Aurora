namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt
{
    partial class TopReceiptReportObject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopReceiptReportObject));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.billDateLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.repNameLbl = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.bankDetailsLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.monthChargeTableCell = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.OverpaymentLabel = new DevExpress.XtraReports.UI.XRTableCell();
            this.overpaymentValue = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.totalChargeTableCell = new DevExpress.XtraReports.UI.XRTableCell();
            this.serviceTableSubreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.peopleCountLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.areaLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.payerAddressLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.payerAccountLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.printDateLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.payerAkaLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.payBeforeDateLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.dataSet1 = new Taumis.Alpha.Server.PrintForms.DataSets.RegularBillDataSet();
            this.CustId = new DevExpress.XtraReports.Parameters.Parameter();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel2,
            this.xrPanel1,
            this.xrLabel4,
            this.bankDetailsLabel,
            this.xrTable3,
            this.serviceTableSubreport,
            this.xrLabel2,
            this.peopleCountLabel,
            this.xrLabel7,
            this.areaLabel,
            this.xrLabel5,
            this.payerAddressLabel,
            this.payerAccountLabel,
            this.xrLabel1,
            this.printDateLabel,
            this.xrLabel9,
            this.payerAkaLabel,
            this.xrLabel3,
            this.xrLabel11,
            this.payBeforeDateLabel});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // xrPanel2
            // 
            this.xrPanel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPanel2.CanGrow = false;
            resources.ApplyResources(this.xrPanel2, "xrPanel2");
            this.xrPanel2.Name = "xrPanel2";
            this.xrPanel2.StylePriority.UseBorders = false;
            this.xrPanel2.StylePriority.UseBorderWidth = false;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.billDateLabel,
            this.repNameLbl});
            resources.ApplyResources(this.xrPanel1, "xrPanel1");
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.StylePriority.UseBorders = false;
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            // 
            // billDateLabel
            // 
            this.billDateLabel.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.billDateLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.BillDateTime")});
            resources.ApplyResources(this.billDateLabel, "billDateLabel");
            this.billDateLabel.Multiline = true;
            this.billDateLabel.Name = "billDateLabel";
            this.billDateLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.billDateLabel.StylePriority.UseBorders = false;
            this.billDateLabel.StylePriority.UseFont = false;
            this.billDateLabel.StylePriority.UseTextAlignment = false;
            // 
            // repNameLbl
            // 
            this.repNameLbl.Borders = DevExpress.XtraPrinting.BorderSide.None;
            resources.ApplyResources(this.repNameLbl, "repNameLbl");
            this.repNameLbl.Name = "repNameLbl";
            this.repNameLbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.repNameLbl.StylePriority.UseBorders = false;
            this.repNameLbl.StylePriority.UseFont = false;
            this.repNameLbl.StylePriority.UseTextAlignment = false;
            this.repNameLbl.WordWrap = false;
            // 
            // xrLabel4
            // 
            this.xrLabel4.CanGrow = false;
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 15, 0, 0, 254F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseForeColor = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // bankDetailsLabel
            // 
            this.bankDetailsLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.RightHeaderString")});
            resources.ApplyResources(this.bankDetailsLabel, "bankDetailsLabel");
            this.bankDetailsLabel.Multiline = true;
            this.bankDetailsLabel.Name = "bankDetailsLabel";
            this.bankDetailsLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.bankDetailsLabel.StylePriority.UseFont = false;
            this.bankDetailsLabel.StylePriority.UseTextAlignment = false;
            // 
            // xrTable3
            // 
            resources.ApplyResources(this.xrTable3, "xrTable3");
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3,
            this.xrTableRow4,
            this.xrTableRow5});
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.monthChargeTableCell});
            resources.ApplyResources(this.xrTableRow3, "xrTableRow3");
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1.0079365079365079D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell13, "xrTableCell13");
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Weight = 1.8896880114120758D;
            // 
            // monthChargeTableCell
            // 
            this.monthChargeTableCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.monthChargeTableCell.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.MonthChargeValue")});
            resources.ApplyResources(this.monthChargeTableCell, "monthChargeTableCell");
            this.monthChargeTableCell.Name = "monthChargeTableCell";
            this.monthChargeTableCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.monthChargeTableCell.StylePriority.UseBorders = false;
            this.monthChargeTableCell.StylePriority.UseFont = false;
            this.monthChargeTableCell.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary1, "xrSummary1");
            this.monthChargeTableCell.Summary = xrSummary1;
            this.monthChargeTableCell.Weight = 0.47065701117926467D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.OverpaymentLabel,
            this.overpaymentValue});
            resources.ApplyResources(this.xrTableRow4, "xrTableRow4");
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1.0079365079365079D;
            // 
            // OverpaymentLabel
            // 
            this.OverpaymentLabel.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.OverpaymentLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.OverpaymentLabel")});
            resources.ApplyResources(this.OverpaymentLabel, "OverpaymentLabel");
            this.OverpaymentLabel.Name = "OverpaymentLabel";
            this.OverpaymentLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.OverpaymentLabel.StylePriority.UseBorders = false;
            this.OverpaymentLabel.StylePriority.UseFont = false;
            this.OverpaymentLabel.StylePriority.UsePadding = false;
            this.OverpaymentLabel.StylePriority.UseTextAlignment = false;
            this.OverpaymentLabel.Weight = 1.8896880114120758D;
            // 
            // overpaymentValue
            // 
            this.overpaymentValue.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.overpaymentValue.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.OverpaymentValue")});
            resources.ApplyResources(this.overpaymentValue, "overpaymentValue");
            this.overpaymentValue.Name = "overpaymentValue";
            this.overpaymentValue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.overpaymentValue.StylePriority.UseBorders = false;
            this.overpaymentValue.StylePriority.UseFont = false;
            this.overpaymentValue.StylePriority.UseTextAlignment = false;
            this.overpaymentValue.Weight = 0.47065701117926467D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.totalChargeTableCell});
            resources.ApplyResources(this.xrTableRow5, "xrTableRow5");
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1.0079365079365079D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            resources.ApplyResources(this.xrTableCell17, "xrTableCell17");
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 20, 0, 0, 254F);
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Weight = 1.8896880114120758D;
            // 
            // totalChargeTableCell
            // 
            this.totalChargeTableCell.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.totalChargeTableCell.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.TotalValue")});
            resources.ApplyResources(this.totalChargeTableCell, "totalChargeTableCell");
            this.totalChargeTableCell.Name = "totalChargeTableCell";
            this.totalChargeTableCell.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.totalChargeTableCell.StylePriority.UseBorders = false;
            this.totalChargeTableCell.StylePriority.UseFont = false;
            this.totalChargeTableCell.StylePriority.UseTextAlignment = false;
            resources.ApplyResources(xrSummary2, "xrSummary2");
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            xrSummary2.IgnoreNullValues = true;
            this.totalChargeTableCell.Summary = xrSummary2;
            this.totalChargeTableCell.Weight = 0.47065701117926467D;
            // 
            // serviceTableSubreport
            // 
            this.serviceTableSubreport.CanShrink = true;
            resources.ApplyResources(this.serviceTableSubreport, "serviceTableSubreport");
            this.serviceTableSubreport.Id = 0;
            this.serviceTableSubreport.Name = "serviceTableSubreport";
            this.serviceTableSubreport.ReportSource = new Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt.ServiceTableReportObject();
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.WordWrap = false;
            // 
            // peopleCountLabel
            // 
            this.peopleCountLabel.AutoWidth = true;
            this.peopleCountLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.LivingPeopleCount")});
            resources.ApplyResources(this.peopleCountLabel, "peopleCountLabel");
            this.peopleCountLabel.Name = "peopleCountLabel";
            this.peopleCountLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.peopleCountLabel.StylePriority.UseFont = false;
            this.peopleCountLabel.StylePriority.UseTextAlignment = false;
            this.peopleCountLabel.WordWrap = false;
            // 
            // xrLabel7
            // 
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.WordWrap = false;
            // 
            // areaLabel
            // 
            this.areaLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.Area")});
            resources.ApplyResources(this.areaLabel, "areaLabel");
            this.areaLabel.Name = "areaLabel";
            this.areaLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.areaLabel.StylePriority.UseFont = false;
            this.areaLabel.StylePriority.UseTextAlignment = false;
            this.areaLabel.WordWrap = false;
            // 
            // xrLabel5
            // 
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.WordWrap = false;
            // 
            // payerAddressLabel
            // 
            this.payerAddressLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.Address")});
            resources.ApplyResources(this.payerAddressLabel, "payerAddressLabel");
            this.payerAddressLabel.Name = "payerAddressLabel";
            this.payerAddressLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payerAddressLabel.StylePriority.UseFont = false;
            this.payerAddressLabel.StylePriority.UseTextAlignment = false;
            // 
            // payerAccountLabel
            // 
            this.payerAccountLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.Account")});
            resources.ApplyResources(this.payerAccountLabel, "payerAccountLabel");
            this.payerAccountLabel.Name = "payerAccountLabel";
            this.payerAccountLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payerAccountLabel.StylePriority.UseFont = false;
            this.payerAccountLabel.StylePriority.UseTextAlignment = false;
            this.payerAccountLabel.WordWrap = false;
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.WordWrap = false;
            // 
            // printDateLabel
            // 
            this.printDateLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.PrintDateTime")});
            resources.ApplyResources(this.printDateLabel, "printDateLabel");
            this.printDateLabel.Name = "printDateLabel";
            this.printDateLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.printDateLabel.StylePriority.UseFont = false;
            this.printDateLabel.StylePriority.UseTextAlignment = false;
            this.printDateLabel.WordWrap = false;
            // 
            // xrLabel9
            // 
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.WordWrap = false;
            // 
            // payerAkaLabel
            // 
            this.payerAkaLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.Aka")});
            resources.ApplyResources(this.payerAkaLabel, "payerAkaLabel");
            this.payerAkaLabel.Name = "payerAkaLabel";
            this.payerAkaLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payerAkaLabel.StylePriority.UseFont = false;
            this.payerAkaLabel.StylePriority.UseTextAlignment = false;
            this.payerAkaLabel.WordWrap = false;
            // 
            // xrLabel3
            // 
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.WordWrap = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.CanGrow = false;
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.WordWrap = false;
            // 
            // payBeforeDateLabel
            // 
            this.payBeforeDateLabel.CanGrow = false;
            this.payBeforeDateLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Customers.PayBeforeDateTime")});
            resources.ApplyResources(this.payBeforeDateLabel, "payBeforeDateLabel");
            this.payBeforeDateLabel.Name = "payBeforeDateLabel";
            this.payBeforeDateLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payBeforeDateLabel.StylePriority.UseFont = false;
            this.payBeforeDateLabel.StylePriority.UseTextAlignment = false;
            this.payBeforeDateLabel.WordWrap = false;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.xrControlStyle1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrControlStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xrControlStyle2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // xrControlStyle3
            // 
            this.xrControlStyle3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrControlStyle3.Name = "xrControlStyle3";
            this.xrControlStyle3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CustId
            // 
            this.CustId.Name = "CustId";
            this.CustId.Type = typeof(int);
            this.CustId.ValueInfo = "0";
            this.CustId.Visible = false;
            // 
            // topMarginBand1
            // 
            resources.ApplyResources(this.topMarginBand1, "topMarginBand1");
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            resources.ApplyResources(this.bottomMarginBand1, "bottomMarginBand1");
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // TopReceiptReportObject
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataMember = "Customers";
            this.DataSource = this.dataSet1;
            resources.ApplyResources(this, "$this");
            this.FilterString = "[CustomerId] = ?CustId";
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CustId});
            this.RequestParameters = false;
            this.ShowPreviewMarginLines = false;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2,
            this.xrControlStyle3});
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel repNameLbl;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle3;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRLabel billDateLabel;
        private DevExpress.XtraReports.UI.XRLabel payBeforeDateLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel printDateLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel payerAddressLabel;
        private DevExpress.XtraReports.UI.XRLabel payerAkaLabel;
        private DevExpress.XtraReports.UI.XRLabel payerAccountLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel peopleCountLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel areaLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell monthChargeTableCell;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell OverpaymentLabel;
        private DevExpress.XtraReports.UI.XRTableCell overpaymentValue;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableCell totalChargeTableCell;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRSubreport serviceTableSubreport;
        public DevExpress.XtraReports.Parameters.Parameter CustId;
        private Taumis.Alpha.Server.PrintForms.DataSets.RegularBillDataSet dataSet1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRLabel bankDetailsLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel2;
    }
}
