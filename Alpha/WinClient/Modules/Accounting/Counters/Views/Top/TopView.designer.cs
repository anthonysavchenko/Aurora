namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    partial class TopView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showOnlyWoPeriodValuesCheckBox = new System.Windows.Forms.CheckBox();
            this.zipCodeRadioButton = new System.Windows.Forms.RadioButton();
            this.accountRadioButton = new System.Windows.Forms.RadioButton();
            this.addressRadioButton = new System.Windows.Forms.RadioButton();
            this.zipCodeTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AccountTextBox = new System.Windows.Forms.TextBox();
            this.ToLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FromLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.printCollectFormButton = new System.Windows.Forms.Button();
            this.districtLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.ApartmentTextBox = new System.Windows.Forms.TextBox();
            this.HouseTextBox = new System.Windows.Forms.TextBox();
            this.StreetTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.districtLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.showOnlyWoPeriodValuesCheckBox);
            this.groupBox1.Controls.Add(this.zipCodeRadioButton);
            this.groupBox1.Controls.Add(this.accountRadioButton);
            this.groupBox1.Controls.Add(this.addressRadioButton);
            this.groupBox1.Controls.Add(this.zipCodeTextBox);
            this.groupBox1.Controls.Add(this.ApartmentTextBox);
            this.groupBox1.Controls.Add(this.HouseTextBox);
            this.groupBox1.Controls.Add(this.StreetTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AccountTextBox);
            this.groupBox1.Controls.Add(this.ToLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FromLabel);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 130);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтр";
            // 
            // showOnlyWoPeriodValuesCheckBox
            // 
            this.showOnlyWoPeriodValuesCheckBox.AutoSize = true;
            this.showOnlyWoPeriodValuesCheckBox.Location = new System.Drawing.Point(6, 107);
            this.showOnlyWoPeriodValuesCheckBox.Name = "showOnlyWoPeriodValuesCheckBox";
            this.showOnlyWoPeriodValuesCheckBox.Size = new System.Drawing.Size(202, 17);
            this.showOnlyWoPeriodValuesCheckBox.TabIndex = 13;
            this.showOnlyWoPeriodValuesCheckBox.Text = "Без показаний за текущий период";
            this.showOnlyWoPeriodValuesCheckBox.UseVisualStyleBackColor = true;
            // 
            // zipCodeRadioButton
            // 
            this.zipCodeRadioButton.AutoSize = true;
            this.zipCodeRadioButton.Location = new System.Drawing.Point(6, 74);
            this.zipCodeRadioButton.Name = "zipCodeRadioButton";
            this.zipCodeRadioButton.Size = new System.Drawing.Size(63, 17);
            this.zipCodeRadioButton.TabIndex = 5;
            this.zipCodeRadioButton.Text = "Индекс";
            this.zipCodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // accountRadioButton
            // 
            this.accountRadioButton.AutoSize = true;
            this.accountRadioButton.Location = new System.Drawing.Point(6, 48);
            this.accountRadioButton.Name = "accountRadioButton";
            this.accountRadioButton.Size = new System.Drawing.Size(94, 17);
            this.accountRadioButton.TabIndex = 3;
            this.accountRadioButton.Text = "Лицевой счет";
            this.accountRadioButton.UseVisualStyleBackColor = true;
            // 
            // addressRadioButton
            // 
            this.addressRadioButton.AutoSize = true;
            this.addressRadioButton.Checked = true;
            this.addressRadioButton.Location = new System.Drawing.Point(6, 22);
            this.addressRadioButton.Name = "addressRadioButton";
            this.addressRadioButton.Size = new System.Drawing.Size(56, 17);
            this.addressRadioButton.TabIndex = 0;
            this.addressRadioButton.TabStop = true;
            this.addressRadioButton.Text = "Адрес";
            this.addressRadioButton.UseVisualStyleBackColor = true;
            // 
            // zipCodeTextBox
            // 
            this.zipCodeTextBox.Location = new System.Drawing.Point(151, 73);
            this.zipCodeTextBox.Mask = "000000";
            this.zipCodeTextBox.Name = "zipCodeTextBox";
            this.zipCodeTextBox.PromptChar = ' ';
            this.zipCodeTextBox.Size = new System.Drawing.Size(148, 20);
            this.zipCodeTextBox.TabIndex = 5;
            this.zipCodeTextBox.Enter += new System.EventHandler(this.zipCodeTextBox_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Кв.";
            // 
            // AccountTextBox
            // 
            this.AccountTextBox.Location = new System.Drawing.Point(151, 47);
            this.AccountTextBox.Name = "AccountTextBox";
            this.AccountTextBox.Size = new System.Drawing.Size(148, 20);
            this.AccountTextBox.TabIndex = 4;
            this.AccountTextBox.Enter += new System.EventHandler(this.AccountTextBox_Enter);
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Location = new System.Drawing.Point(309, 25);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(30, 13);
            this.ToLabel.TabIndex = 6;
            this.ToLabel.Text = "Дом";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "№";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "№";
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Location = new System.Drawing.Point(110, 24);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(39, 13);
            this.FromLabel.TabIndex = 5;
            this.FromLabel.Text = "Улица";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.printCollectFormButton);
            this.groupBox2.Controls.Add(this.districtLookUpEdit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(478, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 130);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Форма сбора показаний";
            // 
            // printCollectFormButton
            // 
            this.printCollectFormButton.Location = new System.Drawing.Point(173, 98);
            this.printCollectFormButton.Name = "printCollectFormButton";
            this.printCollectFormButton.Size = new System.Drawing.Size(75, 23);
            this.printCollectFormButton.TabIndex = 9;
            this.printCollectFormButton.Text = "Печать";
            this.printCollectFormButton.UseVisualStyleBackColor = true;
            this.printCollectFormButton.Click += new System.EventHandler(this.printCollectFormButton_Click);
            // 
            // districtLookUpEdit
            // 
            this.districtLookUpEdit.Location = new System.Drawing.Point(61, 21);
            this.districtLookUpEdit.Name = "districtLookUpEdit";
            this.districtLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.districtLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Наименование")});
            this.districtLookUpEdit.Properties.DisplayMember = "Name";
            this.districtLookUpEdit.Properties.NullText = "<Введите значение>";
            this.districtLookUpEdit.Properties.ValueMember = "ID";
            this.districtLookUpEdit.Size = new System.Drawing.Size(187, 20);
            this.districtLookUpEdit.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Участок";
            // 
            // ApartmentTextBox
            // 
            this.ApartmentTextBox.Location = new System.Drawing.Point(416, 21);
            this.ApartmentTextBox.Name = "ApartmentTextBox";
            this.ApartmentTextBox.Size = new System.Drawing.Size(40, 20);
            this.ApartmentTextBox.TabIndex = 3;
            this.ApartmentTextBox.Enter += new System.EventHandler(this.HouseTextBox_Enter);
            // 
            // HouseTextBox
            // 
            this.HouseTextBox.Location = new System.Drawing.Point(341, 21);
            this.HouseTextBox.Name = "HouseTextBox";
            this.HouseTextBox.Size = new System.Drawing.Size(40, 20);
            this.HouseTextBox.TabIndex = 2;
            this.HouseTextBox.Enter += new System.EventHandler(this.HouseTextBox_Enter);
            // 
            // StreetTextBox
            // 
            this.StreetTextBox.Location = new System.Drawing.Point(151, 21);
            this.StreetTextBox.Name = "StreetTextBox";
            this.StreetTextBox.Size = new System.Drawing.Size(148, 20);
            this.StreetTextBox.TabIndex = 1;
            this.StreetTextBox.Enter += new System.EventHandler(this.StreetTextBox_Enter);
            // 
            // TopView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TopView";
            this.Size = new System.Drawing.Size(732, 130);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.districtLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox AccountTextBox;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox zipCodeTextBox;
        private System.Windows.Forms.RadioButton addressRadioButton;
        private System.Windows.Forms.RadioButton zipCodeRadioButton;
        private System.Windows.Forms.RadioButton accountRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox showOnlyWoPeriodValuesCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button printCollectFormButton;
        private DevExpress.XtraEditors.LookUpEdit districtLookUpEdit;
        private System.Windows.Forms.TextBox ApartmentTextBox;
        private System.Windows.Forms.TextBox HouseTextBox;
        private System.Windows.Forms.TextBox StreetTextBox;
    }
}

