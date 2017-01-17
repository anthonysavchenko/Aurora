namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.ChargeDetail
{
    partial class ChargeDetailView
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
            this.chargeValueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.AddressGroupBox = new System.Windows.Forms.GroupBox();
            this.squareValuelabel = new System.Windows.Forms.Label();
            this.squareLabel = new System.Windows.Forms.Label();
            this.apartmentLabel = new System.Windows.Forms.Label();
            this.houseLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.apartmentValueLabel = new System.Windows.Forms.Label();
            this.houseValueLabel = new System.Windows.Forms.Label();
            this.streetValueLabel = new System.Windows.Forms.Label();
            this._servicesGroupBox = new System.Windows.Forms.GroupBox();
            this.paymentPosGridControl = new DevExpress.XtraGrid.GridControl();
            this.chargePosGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceTypeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.serviceColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.typeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contractorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupByPeriodAndServiceTypeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PaymentTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.BillLinkLabel = new System.Windows.Forms.LinkLabel();
            this.benefitSumValueLabel = new System.Windows.Forms.Label();
            this.benefitSumLabel = new System.Windows.Forms.Label();
            this.periodChargedValueLabel = new System.Windows.Forms.Label();
            this.periodChargedLabel = new System.Windows.Forms.Label();
            this.chargeOperValueLabel = new System.Windows.Forms.Label();
            this.paymentSumLabel = new System.Windows.Forms.Label();
            this.accountValueLabel = new System.Windows.Forms.Label();
            this.ownerValueLabel = new System.Windows.Forms.Label();
            this.accountLabel = new System.Windows.Forms.Label();
            this.ownerLabel = new System.Windows.Forms.Label();
            this.customerGroupBox = new System.Windows.Forms.GroupBox();
            this.rechargeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.AddressGroupBox.SuspendLayout();
            this._servicesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paymentPosGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargePosGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.PaymentTypeGroupBox.SuspendLayout();
            this.customerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // chargeValueColumn
            // 
            this.chargeValueColumn.Caption = "Сумма";
            this.chargeValueColumn.DisplayFormat.FormatString = "{0:c2}";
            this.chargeValueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.chargeValueColumn.FieldName = "Value";
            this.chargeValueColumn.Name = "chargeValueColumn";
            this.chargeValueColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.chargeValueColumn.Visible = true;
            this.chargeValueColumn.VisibleIndex = 1;
            // 
            // AddressGroupBox
            // 
            this.AddressGroupBox.Controls.Add(this.squareValuelabel);
            this.AddressGroupBox.Controls.Add(this.squareLabel);
            this.AddressGroupBox.Controls.Add(this.apartmentLabel);
            this.AddressGroupBox.Controls.Add(this.houseLabel);
            this.AddressGroupBox.Controls.Add(this.streetLabel);
            this.AddressGroupBox.Controls.Add(this.apartmentValueLabel);
            this.AddressGroupBox.Controls.Add(this.houseValueLabel);
            this.AddressGroupBox.Controls.Add(this.streetValueLabel);
            this.AddressGroupBox.Location = new System.Drawing.Point(3, 93);
            this.AddressGroupBox.Name = "AddressGroupBox";
            this.AddressGroupBox.Size = new System.Drawing.Size(343, 134);
            this.AddressGroupBox.TabIndex = 32;
            this.AddressGroupBox.TabStop = false;
            this.AddressGroupBox.Text = "Адрес";
            // 
            // squareValuelabel
            // 
            this.squareValuelabel.AutoSize = true;
            this.squareValuelabel.Location = new System.Drawing.Point(116, 101);
            this.squareValuelabel.Name = "squareValuelabel";
            this.squareValuelabel.Size = new System.Drawing.Size(45, 13);
            this.squareValuelabel.TabIndex = 35;
            this.squareValuelabel.Text = "0 кв. м.";
            // 
            // squareLabel
            // 
            this.squareLabel.AutoSize = true;
            this.squareLabel.Location = new System.Drawing.Point(17, 101);
            this.squareLabel.Name = "squareLabel";
            this.squareLabel.Size = new System.Drawing.Size(90, 13);
            this.squareLabel.TabIndex = 34;
            this.squareLabel.Text = "Общая площадь";
            // 
            // apartmentLabel
            // 
            this.apartmentLabel.AutoSize = true;
            this.apartmentLabel.Location = new System.Drawing.Point(17, 75);
            this.apartmentLabel.Name = "apartmentLabel";
            this.apartmentLabel.Size = new System.Drawing.Size(55, 13);
            this.apartmentLabel.TabIndex = 33;
            this.apartmentLabel.Text = "Квартира";
            // 
            // houseLabel
            // 
            this.houseLabel.AutoSize = true;
            this.houseLabel.Location = new System.Drawing.Point(17, 49);
            this.houseLabel.Name = "houseLabel";
            this.houseLabel.Size = new System.Drawing.Size(30, 13);
            this.houseLabel.TabIndex = 32;
            this.houseLabel.Text = "Дом";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(17, 23);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(39, 13);
            this.streetLabel.TabIndex = 31;
            this.streetLabel.Text = "Улица";
            // 
            // apartmentValueLabel
            // 
            this.apartmentValueLabel.AutoSize = true;
            this.apartmentValueLabel.Location = new System.Drawing.Point(116, 75);
            this.apartmentValueLabel.Name = "apartmentValueLabel";
            this.apartmentValueLabel.Size = new System.Drawing.Size(13, 13);
            this.apartmentValueLabel.TabIndex = 30;
            this.apartmentValueLabel.Text = "0";
            // 
            // houseValueLabel
            // 
            this.houseValueLabel.AutoSize = true;
            this.houseValueLabel.Location = new System.Drawing.Point(116, 49);
            this.houseValueLabel.Name = "houseValueLabel";
            this.houseValueLabel.Size = new System.Drawing.Size(13, 13);
            this.houseValueLabel.TabIndex = 29;
            this.houseValueLabel.Text = "0";
            // 
            // streetValueLabel
            // 
            this.streetValueLabel.AutoSize = true;
            this.streetValueLabel.Location = new System.Drawing.Point(116, 23);
            this.streetValueLabel.Name = "streetValueLabel";
            this.streetValueLabel.Size = new System.Drawing.Size(36, 13);
            this.streetValueLabel.TabIndex = 28;
            this.streetValueLabel.Text = "улица";
            // 
            // _servicesGroupBox
            // 
            this._servicesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._servicesGroupBox.Controls.Add(this.paymentPosGridControl);
            this._servicesGroupBox.Controls.Add(this.groupBox1);
            this._servicesGroupBox.Location = new System.Drawing.Point(352, 3);
            this._servicesGroupBox.Name = "_servicesGroupBox";
            this._servicesGroupBox.Size = new System.Drawing.Size(372, 353);
            this._servicesGroupBox.TabIndex = 35;
            this._servicesGroupBox.TabStop = false;
            this._servicesGroupBox.Text = "Распределение начисления";
            // 
            // paymentPosGridControl
            // 
            this.paymentPosGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentPosGridControl.Location = new System.Drawing.Point(3, 56);
            this.paymentPosGridControl.MainView = this.chargePosGridView;
            this.paymentPosGridControl.Name = "paymentPosGridControl";
            this.paymentPosGridControl.Size = new System.Drawing.Size(366, 294);
            this.paymentPosGridControl.TabIndex = 0;
            this.paymentPosGridControl.TabStop = false;
            this.paymentPosGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.chargePosGridView});
            // 
            // chargePosGridView
            // 
            this.chargePosGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.serviceTypeColumn,
            this.serviceColumn,
            this.chargeValueColumn,
            this.typeColumn,
            this.contractorColumn});
            this.chargePosGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.chargePosGridView.GridControl = this.paymentPosGridControl;
            this.chargePosGridView.GroupCount = 2;
            this.chargePosGridView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", this.chargeValueColumn, "Итого: {0:c2}")});
            this.chargePosGridView.Name = "chargePosGridView";
            this.chargePosGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.chargePosGridView.OptionsBehavior.Editable = false;
            this.chargePosGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.chargePosGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.chargePosGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.typeColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.serviceTypeColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // serviceTypeColumn
            // 
            this.serviceTypeColumn.Caption = "Тип услуги";
            this.serviceTypeColumn.FieldName = "ServiceTypeName";
            this.serviceTypeColumn.Name = "serviceTypeColumn";
            this.serviceTypeColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // serviceColumn
            // 
            this.serviceColumn.Caption = "Услуга";
            this.serviceColumn.FieldName = "ServiceName";
            this.serviceColumn.Name = "serviceColumn";
            this.serviceColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.serviceColumn.Visible = true;
            this.serviceColumn.VisibleIndex = 0;
            this.serviceColumn.Width = 77;
            // 
            // typeColumn
            // 
            this.typeColumn.Caption = "Операция";
            this.typeColumn.FieldName = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.OptionsColumn.AllowEdit = false;
            this.typeColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
            // 
            // contractorColumn
            // 
            this.contractorColumn.Caption = "Подрядчик";
            this.contractorColumn.FieldName = "Contractor";
            this.contractorColumn.Name = "contractorColumn";
            this.contractorColumn.Visible = true;
            this.contractorColumn.VisibleIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupByPeriodAndServiceTypeLinkLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 40);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Группировка";
            // 
            // groupByPeriodAndServiceTypeLinkLabel
            // 
            this.groupByPeriodAndServiceTypeLinkLabel.AutoSize = true;
            this.groupByPeriodAndServiceTypeLinkLabel.Location = new System.Drawing.Point(13, 16);
            this.groupByPeriodAndServiceTypeLinkLabel.Name = "groupByPeriodAndServiceTypeLinkLabel";
            this.groupByPeriodAndServiceTypeLinkLabel.Size = new System.Drawing.Size(140, 13);
            this.groupByPeriodAndServiceTypeLinkLabel.TabIndex = 3;
            this.groupByPeriodAndServiceTypeLinkLabel.TabStop = true;
            this.groupByPeriodAndServiceTypeLinkLabel.Text = "по операции и типу услуги";
            this.groupByPeriodAndServiceTypeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.groupByPeriodAndServiceTypeLinkLabel_LinkClicked);
            // 
            // PaymentTypeGroupBox
            // 
            this.PaymentTypeGroupBox.Controls.Add(this.rechargeLinkLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.BillLinkLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.benefitSumValueLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.benefitSumLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.periodChargedValueLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.periodChargedLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.chargeOperValueLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.paymentSumLabel);
            this.PaymentTypeGroupBox.Location = new System.Drawing.Point(3, 233);
            this.PaymentTypeGroupBox.Name = "PaymentTypeGroupBox";
            this.PaymentTypeGroupBox.Size = new System.Drawing.Size(343, 123);
            this.PaymentTypeGroupBox.TabIndex = 2;
            this.PaymentTypeGroupBox.TabStop = false;
            this.PaymentTypeGroupBox.Text = "Начисление";
            // 
            // BillLinkLabel
            // 
            this.BillLinkLabel.AutoSize = true;
            this.BillLinkLabel.Location = new System.Drawing.Point(17, 93);
            this.BillLinkLabel.Name = "BillLinkLabel";
            this.BillLinkLabel.Size = new System.Drawing.Size(61, 13);
            this.BillLinkLabel.TabIndex = 45;
            this.BillLinkLabel.TabStop = true;
            this.BillLinkLabel.Text = "Квитанция";
            this.BillLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BillLinkLabel_LinkClicked);
            // 
            // benefitSumValueLabel
            // 
            this.benefitSumValueLabel.AutoSize = true;
            this.benefitSumValueLabel.Location = new System.Drawing.Point(116, 71);
            this.benefitSumValueLabel.Name = "benefitSumValueLabel";
            this.benefitSumValueLabel.Size = new System.Drawing.Size(13, 13);
            this.benefitSumValueLabel.TabIndex = 44;
            this.benefitSumValueLabel.Text = "0";
            // 
            // benefitSumLabel
            // 
            this.benefitSumLabel.AutoSize = true;
            this.benefitSumLabel.Location = new System.Drawing.Point(17, 71);
            this.benefitSumLabel.Name = "benefitSumLabel";
            this.benefitSumLabel.Size = new System.Drawing.Size(80, 13);
            this.benefitSumLabel.TabIndex = 43;
            this.benefitSumLabel.Text = "Сумма льготы";
            // 
            // periodChargedValueLabel
            // 
            this.periodChargedValueLabel.AutoSize = true;
            this.periodChargedValueLabel.Location = new System.Drawing.Point(116, 27);
            this.periodChargedValueLabel.Name = "periodChargedValueLabel";
            this.periodChargedValueLabel.Size = new System.Drawing.Size(61, 13);
            this.periodChargedValueLabel.TabIndex = 42;
            this.periodChargedValueLabel.Text = "01.01.2013";
            // 
            // periodChargedLabel
            // 
            this.periodChargedLabel.AutoSize = true;
            this.periodChargedLabel.Location = new System.Drawing.Point(17, 27);
            this.periodChargedLabel.Name = "periodChargedLabel";
            this.periodChargedLabel.Size = new System.Drawing.Size(90, 13);
            this.periodChargedLabel.TabIndex = 41;
            this.periodChargedLabel.Text = "Учетный период";
            // 
            // chargeOperValueLabel
            // 
            this.chargeOperValueLabel.AutoSize = true;
            this.chargeOperValueLabel.Location = new System.Drawing.Point(116, 49);
            this.chargeOperValueLabel.Name = "chargeOperValueLabel";
            this.chargeOperValueLabel.Size = new System.Drawing.Size(13, 13);
            this.chargeOperValueLabel.TabIndex = 40;
            this.chargeOperValueLabel.Text = "0";
            // 
            // paymentSumLabel
            // 
            this.paymentSumLabel.AutoSize = true;
            this.paymentSumLabel.Location = new System.Drawing.Point(17, 49);
            this.paymentSumLabel.Name = "paymentSumLabel";
            this.paymentSumLabel.Size = new System.Drawing.Size(41, 13);
            this.paymentSumLabel.TabIndex = 37;
            this.paymentSumLabel.Text = "Сумма";
            // 
            // accountValueLabel
            // 
            this.accountValueLabel.AutoSize = true;
            this.accountValueLabel.Location = new System.Drawing.Point(116, 28);
            this.accountValueLabel.Name = "accountValueLabel";
            this.accountValueLabel.Size = new System.Drawing.Size(85, 13);
            this.accountValueLabel.TabIndex = 36;
            this.accountValueLabel.Text = "EG-0000-000-00";
            // 
            // ownerValueLabel
            // 
            this.ownerValueLabel.AutoSize = true;
            this.ownerValueLabel.Location = new System.Drawing.Point(116, 54);
            this.ownerValueLabel.Name = "ownerValueLabel";
            this.ownerValueLabel.Size = new System.Drawing.Size(131, 13);
            this.ownerValueLabel.TabIndex = 37;
            this.ownerValueLabel.Text = "Фамилия Имя Отчество";
            // 
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(17, 28);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(76, 13);
            this.accountLabel.TabIndex = 38;
            this.accountLabel.Text = "Лицевой счет";
            // 
            // ownerLabel
            // 
            this.ownerLabel.AutoSize = true;
            this.ownerLabel.Location = new System.Drawing.Point(17, 54);
            this.ownerLabel.Name = "ownerLabel";
            this.ownerLabel.Size = new System.Drawing.Size(73, 13);
            this.ownerLabel.TabIndex = 39;
            this.ownerLabel.Text = "Собственник";
            // 
            // customerGroupBox
            // 
            this.customerGroupBox.Controls.Add(this.accountLabel);
            this.customerGroupBox.Controls.Add(this.ownerLabel);
            this.customerGroupBox.Controls.Add(this.accountValueLabel);
            this.customerGroupBox.Controls.Add(this.ownerValueLabel);
            this.customerGroupBox.Location = new System.Drawing.Point(3, 3);
            this.customerGroupBox.Name = "customerGroupBox";
            this.customerGroupBox.Size = new System.Drawing.Size(343, 84);
            this.customerGroupBox.TabIndex = 40;
            this.customerGroupBox.TabStop = false;
            this.customerGroupBox.Text = "Абонент";
            // 
            // rechargeLinkLabel
            // 
            this.rechargeLinkLabel.AutoSize = true;
            this.rechargeLinkLabel.Location = new System.Drawing.Point(84, 93);
            this.rechargeLinkLabel.Name = "rechargeLinkLabel";
            this.rechargeLinkLabel.Size = new System.Drawing.Size(67, 13);
            this.rechargeLinkLabel.TabIndex = 46;
            this.rechargeLinkLabel.TabStop = true;
            this.rechargeLinkLabel.Text = "Перерасчет";
            this.rechargeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.rechargeLinkLabel_LinkClicked);
            // 
            // ChargeDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.customerGroupBox);
            this.Controls.Add(this.PaymentTypeGroupBox);
            this.Controls.Add(this._servicesGroupBox);
            this.Controls.Add(this.AddressGroupBox);
            this.Name = "ChargeDetailView";
            this.Size = new System.Drawing.Size(727, 359);
            this.AddressGroupBox.ResumeLayout(false);
            this.AddressGroupBox.PerformLayout();
            this._servicesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paymentPosGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargePosGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.PaymentTypeGroupBox.ResumeLayout(false);
            this.PaymentTypeGroupBox.PerformLayout();
            this.customerGroupBox.ResumeLayout(false);
            this.customerGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox AddressGroupBox;
        private System.Windows.Forms.GroupBox _servicesGroupBox;
        private DevExpress.XtraGrid.GridControl paymentPosGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView chargePosGridView;
        private DevExpress.XtraGrid.Columns.GridColumn serviceColumn;
        private DevExpress.XtraGrid.Columns.GridColumn chargeValueColumn;
        private System.Windows.Forms.GroupBox PaymentTypeGroupBox;
        private System.Windows.Forms.Label accountValueLabel;
        private System.Windows.Forms.Label ownerValueLabel;
        private System.Windows.Forms.Label apartmentValueLabel;
        private System.Windows.Forms.Label houseValueLabel;
        private System.Windows.Forms.Label streetValueLabel;
        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.Label ownerLabel;
        private System.Windows.Forms.Label squareValuelabel;
        private System.Windows.Forms.Label squareLabel;
        private System.Windows.Forms.Label apartmentLabel;
        private System.Windows.Forms.Label houseLabel;
        private System.Windows.Forms.Label streetLabel;
        private System.Windows.Forms.Label paymentSumLabel;
        private System.Windows.Forms.GroupBox customerGroupBox;
        private System.Windows.Forms.Label chargeOperValueLabel;
        private DevExpress.XtraGrid.Columns.GridColumn serviceTypeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private System.Windows.Forms.LinkLabel groupByPeriodAndServiceTypeLinkLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label periodChargedValueLabel;
        private System.Windows.Forms.Label periodChargedLabel;
        private DevExpress.XtraGrid.Columns.GridColumn typeColumn;
        private System.Windows.Forms.Label benefitSumLabel;
        private System.Windows.Forms.Label benefitSumValueLabel;
        private DevExpress.XtraGrid.Columns.GridColumn contractorColumn;
        private System.Windows.Forms.LinkLabel BillLinkLabel;
        private System.Windows.Forms.LinkLabel rechargeLinkLabel;


    }
}