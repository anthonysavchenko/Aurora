namespace Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt
{
    partial class ReceiptWithCountLayoutReportObject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiptWithCountLayoutReportObject));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.countSubreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.ButtomReceiptSubreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopReceiptSubreport = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle3 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.dataSet1 = new Taumis.Alpha.Server.PrintForms.DataSets.RegularBillDataSet();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.countSubreport,
            this.ButtomReceiptSubreport,
            this.TopReceiptSubreport,
            this.xrLine1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseTextAlignment = false;
            // 
            // countSubreport
            // 
            resources.ApplyResources(this.countSubreport, "countSubreport");
            this.countSubreport.Id = 0;
            this.countSubreport.Name = "countSubreport";
            this.countSubreport.ReportSource = new Taumis.Alpha.Server.PrintForms.Reports.RegularBills.CountReport.TableCountReportObject();
            // 
            // ButtomReceiptSubreport
            // 
            resources.ApplyResources(this.ButtomReceiptSubreport, "ButtomReceiptSubreport");
            this.ButtomReceiptSubreport.Id = 0;
            this.ButtomReceiptSubreport.Name = "ButtomReceiptSubreport";
            this.ButtomReceiptSubreport.ReportSource = new Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt.BottomReceiptReportObject();
            // 
            // TopReceiptSubreport
            // 
            resources.ApplyResources(this.TopReceiptSubreport, "TopReceiptSubreport");
            this.TopReceiptSubreport.Id = 0;
            this.TopReceiptSubreport.Name = "TopReceiptSubreport";
            this.TopReceiptSubreport.ReportSource = new Taumis.Alpha.Server.PrintForms.Reports.RegularBills.Receipt.TopReceiptReportObject();
            // 
            // xrLine1
            // 
            resources.ApplyResources(this.xrLine1, "xrLine1");
            this.xrLine1.LineWidth = 3;
            this.xrLine1.Name = "xrLine1";
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
            // xrLabel5
            // 
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.WordWrap = false;
            // 
            // ReceiptWithCountLayoutReportObject
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            resources.ApplyResources(this, "$this");
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.RequestParameters = false;
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
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRSubreport ButtomReceiptSubreport;
        private DevExpress.XtraReports.UI.XRSubreport TopReceiptSubreport;
        private Taumis.Alpha.Server.PrintForms.DataSets.RegularBillDataSet dataSet1;
        private DevExpress.XtraReports.UI.XRSubreport countSubreport;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
    }
}
