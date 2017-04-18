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
            this.chargeRuleComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Шифр";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Тип услуги";
            // 
            // serviceNameTextBox
            // 
            this.serviceNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceNameTextBox.Location = new System.Drawing.Point(120, 6);
            this.serviceNameTextBox.Name = "serviceNameTextBox";
            this.serviceNameTextBox.Size = new System.Drawing.Size(302, 20);
            this.serviceNameTextBox.TabIndex = 0;
            // 
            // serviceCodeTextBox
            // 
            this.serviceCodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceCodeTextBox.Location = new System.Drawing.Point(120, 32);
            this.serviceCodeTextBox.Name = "serviceCodeTextBox";
            this.serviceCodeTextBox.Size = new System.Drawing.Size(302, 20);
            this.serviceCodeTextBox.TabIndex = 1;
            // 
            // serviceTypeLookUpEdit
            // 
            this.serviceTypeLookUpEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceTypeLookUpEdit.Location = new System.Drawing.Point(120, 58);
            this.serviceTypeLookUpEdit.Name = "serviceTypeLookUpEdit";
            this.serviceTypeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.serviceTypeLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Наименование")});
            this.serviceTypeLookUpEdit.Properties.DisplayMember = "Name";
            this.serviceTypeLookUpEdit.Properties.ValueMember = "ID";
            this.serviceTypeLookUpEdit.Size = new System.Drawing.Size(302, 20);
            this.serviceTypeLookUpEdit.TabIndex = 2;
            this.serviceTypeLookUpEdit.Tag = "Aka";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Норматив";
            // 
            // normNumericUpDown
            // 
            this.normNumericUpDown.DecimalPlaces = 3;
            this.normNumericUpDown.Location = new System.Drawing.Point(120, 84);
            this.normNumericUpDown.Name = "normNumericUpDown";
            this.normNumericUpDown.Size = new System.Drawing.Size(70, 20);
            this.normNumericUpDown.TabIndex = 5;
            this.normNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ед. изм.";
            // 
            // normMeasureTextBox
            // 
            this.normMeasureTextBox.Location = new System.Drawing.Point(250, 83);
            this.normMeasureTextBox.MaxLength = 10;
            this.normMeasureTextBox.Name = "normMeasureTextBox";
            this.normMeasureTextBox.Size = new System.Drawing.Size(70, 20);
            this.normMeasureTextBox.TabIndex = 7;
            // 
            // chargeRuleComboBox
            // 
            this.chargeRuleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chargeRuleComboBox.FormattingEnabled = true;
            this.chargeRuleComboBox.Location = new System.Drawing.Point(120, 110);
            this.chargeRuleComboBox.Name = "chargeRuleComboBox";
            this.chargeRuleComboBox.Size = new System.Drawing.Size(302, 21);
            this.chargeRuleComboBox.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Правило начисления";
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chargeRuleComboBox);
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
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(425, 176);
            ((System.ComponentModel.ISupportInitialize)(this.serviceTypeLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serviceNameTextBox;
        private System.Windows.Forms.TextBox serviceCodeTextBox;
        private DevExpress.XtraEditors.LookUpEdit serviceTypeLookUpEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown normNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox normMeasureTextBox;
        private System.Windows.Forms.ComboBox chargeRuleComboBox;
        private System.Windows.Forms.Label label6;
    }
}
