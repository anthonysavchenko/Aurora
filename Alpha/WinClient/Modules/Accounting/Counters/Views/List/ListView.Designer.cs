
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.List
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
            this._gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addressColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            _gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridViewOfListView
            // 
            this._gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.accountColumn,
            this.numberColumn,
            this.serviceColumn,
            this.addressColumn});
            this._gridViewOfListView.GridControl = _gridControlOfListView;
            this._gridViewOfListView.GroupCount = 1;
            this._gridViewOfListView.GroupFormat = "{1}";
            this._gridViewOfListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this._gridViewOfListView.Name = "_gridViewOfListView";
            this._gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this._gridViewOfListView.OptionsBehavior.Editable = false;
            this._gridViewOfListView.OptionsSelection.MultiSelect = true;
            this._gridViewOfListView.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this._gridViewOfListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.addressColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.OptionsColumn.ReadOnly = true;
            // 
            // numberColumn
            // 
            this.numberColumn.Caption = "Номер";
            this.numberColumn.FieldName = "Number";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.Visible = true;
            this.numberColumn.VisibleIndex = 1;
            // 
            // serviceColumn
            // 
            this.serviceColumn.Caption = "Услуга";
            this.serviceColumn.FieldName = "Service";
            this.serviceColumn.Name = "serviceColumn";
            this.serviceColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.serviceColumn.Visible = true;
            this.serviceColumn.VisibleIndex = 2;
            // 
            // accountColumn
            // 
            this.accountColumn.Caption = "Лицевой счет";
            this.accountColumn.FieldName = "Account";
            this.accountColumn.Name = "accountColumn";
            this.accountColumn.Visible = true;
            this.accountColumn.VisibleIndex = 0;
            // 
            // addressColumn
            // 
            this.addressColumn.Caption = "Адрес";
            this.addressColumn.FieldName = "Address";
            this.addressColumn.Name = "addressColumn";
            this.addressColumn.Visible = true;
            this.addressColumn.VisibleIndex = 3;
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
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_gridControlOfListView);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(765, 305);
            ((System.ComponentModel.ISupportInitialize)(this._gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(_gridControlOfListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewOfListView;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn numberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serviceColumn;
        private DevExpress.XtraGrid.Columns.GridColumn addressColumn;
        private DevExpress.XtraGrid.Columns.GridColumn accountColumn;
    }
}