namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView
{
	partial class StatusView {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
            this._statusProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // _statusProgressBar
            // 
            this._statusProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._statusProgressBar.Location = new System.Drawing.Point(3, 3);
            this._statusProgressBar.MarqueeAnimationSpeed = 50;
            this._statusProgressBar.Name = "_statusProgressBar";
            this._statusProgressBar.Size = new System.Drawing.Size(607, 18);
            this._statusProgressBar.TabIndex = 0;
            // 
            // StatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._statusProgressBar);
            this.Name = "StatusView";
            this.Size = new System.Drawing.Size(613, 24);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ProgressBar _statusProgressBar;

    }
}
