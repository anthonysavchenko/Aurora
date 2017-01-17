﻿
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
            this.RateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorRepositoryItemLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOfServicesListView
            // 
            this.gridControlOfServicesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfServicesListView.Location = new System.Drawing.Point(0, 0);
            this.gridControlOfServicesListView.MainView = this.gridViewOfServicesListView;
            this.gridControlOfServicesListView.Name = "gridControlOfServicesListView";
            this.gridControlOfServicesListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ServiceRepositoryItemLookUpEdit,
            this.ContractorRepositoryItemLookUpEdit,
            this.SinceAndTillRepositoryItemDateEdit});
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
            this.SinceGridColumn,
            this.TillGridColumn,
            this.ContractorGridColumn,
            this.RateGridColumn});
            this.gridViewOfServicesListView.GridControl = this.gridControlOfServicesListView;
            this.gridViewOfServicesListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "", null, "")});
            this.gridViewOfServicesListView.Name = "gridViewOfServicesListView";
            this.gridViewOfServicesListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.ServiceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.SinceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.TillGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewOfServicesListView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewOfServicesListView_FocusedRowChanged);
            this.gridViewOfServicesListView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewOfServicesListView_RowStyle);
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
            this.SinceAndTillRepositoryItemDateEdit.DisplayFormat.FormatString = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceAndTillRepositoryItemDateEdit.EditFormat.FormatString = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceAndTillRepositoryItemDateEdit.Mask.EditMask = "MM.yyyy";
            this.SinceAndTillRepositoryItemDateEdit.Name = "SinceAndTillRepositoryItemDateEdit";
            this.SinceAndTillRepositoryItemDateEdit.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
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
            // RateGridColumn
            // 
            this.RateGridColumn.Caption = "Тариф";
            this.RateGridColumn.FieldName = "Rate";
            this.RateGridColumn.Name = "RateGridColumn";
            this.RateGridColumn.Visible = true;
            this.RateGridColumn.VisibleIndex = 4;
            // 
            // CustomerPosListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfServicesListView);
            this.Name = "CustomerPosListView";
            this.Size = new System.Drawing.Size(707, 203);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfServicesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfServicesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceRepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceAndTillRepositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorRepositoryItemLookUpEdit)).EndInit();
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
    }
}

