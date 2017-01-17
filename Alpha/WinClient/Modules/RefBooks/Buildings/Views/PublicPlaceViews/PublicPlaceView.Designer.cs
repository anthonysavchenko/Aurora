namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews
{
    partial class PublicPlaceView
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
            this.serviceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.areaColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.areaItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaItemSpinEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // counterGridControl
            // 
            this.counterGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterGridControl.Location = new System.Drawing.Point(0, 0);
            this.counterGridControl.MainView = this.counterGridView;
            this.counterGridControl.Name = "counterGridControl";
            this.counterGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.serviceLookUpEdit,
            this.areaItemSpinEdit});
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
            this.serviceColumn,
            this.areaColumn});
            this.counterGridView.GridControl = this.counterGridControl;
            this.counterGridView.Name = "counterGridView";
            this.counterGridView.OptionsView.ShowGroupPanel = false;
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // serviceColumn
            // 
            this.serviceColumn.Caption = "Услуга";
            this.serviceColumn.ColumnEdit = this.serviceLookUpEdit;
            this.serviceColumn.FieldName = "Service";
            this.serviceColumn.Name = "serviceColumn";
            this.serviceColumn.Visible = true;
            this.serviceColumn.VisibleIndex = 0;
            // 
            // serviceLookUpEdit
            // 
            this.serviceLookUpEdit.AutoHeight = false;
            this.serviceLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.serviceLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Услуга")});
            this.serviceLookUpEdit.DisplayMember = "Name";
            this.serviceLookUpEdit.Name = "serviceLookUpEdit";
            this.serviceLookUpEdit.ValueMember = "ID";
            // 
            // areaColumn
            // 
            this.areaColumn.Caption = "Площадь";
            this.areaColumn.ColumnEdit = this.areaItemSpinEdit;
            this.areaColumn.FieldName = "Area";
            this.areaColumn.Name = "areaColumn";
            this.areaColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.areaColumn.Visible = true;
            this.areaColumn.VisibleIndex = 1;
            // 
            // areaItemSpinEdit
            // 
            this.areaItemSpinEdit.AutoHeight = false;
            this.areaItemSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.areaItemSpinEdit.DisplayFormat.FormatString = "0.00";
            this.areaItemSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.areaItemSpinEdit.EditFormat.FormatString = "0.00";
            this.areaItemSpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.areaItemSpinEdit.Name = "areaItemSpinEdit";
            // 
            // PublicPlaceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.counterGridControl);
            this.Name = "PublicPlaceView";
            this.Size = new System.Drawing.Size(736, 336);
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaItemSpinEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl counterGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterGridView;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn areaColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serviceColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit serviceLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit areaItemSpinEdit;
    }
}
