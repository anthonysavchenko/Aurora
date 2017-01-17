namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
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
            if (disposing && (components != null))
            {
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
            this.chargeRuleGroupBox = new System.Windows.Forms.GroupBox();
            this.publicPlaceRadioButton = new System.Windows.Forms.RadioButton();
            this.counterRuleRadioButton = new System.Windows.Forms.RadioButton();
            this.ResidentsRateRadioButton = new System.Windows.Forms.RadioButton();
            this.fixedRuleRadioButton = new System.Windows.Forms.RadioButton();
            this.squareRuleRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serviceNameTextBox = new System.Windows.Forms.TextBox();
            this.serviceCodeTextBox = new System.Windows.Forms.TextBox();
            this.serviceTypeLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.normNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.normMeasureTextBox = new System.Windows.Forms.TextBox();
            this.chargeRuleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // chargeRuleGroupBox
            // 
            this.chargeRuleGroupBox.Controls.Add(this.publicPlaceRadioButton);
            this.chargeRuleGroupBox.Controls.Add(this.counterRuleRadioButton);
            this.chargeRuleGroupBox.Controls.Add(this.ResidentsRateRadioButton);
            this.chargeRuleGroupBox.Controls.Add(this.fixedRuleRadioButton);
            this.chargeRuleGroupBox.Controls.Add(this.squareRuleRadioButton);
            this.chargeRuleGroupBox.Location = new System.Drawing.Point(3, 107);
            this.chargeRuleGroupBox.Name = "chargeRuleGroupBox";
            this.chargeRuleGroupBox.Size = new System.Drawing.Size(419, 136);
            this.chargeRuleGroupBox.TabIndex = 3;
            this.chargeRuleGroupBox.TabStop = false;
            this.chargeRuleGroupBox.Text = "Правило начисления";
            // 
            // publicPlaceRadioButton
            // 
            this.publicPlaceRadioButton.AutoSize = true;
            this.publicPlaceRadioButton.Location = new System.Drawing.Point(6, 112);
            this.publicPlaceRadioButton.Name = "publicPlaceRadioButton";
            this.publicPlaceRadioButton.Size = new System.Drawing.Size(213, 17);
            this.publicPlaceRadioButton.TabIndex = 7;
            this.publicPlaceRadioButton.Text = "Содержание общедового имущества";
            this.publicPlaceRadioButton.UseVisualStyleBackColor = true;
            this.publicPlaceRadioButton.CheckedChanged += new System.EventHandler(this.publicPlaceRadioButton_CheckedChanged);
            // 
            // counterRuleRadioButton
            // 
            this.counterRuleRadioButton.AutoSize = true;
            this.counterRuleRadioButton.Location = new System.Drawing.Point(6, 89);
            this.counterRuleRadioButton.Name = "counterRuleRadioButton";
            this.counterRuleRadioButton.Size = new System.Drawing.Size(86, 17);
            this.counterRuleRadioButton.TabIndex = 6;
            this.counterRuleRadioButton.Text = "По счетчику";
            this.counterRuleRadioButton.UseVisualStyleBackColor = true;
            this.counterRuleRadioButton.CheckedChanged += new System.EventHandler(this.counterRuleRadioButton_CheckedChanged);
            // 
            // ResidentsRateRadioButton
            // 
            this.ResidentsRateRadioButton.AutoSize = true;
            this.ResidentsRateRadioButton.Location = new System.Drawing.Point(6, 66);
            this.ResidentsRateRadioButton.Name = "ResidentsRateRadioButton";
            this.ResidentsRateRadioButton.Size = new System.Drawing.Size(201, 17);
            this.ResidentsRateRadioButton.TabIndex = 5;
            this.ResidentsRateRadioButton.Text = "По тарифу на количество жильцов";
            this.ResidentsRateRadioButton.UseVisualStyleBackColor = true;
            // 
            // fixedRuleRadioButton
            // 
            this.fixedRuleRadioButton.AutoSize = true;
            this.fixedRuleRadioButton.Checked = true;
            this.fixedRuleRadioButton.Location = new System.Drawing.Point(6, 19);
            this.fixedRuleRadioButton.Name = "fixedRuleRadioButton";
            this.fixedRuleRadioButton.Size = new System.Drawing.Size(108, 17);
            this.fixedRuleRadioButton.TabIndex = 3;
            this.fixedRuleRadioButton.TabStop = true;
            this.fixedRuleRadioButton.Text = "Фиксированное";
            this.fixedRuleRadioButton.UseVisualStyleBackColor = true;
            // 
            // squareRuleRadioButton
            // 
            this.squareRuleRadioButton.AutoSize = true;
            this.squareRuleRadioButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.squareRuleRadioButton.Location = new System.Drawing.Point(6, 43);
            this.squareRuleRadioButton.Name = "squareRuleRadioButton";
            this.squareRuleRadioButton.Size = new System.Drawing.Size(125, 17);
            this.squareRuleRadioButton.TabIndex = 4;
            this.squareRuleRadioButton.Text = "По тарифу за кв. м.";
            this.squareRuleRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Шифр";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Тип услуги";
            // 
            // serviceNameTextBox
            // 
            this.serviceNameTextBox.Location = new System.Drawing.Point(72, 3);
            this.serviceNameTextBox.Name = "serviceNameTextBox";
            this.serviceNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.serviceNameTextBox.TabIndex = 0;
            // 
            // serviceCodeTextBox
            // 
            this.serviceCodeTextBox.Location = new System.Drawing.Point(72, 29);
            this.serviceCodeTextBox.Name = "serviceCodeTextBox";
            this.serviceCodeTextBox.Size = new System.Drawing.Size(200, 20);
            this.serviceCodeTextBox.TabIndex = 1;
            // 
            // serviceTypeLookUpEdit
            // 
            this.serviceTypeLookUpEdit.Location = new System.Drawing.Point(72, 55);
            this.serviceTypeLookUpEdit.Name = "serviceTypeLookUpEdit";
            this.serviceTypeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.serviceTypeLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Наименование")});
            this.serviceTypeLookUpEdit.Properties.DisplayMember = "Name";
            this.serviceTypeLookUpEdit.Properties.ValueMember = "ID";
            this.serviceTypeLookUpEdit.Size = new System.Drawing.Size(200, 20);
            this.serviceTypeLookUpEdit.TabIndex = 2;
            this.serviceTypeLookUpEdit.Tag = "Aka";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Норматив";
            // 
            // normNumericUpDown
            // 
            this.normNumericUpDown.DecimalPlaces = 3;
            this.normNumericUpDown.Location = new System.Drawing.Point(72, 81);
            this.normNumericUpDown.Name = "normNumericUpDown";
            this.normNumericUpDown.Size = new System.Drawing.Size(70, 20);
            this.normNumericUpDown.TabIndex = 5;
            this.normNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ед. изм.";
            // 
            // normMeasureTextBox
            // 
            this.normMeasureTextBox.Location = new System.Drawing.Point(202, 80);
            this.normMeasureTextBox.MaxLength = 10;
            this.normMeasureTextBox.Name = "normMeasureTextBox";
            this.normMeasureTextBox.Size = new System.Drawing.Size(70, 20);
            this.normMeasureTextBox.TabIndex = 7;
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.normMeasureTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.normNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serviceTypeLookUpEdit);
            this.Controls.Add(this.serviceCodeTextBox);
            this.Controls.Add(this.serviceNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chargeRuleGroupBox);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(425, 252);
            this.chargeRuleGroupBox.ResumeLayout(false);
            this.chargeRuleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox chargeRuleGroupBox;
        private System.Windows.Forms.RadioButton counterRuleRadioButton;
        private System.Windows.Forms.RadioButton fixedRuleRadioButton;
        private System.Windows.Forms.RadioButton squareRuleRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serviceNameTextBox;
        private System.Windows.Forms.TextBox serviceCodeTextBox;
        private DevExpress.XtraEditors.LookUpEdit serviceTypeLookUpEdit;
        private System.Windows.Forms.RadioButton ResidentsRateRadioButton;
        private System.Windows.Forms.RadioButton publicPlaceRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown normNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox normMeasureTextBox;
    }
}
