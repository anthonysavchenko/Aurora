namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Item
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
            this.AccountColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AddressColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ValueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControlOfListView
            // 
            _gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Append.Enabled = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Append.Visible = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Edit.Visible = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Last.Visible = false;
            _gridControlOfListView.EmbeddedNavigator.Buttons.Remove.Visible = false;
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
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.AccountColumn,
            this.AddressColumn,
            this.ValueColumn});
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
            // AccountColumn
            // 
            this.AccountColumn.Caption = "Лицевой счет";
            this.AccountColumn.FieldName = "Account";
            this.AccountColumn.Name = "AccountColumn";
            this.AccountColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.AccountColumn.Visible = true;
            this.AccountColumn.VisibleIndex = 0;
            // 
            // AddressColumn
            // 
            this.AddressColumn.Caption = "Адрес";
            this.AddressColumn.FieldName = "Address";
            this.AddressColumn.Name = "AddressColumn";
            this.AddressColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.AddressColumn.Visible = true;
            this.AddressColumn.VisibleIndex = 1;
            // 
            // ValueColumn
            // 
            this.ValueColumn.Caption = "Сумма";
            this.ValueColumn.DisplayFormat.FormatString = "{0:c2}";
            this.ValueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ValueColumn.FieldName = "Value";
            this.ValueColumn.Name = "ValueColumn";
            this.ValueColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", "{0:c2}")});
            this.ValueColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.ValueColumn.Visible = true;
            this.ValueColumn.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ReadOnly = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn AccountColumn;
        private DevExpress.XtraGrid.Columns.GridColumn AddressColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ValueColumn;
    }
}