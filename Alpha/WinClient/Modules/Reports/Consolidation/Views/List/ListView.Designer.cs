namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Consolidation.Views.List
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
            this.GridControlOfListView = new DevExpress.XtraGrid.GridControl();
            this.GridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ToolTipController = new DevExpress.Utils.ToolTipController();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ShowArchivedCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SinceDateEdit = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SinceDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // GridControlOfListView
            // 
            this.GridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.First.Visible = false;
            this.GridControlOfListView.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.GridControlOfListView.Location = new System.Drawing.Point(0, 52);
            this.GridControlOfListView.MainView = this.GridViewOfListView;
            this.GridControlOfListView.Name = "GridControlOfListView";
            this.GridControlOfListView.Size = new System.Drawing.Size(750, 298);
            this.GridControlOfListView.TabIndex = 0;
            this.GridControlOfListView.ToolTipController = this.ToolTipController;
            this.GridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewOfListView});
            // 
            // GridViewOfListView
            // 
            this.GridViewOfListView.Appearance.GroupRow.Options.UseTextOptions = true;
            this.GridViewOfListView.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewOfListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewOfListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewOfListView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewOfListView.GridControl = this.GridControlOfListView;
            this.GridViewOfListView.Name = "GridViewOfListView";
            this.GridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.GridViewOfListView.OptionsBehavior.Editable = false;
            this.GridViewOfListView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewOfListView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GridViewOfListView.OptionsSelection.MultiSelect = true;
            this.GridViewOfListView.OptionsView.ShowGroupPanel = false;
            this.GridViewOfListView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GridViewOfListView_CustomDrawCell);
            this.GridViewOfListView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewOfListView_RowCellStyle);
            this.GridViewOfListView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.GridViewOfListView_CustomColumnDisplayText);
            // 
            // ToolTipController
            // 
            this.ToolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.ToolTipController_GetActiveObjectInfo);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ShowArchivedCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SinceDateEdit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры отчета";
            // 
            // ShowArchivedCheckBox
            // 
            this.ShowArchivedCheckBox.AutoSize = true;
            this.ShowArchivedCheckBox.Location = new System.Drawing.Point(195, 21);
            this.ShowArchivedCheckBox.Name = "ShowArchivedCheckBox";
            this.ShowArchivedCheckBox.Size = new System.Drawing.Size(170, 17);
            this.ShowArchivedCheckBox.TabIndex = 41;
            this.ShowArchivedCheckBox.Text = "Показывать архивные дома";
            this.ShowArchivedCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Период с:";
            // 
            // SinceDateEdit
            // 
            this.SinceDateEdit.EditValue = null;
            this.SinceDateEdit.Location = new System.Drawing.Point(69, 19);
            this.SinceDateEdit.Name = "SinceDateEdit";
            this.SinceDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SinceDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.SinceDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.SinceDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.SinceDateEdit.Properties.DisplayFormat.FormatString = "g";
            this.SinceDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceDateEdit.Properties.EditFormat.FormatString = "g";
            this.SinceDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.SinceDateEdit.Properties.Mask.EditMask = "g";
            this.SinceDateEdit.Size = new System.Drawing.Size(120, 20);
            this.SinceDateEdit.TabIndex = 37;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControlOfListView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(750, 350);
            ((System.ComponentModel.ISupportInitialize)(this.GridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SinceDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SinceDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl GridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView GridViewOfListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit SinceDateEdit;
        private System.Windows.Forms.CheckBox ShowArchivedCheckBox;
        private DevExpress.Utils.ToolTipController ToolTipController;
    }
}