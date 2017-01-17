namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.ServiceTypes
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
            this._name = new DevExpress.XtraGrid.Columns.GridColumn();
            this._code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContainerTypeLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this._listItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._listView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContainerTypeLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // _listItems
            // 
            this._listItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listItems.Location = new System.Drawing.Point(0, 0);
            this._listItems.MainView = this._listView;
            this._listItems.Name = "_listItems";
            this._listItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ContainerTypeLookUpEdit});
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
            this._name,
            this._code});
            this._listView.GridControl = this._listItems;
            this._listView.Name = "_listView";
            // 
            // _id
            // 
            this._id.Caption = "ID";
            this._id.FieldName = "ID";
            this._id.Name = "_id";
            // 
            // _name
            // 
            this._name.Caption = "Название";
            this._name.FieldName = "Name";
            this._name.Name = "_name";
            this._name.Visible = true;
            this._name.VisibleIndex = 1;
            this._name.Width = 195;
            // 
            // _code
            // 
            this._code.Caption = "Шифр";
            this._code.FieldName = "Code";
            this._code.Name = "_code";
            this._code.Visible = true;
            this._code.VisibleIndex = 0;
            this._code.Width = 87;
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
            ((System.ComponentModel.ISupportInitialize)(this.ContainerTypeLookUpEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl _listItems;
        private DevExpress.XtraGrid.Views.Grid.GridView _listView;
        private DevExpress.XtraGrid.Columns.GridColumn _name;
        private DevExpress.XtraGrid.Columns.GridColumn _code;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit ContainerTypeLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn _id;
    }
}

