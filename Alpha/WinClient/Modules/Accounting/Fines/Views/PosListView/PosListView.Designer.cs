namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView
{
    partial class PosListView
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
            this.posGridControl = new DevExpress.XtraGrid.GridControl();
            this.posGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customerColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customerLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.valueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.posGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // posGridControl
            // 
            this.posGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.posGridControl.Location = new System.Drawing.Point(0, 0);
            this.posGridControl.MainView = this.posGridView;
            this.posGridControl.Name = "posGridControl";
            this.posGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.customerLookUpEdit,
            this.repositoryItemSpinEdit1});
            this.posGridControl.Size = new System.Drawing.Size(736, 336);
            this.posGridControl.TabIndex = 1;
            this.posGridControl.UseEmbeddedNavigator = true;
            this.posGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.posGridView});
            // 
            // posGridView
            // 
            this.posGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.posGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.posGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idColumn,
            this.customerColumn,
            this.valueColumn});
            this.posGridView.GridControl = this.posGridControl;
            this.posGridView.Name = "posGridView";
            this.posGridView.OptionsView.ShowFooter = true;
            this.posGridView.OptionsView.ShowGroupPanel = false;
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // customerColumn
            // 
            this.customerColumn.Caption = "Абонент";
            this.customerColumn.ColumnEdit = this.customerLookUpEdit;
            this.customerColumn.FieldName = "Customer";
            this.customerColumn.Name = "customerColumn";
            this.customerColumn.Visible = true;
            this.customerColumn.VisibleIndex = 0;
            // 
            // customerLookUpEdit
            // 
            this.customerLookUpEdit.AutoHeight = false;
            this.customerLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.customerLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Account", "Лицевой счет")});
            this.customerLookUpEdit.DisplayMember = "Account";
            this.customerLookUpEdit.Name = "customerLookUpEdit";
            this.customerLookUpEdit.ValueMember = "ID";
            // 
            // valueColumn
            // 
            this.valueColumn.Caption = "Пеня";
            this.valueColumn.ColumnEdit = this.repositoryItemSpinEdit1;
            this.valueColumn.FieldName = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.valueColumn.Visible = true;
            this.valueColumn.VisibleIndex = 1;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.DisplayFormat.FormatString = "0.00";
            this.repositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.EditFormat.FormatString = "0.00";
            this.repositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // PosListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.posGridControl);
            this.Name = "PosListView";
            this.Size = new System.Drawing.Size(736, 336);
            ((System.ComponentModel.ISupportInitialize)(this.posGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl posGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView posGridView;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn customerColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit customerLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn valueColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}
