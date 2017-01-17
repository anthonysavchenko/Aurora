namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.List
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
            this.serviceGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.customerGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.apartmentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chargesGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.actsGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rechargesGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.benefitGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.totalGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.paidGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.overpaymentDebtGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serviceLabel = new System.Windows.Forms.Label();
            this.houseLabel = new System.Windows.Forms.Label();
            this.serviceTypeLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.byServiceRadioButton = new System.Windows.Forms.RadioButton();
            this.byCustomerRadioButton = new System.Windows.Forms.RadioButton();
            this.serviceLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.serviceTypeLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.buildingLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.streetLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.splitByServicesCheckBox = new System.Windows.Forms.CheckBox();
            this.tillDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.sinceDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).BeginInit();
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
            this.gridControlOfListView.Location = new System.Drawing.Point(0, 100);
            this.gridControlOfListView.MainView = this.gridViewOfListView;
            this.gridControlOfListView.Name = "gridControlOfListView";
            this.gridControlOfListView.Size = new System.Drawing.Size(871, 250);
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
            this.serviceGridColumn,
            this.customerGridColumn,
            this.apartmentGridColumn,
            this.chargesGridColumn,
            this.actsGridColumn,
            this.rechargesGridColumn,
            this.benefitGridColumn,
            this.totalGridColumn,
            this.paidGridColumn,
            this.overpaymentDebtGridColumn});
            this.gridViewOfListView.GridControl = this.gridControlOfListView;
            this.gridViewOfListView.GroupCount = 2;
            this.gridViewOfListView.Name = "gridViewOfListView";
            this.gridViewOfListView.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridViewOfListView.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewOfListView.OptionsBehavior.Editable = false;
            this.gridViewOfListView.OptionsMenu.EnableFooterMenu = false;
            this.gridViewOfListView.OptionsView.ColumnAutoWidth = false;
            this.gridViewOfListView.OptionsView.RowAutoHeight = true;
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
            this.buildingGridColumn.VisibleIndex = 1;
            // 
            // serviceGridColumn
            // 
            this.serviceGridColumn.Caption = "Тип услуги / Услуга";
            this.serviceGridColumn.FieldName = "Service";
            this.serviceGridColumn.Name = "serviceGridColumn";
            this.serviceGridColumn.Visible = true;
            this.serviceGridColumn.VisibleIndex = 0;
            // 
            // customerGridColumn
            // 
            this.customerGridColumn.Caption = "Абонент";
            this.customerGridColumn.FieldName = "Customer";
            this.customerGridColumn.Name = "customerGridColumn";
            this.customerGridColumn.Visible = true;
            this.customerGridColumn.VisibleIndex = 1;
            // 
            // apartmentGridColumn
            // 
            this.apartmentGridColumn.Caption = "Квартира";
            this.apartmentGridColumn.FieldName = "Apartment";
            this.apartmentGridColumn.Name = "apartmentGridColumn";
            this.apartmentGridColumn.Visible = true;
            this.apartmentGridColumn.VisibleIndex = 2;
            // 
            // chargesGridColumn
            // 
            this.chargesGridColumn.Caption = "Начислено";
            this.chargesGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.chargesGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.chargesGridColumn.FieldName = "Charges";
            this.chargesGridColumn.Name = "chargesGridColumn";
            this.chargesGridColumn.Visible = true;
            this.chargesGridColumn.VisibleIndex = 3;
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
            // rechargesGridColumn
            // 
            this.rechargesGridColumn.Caption = "Перерасчеты";
            this.rechargesGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.rechargesGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rechargesGridColumn.FieldName = "Recharges";
            this.rechargesGridColumn.Name = "rechargesGridColumn";
            this.rechargesGridColumn.Visible = true;
            this.rechargesGridColumn.VisibleIndex = 5;
            // 
            // benefitGridColumn
            // 
            this.benefitGridColumn.Caption = "Льготы";
            this.benefitGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.benefitGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.benefitGridColumn.FieldName = "Benefits";
            this.benefitGridColumn.Name = "benefitGridColumn";
            this.benefitGridColumn.Visible = true;
            this.benefitGridColumn.VisibleIndex = 6;
            // 
            // totalGridColumn
            // 
            this.totalGridColumn.Caption = "К оплате";
            this.totalGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.totalGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.totalGridColumn.FieldName = "Payable";
            this.totalGridColumn.Name = "totalGridColumn";
            this.totalGridColumn.Visible = true;
            this.totalGridColumn.VisibleIndex = 7;
            // 
            // paidGridColumn
            // 
            this.paidGridColumn.Caption = "Оплачено";
            this.paidGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.paidGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.paidGridColumn.FieldName = "Paid";
            this.paidGridColumn.Name = "paidGridColumn";
            this.paidGridColumn.Visible = true;
            this.paidGridColumn.VisibleIndex = 8;
            // 
            // overpaymentDebtGridColumn
            // 
            this.overpaymentDebtGridColumn.Caption = "Переплата/Долг";
            this.overpaymentDebtGridColumn.DisplayFormat.FormatString = "{0:0.00}";
            this.overpaymentDebtGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.overpaymentDebtGridColumn.FieldName = "OverpaymentDebt";
            this.overpaymentDebtGridColumn.Name = "overpaymentDebtGridColumn";
            this.overpaymentDebtGridColumn.Visible = true;
            this.overpaymentDebtGridColumn.VisibleIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serviceLabel);
            this.groupBox1.Controls.Add(this.houseLabel);
            this.groupBox1.Controls.Add(this.serviceTypeLabel);
            this.groupBox1.Controls.Add(this.streetLabel);
            this.groupBox1.Controls.Add(this.byServiceRadioButton);
            this.groupBox1.Controls.Add(this.byCustomerRadioButton);
            this.groupBox1.Controls.Add(this.serviceLookUpEdit);
            this.groupBox1.Controls.Add(this.serviceTypeLookUpEdit);
            this.groupBox1.Controls.Add(this.buildingLookUpEdit);
            this.groupBox1.Controls.Add(this.streetLookUpEdit);
            this.groupBox1.Controls.Add(this.splitByServicesCheckBox);
            this.groupBox1.Controls.Add(this.tillDateEdit);
            this.groupBox1.Controls.Add(this.sinceDateEdit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(871, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры отчета";
            // 
            // serviceLabel
            // 
            this.serviceLabel.AutoSize = true;
            this.serviceLabel.Location = new System.Drawing.Point(450, 74);
            this.serviceLabel.Name = "serviceLabel";
            this.serviceLabel.Size = new System.Drawing.Size(46, 13);
            this.serviceLabel.TabIndex = 37;
            this.serviceLabel.Text = "Услуга:";
            // 
            // houseLabel
            // 
            this.houseLabel.AutoSize = true;
            this.houseLabel.Location = new System.Drawing.Point(450, 48);
            this.houseLabel.Name = "houseLabel";
            this.houseLabel.Size = new System.Drawing.Size(33, 13);
            this.houseLabel.TabIndex = 36;
            this.houseLabel.Text = "Дом:";
            // 
            // serviceTypeLabel
            // 
            this.serviceTypeLabel.AutoSize = true;
            this.serviceTypeLabel.Location = new System.Drawing.Point(162, 74);
            this.serviceTypeLabel.Name = "serviceTypeLabel";
            this.serviceTypeLabel.Size = new System.Drawing.Size(65, 13);
            this.serviceTypeLabel.TabIndex = 34;
            this.serviceTypeLabel.Text = "Тип услуги:";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(162, 48);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(42, 13);
            this.streetLabel.TabIndex = 33;
            this.streetLabel.Text = "Улица:";
            // 
            // byServiceRadioButton
            // 
            this.byServiceRadioButton.AutoSize = true;
            this.byServiceRadioButton.Location = new System.Drawing.Point(6, 46);
            this.byServiceRadioButton.Name = "byServiceRadioButton";
            this.byServiceRadioButton.Size = new System.Drawing.Size(83, 17);
            this.byServiceRadioButton.TabIndex = 32;
            this.byServiceRadioButton.Text = "По услугам";
            this.byServiceRadioButton.UseVisualStyleBackColor = true;
            // 
            // byCustomerRadioButton
            // 
            this.byCustomerRadioButton.AutoSize = true;
            this.byCustomerRadioButton.Checked = true;
            this.byCustomerRadioButton.Location = new System.Drawing.Point(6, 19);
            this.byCustomerRadioButton.Name = "byCustomerRadioButton";
            this.byCustomerRadioButton.Size = new System.Drawing.Size(97, 17);
            this.byCustomerRadioButton.TabIndex = 31;
            this.byCustomerRadioButton.TabStop = true;
            this.byCustomerRadioButton.Text = "По абонентам";
            this.byCustomerRadioButton.UseVisualStyleBackColor = true;
            this.byCustomerRadioButton.CheckedChanged += new System.EventHandler(this.byCustomerRadioButton_CheckedChanged);
            // 
            // serviceLookUpEdit
            // 
            this.serviceLookUpEdit.Location = new System.Drawing.Point(502, 71);
            this.serviceLookUpEdit.Name = "serviceLookUpEdit";
            this.serviceLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.serviceLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.serviceLookUpEdit.Properties.DisplayMember = "Name";
            this.serviceLookUpEdit.Properties.NullText = "(все)";
            this.serviceLookUpEdit.Properties.ValueMember = "ID";
            this.serviceLookUpEdit.Size = new System.Drawing.Size(213, 20);
            this.serviceLookUpEdit.TabIndex = 28;
            this.serviceLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // serviceTypeLookUpEdit
            // 
            this.serviceTypeLookUpEdit.Location = new System.Drawing.Point(233, 71);
            this.serviceTypeLookUpEdit.Name = "serviceTypeLookUpEdit";
            this.serviceTypeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.serviceTypeLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.serviceTypeLookUpEdit.Properties.DisplayMember = "Name";
            this.serviceTypeLookUpEdit.Properties.NullText = "(все)";
            this.serviceTypeLookUpEdit.Properties.ValueMember = "ID";
            this.serviceTypeLookUpEdit.Size = new System.Drawing.Size(201, 20);
            this.serviceTypeLookUpEdit.TabIndex = 29;
            this.serviceTypeLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // buildingLookUpEdit
            // 
            this.buildingLookUpEdit.Location = new System.Drawing.Point(502, 45);
            this.buildingLookUpEdit.Name = "buildingLookUpEdit";
            this.buildingLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.buildingLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер дома")});
            this.buildingLookUpEdit.Properties.DisplayMember = "Number";
            this.buildingLookUpEdit.Properties.NullText = "(все)";
            this.buildingLookUpEdit.Properties.ValueMember = "ID";
            this.buildingLookUpEdit.Size = new System.Drawing.Size(213, 20);
            this.buildingLookUpEdit.TabIndex = 28;
            this.buildingLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // streetLookUpEdit
            // 
            this.streetLookUpEdit.Location = new System.Drawing.Point(233, 45);
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
            this.streetLookUpEdit.TabIndex = 29;
            this.streetLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            this.streetLookUpEdit.EditValueChanged += new System.EventHandler(this.streetLookUpEdit_EditValueChanged);
            // 
            // splitByServicesCheckBox
            // 
            this.splitByServicesCheckBox.AutoSize = true;
            this.splitByServicesCheckBox.Enabled = false;
            this.splitByServicesCheckBox.Location = new System.Drawing.Point(22, 74);
            this.splitByServicesCheckBox.Name = "splitByServicesCheckBox";
            this.splitByServicesCheckBox.Size = new System.Drawing.Size(127, 17);
            this.splitByServicesCheckBox.TabIndex = 4;
            this.splitByServicesCheckBox.Text = "Разбить по услугам";
            this.splitByServicesCheckBox.UseVisualStyleBackColor = true;
            // 
            // tillDateEdit
            // 
            this.tillDateEdit.EditValue = null;
            this.tillDateEdit.Location = new System.Drawing.Point(502, 19);
            this.tillDateEdit.Name = "tillDateEdit";
            this.tillDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tillDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.tillDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.tillDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.tillDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.tillDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tillDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.tillDateEdit.Size = new System.Drawing.Size(213, 20);
            this.tillDateEdit.TabIndex = 3;
            // 
            // sinceDateEdit
            // 
            this.sinceDateEdit.EditValue = null;
            this.sinceDateEdit.Location = new System.Drawing.Point(233, 19);
            this.sinceDateEdit.Name = "sinceDateEdit";
            this.sinceDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sinceDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.sinceDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.sinceDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.sinceDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.sinceDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.sinceDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.sinceDateEdit.Size = new System.Drawing.Size(201, 20);
            this.sinceDateEdit.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(450, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "По:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(162, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "С:";
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlOfListView);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(871, 350);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOfListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOfListView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sinceDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlOfListView;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOfListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit tillDateEdit;
        private DevExpress.XtraEditors.DateEdit sinceDateEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn streetGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn buildingGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn serviceGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn chargesGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn paidGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn overpaymentDebtGridColumn;
        private System.Windows.Forms.CheckBox splitByServicesCheckBox;
        private DevExpress.XtraEditors.LookUpEdit buildingLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit streetLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit serviceLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit serviceTypeLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn customerGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn apartmentGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn actsGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn rechargesGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn benefitGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn totalGridColumn;
        private System.Windows.Forms.RadioButton byServiceRadioButton;
        private System.Windows.Forms.RadioButton byCustomerRadioButton;
        private System.Windows.Forms.Label serviceLabel;
        private System.Windows.Forms.Label houseLabel;
        private System.Windows.Forms.Label serviceTypeLabel;
        private System.Windows.Forms.Label streetLabel;
    }
}