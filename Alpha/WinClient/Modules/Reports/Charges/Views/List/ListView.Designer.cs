namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Charges.Views.List
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
            this.ContractorGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountCountGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.squareGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chargeSumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rechargeSumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitSumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.totalSumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.squareLabel = new System.Windows.Forms.Label();
            this.buildingCountLabel = new System.Windows.Forms.Label();
            this.apartmentPrivatizedCountLabel = new System.Windows.Forms.Label();
            this.apartmentMunicipalCountLabel = new System.Windows.Forms.Label();
            this.apartmentTotalCountLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
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
            this.gridControlOfListView.Location = new System.Drawing.Point(0, 56);
            this.gridControlOfListView.MainView = this.gridViewOfListView;
            this.gridControlOfListView.Name = "gridControlOfListView";
            this.gridControlOfListView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlOfListView.Size = new System.Drawing.Size(871, 228);
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
            this.ContractorGridColumn,
            this.serviceGridColumn,
            this.accountCountGridColumn,
            this.squareGridColumn,
            this.chargeSumGridColumn,
            this.actsGridColumn,
            this.rechargeSumGridColumn,
            this.benefitSumGridColumn,
            this.totalSumGridColumn});
            this.gridViewOfListView.GridControl = this.gridControlOfListView;
            this.gridViewOfListView.GroupCount = 1;
            this.gridViewOfListView.GroupFormat = "{1}";
            this.gridViewOfListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ChargeSum", this.chargeSumGridColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Acts", this.actsGridColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RechargeSum", this.rechargeSumGridColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BenefitSum", this.benefitSumGridColumn, "{0:0.00}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalSum", this.totalSumGridColumn, "{0:0.00}")});
            this.gridViewOfListView.Name = "gridViewOfListView";
            this.gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewOfListView.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewOfListView.OptionsBehavior.Editable = false;
            this.gridViewOfListView.OptionsMenu.EnableFooterMenu = false;
            this.gridViewOfListView.OptionsView.ColumnAutoWidth = false;
            this.gridViewOfListView.OptionsView.RowAutoHeight = true;
            this.gridViewOfListView.OptionsView.ShowGroupPanel = false;
            this.gridViewOfListView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.ContractorGridColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.serviceGridColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // ContractorGridColumn
            // 
            this.ContractorGridColumn.Caption = "Подрядчик";
            this.ContractorGridColumn.FieldName = "Contractor";
            this.ContractorGridColumn.FieldNameSortGroup = "Number";
            this.ContractorGridColumn.Name = "ContractorGridColumn";
            this.ContractorGridColumn.Visible = true;
            this.ContractorGridColumn.VisibleIndex = 8;
            // 
            // serviceGridColumn
            // 
            this.serviceGridColumn.Caption = "Услуга";
            this.serviceGridColumn.FieldName = "Service";
            this.serviceGridColumn.Name = "serviceGridColumn";
            this.serviceGridColumn.OptionsColumn.AllowEdit = false;
            this.serviceGridColumn.Visible = true;
            this.serviceGridColumn.VisibleIndex = 0;
            // 
            // accountCountGridColumn
            // 
            this.accountCountGridColumn.Caption = "Количество счетов";
            this.accountCountGridColumn.FieldName = "AccountCount";
            this.accountCountGridColumn.Name = "accountCountGridColumn";
            this.accountCountGridColumn.OptionsColumn.AllowEdit = false;
            this.accountCountGridColumn.Visible = true;
            this.accountCountGridColumn.VisibleIndex = 1;
            // 
            // squareGridColumn
            // 
            this.squareGridColumn.Caption = "Площадь";
            this.squareGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.squareGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.squareGridColumn.FieldName = "Square";
            this.squareGridColumn.Name = "squareGridColumn";
            this.squareGridColumn.OptionsColumn.AllowEdit = false;
            this.squareGridColumn.Visible = true;
            this.squareGridColumn.VisibleIndex = 2;
            // 
            // chargeSumGridColumn
            // 
            this.chargeSumGridColumn.Caption = "Начислено";
            this.chargeSumGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.chargeSumGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.chargeSumGridColumn.FieldName = "ChargeSum";
            this.chargeSumGridColumn.Name = "chargeSumGridColumn";
            this.chargeSumGridColumn.OptionsColumn.AllowEdit = false;
            this.chargeSumGridColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Charges", "{0:0.00}")});
            this.chargeSumGridColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.chargeSumGridColumn.Visible = true;
            this.chargeSumGridColumn.VisibleIndex = 3;
            // 
            // actsGridColumn
            // 
            this.actsGridColumn.Caption = "Акты";
            this.actsGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.actsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.actsGridColumn.FieldName = "Acts";
            this.actsGridColumn.Name = "actsGridColumn";
            this.actsGridColumn.Visible = true;
            this.actsGridColumn.VisibleIndex = 4;
            // 
            // rechargeSumGridColumn
            // 
            this.rechargeSumGridColumn.Caption = "Перерасчеты";
            this.rechargeSumGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.rechargeSumGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rechargeSumGridColumn.FieldName = "RechargeSum";
            this.rechargeSumGridColumn.Name = "rechargeSumGridColumn";
            this.rechargeSumGridColumn.Visible = true;
            this.rechargeSumGridColumn.VisibleIndex = 5;
            // 
            // benefitSumGridColumn
            // 
            this.benefitSumGridColumn.Caption = "Льготы";
            this.benefitSumGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.benefitSumGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.benefitSumGridColumn.FieldName = "BenefitSum";
            this.benefitSumGridColumn.Name = "benefitSumGridColumn";
            this.benefitSumGridColumn.Visible = true;
            this.benefitSumGridColumn.VisibleIndex = 6;
            // 
            // totalSumGridColumn
            // 
            this.totalSumGridColumn.Caption = "К оплате";
            this.totalSumGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.totalSumGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.totalSumGridColumn.FieldName = "TotalSum";
            this.totalSumGridColumn.Name = "totalSumGridColumn";
            this.totalSumGridColumn.Visible = true;
            this.totalSumGridColumn.VisibleIndex = 7;
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
            this.groupBox1.Controls.Add(this.periodDateEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(871, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(64, 19);
            this.periodDateEdit.Name = "periodDateEdit";
            this.periodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.periodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.periodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.periodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.periodDateEdit.Size = new System.Drawing.Size(130, 20);
            this.periodDateEdit.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.squareLabel);
            this.panel1.Controls.Add(this.buildingCountLabel);
            this.panel1.Controls.Add(this.apartmentPrivatizedCountLabel);
            this.panel1.Controls.Add(this.apartmentMunicipalCountLabel);
            this.panel1.Controls.Add(this.apartmentTotalCountLabel);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 284);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 66);
            this.panel1.TabIndex = 2;
            // 
            // squareLabel
            // 
            this.squareLabel.AutoSize = true;
            this.squareLabel.Location = new System.Drawing.Point(335, 25);
            this.squareLabel.Name = "squareLabel";
            this.squareLabel.Size = new System.Drawing.Size(13, 13);
            this.squareLabel.TabIndex = 9;
            this.squareLabel.Text = "0";
            // 
            // buildingCountLabel
            // 
            this.buildingCountLabel.AutoSize = true;
            this.buildingCountLabel.Location = new System.Drawing.Point(335, 3);
            this.buildingCountLabel.Name = "buildingCountLabel";
            this.buildingCountLabel.Size = new System.Drawing.Size(13, 13);
            this.buildingCountLabel.TabIndex = 8;
            this.buildingCountLabel.Text = "0";
            // 
            // apartmentPrivatizedCountLabel
            // 
            this.apartmentPrivatizedCountLabel.AutoSize = true;
            this.apartmentPrivatizedCountLabel.Location = new System.Drawing.Point(172, 3);
            this.apartmentPrivatizedCountLabel.Name = "apartmentPrivatizedCountLabel";
            this.apartmentPrivatizedCountLabel.Size = new System.Drawing.Size(13, 13);
            this.apartmentPrivatizedCountLabel.TabIndex = 7;
            this.apartmentPrivatizedCountLabel.Text = "0";
            // 
            // apartmentMunicipalCountLabel
            // 
            this.apartmentMunicipalCountLabel.AutoSize = true;
            this.apartmentMunicipalCountLabel.Location = new System.Drawing.Point(172, 25);
            this.apartmentMunicipalCountLabel.Name = "apartmentMunicipalCountLabel";
            this.apartmentMunicipalCountLabel.Size = new System.Drawing.Size(13, 13);
            this.apartmentMunicipalCountLabel.TabIndex = 6;
            this.apartmentMunicipalCountLabel.Text = "0";
            // 
            // apartmentTotalCountLabel
            // 
            this.apartmentTotalCountLabel.AutoSize = true;
            this.apartmentTotalCountLabel.Location = new System.Drawing.Point(172, 47);
            this.apartmentTotalCountLabel.Name = "apartmentTotalCountLabel";
            this.apartmentTotalCountLabel.Size = new System.Drawing.Size(13, 13);
            this.apartmentTotalCountLabel.TabIndex = 5;
            this.apartmentTotalCountLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Площадь:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Квартиры в собственности:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Дома:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Квартиры не в собственности:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Квартиры:";
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfListView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(871, 350);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn accountCountGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn squareGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serviceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn chargeSumGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn actsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rechargeSumGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitSumGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn totalSumGridColumn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label squareLabel;
        private System.Windows.Forms.Label buildingCountLabel;
        private System.Windows.Forms.Label apartmentPrivatizedCountLabel;
        private System.Windows.Forms.Label apartmentMunicipalCountLabel;
        private System.Windows.Forms.Label apartmentTotalCountLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.Columns.GridColumn ContractorGridColumn;
    }
}