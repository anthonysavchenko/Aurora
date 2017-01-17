namespace Taumis.Infrastructure.Layout
{
    partial class ShellLayoutView
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
            this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this._fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this._mainMenuStrip.SuspendLayout();
            this._mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainMenuStrip
            // 
            this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileToolStripMenuItem});
            this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._mainMenuStrip.Name = "_mainMenuStrip";
            this._mainMenuStrip.Size = new System.Drawing.Size(640, 24);
            this._mainMenuStrip.TabIndex = 4;
            this._mainMenuStrip.Text = "_mainMenuStrip";
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newToolStripMenuItem,
            this._saveToolStripMenuItem,
            this._deleteToolStripMenuItem,
            this._refreshListToolStripMenuItem,
            this._printToolStripMenuItem,
            this._exportToExcelToolStripMenuItem,
            this._aboutToolStripMenuItem,
            this._exitToolStripMenuItem});
            this._fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            this._fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this._fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this._fileToolStripMenuItem.Text = "Файл";
            // 
            // _newToolStripMenuItem
            // 
            this._newToolStripMenuItem.Name = "_newToolStripMenuItem";
            this._newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._newToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._newToolStripMenuItem.Text = "Создать";
            // 
            // _saveToolStripMenuItem
            // 
            this._saveToolStripMenuItem.Name = "_saveToolStripMenuItem";
            this._saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._saveToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._saveToolStripMenuItem.Text = "Сохранить";
            // 
            // _deleteToolStripMenuItem
            // 
            this._deleteToolStripMenuItem.Name = "_deleteToolStripMenuItem";
            this._deleteToolStripMenuItem.ShortcutKeyDisplayString = "Delete";
            this._deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this._deleteToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._deleteToolStripMenuItem.Text = "Корректировать";
            // 
            // _refreshListToolStripMenuItem
            // 
            this._refreshListToolStripMenuItem.Name = "_refreshListToolStripMenuItem";
            this._refreshListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this._refreshListToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._refreshListToolStripMenuItem.Text = "Обновить";
            // 
            // _printToolStripMenuItem
            // 
            this._printToolStripMenuItem.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this._printToolStripMenuItem.Name = "_printToolStripMenuItem";
            this._printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this._printToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._printToolStripMenuItem.Text = "Печатать";
            // 
            // _exportToExcelToolStripMenuItem
            // 
            this._exportToExcelToolStripMenuItem.Name = "_exportToExcelToolStripMenuItem";
            this._exportToExcelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this._exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._exportToExcelToolStripMenuItem.Text = "Экспортировать";
            // 
            // _aboutToolStripMenuItem
            // 
            this._aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            this._aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this._aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._aboutToolStripMenuItem.Text = "О системе";
            this._aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClick);
            // 
            // _exitToolStripMenuItem
            // 
            this._exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            this._exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this._exitToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this._exitToolStripMenuItem.Text = "Закрыть";
            this._exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
            // 
            // _mainToolStrip
            // 
            this._mainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this._mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnDelete,
            this.btnRefresh,
            this.btnPrint,
            this.btnExportToExcel,
            this.toolStripSeparator9});
            this._mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this._mainToolStrip.Name = "_mainToolStrip";
            this._mainToolStrip.Size = new System.Drawing.Size(640, 54);
            this._mainToolStrip.TabIndex = 5;
            this._mainToolStrip.Text = "_mainToolStrip";
            // 
            // btnNew
            // 
            this.btnNew.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.New_32x32;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(54, 51);
            this.btnNew.Text = "Создать";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.Save_32x32;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 51);
            this.btnSave.Text = "Сохранить";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.Delete_32x32;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 51);
            this.btnDelete.Text = "Корректировать";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.Refresh_32x32;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(65, 51);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.Print_32x32;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 51);
            this.btnPrint.Text = "Печатать";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.ExportToXLS_32x32;
            this.btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(100, 51);
            this.btnExportToExcel.Text = "Экспортировать";
            this.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToExcel.ToolTipText = "Экспортировать";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 54);
            // 
            // ShellLayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this._mainToolStrip);
            this.Controls.Add(this._mainMenuStrip);
            this.Name = "ShellLayoutView";
            this.Size = new System.Drawing.Size(640, 80);
            this._mainMenuStrip.ResumeLayout(false);
            this._mainMenuStrip.PerformLayout();
            this._mainToolStrip.ResumeLayout(false);
            this._mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _exitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip _mainToolStrip;
		private System.Windows.Forms.ToolStripButton btnNew;
		private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.ToolStripMenuItem _newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem _exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _refreshListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _printToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripMenuItem _aboutToolStripMenuItem;
    }
}
