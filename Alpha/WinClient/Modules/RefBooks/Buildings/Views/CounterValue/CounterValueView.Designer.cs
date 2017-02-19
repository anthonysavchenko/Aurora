namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue
{
    partial class CounterValueView
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
            this.counterValueGridControl = new DevExpress.XtraGrid.GridControl();
            this.counterValueGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.periodColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.periodRepositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.valueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodRepositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodRepositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueSpinEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // counterValueGridControl
            // 
            this.counterValueGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.counterValueGridControl.Location = new System.Drawing.Point(0, 0);
            this.counterValueGridControl.MainView = this.counterValueGridView;
            this.counterValueGridControl.Name = "counterValueGridControl";
            this.counterValueGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.periodRepositoryItemDateEdit,
            this.valueSpinEdit});
            this.counterValueGridControl.Size = new System.Drawing.Size(803, 458);
            this.counterValueGridControl.TabIndex = 2;
            this.counterValueGridControl.UseEmbeddedNavigator = true;
            this.counterValueGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.counterValueGridView});
            // 
            // counterValueGridView
            // 
            this.counterValueGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.counterValueGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.counterValueGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idColumn,
            this.periodColumn,
            this.valueColumn});
            this.counterValueGridView.GridControl = this.counterValueGridControl;
            this.counterValueGridView.Name = "counterValueGridView";
            this.counterValueGridView.OptionsView.ShowGroupPanel = false;
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // periodColumn
            // 
            this.periodColumn.Caption = "Период";
            this.periodColumn.ColumnEdit = this.periodRepositoryItemDateEdit;
            this.periodColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.periodColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodColumn.FieldName = "Period";
            this.periodColumn.Name = "periodColumn";
            this.periodColumn.Visible = true;
            this.periodColumn.VisibleIndex = 0;
            // 
            // periodRepositoryItemDateEdit
            // 
            this.periodRepositoryItemDateEdit.AutoHeight = false;
            this.periodRepositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodRepositoryItemDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.periodRepositoryItemDateEdit.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodRepositoryItemDateEdit.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodRepositoryItemDateEdit.DisplayFormat.FormatString = "MM.yyyy";
            this.periodRepositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodRepositoryItemDateEdit.EditFormat.FormatString = "MM.yyyy";
            this.periodRepositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodRepositoryItemDateEdit.Mask.EditMask = "MM.yyyy";
            this.periodRepositoryItemDateEdit.Name = "periodRepositoryItemDateEdit";
            // 
            // valueColumn
            // 
            this.valueColumn.Caption = "Показания";
            this.valueColumn.ColumnEdit = this.valueSpinEdit;
            this.valueColumn.DisplayFormat.FormatString = "n3";
            this.valueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueColumn.FieldName = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.Visible = true;
            this.valueColumn.VisibleIndex = 1;
            // 
            // valueSpinEdit
            // 
            this.valueSpinEdit.AutoHeight = false;
            this.valueSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.valueSpinEdit.DisplayFormat.FormatString = "n3";
            this.valueSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueSpinEdit.EditFormat.FormatString = "n3";
            this.valueSpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueSpinEdit.Mask.EditMask = "n3";
            this.valueSpinEdit.Name = "valueSpinEdit";
            // 
            // CounterValueView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.counterValueGridControl);
            this.Name = "CounterValueView";
            this.Size = new System.Drawing.Size(803, 458);
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodRepositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodRepositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueSpinEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl counterValueGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterValueGridView;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn periodColumn;
        private DevExpress.XtraGrid.Columns.GridColumn valueColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit periodRepositoryItemDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit valueSpinEdit;
    }
}
