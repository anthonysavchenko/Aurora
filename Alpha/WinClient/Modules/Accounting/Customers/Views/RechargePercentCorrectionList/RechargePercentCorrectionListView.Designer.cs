
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    partial class RechargePercentCorrectionListView
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
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControlOfServicesListView = new DevExpress.XtraGrid.GridControl();
            this.gridViewOfServicesListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ServiceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PeriodGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DaysGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PercentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOfServicesListView
            // 
            this.gridControlOfServicesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfServicesListView.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlOfServicesListView.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlOfServicesListView.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlOfServicesListView.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlOfServicesListView.Location = new System.Drawing.Point(0, 0);
            this.gridControlOfServicesListView.MainView = this.gridViewOfServicesListView;
            this.gridControlOfServicesListView.Name = "gridControlOfServicesListView";
            this.gridControlOfServicesListView.Size = new System.Drawing.Size(707, 203);
            this.gridControlOfServicesListView.TabIndex = 0;
            this.gridControlOfServicesListView.UseEmbeddedNavigator = true;
            this.gridControlOfServicesListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOfServicesListView});
            // 
            // gridViewOfServicesListView
            // 
            this.gridViewOfServicesListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewOfServicesListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewOfServicesListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDGridColumn,
            this.ServiceGridColumn,
            this.PeriodGridColumn,
            this.DaysGridColumn,
            this.PercentGridColumn});
            this.gridViewOfServicesListView.GridControl = this.gridControlOfServicesListView;
            this.gridViewOfServicesListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewOfServicesListView.Name = "gridViewOfServicesListView";
            this.gridViewOfServicesListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.ServiceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.PeriodGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // IDGridColumn
            // 
            this.IDGridColumn.Caption = "ID";
            this.IDGridColumn.FieldName = "ID";
            this.IDGridColumn.Name = "IDGridColumn";
            this.IDGridColumn.OptionsColumn.AllowEdit = false;
            this.IDGridColumn.OptionsColumn.ReadOnly = true;
            // 
            // ServiceGridColumn
            // 
            this.ServiceGridColumn.Caption = "Услуга";
            this.ServiceGridColumn.FieldName = "ServiceName";
            this.ServiceGridColumn.Name = "ServiceGridColumn";
            this.ServiceGridColumn.OptionsColumn.AllowEdit = false;
            this.ServiceGridColumn.OptionsColumn.ReadOnly = true;
            this.ServiceGridColumn.Visible = true;
            this.ServiceGridColumn.VisibleIndex = 0;
            // 
            // PeriodGridColumn
            // 
            this.PeriodGridColumn.Caption = "Период";
            this.PeriodGridColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.PeriodGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.PeriodGridColumn.FieldName = "Period";
            this.PeriodGridColumn.Name = "PeriodGridColumn";
            this.PeriodGridColumn.OptionsColumn.AllowEdit = false;
            this.PeriodGridColumn.OptionsColumn.ReadOnly = true;
            this.PeriodGridColumn.Visible = true;
            this.PeriodGridColumn.VisibleIndex = 1;
            // 
            // DaysGridColumn
            // 
            this.DaysGridColumn.Caption = "Кол-во дней";
            this.DaysGridColumn.FieldName = "Days";
            this.DaysGridColumn.Name = "DaysGridColumn";
            this.DaysGridColumn.OptionsColumn.AllowEdit = false;
            this.DaysGridColumn.OptionsColumn.ReadOnly = true;
            this.DaysGridColumn.Visible = true;
            this.DaysGridColumn.VisibleIndex = 2;
            // 
            // PercentGridColumn
            // 
            this.PercentGridColumn.Caption = "Процент";
            this.PercentGridColumn.FieldName = "Percent";
            this.PercentGridColumn.Name = "PercentGridColumn";
            this.PercentGridColumn.OptionsColumn.AllowEdit = false;
            this.PercentGridColumn.OptionsColumn.ReadOnly = true;
            this.PercentGridColumn.Visible = true;
            this.PercentGridColumn.VisibleIndex = 3;
            // 
            // RechargePercentCorrectionListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfServicesListView);
            this.Name = "RechargePercentCorrectionListView";
            this.Size = new System.Drawing.Size(707, 203);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfServicesListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfServicesListView;
        private DevExpress.XtraGrid.Columns.GridColumn IDGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ServiceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PeriodGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn DaysGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn PercentGridColumn;
    }
}

