namespace Taumis.EnterpriseLibrary.Win.Forms
{
    partial class ReasonForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._reasonText = new DevExpress.XtraEditors.MemoEdit();
            this._lblInfo = new DevExpress.XtraEditors.LabelControl();
            this._btnOk = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._reasonText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _reasonText
            // 
            this._reasonText.Location = new System.Drawing.Point(12, 32);
            this._reasonText.Name = "_reasonText";
            this._reasonText.Size = new System.Drawing.Size(368, 96);
            this._reasonText.TabIndex = 0;
            this._reasonText.TextChanged += new System.EventHandler(this._reasonText_TextChanged);
            // 
            // _lblInfo
            // 
            this._lblInfo.Location = new System.Drawing.Point(12, 13);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(94, 13);
            this._lblInfo.TabIndex = 3;
            this._lblInfo.Text = "Укажите причину:";
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Enabled = false;
            this._btnOk.Location = new System.Drawing.Point(174, 151);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(100, 23);
            this._btnOk.TabIndex = 4;
            this._btnOk.Text = "Ok";
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(280, 151);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ReasonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 186);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this._btnOk);
            this.Controls.Add(this._lblInfo);
            this.Controls.Add(this._reasonText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ReasonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Причина";
            ((System.ComponentModel.ISupportInitialize)(this._reasonText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit _reasonText;
        private DevExpress.XtraEditors.LabelControl _lblInfo;
        private System.Windows.Forms.Button _btnOk;
        private new System.Windows.Forms.Button CancelButton;
    }
}