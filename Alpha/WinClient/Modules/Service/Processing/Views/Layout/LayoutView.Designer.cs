namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout
{
    partial class LayoutView
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RenameButton = new System.Windows.Forms.Button();
            this.ChooseForRenameButton = new System.Windows.Forms.Button();
            this.DirectoryPathForRenameTextBox = new System.Windows.Forms.TextBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AnalyzeButton = new System.Windows.Forms.Button();
            this.ChooseForAnalyzeButton = new System.Windows.Forms.Button();
            this.DirectoryPathForAnalyzeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Папка с исходными файлами";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.RenameButton);
            this.groupBox1.Controls.Add(this.ChooseForRenameButton);
            this.groupBox1.Controls.Add(this.DirectoryPathForRenameTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1359, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Переименование файлов ДЭК";
            // 
            // RenameButton
            // 
            this.RenameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RenameButton.Location = new System.Drawing.Point(1219, 15);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(135, 23);
            this.RenameButton.TabIndex = 3;
            this.RenameButton.Text = "&Переименовать";
            this.RenameButton.UseVisualStyleBackColor = true;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // ChooseForRenameButton
            // 
            this.ChooseForRenameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseForRenameButton.Location = new System.Drawing.Point(1078, 15);
            this.ChooseForRenameButton.Name = "ChooseForRenameButton";
            this.ChooseForRenameButton.Size = new System.Drawing.Size(135, 23);
            this.ChooseForRenameButton.TabIndex = 3;
            this.ChooseForRenameButton.Text = "&Выбрать";
            this.ChooseForRenameButton.UseVisualStyleBackColor = true;
            this.ChooseForRenameButton.Click += new System.EventHandler(this.ChooseForRenameButton_Click);
            // 
            // DirectoryPathForRenameTextBox
            // 
            this.DirectoryPathForRenameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryPathForRenameTextBox.Location = new System.Drawing.Point(169, 17);
            this.DirectoryPathForRenameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DirectoryPathForRenameTextBox.Name = "DirectoryPathForRenameTextBox";
            this.DirectoryPathForRenameTextBox.Size = new System.Drawing.Size(904, 20);
            this.DirectoryPathForRenameTextBox.TabIndex = 2;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultTextBox.Location = new System.Drawing.Point(4, 17);
            this.ResultTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ReadOnly = true;
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(1351, 787);
            this.ResultTextBox.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ResultTextBox);
            this.groupBox2.Location = new System.Drawing.Point(2, 108);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1359, 808);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Результат выполнения обработки";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.AnalyzeButton);
            this.groupBox3.Controls.Add(this.ChooseForAnalyzeButton);
            this.groupBox3.Controls.Add(this.DirectoryPathForAnalyzeTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(2, 55);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1359, 49);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Анализ файлов ДЭК";
            // 
            // AnalyzeButton
            // 
            this.AnalyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalyzeButton.Location = new System.Drawing.Point(1219, 15);
            this.AnalyzeButton.Name = "AnalyzeButton";
            this.AnalyzeButton.Size = new System.Drawing.Size(135, 23);
            this.AnalyzeButton.TabIndex = 3;
            this.AnalyzeButton.Text = "Про&анализировать";
            this.AnalyzeButton.UseVisualStyleBackColor = true;
            this.AnalyzeButton.Click += new System.EventHandler(this.AnalyzeButton_Click);
            // 
            // ChooseForAnalyzeButton
            // 
            this.ChooseForAnalyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseForAnalyzeButton.Location = new System.Drawing.Point(1078, 15);
            this.ChooseForAnalyzeButton.Name = "ChooseForAnalyzeButton";
            this.ChooseForAnalyzeButton.Size = new System.Drawing.Size(135, 23);
            this.ChooseForAnalyzeButton.TabIndex = 3;
            this.ChooseForAnalyzeButton.Text = "В&ыбрать";
            this.ChooseForAnalyzeButton.UseVisualStyleBackColor = true;
            this.ChooseForAnalyzeButton.Click += new System.EventHandler(this.ChooseForAnalyzeButton_Click);
            // 
            // DirectoryPathForAnalyzeTextBox
            // 
            this.DirectoryPathForAnalyzeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryPathForAnalyzeTextBox.Location = new System.Drawing.Point(169, 17);
            this.DirectoryPathForAnalyzeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DirectoryPathForAnalyzeTextBox.Name = "DirectoryPathForAnalyzeTextBox";
            this.DirectoryPathForAnalyzeTextBox.Size = new System.Drawing.Size(904, 20);
            this.DirectoryPathForAnalyzeTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Папка с исходными файлами";
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1363, 918);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox DirectoryPathForRenameTextBox;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button ChooseForRenameButton;
        private System.Windows.Forms.Button RenameButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button AnalyzeButton;
        private System.Windows.Forms.Button ChooseForAnalyzeButton;
        private System.Windows.Forms.TextBox DirectoryPathForAnalyzeTextBox;
        private System.Windows.Forms.Label label1;
    }
}
