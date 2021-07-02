
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    partial class CustomerPosListView
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
            this.ServiceRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.SinceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SinceAndTillRepositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.TillGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContractorGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContractorRepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.CounterGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CounterItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.RateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showAllCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CounterItemLookUpEdit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlOfServicesListView
            // 
            this.gridControlOfServicesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfServicesListView.Location = new System.Drawing.Point(0, 24);
            this.gridControlOfServicesListView.MainView = this.gridViewOfServicesListView;
            this.gridControlOfServicesListView.Name = "gridControlOfServicesListView";
            this.gridControlOfServicesListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ServiceRepositoryItemLookUpEdit,
            this.ContractorRepositoryItemLookUpEdit,
            this.SinceAndTillRepositoryItemDateEdit,
            this.CounterItemLookUpEdit});
            this.gridControlOfServicesListView.Size = new System.Drawing.Size(707, 179);
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
            this.SinceGridColumn,
            this.TillGridColumn,
            this.ContractorGridColumn,
            this.CounterGridColumn,
            this.RateGridColumn});
            this.gridViewOfServicesListView.GridControl = this.gridControlOfServicesListView;
            this.gridViewOfServicesListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewOfServicesListView.Name = "gridViewOfServicesListView";
            this.gridViewOfServicesListView.OptionsCustomization.AllowGroup = false;
            this.gridViewOfServicesListView.OptionsView.ShowGroupPanel = false;
            this.gridViewOfServicesListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.ServiceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.SinceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.TillGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewOfServicesListView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewOfServicesListView_RowStyle);
            this.gridViewOfServicesListView.ShownEditor += new System.EventHandler(this.gridViewOfServicesListView_ShownEditor);
            // 
            // IDGridColumn
            // 
            this.IDGridColumn.Caption = "ID";
            this.IDGridColumn.FieldName = "ID";
            this.IDGridColumn.Name = "IDGridColumn";
            // 
            // ServiceGridColumn
            // 
            this.ServiceGridColumn.Caption = "Услуга";
            this.ServiceGridColumn.ColumnEdit = this.ServiceRepositoryItemLookUpEdit;
            this.ServiceGridColumn.FieldName = "Service";
            this.ServiceGridColumn.Name = "ServiceGridColumn";
            this.ServiceGridColumn.Visible = true;
            this.ServiceGridColumn.VisibleIndex = 0;
            // 
            // ServiceRepositoryItemLookUpEdit
            // 
            this.ServiceRepositoryItemLookUpEdit.AutoHeight = false;
            this.ServiceRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ServiceRepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Услуга")});
            this.ServiceRepositoryItemLookUpEdit.DisplayMember = "Name";
            this.ServiceRepositoryItemLookUpEdit.Name = "ServiceRepositoryItemLookUpEdit";
            this.ServiceRepositoryItemLookUpEdit.ValueMember = "ID";
            this.ServiceRepositoryItemLookUpEdit.EditValueChanged += new System.EventHandler(this.ServiceRepositoryItemLookUpEdit_EditValueChanged);
            // 
            // SinceGridColumn
            // 
            this.SinceGridColumn.Caption = "Начальный период";
            this.SinceGridColumn.ColumnEdit = this.SinceAndTillRepositoryItemDateEdit;
            this.SinceGridColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.SinceGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceGridColumn.FieldName = "Since";
            this.SinceGridColumn.Name = "SinceGridColumn";
            this.SinceGridColumn.Visible = true;
            this.SinceGridColumn.VisibleIndex = 1;
            // 
            // SinceAndTillRepositoryItemDateEdit
            // 
            this.SinceAndTillRepositoryItemDateEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.SinceAndTillRepositoryItemDateEdit.AutoHeight = false;
            this.SinceAndTillRepositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SinceAndTillRepositoryItemDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SinceAndTillRepositoryItemDateEdit.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.SinceAndTillRepositoryItemDateEdit.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.SinceAndTillRepositoryItemDateEdit.DisplayFormat.FormatString = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceAndTillRepositoryItemDateEdit.EditFormat.FormatString = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceAndTillRepositoryItemDateEdit.Mask.EditMask = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.Name = "SinceAndTillRepositoryItemDateEdit";
            // 
            // TillGridColumn
            // 
            this.TillGridColumn.Caption = "Конечный период";
            this.TillGridColumn.ColumnEdit = this.SinceAndTillRepositoryItemDateEdit;
            this.TillGridColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.TillGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TillGridColumn.FieldName = "Till";
            this.TillGridColumn.Name = "TillGridColumn";
            this.TillGridColumn.Visible = true;
            this.TillGridColumn.VisibleIndex = 2;
            // 
            // ContractorGridColumn
            // 
            this.ContractorGridColumn.Caption = "Подрядчик";
            this.ContractorGridColumn.ColumnEdit = this.ContractorRepositoryItemLookUpEdit;
            this.ContractorGridColumn.FieldName = "Contractor";
            this.ContractorGridColumn.Name = "ContractorGridColumn";
            this.ContractorGridColumn.Visible = true;
            this.ContractorGridColumn.VisibleIndex = 3;
            // 
            // ContractorRepositoryItemLookUpEdit
            // 
            this.ContractorRepositoryItemLookUpEdit.AutoHeight = false;
            this.ContractorRepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ContractorRepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Подрядчик")});
            this.ContractorRepositoryItemLookUpEdit.DisplayMember = "Name";
            this.ContractorRepositoryItemLookUpEdit.Name = "ContractorRepositoryItemLookUpEdit";
            this.ContractorRepositoryItemLookUpEdit.ValueMember = "ID";
            // 
            // CounterGridColumn
            // 
            this.CounterGridColumn.Caption = "Прибор учета";
            this.CounterGridColumn.ColumnEdit = this.CounterItemLookUpEdit;
            this.CounterGridColumn.FieldName = "Counter";
            this.CounterGridColumn.Name = "CounterGridColumn";
            this.CounterGridColumn.Visible = true;
            this.CounterGridColumn.VisibleIndex = 4;
            // 
            // CounterItemLookUpEdit
            // 
            this.CounterItemLookUpEdit.AutoHeight = false;
            this.CounterItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CounterItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Прибор учета"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ServiceName", "Услуга")});
            this.CounterItemLookUpEdit.DisplayMember = "Number";
            this.CounterItemLookUpEdit.Name = "CounterItemLookUpEdit";
            this.CounterItemLookUpEdit.ValueMember = "ID";
            // 
            // RateGridColumn
            // 
            this.RateGridColumn.Caption = "Тариф";
            this.RateGridColumn.FieldName = "Rate";
            this.RateGridColumn.Name = "RateGridColumn";
            this.RateGridColumn.Visible = true;
            this.RateGridColumn.VisibleIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.showAllCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 24);
            this.panel1.TabIndex = 1;
            // 
            // showAllCheckBox
            // 
            this.showAllCheckBox.AutoSize = true;
            this.showAllCheckBox.Location = new System.Drawing.Point(4, 4);
            this.showAllCheckBox.Name = "showAllCheckBox";
            this.showAllCheckBox.Size = new System.Drawing.Size(96, 17);
            this.showAllCheckBox.TabIndex = 0;
            this.showAllCheckBox.Text = "Показать все";
            this.showAllCheckBox.UseVisualStyleBackColor = true;
            this.showAllCheckBox.Click += new System.EventHandler(this.showAllCheckBox_Click);
            // 
            // CustomerPosListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfServicesListView);
            this.Controls.Add(this.panel1);
            this.Name = "CustomerPosListView";
            this.Size = new System.Drawing.Size(707, 203);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CounterItemLookUpEdit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfServicesListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfServicesListView;
        private DevExpress.XtraGrid.Columns.GridColumn IDGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ServiceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ContractorGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn RateGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ServiceRepositoryItemLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ContractorRepositoryItemLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn SinceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn TillGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit SinceAndTillRepositoryItemDateEdit;
        private DevExpress.XtraGrid.Columns.GridColumn CounterGridColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit CounterItemLookUpEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox showAllCheckBox;
    }
}

