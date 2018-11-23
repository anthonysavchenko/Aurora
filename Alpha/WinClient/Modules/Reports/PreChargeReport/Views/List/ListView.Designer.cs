namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Views.List
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
            this.gridControlOfListView = new DevExpress.XtraGrid.GridControl();
            this.gridViewOfListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.streetGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buildingGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.apartmentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.normChargeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.counterChargeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.diffGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.houseLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.buildingLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.streetLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlOfListView
            // 
            this.gridControlOfListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gridControlOfListView.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControlOfListView.Location = new System.Drawing.Point(0, 52);
            this.gridControlOfListView.MainView = this.gridViewOfListView;
            this.gridControlOfListView.Name = "gridControlOfListView";
            this.gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlOfListView.Size = new System.Drawing.Size(750, 298);
            this.gridControlOfListView.TabIndex = 0;
            this.gridControlOfListView.UseEmbeddedNavigator = true;
            this.gridControlOfListView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOfListView});
            // 
            // gridViewOfListView
            // 
            this.gridViewOfListView.Appearance.GroupRow.Options.UseTextOptions = true;
            this.gridViewOfListView.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewOfListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewOfListView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewOfListView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridViewOfListView.ColumnPanelRowHeight = 34;
            this.gridViewOfListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.streetGridColumn,
            this.buildingGridColumn,
            this.apartmentGridColumn,
            this.accountGridColumn,
            this.normChargeGridColumn,
            this.counterChargeGridColumn,
            this.diffGridColumn});
            this.gridViewOfListView.GridControl = this.gridControlOfListView;
            this.gridViewOfListView.GroupCount = 2;
            this.gridViewOfListView.Name = "gridViewOfListView";
            this.gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewOfListView.OptionsBehavior.Editable = false;
            this.gridViewOfListView.OptionsView.ColumnAutoWidth = false;
            this.gridViewOfListView.OptionsView.RowAutoHeight = true;
            this.gridViewOfListView.OptionsView.ShowFooter = true;
            this.gridViewOfListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.streetGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.buildingGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // streetGridColumn
            // 
            this.streetGridColumn.Caption = "Улица";
            this.streetGridColumn.FieldName = "Street";
            this.streetGridColumn.Name = "streetGridColumn";
            this.streetGridColumn.Visible = true;
            this.streetGridColumn.VisibleIndex = 0;
            // 
            // buildingGridColumn
            // 
            this.buildingGridColumn.Caption = "Дом";
            this.buildingGridColumn.FieldName = "Building";
            this.buildingGridColumn.Name = "buildingGridColumn";
            this.buildingGridColumn.Visible = true;
            this.buildingGridColumn.VisibleIndex = 0;
            // 
            // apartmentGridColumn
            // 
            this.apartmentGridColumn.Caption = "Квартира";
            this.apartmentGridColumn.FieldName = "Apartment";
            this.apartmentGridColumn.Name = "apartmentGridColumn";
            this.apartmentGridColumn.Visible = true;
            this.apartmentGridColumn.VisibleIndex = 0;
            // 
            // accountGridColumn
            // 
            this.accountGridColumn.Caption = "Лицевой счет";
            this.accountGridColumn.FieldName = "Account";
            this.accountGridColumn.Name = "accountGridColumn";
            this.accountGridColumn.Visible = true;
            this.accountGridColumn.VisibleIndex = 1;
            // 
            // normChargeGridColumn
            // 
            this.normChargeGridColumn.Caption = "По норме";
            this.normChargeGridColumn.FieldName = "NormCharge";
            this.normChargeGridColumn.Name = "normChargeGridColumn";
            this.normChargeGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.normChargeGridColumn.Visible = true;
            this.normChargeGridColumn.VisibleIndex = 2;
            // 
            // counterChargeGridColumn
            // 
            this.counterChargeGridColumn.Caption = "По прибору учета";
            this.counterChargeGridColumn.FieldName = "CounterCharge";
            this.counterChargeGridColumn.Name = "counterChargeGridColumn";
            this.counterChargeGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.counterChargeGridColumn.Visible = true;
            this.counterChargeGridColumn.VisibleIndex = 3;
            // 
            // diffGridColumn
            // 
            this.diffGridColumn.Caption = "Разница";
            this.diffGridColumn.FieldName = "Diff";
            this.diffGridColumn.Name = "diffGridColumn";
            this.diffGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.diffGridColumn.Visible = true;
            this.diffGridColumn.VisibleIndex = 4;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.DisplayValueChecked = "1";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "0";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.houseLabel);
            this.groupBox1.Controls.Add(this.streetLabel);
            this.groupBox1.Controls.Add(this.buildingLookUpEdit);
            this.groupBox1.Controls.Add(this.streetLookUpEdit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // houseLabel
            // 
            this.houseLabel.AutoSize = true;
            this.houseLabel.Location = new System.Drawing.Point(259, 22);
            this.houseLabel.Name = "houseLabel";
            this.houseLabel.Size = new System.Drawing.Size(33, 13);
            this.houseLabel.TabIndex = 40;
            this.houseLabel.Text = "Дом:";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(4, 22);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(42, 13);
            this.streetLabel.TabIndex = 39;
            this.streetLabel.Text = "Улица:";
            // 
            // buildingLookUpEdit
            // 
            this.buildingLookUpEdit.Location = new System.Drawing.Point(298, 19);
            this.buildingLookUpEdit.Name = "buildingLookUpEdit";
            this.buildingLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.buildingLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер дома")});
            this.buildingLookUpEdit.Properties.DisplayMember = "Number";
            this.buildingLookUpEdit.Properties.NullText = "(все)";
            this.buildingLookUpEdit.Properties.ValueMember = "ID";
            this.buildingLookUpEdit.Size = new System.Drawing.Size(86, 20);
            this.buildingLookUpEdit.TabIndex = 37;
            this.buildingLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // streetLookUpEdit
            // 
            this.streetLookUpEdit.Location = new System.Drawing.Point(52, 19);
            this.streetLookUpEdit.Name = "streetLookUpEdit";
            this.streetLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.streetLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.streetLookUpEdit.Properties.DisplayMember = "Name";
            this.streetLookUpEdit.Properties.NullText = "(все)";
            this.streetLookUpEdit.Properties.ValueMember = "ID";
            this.streetLookUpEdit.Size = new System.Drawing.Size(201, 20);
            this.streetLookUpEdit.TabIndex = 38;
            this.streetLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            this.streetLookUpEdit.EditValueChanged += new System.EventHandler(this.streetLookUpEdit_EditValueChanged);
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfListView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(750, 350);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.Columns.GridColumn streetGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn buildingGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn apartmentGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn accountGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn normChargeGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn counterChargeGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn diffGridColumn;
        private System.Windows.Forms.Label houseLabel;
        private System.Windows.Forms.Label streetLabel;
        private DevExpress.XtraEditors.LookUpEdit buildingLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit streetLookUpEdit;
    }
}