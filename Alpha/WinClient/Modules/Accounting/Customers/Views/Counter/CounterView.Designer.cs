﻿namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
{
    partial class CounterView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.counterGridControl = new DevExpress.XtraGrid.GridControl();
            this.counterGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // counterGridControl
            // 
            this.counterGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterGridControl.Location = new System.Drawing.Point(0, 0);
            this.counterGridControl.MainView = this.counterGridView;
            this.counterGridControl.Name = "counterGridControl";
            this.counterGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.serviceLookUpEdit});
            this.counterGridControl.Size = new System.Drawing.Size(736, 336);
            this.counterGridControl.TabIndex = 1;
            this.counterGridControl.UseEmbeddedNavigator = true;
            this.counterGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.counterGridView});
            // 
            // counterGridView
            // 
            this.counterGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.counterGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.counterGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idColumn,
            this.numberColumn,
            this.serviceColumn});
            this.counterGridView.GridControl = this.counterGridControl;
            this.counterGridView.Name = "counterGridView";
            this.counterGridView.OptionsView.ShowGroupPanel = false;
            this.counterGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.counterGridView_FocusedRowChanged);
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // numberColumn
            // 
            this.numberColumn.Caption = "Номер";
            this.numberColumn.FieldName = "Number";
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.Visible = true;
            this.numberColumn.VisibleIndex = 0;
            // 
            // serviceColumn
            // 
            this.serviceColumn.Caption = "Услуга";
            this.serviceColumn.ColumnEdit = this.serviceLookUpEdit;
            this.serviceColumn.FieldName = "Service";
            this.serviceColumn.Name = "serviceColumn";
            this.serviceColumn.Visible = true;
            this.serviceColumn.VisibleIndex = 1;
            // 
            // serviceLookUpEdit
            // 
            this.serviceLookUpEdit.AutoHeight = false;
            this.serviceLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.serviceLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Услуга")});
            this.serviceLookUpEdit.DisplayMember = "Name";
            this.serviceLookUpEdit.Name = "serviceLookUpEdit";
            this.serviceLookUpEdit.ValueMember = "ID";
            // 
            // CounterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.counterGridControl);
            this.Name = "CounterView";
            this.Size = new System.Drawing.Size(736, 336);
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl counterGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterGridView;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn numberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serviceColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit serviceLookUpEdit;
    }
}
