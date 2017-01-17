namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Benefits.Views.List
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
            this.dateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.numberGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.surnameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.patronymicGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.streetGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buildingNumberGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.apartmentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.floorGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.liftPresenceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rubbishChutePresenceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.squareGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.categoryGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitTypeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.residentDocumentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentNumberGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentIssueDateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.documentValidityPeriodGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.socialNormsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actualIntakeGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chargesGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.recalculationsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.payableGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitSumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.onlyFederalBenefitsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
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
            this.dateGridColumn,
            this.numberGridColumn,
            this.surnameGridColumn,
            this.nameGridColumn,
            this.patronymicGridColumn,
            this.streetGridColumn,
            this.buildingNumberGridColumn,
            this.apartmentGridColumn,
            this.floorGridColumn,
            this.liftPresenceGridColumn,
            this.rubbishChutePresenceGridColumn,
            this.squareGridColumn,
            this.categoryGridColumn,
            this.benefitTypeGridColumn,
            this.residentDocumentGridColumn,
            this.documentNumberGridColumn,
            this.documentIssueDateGridColumn,
            this.documentValidityPeriodGridColumn,
            this.socialNormsGridColumn,
            this.actualIntakeGridColumn,
            this.rateGridColumn,
            this.chargesGridColumn,
            this.actsGridColumn,
            this.recalculationsGridColumn,
            this.payableGridColumn,
            this.benefitSumGridColumn});
            this.gridViewOfListView.GridControl = this.gridControlOfListView;
            this.gridViewOfListView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Charges", null, "(Платежи: {0})", "")});
            this.gridViewOfListView.Name = "gridViewOfListView";
            this.gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewOfListView.OptionsBehavior.Editable = false;
            this.gridViewOfListView.OptionsView.ColumnAutoWidth = false;
            this.gridViewOfListView.OptionsView.RowAutoHeight = true;
            this.gridViewOfListView.OptionsView.ShowFooter = true;
            // 
            // dateGridColumn
            // 
            this.dateGridColumn.Caption = "Дата";
            this.dateGridColumn.DisplayFormat.FormatString = "{0:dd.MM.yyyy}";
            this.dateGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateGridColumn.FieldName = "Date";
            this.dateGridColumn.Name = "dateGridColumn";
            this.dateGridColumn.Visible = true;
            this.dateGridColumn.VisibleIndex = 0;
            // 
            // numberGridColumn
            // 
            this.numberGridColumn.Caption = "Порядковый номер";
            this.numberGridColumn.FieldName = "Number";
            this.numberGridColumn.Name = "numberGridColumn";
            this.numberGridColumn.Visible = true;
            this.numberGridColumn.VisibleIndex = 1;
            // 
            // surnameGridColumn
            // 
            this.surnameGridColumn.Caption = "Фамилия";
            this.surnameGridColumn.FieldName = "Surname";
            this.surnameGridColumn.Name = "surnameGridColumn";
            this.surnameGridColumn.Visible = true;
            this.surnameGridColumn.VisibleIndex = 2;
            // 
            // nameGridColumn
            // 
            this.nameGridColumn.Caption = "Имя";
            this.nameGridColumn.FieldName = "FirstName";
            this.nameGridColumn.Name = "nameGridColumn";
            this.nameGridColumn.Visible = true;
            this.nameGridColumn.VisibleIndex = 3;
            // 
            // patronymicGridColumn
            // 
            this.patronymicGridColumn.Caption = "Отчество";
            this.patronymicGridColumn.FieldName = "Patronymic";
            this.patronymicGridColumn.Name = "patronymicGridColumn";
            this.patronymicGridColumn.Visible = true;
            this.patronymicGridColumn.VisibleIndex = 4;
            // 
            // streetGridColumn
            // 
            this.streetGridColumn.Caption = "Улица";
            this.streetGridColumn.FieldName = "Street";
            this.streetGridColumn.Name = "streetGridColumn";
            this.streetGridColumn.Visible = true;
            this.streetGridColumn.VisibleIndex = 5;
            // 
            // buildingNumberGridColumn
            // 
            this.buildingNumberGridColumn.Caption = "Дом";
            this.buildingNumberGridColumn.FieldName = "BuildingNumber";
            this.buildingNumberGridColumn.Name = "buildingNumberGridColumn";
            this.buildingNumberGridColumn.Visible = true;
            this.buildingNumberGridColumn.VisibleIndex = 6;
            // 
            // apartmentGridColumn
            // 
            this.apartmentGridColumn.Caption = "Квартира";
            this.apartmentGridColumn.FieldName = "Apartment";
            this.apartmentGridColumn.Name = "apartmentGridColumn";
            this.apartmentGridColumn.Visible = true;
            this.apartmentGridColumn.VisibleIndex = 7;
            // 
            // floorGridColumn
            // 
            this.floorGridColumn.Caption = "Этаж проживания";
            this.floorGridColumn.FieldName = "Floor";
            this.floorGridColumn.Name = "floorGridColumn";
            this.floorGridColumn.Visible = true;
            this.floorGridColumn.VisibleIndex = 8;
            // 
            // liftPresenceGridColumn
            // 
            this.liftPresenceGridColumn.Caption = "Наличие лифта";
            this.liftPresenceGridColumn.FieldName = "LiftPresence";
            this.liftPresenceGridColumn.Name = "liftPresenceGridColumn";
            this.liftPresenceGridColumn.Visible = true;
            this.liftPresenceGridColumn.VisibleIndex = 9;
            // 
            // rubbishChutePresenceGridColumn
            // 
            this.rubbishChutePresenceGridColumn.Caption = "Наличие мусоропровода";
            this.rubbishChutePresenceGridColumn.FieldName = "RubbishChutePresence";
            this.rubbishChutePresenceGridColumn.Name = "rubbishChutePresenceGridColumn";
            this.rubbishChutePresenceGridColumn.Visible = true;
            this.rubbishChutePresenceGridColumn.VisibleIndex = 10;
            // 
            // squareGridColumn
            // 
            this.squareGridColumn.Caption = "Размер занимаемой площади";
            this.squareGridColumn.FieldName = "Square";
            this.squareGridColumn.Name = "squareGridColumn";
            this.squareGridColumn.Visible = true;
            this.squareGridColumn.VisibleIndex = 11;
            // 
            // categoryGridColumn
            // 
            this.categoryGridColumn.Caption = "Категория";
            this.categoryGridColumn.FieldName = "Category";
            this.categoryGridColumn.Name = "categoryGridColumn";
            this.categoryGridColumn.Visible = true;
            this.categoryGridColumn.VisibleIndex = 12;
            // 
            // benefitTypeGridColumn
            // 
            this.benefitTypeGridColumn.Caption = "Название льготы";
            this.benefitTypeGridColumn.FieldName = "BenefitType";
            this.benefitTypeGridColumn.Name = "benefitTypeGridColumn";
            this.benefitTypeGridColumn.Visible = true;
            this.benefitTypeGridColumn.VisibleIndex = 13;
            // 
            // residentDocumentGridColumn
            // 
            this.residentDocumentGridColumn.Caption = "Документ";
            this.residentDocumentGridColumn.FieldName = "ResidentDocument";
            this.residentDocumentGridColumn.Name = "residentDocumentGridColumn";
            this.residentDocumentGridColumn.Visible = true;
            this.residentDocumentGridColumn.VisibleIndex = 14;
            // 
            // documentNumberGridColumn
            // 
            this.documentNumberGridColumn.Caption = "Номер документа";
            this.documentNumberGridColumn.FieldName = "DocumentNumber";
            this.documentNumberGridColumn.Name = "documentNumberGridColumn";
            this.documentNumberGridColumn.Visible = true;
            this.documentNumberGridColumn.VisibleIndex = 15;
            // 
            // documentIssueDateGridColumn
            // 
            this.documentIssueDateGridColumn.Caption = "Дата выдачи";
            this.documentIssueDateGridColumn.FieldName = "DocumentIssueDate";
            this.documentIssueDateGridColumn.Name = "documentIssueDateGridColumn";
            this.documentIssueDateGridColumn.Visible = true;
            this.documentIssueDateGridColumn.VisibleIndex = 16;
            // 
            // documentValidityPeriodGridColumn
            // 
            this.documentValidityPeriodGridColumn.Caption = "Срок действия документа";
            this.documentValidityPeriodGridColumn.FieldName = "DocumentValidityPeriod";
            this.documentValidityPeriodGridColumn.Name = "documentValidityPeriodGridColumn";
            this.documentValidityPeriodGridColumn.Visible = true;
            this.documentValidityPeriodGridColumn.VisibleIndex = 17;
            // 
            // socialNormsGridColumn
            // 
            this.socialNormsGridColumn.Caption = "Социальные нормы";
            this.socialNormsGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.socialNormsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.socialNormsGridColumn.FieldName = "SocialNorms";
            this.socialNormsGridColumn.Name = "socialNormsGridColumn";
            this.socialNormsGridColumn.Visible = true;
            this.socialNormsGridColumn.VisibleIndex = 18;
            // 
            // actualIntakeGridColumn
            // 
            this.actualIntakeGridColumn.Caption = "Фактическое потребление";
            this.actualIntakeGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.actualIntakeGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.actualIntakeGridColumn.FieldName = "ActualIntake";
            this.actualIntakeGridColumn.Name = "actualIntakeGridColumn";
            this.actualIntakeGridColumn.Visible = true;
            this.actualIntakeGridColumn.VisibleIndex = 19;
            // 
            // rateGridColumn
            // 
            this.rateGridColumn.Caption = "Тариф";
            this.rateGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.rateGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rateGridColumn.FieldName = "Rate";
            this.rateGridColumn.Name = "rateGridColumn";
            this.rateGridColumn.Visible = true;
            this.rateGridColumn.VisibleIndex = 20;
            // 
            // chargesGridColumn
            // 
            this.chargesGridColumn.Caption = "Полные начисления";
            this.chargesGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.chargesGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.chargesGridColumn.FieldName = "Charges";
            this.chargesGridColumn.Name = "chargesGridColumn";
            this.chargesGridColumn.Visible = true;
            this.chargesGridColumn.VisibleIndex = 21;
            // 
            // actsGridColumn
            // 
            this.actsGridColumn.Caption = "Акты";
            this.actsGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.actsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.actsGridColumn.FieldName = "Acts";
            this.actsGridColumn.Name = "actsGridColumn";
            this.actsGridColumn.Visible = true;
            this.actsGridColumn.VisibleIndex = 22;
            // 
            // recalculationsGridColumn
            // 
            this.recalculationsGridColumn.Caption = "Перерачеты";
            this.recalculationsGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.recalculationsGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.recalculationsGridColumn.FieldName = "Recalculations";
            this.recalculationsGridColumn.Name = "recalculationsGridColumn";
            this.recalculationsGridColumn.Visible = true;
            this.recalculationsGridColumn.VisibleIndex = 23;
            // 
            // payableGridColumn
            // 
            this.payableGridColumn.Caption = "Выстовлено к оплате";
            this.payableGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.payableGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.payableGridColumn.FieldName = "Payable";
            this.payableGridColumn.Name = "payableGridColumn";
            this.payableGridColumn.Visible = true;
            this.payableGridColumn.VisibleIndex = 24;
            // 
            // benefitSumGridColumn
            // 
            this.benefitSumGridColumn.Caption = "Сумма льготы";
            this.benefitSumGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.benefitSumGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.benefitSumGridColumn.FieldName = "BenefitSum";
            this.benefitSumGridColumn.Name = "benefitSumGridColumn";
            this.benefitSumGridColumn.Visible = true;
            this.benefitSumGridColumn.VisibleIndex = 25;
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
            this.groupBox1.Controls.Add(this.onlyFederalBenefitsCheckBox);
            this.groupBox1.Controls.Add(this.periodDateEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(57, 22);
            this.periodDateEdit.Name = "periodDateEdit";
            this.periodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodDateEdit.Properties.DisplayFormat.FormatString = "MM.yyyy";
            this.periodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.periodDateEdit.Size = new System.Drawing.Size(150, 20);
            this.periodDateEdit.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Период";
            // 
            // onlyFederalBenefitsCheckBox
            // 
            this.onlyFederalBenefitsCheckBox.AutoSize = true;
            this.onlyFederalBenefitsCheckBox.Checked = true;
            this.onlyFederalBenefitsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyFederalBenefitsCheckBox.Location = new System.Drawing.Point(232, 25);
            this.onlyFederalBenefitsCheckBox.Name = "onlyFederalBenefitsCheckBox";
            this.onlyFederalBenefitsCheckBox.Size = new System.Drawing.Size(150, 17);
            this.onlyFederalBenefitsCheckBox.TabIndex = 2;
            this.onlyFederalBenefitsCheckBox.Text = "Только местные льготы";
            this.onlyFederalBenefitsCheckBox.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfListView;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn numberGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn surnameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn nameGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn patronymicGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn streetGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn buildingNumberGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn apartmentGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn floorGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn liftPresenceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn squareGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn categoryGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitTypeGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn residentDocumentGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn documentNumberGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn documentIssueDateGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn documentValidityPeriodGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn socialNormsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn actualIntakeGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rateGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn chargesGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn actsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn recalculationsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn payableGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitSumGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn dateGridColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
        private DevExpress.XtraGrid.Columns.GridColumn rubbishChutePresenceGridColumn;
        private System.Windows.Forms.CheckBox onlyFederalBenefitsCheckBox;
    }
}