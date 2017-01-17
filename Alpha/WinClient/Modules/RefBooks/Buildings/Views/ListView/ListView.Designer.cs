namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.List
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
            this._id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.street = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buildingNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.zipCode = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this._id,
            this.street,
            this.buildingNumber,
            this.zipCode});
            this._gridView.GridControl = this._gridControl;
            this._gridView.Name = "_gridView";
            this._gridView.OptionsBehavior.Editable = false;
            // 
            // _id
            // 
            this._id.Caption = "ID";
            this._id.FieldName = "ID";
            this._id.Name = "_id";
            // 
            // street
            // 
            this.street.Caption = "Улица";
            this.street.FieldName = "Street";
            this.street.Name = "street";
            this.street.Visible = true;
            this.street.VisibleIndex = 0;
            this.street.Width = 195;
            // 
            // buildingNumber
            // 
            this.buildingNumber.Caption = "Номер";
            this.buildingNumber.FieldName = "BuildingNumber";
            this.buildingNumber.Name = "buildingNumber";
            this.buildingNumber.Visible = true;
            this.buildingNumber.VisibleIndex = 1;
            this.buildingNumber.Width = 87;
            // 
            // zipCode
            // 
            this.zipCode.Caption = "Почтовый индекс";
            this.zipCode.FieldName = "ZipCode";
            this.zipCode.Name = "zipCode";
            this.zipCode.Visible = true;
            this.zipCode.VisibleIndex = 2;
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
        private DevExpress.XtraGrid.Columns.GridColumn street;
        private DevExpress.XtraGrid.Columns.GridColumn buildingNumber;
        private DevExpress.XtraGrid.Columns.GridColumn _id;
        private DevExpress.XtraGrid.Columns.GridColumn zipCode;
    }
}

