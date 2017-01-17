namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Item
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
            this.fixedRuleRadioButton = new System.Windows.Forms.RadioButton();
            this.squareRuleRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.providingRuleGroupBox = new System.Windows.Forms.GroupBox();
            this.beneitPercentValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.percentTrackBar = new System.Windows.Forms.TrackBar();
            this.chargeRuleGroupBox.SuspendLayout();
            this.providingRuleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // chargeRuleGroupBox
            // 
            this.chargeRuleGroupBox.Controls.Add(this.fixedRuleRadioButton);
            this.chargeRuleGroupBox.Controls.Add(this.squareRuleRadioButton);
            this.chargeRuleGroupBox.Location = new System.Drawing.Point(3, 65);
            this.chargeRuleGroupBox.Name = "chargeRuleGroupBox";
            this.chargeRuleGroupBox.Size = new System.Drawing.Size(396, 68);
            this.chargeRuleGroupBox.TabIndex = 3;
            this.chargeRuleGroupBox.TabStop = false;
            this.chargeRuleGroupBox.Text = "Правило начисления";
            // 
            // fixedRuleRadioButton
            // 
            this.fixedRuleRadioButton.AutoSize = true;
            this.fixedRuleRadioButton.Location = new System.Drawing.Point(6, 43);
            this.fixedRuleRadioButton.Name = "fixedRuleRadioButton";
            this.fixedRuleRadioButton.Size = new System.Drawing.Size(209, 17);
            this.fixedRuleRadioButton.TabIndex = 1;
            this.fixedRuleRadioButton.Text = "Льгота в фиксированных процентах";
            this.fixedRuleRadioButton.UseVisualStyleBackColor = true;
            this.fixedRuleRadioButton.CheckedChanged += new System.EventHandler(this.fixedRuleRadioButton_CheckedChanged);
            // 
            // squareRuleRadioButton
            // 
            this.squareRuleRadioButton.AutoSize = true;
            this.squareRuleRadioButton.Checked = true;
            this.squareRuleRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.squareRuleRadioButton.Location = new System.Drawing.Point(6, 19);
            this.squareRuleRadioButton.Name = "squareRuleRadioButton";
            this.squareRuleRadioButton.Size = new System.Drawing.Size(383, 17);
            this.squareRuleRadioButton.TabIndex = 0;
            this.squareRuleRadioButton.TabStop = true;
            this.squareRuleRadioButton.Text = "Льгота 50% по норме площади в зависимости от количества жильцов";
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
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(72, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(200, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // codeTextBox
            // 
            this.codeTextBox.Location = new System.Drawing.Point(72, 29);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(200, 20);
            this.codeTextBox.TabIndex = 1;
            // 
            // providingRuleGroupBox
            // 
            this.providingRuleGroupBox.Controls.Add(this.beneitPercentValueLabel);
            this.providingRuleGroupBox.Controls.Add(this.label5);
            this.providingRuleGroupBox.Controls.Add(this.label4);
            this.providingRuleGroupBox.Controls.Add(this.label3);
            this.providingRuleGroupBox.Controls.Add(this.percentTrackBar);
            this.providingRuleGroupBox.Enabled = false;
            this.providingRuleGroupBox.Location = new System.Drawing.Point(3, 140);
            this.providingRuleGroupBox.Name = "providingRuleGroupBox";
            this.providingRuleGroupBox.Size = new System.Drawing.Size(396, 95);
            this.providingRuleGroupBox.TabIndex = 4;
            this.providingRuleGroupBox.TabStop = false;
            this.providingRuleGroupBox.Text = "Процент льготы";
            // 
            // beneitPercentValueLabel
            // 
            this.beneitPercentValueLabel.AutoSize = true;
            this.beneitPercentValueLabel.Location = new System.Drawing.Point(10, 21);
            this.beneitPercentValueLabel.Name = "beneitPercentValueLabel";
            this.beneitPercentValueLabel.Size = new System.Drawing.Size(21, 13);
            this.beneitPercentValueLabel.TabIndex = 4;
            this.beneitPercentValueLabel.Text = "0%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "100%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "50%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "0";
            // 
            // percentTrackBar
            // 
            this.percentTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.percentTrackBar.AutoSize = false;
            this.percentTrackBar.LargeChange = 10;
            this.percentTrackBar.Location = new System.Drawing.Point(3, 39);
            this.percentTrackBar.Maximum = 100;
            this.percentTrackBar.Name = "percentTrackBar";
            this.percentTrackBar.Size = new System.Drawing.Size(390, 35);
            this.percentTrackBar.TabIndex = 0;
            this.percentTrackBar.TickFrequency = 10;
            this.percentTrackBar.ValueChanged += new System.EventHandler(this.percentTrackBar_ValueChanged);
            // 
            // ItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.providingRuleGroupBox);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chargeRuleGroupBox);
            this.Name = "ItemView";
            this.Size = new System.Drawing.Size(425, 251);
            this.chargeRuleGroupBox.ResumeLayout(false);
            this.chargeRuleGroupBox.PerformLayout();
            this.providingRuleGroupBox.ResumeLayout(false);
            this.providingRuleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox chargeRuleGroupBox;
        private System.Windows.Forms.RadioButton fixedRuleRadioButton;
        private System.Windows.Forms.RadioButton squareRuleRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.GroupBox providingRuleGroupBox;
        private System.Windows.Forms.TrackBar percentTrackBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label beneitPercentValueLabel;
    }
}
