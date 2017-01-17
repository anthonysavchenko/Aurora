namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Item
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
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountNumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.correctionColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.Location = new System.Drawing.Point(0, 0);
            _gridControlOfListView.MainView = this._gridViewOfListView;
            _gridControlOfListView.Name = "_gridControlOfListView";
            _gridControlOfListView.Size = new System.Drawing.Size(765, 305);
            _gridControlOfListView.TabIndex = 2;
            _gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridViewOfListView});
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.accountNumberColumn,
            this.valueColumn,
            this.benefitColumn,
            this.correctionColumn});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.ShowFooter = true;
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
            this.accountNumberColumn.SummaryItem.DisplayFormat = "{0}";
            this.accountNumberColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.accountNumberColumn.Visible = true;
            this.accountNumberColumn.VisibleIndex = 0;
            // 
            // valueColumn
            // 
            this.valueColumn.Caption = "Сумма";
            this.valueColumn.DisplayFormat.FormatString = "{0:c2}";
            this.valueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueColumn.FieldName = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.SummaryItem.DisplayFormat = "{0:c2}";
            this.valueColumn.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.valueColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.valueColumn.Visible = true;
            this.valueColumn.VisibleIndex = 1;
            // 
            // benefitColumn
            // 
            this.benefitColumn.Caption = "Льгота";
            this.benefitColumn.DisplayFormat.FormatString = "{0:c2}";
            this.benefitColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.benefitColumn.FieldName = "Benefit";
            this.benefitColumn.Name = "benefitColumn";
            this.benefitColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.benefitColumn.Visible = true;
            this.benefitColumn.VisibleIndex = 2;
            // 
            // correctionColumn
            // 
            this.correctionColumn.Caption = "Корректировка";
            this.correctionColumn.FieldName = "IsCorrected";
            this.correctionColumn.Name = "correctionColumn";
            this.correctionColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.correctionColumn.Visible = true;
            this.correctionColumn.VisibleIndex = 3;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn accountNumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn valueColumn;
        private DevExpress.XtraGrid.Columns.GridColumn correctionColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitColumn;
    }
}