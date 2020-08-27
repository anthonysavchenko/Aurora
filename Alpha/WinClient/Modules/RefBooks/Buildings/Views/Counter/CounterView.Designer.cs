namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter
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
            this.CounterNumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UtilityServiceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UtilityServiceEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.CoefficientColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CoefficientEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.CheckedSinceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CheckedSinceEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.CheckedTillColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UtilityServiceEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoefficientEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedSinceEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedSinceEdit.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // counterGridControl
            // 
            this.counterGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterGridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.First.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.counterGridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.counterGridControl.EmbeddedNavigator.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] {
            new DevExpress.XtraEditors.NavigatorCustomButton()});
            this.counterGridControl.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
            this.counterGridControl.Location = new System.Drawing.Point(0, 0);
            this.counterGridControl.MainView = this.counterGridView;
            this.counterGridControl.Name = "counterGridControl";
            this.counterGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.UtilityServiceEdit,
            this.CoefficientEdit,
            this.CheckedSinceEdit});
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
            this.CounterNumberColumn,
            this.UtilityServiceColumn,
            this.CoefficientColumn,
            this.CheckedSinceColumn,
            this.CheckedTillColumn});
            this.counterGridView.GridControl = this.counterGridControl;
            this.counterGridView.Name = "counterGridView";
            this.counterGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.counterGridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.counterGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.counterGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.counterGridView.OptionsView.ShowGroupPanel = false;
            this.counterGridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.counterGridView_RowStyle);
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // CounterNumberColumn
            // 
            this.CounterNumberColumn.Caption = "Номер";
            this.CounterNumberColumn.FieldName = "CounterNumber";
            this.CounterNumberColumn.Name = "CounterNumberColumn";
            this.CounterNumberColumn.Visible = true;
            this.CounterNumberColumn.VisibleIndex = 0;
            // 
            // UtilityServiceColumn
            // 
            this.UtilityServiceColumn.Caption = "Услуга";
            this.UtilityServiceColumn.ColumnEdit = this.UtilityServiceEdit;
            this.UtilityServiceColumn.FieldName = "UtilityService";
            this.UtilityServiceColumn.Name = "UtilityServiceColumn";
            this.UtilityServiceColumn.Visible = true;
            this.UtilityServiceColumn.VisibleIndex = 1;
            // 
            // UtilityServiceEdit
            // 
            this.UtilityServiceEdit.AutoHeight = false;
            this.UtilityServiceEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.UtilityServiceEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Услуга")});
            this.UtilityServiceEdit.DisplayMember = "Name";
            this.UtilityServiceEdit.Name = "UtilityServiceEdit";
            this.UtilityServiceEdit.NullText = "";
            this.UtilityServiceEdit.ValueMember = "ID";
            // 
            // CoefficientColumn
            // 
            this.CoefficientColumn.Caption = "Коэффициент";
            this.CoefficientColumn.ColumnEdit = this.CoefficientEdit;
            this.CoefficientColumn.FieldName = "Coefficient";
            this.CoefficientColumn.Name = "CoefficientColumn";
            this.CoefficientColumn.Visible = true;
            this.CoefficientColumn.VisibleIndex = 2;
            // 
            // CoefficientEdit
            // 
            this.CoefficientEdit.AutoHeight = false;
            this.CoefficientEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CoefficientEdit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CoefficientEdit.IsFloatValue = false;
            this.CoefficientEdit.Mask.EditMask = "N00";
            this.CoefficientEdit.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.CoefficientEdit.Name = "CoefficientEdit";
            // 
            // CheckedSinceColumn
            // 
            this.CheckedSinceColumn.Caption = "Поверка пройдена";
            this.CheckedSinceColumn.ColumnEdit = this.CheckedSinceEdit;
            this.CheckedSinceColumn.FieldName = "CheckedSince";
            this.CheckedSinceColumn.Name = "CheckedSinceColumn";
            this.CheckedSinceColumn.Visible = true;
            this.CheckedSinceColumn.VisibleIndex = 3;
            // 
            // CheckedSinceEdit
            // 
            this.CheckedSinceEdit.AutoHeight = false;
            this.CheckedSinceEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckedSinceEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckedSinceEdit.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.CheckedSinceEdit.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.CheckedSinceEdit.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.CheckedSinceEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.CheckedSinceEdit.EditFormat.FormatString = "dd.MM.yyyy";
            this.CheckedSinceEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.CheckedSinceEdit.Name = "CheckedSinceEdit";
            // 
            // CheckedTillColumn
            // 
            this.CheckedTillColumn.Caption = "Срок поверки истекает";
            this.CheckedTillColumn.ColumnEdit = this.CheckedSinceEdit;
            this.CheckedTillColumn.FieldName = "CheckedTill";
            this.CheckedTillColumn.Name = "CheckedTillColumn";
            this.CheckedTillColumn.Visible = true;
            this.CheckedTillColumn.VisibleIndex = 4;
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
            ((System.ComponentModel.ISupportInitialize)(this.UtilityServiceEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoefficientEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedSinceEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedSinceEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl counterGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterGridView;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CoefficientColumn;
        private DevExpress.XtraGrid.Columns.GridColumn CounterNumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn UtilityServiceColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit UtilityServiceEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit CoefficientEdit;
        private DevExpress.XtraGrid.Columns.GridColumn CheckedSinceColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit CheckedSinceEdit;
        private DevExpress.XtraGrid.Columns.GridColumn CheckedTillColumn;
    }
}
