namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration
{
    partial class MigrationView
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.stateLabel = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.migrationProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.stateLabel);
            this.groupBox3.Controls.Add(this.goButton);
            this.groupBox3.Controls.Add(this.migrationProgressBar);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 73);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Миграция";
            // 
            // stateLabel
            // 
            this.stateLabel.Location = new System.Drawing.Point(6, 45);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(434, 15);
            this.stateLabel.TabIndex = 2;
            this.stateLabel.Text = "Состояние";
            this.stateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(446, 19);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // migrationProgressBar
            // 
            this.migrationProgressBar.Location = new System.Drawing.Point(6, 19);
            this.migrationProgressBar.Maximum = 10000;
            this.migrationProgressBar.Name = "migrationProgressBar";
            this.migrationProgressBar.Size = new System.Drawing.Size(434, 23);
            this.migrationProgressBar.Step = 1;
            this.migrationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.migrationProgressBar.TabIndex = 0;
            // 
            // MigrationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "MigrationView";
            this.Size = new System.Drawing.Size(533, 82);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ProgressBar migrationProgressBar;
    }
}