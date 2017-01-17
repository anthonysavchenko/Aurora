namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter
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
            this.rateColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContainerTypeLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerTypeLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // counterGridControl
            // 
            this.counterGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterGridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.counterGridControl.Location = new System.Drawing.Point(0, 0);
            this.counterGridControl.MainView = this.counterGridView;
            this.counterGridControl.Name = "counterGridControl";
            this.counterGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ContainerTypeLookUpEdit});
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
            this.rateColumn});
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
            // rateColumn
            // 
            this.rateColumn.Caption = "Тариф";
            this.rateColumn.DisplayFormat.FormatString = "{0:c2}";
            this.rateColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rateColumn.FieldName = "Rate";
            this.rateColumn.Name = "rateColumn";
            this.rateColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.rateColumn.Visible = true;
            this.rateColumn.VisibleIndex = 1;
            // 
            // ContainerTypeLookUpEdit
            // 
            this.ContainerTypeLookUpEdit.AutoHeight = false;
            this.ContainerTypeLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ContainerTypeLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Aka", 35, "Наименование")});
            this.ContainerTypeLookUpEdit.Name = "ContainerTypeLookUpEdit";
            this.ContainerTypeLookUpEdit.NullText = "";
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
            ((System.ComponentModel.ISupportInitialize)(this.ContainerTypeLookUpEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl counterGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterGridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ContainerTypeLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn numberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rateColumn;
    }
}
