namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Item
{
    partial class ItemView
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.correctionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountNumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.periodColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.SuspendLayout();
            // 
            // correctionColumn
            // 
            this.correctionColumn.Caption = "Корректировка";
            this.correctionColumn.ColumnEdit = this.repositoryItemCheckEdit1;
            this.correctionColumn.FieldName = "IsCorrected";
            this.correctionColumn.Name = "correctionColumn";
            this.correctionColumn.OptionsColumn.AllowEdit = false;
            this.correctionColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, "IsCorrected", "{0}")});
            this.correctionColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.correctionColumn.Visible = true;
            this.correctionColumn.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ReadOnly = true;
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.accountNumberColumn,
            this.periodColumn,
            this.valueColumn,
            this.correctionColumn});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Silver;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.correctionColumn;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = true;
            this._gridViewOfListView.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.ShowFooter = true;
            this._gridViewOfListView.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this._gridViewOfListView_CustomSummaryCalculate);
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // accountNumberColumn
            // 
            this.accountNumberColumn.Caption = "Лицевой счет";
            this.accountNumberColumn.FieldName = "AccountNumber";
            this.accountNumberColumn.Name = "accountNumberColumn";
            this.accountNumberColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "AccountNumber", "{0}")});
            this.accountNumberColumn.Visible = true;
            this.accountNumberColumn.VisibleIndex = 0;
            // 
            // periodColumn
            // 
            this.periodColumn.Caption = "Период платежа";
            this.periodColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.periodColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodColumn.FieldName = "Period";
            this.periodColumn.Name = "periodColumn";
            this.periodColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.periodColumn.Visible = true;
            this.periodColumn.VisibleIndex = 1;
            // 
            // valueColumn
            // 
            this.valueColumn.Caption = "Сумма";
            this.valueColumn.DisplayFormat.FormatString = "{0:c2}";
            this.valueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueColumn.FieldName = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c2}")});
            this.valueColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.valueColumn.Visible = true;
            this.valueColumn.VisibleIndex = 2;
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 0);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            _gridControlOfListView.Size = new System.Drawing.Size(765, 305);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn accountNumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn periodColumn;
        private DevExpress.XtraGrid.Columns.GridColumn valueColumn;
        private DevExpress.XtraGrid.Columns.GridColumn correctionColumn;
    }
}