namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Views.List
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
            this._listItems = new DevExpress.XtraGrid.GridControl();
            this._listView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountRepositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bikKppRepositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.innRepositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this._listItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._listView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountRepositoryItemTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bikKppRepositoryItemTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.innRepositoryItemTextEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // _listItems
            // 
            this._listItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listItems.Location = new System.Drawing.Point(0, 0);
            this._listItems.MainView = this._listView;
            this._listItems.Name = "_listItems";
            this._listItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bikKppRepositoryItemTextEdit,
            this.accountRepositoryItemTextEdit,
            this.innRepositoryItemTextEdit});
            this._listItems.Size = new System.Drawing.Size(626, 448);
            this._listItems.TabIndex = 0;
            this._listItems.UseEmbeddedNavigator = true;
            this._listItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this._listView});
            // 
            // _listView
            // 
            this._listView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this._listView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this._listView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this._id,
            this._name});
            this._listView.GridControl = this._listItems;
            this._listView.Name = "_listView";
            // 
            // _id
            // 
            this._id.Caption = "ID";
            this._id.FieldName = "ID";
            this._id.Name = "_id";
            // 
            // accountRepositoryItemTextEdit
            // 
            this.accountRepositoryItemTextEdit.AutoHeight = false;
            this.accountRepositoryItemTextEdit.Mask.EditMask = "\\d{20}";
            this.accountRepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.accountRepositoryItemTextEdit.Name = "accountRepositoryItemTextEdit";
            // 
            // _name
            // 
            this._name.Caption = "Наименование";
            this._name.FieldName = "Name";
            this._name.Name = "_name";
            this._name.Visible = true;
            this._name.VisibleIndex = 0;
            this._name.Width = 195;
            // 
            // bikKppRepositoryItemTextEdit
            // 
            this.bikKppRepositoryItemTextEdit.AutoHeight = false;
            this.bikKppRepositoryItemTextEdit.Mask.EditMask = "\\d{9}";
            this.bikKppRepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.bikKppRepositoryItemTextEdit.Name = "bikKppRepositoryItemTextEdit";
            // 
            // innRepositoryItemTextEdit
            // 
            this.innRepositoryItemTextEdit.AutoHeight = false;
            this.innRepositoryItemTextEdit.Mask.EditMask = "\\d{10}";
            this.innRepositoryItemTextEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.innRepositoryItemTextEdit.Name = "innRepositoryItemTextEdit";
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._listItems);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(626, 448);
            ((System.ComponentModel.ISupportInitialize)(this._listItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._listView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountRepositoryItemTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bikKppRepositoryItemTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.innRepositoryItemTextEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _listItems;
        private DevExpress.XtraGrid.Views.Grid.GridView _listView;
        private DevExpress.XtraGrid.Columns.GridColumn _name;
        private DevExpress.XtraGrid.Columns.GridColumn _id;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit accountRepositoryItemTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit bikKppRepositoryItemTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit innRepositoryItemTextEdit;
    }
}

