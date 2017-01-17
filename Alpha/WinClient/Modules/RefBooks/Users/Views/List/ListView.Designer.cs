namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.List
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
            this._gridControl = new DevExpress.XtraGrid.GridControl();
            this._gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.loginColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.akaColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this._gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridControl
            // 
            this._gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridControl.Location = new System.Drawing.Point(0, 0);
            this._gridControl.MainView = this._gridView;
            this._gridControl.Name = "_gridControl";
            this._gridControl.Size = new System.Drawing.Size(626, 448);
            this._gridControl.TabIndex = 0;
            this._gridControl.UseEmbeddedNavigator = true;
            this._gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._gridView});
            // 
            // _gridView
            // 
            this._gridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this._gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idColumn,
            this.akaColumn,
            this.loginColumn});
            this._gridView.GridControl = this._gridControl;
            this._gridView.Name = "_gridView";
            this._gridView.OptionsBehavior.Editable = false;
            // 
            // idColumn
            // 
            this.idColumn.Caption = "ID";
            this.idColumn.FieldName = "ID";
            this.idColumn.Name = "idColumn";
            // 
            // loginColumn
            // 
            this.loginColumn.Caption = "Логин";
            this.loginColumn.FieldName = "Login";
            this.loginColumn.Name = "loginColumn";
            this.loginColumn.Visible = true;
            this.loginColumn.VisibleIndex = 1;
            // 
            // akaColumn
            // 
            this.akaColumn.Caption = "ФИО";
            this.akaColumn.FieldName = "Aka";
            this.akaColumn.Name = "akaColumn";
            this.akaColumn.Visible = true;
            this.akaColumn.VisibleIndex = 0;
            this.akaColumn.Width = 195;
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._gridControl);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(626, 448);
            ((System.ComponentModel.ISupportInitialize)(this._gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridView;
        private DevExpress.XtraGrid.Columns.GridColumn loginColumn;
        private DevExpress.XtraGrid.Columns.GridColumn akaColumn;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
    }
}

