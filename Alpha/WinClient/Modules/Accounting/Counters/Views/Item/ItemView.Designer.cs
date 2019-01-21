namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    partial class ItemView
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
            this._servicesGroupBox = new System.Windows.Forms.GroupBox();
            this.counterValueGridControl = new DevExpress.XtraGrid.GridControl();
            this.counterValueGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dateColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.periodColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PaymentTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.counterArchivedCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.counterServicesLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.counterModelTextBox = new System.Windows.Forms.TextBox();
            this.counterNumTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.periodLabel = new System.Windows.Forms.Label();
            this.intermediaryLabel = new System.Windows.Forms.Label();
            this.customerOwner = new System.Windows.Forms.Label();
            this.customerAccount = new System.Windows.Forms.Label();
            this.ownerLabel = new System.Windows.Forms.Label();
            this.accountLabel = new System.Windows.Forms.Label();
            this.customerStreet = new System.Windows.Forms.Label();
            this.customerBuilding = new System.Windows.Forms.Label();
            this.customerApartment = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.customerArea = new System.Windows.Forms.Label();
            this.customerGroupBox = new System.Windows.Forms.GroupBox();
            this.buildingStreet = new System.Windows.Forms.Label();
            this.buildingNum = new System.Windows.Forms.Label();
            this.buildingCollectionSector = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.houseLabel = new System.Windows.Forms.Label();
            this.apartmentLabel = new System.Windows.Forms.Label();
            this.squareLabel = new System.Windows.Forms.Label();
            this.buildingDwellersNum = new System.Windows.Forms.Label();
            this.buildingGroupBox = new System.Windows.Forms.GroupBox();
            this._servicesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).BeginInit();
            this.PaymentTypeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterServicesLookUpEdit.Properties)).BeginInit();
            this.customerGroupBox.SuspendLayout();
            this.buildingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _servicesGroupBox
            // 
            this._servicesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._servicesGroupBox.Controls.Add(this.counterValueGridControl);
            this._servicesGroupBox.Location = new System.Drawing.Point(352, 3);
            this._servicesGroupBox.Name = "_servicesGroupBox";
            this._servicesGroupBox.Size = new System.Drawing.Size(372, 334);
            this._servicesGroupBox.TabIndex = 35;
            this._servicesGroupBox.TabStop = false;
            this._servicesGroupBox.Text = "Показания";
            // 
            // counterValueGridControl
            // 
            this.counterValueGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.First.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.counterValueGridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.counterValueGridControl.Location = new System.Drawing.Point(3, 16);
            this.counterValueGridControl.MainView = this.counterValueGridView;
            this.counterValueGridControl.Name = "counterValueGridControl";
            this.counterValueGridControl.Size = new System.Drawing.Size(366, 315);
            this.counterValueGridControl.TabIndex = 0;
            this.counterValueGridControl.TabStop = false;
            this.counterValueGridControl.UseEmbeddedNavigator = true;
            this.counterValueGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.counterValueGridView});
            // 
            // counterValueGridView
            // 
            this.counterValueGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.dateColumn,
            this.periodColumn,
            this.valueColumn});
            this.counterValueGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.counterValueGridView.GridControl = this.counterValueGridControl;
            this.counterValueGridView.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", this.valueColumn, "Итого: {0:c2}")});
            this.counterValueGridView.Name = "counterValueGridView";
            this.counterValueGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.counterValueGridView.OptionsBehavior.Editable = false;
            this.counterValueGridView.OptionsCustomization.AllowGroup = false;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.counterValueGridView.OptionsView.ShowGroupPanel = false;
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // dateColumn
            // 
            this.dateColumn.Caption = "Дата";
            this.dateColumn.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.dateColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateColumn.FieldName = "CollectDate";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.Visible = true;
            this.dateColumn.VisibleIndex = 0;
            // 
            // periodColumn
            // 
            this.periodColumn.Caption = "Период";
            this.periodColumn.DisplayFormat.FormatString = "MM.yyyy";
            this.periodColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodColumn.FieldName = "Period";
            this.periodColumn.GroupFormat.FormatString = "MM.yyyy";
            this.periodColumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodColumn.Name = "periodColumn";
            this.periodColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            this.periodColumn.Visible = true;
            this.periodColumn.VisibleIndex = 1;
            // 
            // valueColumn
            // 
            this.valueColumn.Caption = "Показание";
            this.valueColumn.DisplayFormat.FormatString = "0.000";
            this.valueColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueColumn.FieldName = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.valueColumn.Visible = true;
            this.valueColumn.VisibleIndex = 2;
            // 
            // PaymentTypeGroupBox
            // 
            this.PaymentTypeGroupBox.Controls.Add(this.counterArchivedCheckBox);
            this.PaymentTypeGroupBox.Controls.Add(this.label6);
            this.PaymentTypeGroupBox.Controls.Add(this.counterServicesLookUpEdit);
            this.PaymentTypeGroupBox.Controls.Add(this.counterModelTextBox);
            this.PaymentTypeGroupBox.Controls.Add(this.counterNumTextBox);
            this.PaymentTypeGroupBox.Controls.Add(this.label1);
            this.PaymentTypeGroupBox.Controls.Add(this.periodLabel);
            this.PaymentTypeGroupBox.Controls.Add(this.intermediaryLabel);
            this.PaymentTypeGroupBox.Location = new System.Drawing.Point(3, 2);
            this.PaymentTypeGroupBox.Name = "PaymentTypeGroupBox";
            this.PaymentTypeGroupBox.Size = new System.Drawing.Size(343, 133);
            this.PaymentTypeGroupBox.TabIndex = 2;
            this.PaymentTypeGroupBox.TabStop = false;
            this.PaymentTypeGroupBox.Text = "Прибор учета";
            // 
            // counterArchivedCheckBox
            // 
            this.counterArchivedCheckBox.AutoSize = true;
            this.counterArchivedCheckBox.Location = new System.Drawing.Point(107, 103);
            this.counterArchivedCheckBox.Name = "counterArchivedCheckBox";
            this.counterArchivedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.counterArchivedCheckBox.TabIndex = 46;
            this.counterArchivedCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Архивный";
            // 
            // counterServicesLookUpEdit
            // 
            this.counterServicesLookUpEdit.Location = new System.Drawing.Point(107, 48);
            this.counterServicesLookUpEdit.Name = "counterServicesLookUpEdit";
            this.counterServicesLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.counterServicesLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.counterServicesLookUpEdit.Properties.DisplayMember = "Name";
            this.counterServicesLookUpEdit.Properties.ValueMember = "ID";
            this.counterServicesLookUpEdit.Size = new System.Drawing.Size(230, 20);
            this.counterServicesLookUpEdit.TabIndex = 44;
            // 
            // counterModelTextBox
            // 
            this.counterModelTextBox.Location = new System.Drawing.Point(107, 75);
            this.counterModelTextBox.Name = "counterModelTextBox";
            this.counterModelTextBox.Size = new System.Drawing.Size(230, 20);
            this.counterModelTextBox.TabIndex = 43;
            // 
            // counterNumTextBox
            // 
            this.counterNumTextBox.Location = new System.Drawing.Point(107, 22);
            this.counterNumTextBox.Name = "counterNumTextBox";
            this.counterNumTextBox.Size = new System.Drawing.Size(230, 20);
            this.counterNumTextBox.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Модель";
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(17, 51);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(43, 13);
            this.periodLabel.TabIndex = 36;
            this.periodLabel.Text = "Услуга";
            // 
            // intermediaryLabel
            // 
            this.intermediaryLabel.AutoSize = true;
            this.intermediaryLabel.Location = new System.Drawing.Point(17, 25);
            this.intermediaryLabel.Name = "intermediaryLabel";
            this.intermediaryLabel.Size = new System.Drawing.Size(41, 13);
            this.intermediaryLabel.TabIndex = 35;
            this.intermediaryLabel.Text = "Номер";
            // 
            // customerOwner
            // 
            this.customerOwner.AutoSize = true;
            this.customerOwner.Location = new System.Drawing.Point(136, 54);
            this.customerOwner.Name = "customerOwner";
            this.customerOwner.Size = new System.Drawing.Size(131, 13);
            this.customerOwner.TabIndex = 37;
            this.customerOwner.Text = "Фамилия Имя Отчество";
            // 
            // customerAccount
            // 
            this.customerAccount.AutoSize = true;
            this.customerAccount.Location = new System.Drawing.Point(136, 28);
            this.customerAccount.Name = "customerAccount";
            this.customerAccount.Size = new System.Drawing.Size(85, 13);
            this.customerAccount.TabIndex = 36;
            this.customerAccount.Text = "EG-0000-000-00";
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
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(17, 28);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(76, 13);
            this.accountLabel.TabIndex = 38;
            this.accountLabel.Text = "Лицевой счет";
            // 
            // customerStreet
            // 
            this.customerStreet.AutoSize = true;
            this.customerStreet.Location = new System.Drawing.Point(136, 81);
            this.customerStreet.Name = "customerStreet";
            this.customerStreet.Size = new System.Drawing.Size(36, 13);
            this.customerStreet.TabIndex = 40;
            this.customerStreet.Text = "улица";
            // 
            // customerBuilding
            // 
            this.customerBuilding.AutoSize = true;
            this.customerBuilding.Location = new System.Drawing.Point(136, 107);
            this.customerBuilding.Name = "customerBuilding";
            this.customerBuilding.Size = new System.Drawing.Size(13, 13);
            this.customerBuilding.TabIndex = 41;
            this.customerBuilding.Text = "0";
            // 
            // customerApartment
            // 
            this.customerApartment.AutoSize = true;
            this.customerApartment.Location = new System.Drawing.Point(136, 133);
            this.customerApartment.Name = "customerApartment";
            this.customerApartment.Size = new System.Drawing.Size(13, 13);
            this.customerApartment.TabIndex = 42;
            this.customerApartment.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Улица";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Дом";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Квартира";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Общая площадь";
            // 
            // customerArea
            // 
            this.customerArea.AutoSize = true;
            this.customerArea.Location = new System.Drawing.Point(136, 159);
            this.customerArea.Name = "customerArea";
            this.customerArea.Size = new System.Drawing.Size(45, 13);
            this.customerArea.TabIndex = 47;
            this.customerArea.Text = "0 кв. м.";
            // 
            // customerGroupBox
            // 
            this.customerGroupBox.Controls.Add(this.customerArea);
            this.customerGroupBox.Controls.Add(this.label2);
            this.customerGroupBox.Controls.Add(this.label3);
            this.customerGroupBox.Controls.Add(this.label4);
            this.customerGroupBox.Controls.Add(this.label5);
            this.customerGroupBox.Controls.Add(this.customerApartment);
            this.customerGroupBox.Controls.Add(this.customerBuilding);
            this.customerGroupBox.Controls.Add(this.customerStreet);
            this.customerGroupBox.Controls.Add(this.accountLabel);
            this.customerGroupBox.Controls.Add(this.ownerLabel);
            this.customerGroupBox.Controls.Add(this.customerAccount);
            this.customerGroupBox.Controls.Add(this.customerOwner);
            this.customerGroupBox.Location = new System.Drawing.Point(3, 141);
            this.customerGroupBox.Name = "customerGroupBox";
            this.customerGroupBox.Size = new System.Drawing.Size(343, 193);
            this.customerGroupBox.TabIndex = 40;
            this.customerGroupBox.TabStop = false;
            this.customerGroupBox.Text = "Абонент";
            this.customerGroupBox.Visible = false;
            // 
            // buildingStreet
            // 
            this.buildingStreet.AutoSize = true;
            this.buildingStreet.Location = new System.Drawing.Point(136, 23);
            this.buildingStreet.Name = "buildingStreet";
            this.buildingStreet.Size = new System.Drawing.Size(36, 13);
            this.buildingStreet.TabIndex = 28;
            this.buildingStreet.Text = "улица";
            // 
            // buildingNum
            // 
            this.buildingNum.AutoSize = true;
            this.buildingNum.Location = new System.Drawing.Point(136, 49);
            this.buildingNum.Name = "buildingNum";
            this.buildingNum.Size = new System.Drawing.Size(13, 13);
            this.buildingNum.TabIndex = 29;
            this.buildingNum.Text = "0";
            // 
            // buildingCollectionSector
            // 
            this.buildingCollectionSector.AutoSize = true;
            this.buildingCollectionSector.Location = new System.Drawing.Point(136, 75);
            this.buildingCollectionSector.Name = "buildingCollectionSector";
            this.buildingCollectionSector.Size = new System.Drawing.Size(13, 13);
            this.buildingCollectionSector.TabIndex = 30;
            this.buildingCollectionSector.Text = "0";
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
            // houseLabel
            // 
            this.houseLabel.AutoSize = true;
            this.houseLabel.Location = new System.Drawing.Point(17, 49);
            this.houseLabel.Name = "houseLabel";
            this.houseLabel.Size = new System.Drawing.Size(41, 13);
            this.houseLabel.TabIndex = 32;
            this.houseLabel.Text = "Номер";
            // 
            // apartmentLabel
            // 
            this.apartmentLabel.AutoSize = true;
            this.apartmentLabel.Location = new System.Drawing.Point(17, 75);
            this.apartmentLabel.Name = "apartmentLabel";
            this.apartmentLabel.Size = new System.Drawing.Size(82, 13);
            this.apartmentLabel.TabIndex = 33;
            this.apartmentLabel.Text = "Участок сбора";
            // 
            // squareLabel
            // 
            this.squareLabel.AutoSize = true;
            this.squareLabel.Location = new System.Drawing.Point(17, 101);
            this.squareLabel.Name = "squareLabel";
            this.squareLabel.Size = new System.Drawing.Size(113, 13);
            this.squareLabel.TabIndex = 34;
            this.squareLabel.Text = "Количество жильцов";
            // 
            // buildingDwellersNum
            // 
            this.buildingDwellersNum.AutoSize = true;
            this.buildingDwellersNum.Location = new System.Drawing.Point(136, 101);
            this.buildingDwellersNum.Name = "buildingDwellersNum";
            this.buildingDwellersNum.Size = new System.Drawing.Size(13, 13);
            this.buildingDwellersNum.TabIndex = 35;
            this.buildingDwellersNum.Text = "0";
            // 
            // buildingGroupBox
            // 
            this.buildingGroupBox.Controls.Add(this.buildingDwellersNum);
            this.buildingGroupBox.Controls.Add(this.squareLabel);
            this.buildingGroupBox.Controls.Add(this.apartmentLabel);
            this.buildingGroupBox.Controls.Add(this.houseLabel);
            this.buildingGroupBox.Controls.Add(this.streetLabel);
            this.buildingGroupBox.Controls.Add(this.buildingCollectionSector);
            this.buildingGroupBox.Controls.Add(this.buildingNum);
            this.buildingGroupBox.Controls.Add(this.buildingStreet);
            this.buildingGroupBox.Location = new System.Drawing.Point(3, 141);
            this.buildingGroupBox.Name = "buildingGroupBox";
            this.buildingGroupBox.Size = new System.Drawing.Size(343, 193);
            this.buildingGroupBox.TabIndex = 32;
            this.buildingGroupBox.TabStop = false;
            this.buildingGroupBox.Text = "Дом";
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.PaymentTypeGroupBox);
            this.Controls.Add(this._servicesGroupBox);
            this.Controls.Add(this.buildingGroupBox);
            this.Controls.Add(this.customerGroupBox);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(727, 344);
            this._servicesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).EndInit();
            this.PaymentTypeGroupBox.ResumeLayout(false);
            this.PaymentTypeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterServicesLookUpEdit.Properties)).EndInit();
            this.customerGroupBox.ResumeLayout(false);
            this.customerGroupBox.PerformLayout();
            this.buildingGroupBox.ResumeLayout(false);
            this.buildingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox _servicesGroupBox;
        private DevExpress.XtraGrid.GridControl counterValueGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterValueGridView;
        private DevExpress.XtraGrid.Columns.GridColumn valueColumn;
        private System.Windows.Forms.GroupBox PaymentTypeGroupBox;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.Label intermediaryLabel;
        private DevExpress.XtraGrid.Columns.GridColumn periodColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private System.Windows.Forms.Label customerOwner;
        private System.Windows.Forms.Label customerAccount;
        private System.Windows.Forms.Label ownerLabel;
        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.Label customerStreet;
        private System.Windows.Forms.Label customerBuilding;
        private System.Windows.Forms.Label customerApartment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label customerArea;
        private System.Windows.Forms.GroupBox customerGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label buildingStreet;
        private System.Windows.Forms.Label buildingNum;
        private System.Windows.Forms.Label buildingCollectionSector;
        private System.Windows.Forms.Label streetLabel;
        private System.Windows.Forms.Label houseLabel;
        private System.Windows.Forms.Label apartmentLabel;
        private System.Windows.Forms.Label squareLabel;
        private System.Windows.Forms.Label buildingDwellersNum;
        private System.Windows.Forms.GroupBox buildingGroupBox;
        private DevExpress.XtraGrid.Columns.GridColumn dateColumn;
        private System.Windows.Forms.TextBox counterModelTextBox;
        private System.Windows.Forms.TextBox counterNumTextBox;
        private System.Windows.Forms.CheckBox counterArchivedCheckBox;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.LookUpEdit counterServicesLookUpEdit;
    }
}