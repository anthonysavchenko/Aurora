namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.DebtAgency.Views.List
{
    partial class ListView
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
            this.gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            this.gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.accountGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addressGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.oldDebtsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.prevPeriodTotalGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.curPeriodTotalGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.debtAgencyGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.woIntermediaryPaymentsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOfListView
            // 
            this.gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControlOfListView.Location = new System.Drawing.Point(0, 40);
            this.gridControlOfListView.MainView = this.gridViewOfListView;
            this.gridControlOfListView.Name = "gridControlOfListView";
            this.gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlOfListView.Size = new System.Drawing.Size(750, 310);
            this.gridControlOfListView.TabIndex = 0;
            this.gridControlOfListView.UseEmbeddedNavigator = true;
            this.gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOfListView});
            // 
            // gridViewOfListView
            // 
            this.gridViewOfListView.Appearance.GroupRow.Options.UseTextOptions = true;
            this.gridViewOfListView.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridViewOfListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewOfListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewOfListView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridViewOfListView.ColumnPanelRowHeight = 34;
            this.gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.accountGridColumn,
            this.addressGridColumn,
            this.oldDebtsGridColumn,
            this.prevPeriodTotalGridColumn,
            this.curPeriodTotalGridColumn,
            this.debtAgencyGridColumn,
            this.woIntermediaryPaymentsGridColumn});
            this.gridViewOfListView.GridControl = this.gridControlOfListView;
            this.gridViewOfListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Charges", null, "(Платежи: {0})", "")});
            this.gridViewOfListView.Name = "gridViewOfListView";
            this.gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewOfListView.OptionsBehavior.Editable = false;
            this.gridViewOfListView.OptionsView.ColumnAutoWidth = true;
            this.gridViewOfListView.OptionsView.RowAutoHeight = true;
            this.gridViewOfListView.OptionsView.ShowFooter = true;
            // 
            // accountGridColumn
            // 
            this.accountGridColumn.Caption = "л/с";
            this.accountGridColumn.FieldName = "account";
            this.accountGridColumn.Name = "accountGridColumn";
            this.accountGridColumn.Visible = true;
            this.accountGridColumn.VisibleIndex = 0;
            // 
            // addressGridColumn
            // 
            this.addressGridColumn.Caption = "Адрес";
            this.addressGridColumn.FieldName = "address";
            this.addressGridColumn.Name = "addressGridColumn";
            this.addressGridColumn.Visible = true;
            this.addressGridColumn.VisibleIndex = 1;
            // 
            // oldDebtsGridColumn
            // 
            this.oldDebtsGridColumn.Caption = "Старые долги";
            this.oldDebtsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.oldDebtsGridColumn.FieldName = "oldDebts";
            this.oldDebtsGridColumn.Name = "oldDebtsGridColumn";
            this.oldDebtsGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.oldDebtsGridColumn.Visible = true;
            this.oldDebtsGridColumn.VisibleIndex = 2;
            // 
            // prevPeriodTotalGridColumn
            // 
            this.prevPeriodTotalGridColumn.Caption = "Было";
            this.prevPeriodTotalGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.prevPeriodTotalGridColumn.FieldName = "prevPeriodTotal";
            this.prevPeriodTotalGridColumn.Name = "prevPeriodTotalGridColumn";
            this.prevPeriodTotalGridColumn.Visible = true;
            this.prevPeriodTotalGridColumn.VisibleIndex = 3;
            // 
            // curPeriodTotalGridColumn
            // 
            this.curPeriodTotalGridColumn.Caption = "Стало";
            this.curPeriodTotalGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.curPeriodTotalGridColumn.FieldName = "curPeriodTotal";
            this.curPeriodTotalGridColumn.Name = "curPeriodTotalGridColumn";
            this.curPeriodTotalGridColumn.Visible = true;
            this.curPeriodTotalGridColumn.VisibleIndex = 4;
            // 
            // debtAgencyGridColumn
            // 
            this.debtAgencyGridColumn.Caption = "Собрал коллектор";
            this.debtAgencyGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.debtAgencyGridColumn.FieldName = "debtAgencyTotal";
            this.debtAgencyGridColumn.Name = "debtAgencyGridColumn";
            this.debtAgencyGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.debtAgencyGridColumn.Visible = true;
            this.debtAgencyGridColumn.VisibleIndex = 5;
            // 
            // woIntermediaryPaymentsGridColumn
            // 
            this.woIntermediaryPaymentsGridColumn.Caption = "Списания";
            this.woIntermediaryPaymentsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.woIntermediaryPaymentsGridColumn.FieldName = "woIntermediaryPayments";
            this.woIntermediaryPaymentsGridColumn.Name = "woIntermediaryPaymentsGridColumn";
            this.woIntermediaryPaymentsGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.woIntermediaryPaymentsGridColumn.Visible = true;
            this.woIntermediaryPaymentsGridColumn.VisibleIndex = 6;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.DisplayValueChecked = "1";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "0";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.periodDateEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 40);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период";
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(57, 13);
            this.periodDateEdit.Name = "periodDateEdit";
            this.periodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.periodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.periodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.periodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.periodDateEdit.Size = new System.Drawing.Size(130, 20);
            this.periodDateEdit.TabIndex = 3;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfListView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(750, 350);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn accountGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn addressGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn oldDebtsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn prevPeriodTotalGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn curPeriodTotalGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn debtAgencyGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn woIntermediaryPaymentsGridColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
    }
}