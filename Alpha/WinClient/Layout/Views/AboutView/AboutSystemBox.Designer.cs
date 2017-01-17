namespace Taumis.Infrastructure.Layout
{
    partial class AboutSystemBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DeveloperLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(247, 127);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "OK";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(53, 22);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.TitleLabel.MaximumSize = new System.Drawing.Size(0, 17);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(92, 13);
            this.TitleLabel.TabIndex = 20;
            this.TitleLabel.Text = "Aurora 000.00.0.0";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Image = global::Taumis.Alpha.WinClient.Aurora.Layout.Properties.Resources.Logo_32x32;
            this.LogoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(32, 32);
            this.LogoPictureBox.TabIndex = 25;
            this.LogoPictureBox.TabStop = false;
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(56, 50);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ReadOnly = true;
            this.DescriptionTextBox.Size = new System.Drawing.Size(266, 71);
            this.DescriptionTextBox.TabIndex = 26;
            // 
            // DeveloperLinkLabel
            // 
            this.DeveloperLinkLabel.AutoSize = true;
            this.DeveloperLinkLabel.Location = new System.Drawing.Point(53, 132);
            this.DeveloperLinkLabel.Name = "DeveloperLinkLabel";
            this.DeveloperLinkLabel.Size = new System.Drawing.Size(49, 13);
            this.DeveloperLinkLabel.TabIndex = 27;
            this.DeveloperLinkLabel.TabStop = true;
            this.DeveloperLinkLabel.Text = "taumis.ru";
            this.DeveloperLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DeveloperLinkLabel_LinkClicked);
            // 
            // AboutSystemBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 162);
            this.Controls.Add(this.DeveloperLinkLabel);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.LogoPictureBox);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutSystemBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О системе";
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.LinkLabel DeveloperLinkLabel;
    }
}
