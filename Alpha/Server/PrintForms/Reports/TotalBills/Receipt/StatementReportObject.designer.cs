namespace Taumis.Alpha.Server.PrintForms.Reports.TotalBills.Receipt
{
    partial class StatementReportObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatementReportObject));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.statementServiceTableSubreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.peopleCountLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.areaLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.payerAkaLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.payerAddressLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.dataSet1 = new Taumis.Alpha.Server.PrintForms.DataSets.TotalBillDataSet();
            this.BillId = new DevExpress.XtraReports.Parameters.Parameter();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel12,
            this.statementServiceTableSubreport,
            this.xrLabel6,
            this.xrLabel1,
            this.peopleCountLabel,
            this.xrLabel7,
            this.areaLabel,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.payerAkaLabel,
            this.payerAddressLabel,
            this.xrLabel2,
            this.xrLabel8,
            this.xrLabel10});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.PrintDateTime", "{0:dd.MM.yyyy}")});
            resources.ApplyResources(this.xrLabel12, "xrLabel12");
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.StylePriority.UseFont = false;
            // 
            // statementServiceTableSubreport
            // 
            this.statementServiceTableSubreport.CanShrink = true;
            resources.ApplyResources(this.statementServiceTableSubreport, "statementServiceTableSubreport");
            this.statementServiceTableSubreport.Id = 0;
            this.statementServiceTableSubreport.Name = "statementServiceTableSubreport";
            this.statementServiceTableSubreport.ReportSource = new Taumis.Alpha.Server.PrintForms.Reports.TotalBills.Receipt.StatementServiceTableReportObject();
            // 
            // xrLabel6
            // 
            this.xrLabel6.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrLabel6.CanGrow = false;
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrLabel1.CanGrow = false;
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // peopleCountLabel
            // 
            this.peopleCountLabel.AutoWidth = true;
            this.peopleCountLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.LivingPeopleCount")});
            resources.ApplyResources(this.peopleCountLabel, "peopleCountLabel");
            this.peopleCountLabel.Name = "peopleCountLabel";
            this.peopleCountLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.peopleCountLabel.StylePriority.UseFont = false;
            this.peopleCountLabel.StylePriority.UseForeColor = false;
            this.peopleCountLabel.StylePriority.UseTextAlignment = false;
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
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.Area")});
            resources.ApplyResources(this.areaLabel, "areaLabel");
            this.areaLabel.Name = "areaLabel";
            this.areaLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.areaLabel.StylePriority.UseFont = false;
            this.areaLabel.StylePriority.UseForeColor = false;
            this.areaLabel.StylePriority.UseTextAlignment = false;
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
            // xrLabel4
            // 
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel3
            // 
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            // 
            // payerAkaLabel
            // 
            resources.ApplyResources(this.payerAkaLabel, "payerAkaLabel");
            this.payerAkaLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.Aka")});
            this.payerAkaLabel.Name = "payerAkaLabel";
            this.payerAkaLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payerAkaLabel.StylePriority.UseBackColor = false;
            this.payerAkaLabel.StylePriority.UseFont = false;
            this.payerAkaLabel.StylePriority.UseTextAlignment = false;
            // 
            // payerAddressLabel
            // 
            resources.ApplyResources(this.payerAddressLabel, "payerAddressLabel");
            this.payerAddressLabel.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.Address")});
            this.payerAddressLabel.Name = "payerAddressLabel";
            this.payerAddressLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.payerAddressLabel.StylePriority.UseBackColor = false;
            this.payerAddressLabel.StylePriority.UseFont = false;
            this.payerAddressLabel.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel2
            // 
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel8
            // 
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "TotalBillDocs.SinceTillPeriodString", "{0}")});
            resources.ApplyResources(this.xrLabel10, "xrLabel10");
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
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
            // BillId
            // 
            this.BillId.Name = "BillId";
            this.BillId.Type = typeof(int);
            this.BillId.ValueInfo = "0";
            this.BillId.Visible = false;
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
            // StatementReportObject
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataMember = "TotalBillDocs";
            this.DataSource = this.dataSet1;
            resources.ApplyResources(this, "$this");
            this.FilterString = "[TotalBillId] = ?BillId";
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.BillId});
            this.RequestParameters = false;
            this.ShowPreviewMarginLines = false;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2,
            this.xrControlStyle3});
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle3;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRLabel payerAddressLabel;
        private DevExpress.XtraReports.UI.XRLabel payerAkaLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.Parameters.Parameter BillId;
        private Taumis.Alpha.Server.PrintForms.DataSets.TotalBillDataSet dataSet1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel peopleCountLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel areaLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRSubreport statementServiceTableSubreport;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
