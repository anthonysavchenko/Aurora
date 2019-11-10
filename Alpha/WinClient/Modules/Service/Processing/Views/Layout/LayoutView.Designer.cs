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
            this.ChooseButton = new System.Windows.Forms.Button();
            this.DirectoryPathTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.ChooseButton);
            this.groupBox1.Controls.Add(this.DirectoryPathTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(658, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Переименование файлов ДЭК";
            // 
            // ChooseButton
            // 
            this.ChooseButton.Location = new System.Drawing.Point(578, 15);
            this.ChooseButton.Name = "ChooseButton";
            this.ChooseButton.Size = new System.Drawing.Size(75, 23);
            this.ChooseButton.TabIndex = 3;
            this.ChooseButton.Text = "&Выбрать";
            this.ChooseButton.UseVisualStyleBackColor = true;
            this.ChooseButton.Click += new System.EventHandler(this.ChooseButton_Click);
            // 
            // DirectoryPathTextBox
            // 
            this.DirectoryPathTextBox.Location = new System.Drawing.Point(169, 17);
            this.DirectoryPathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DirectoryPathTextBox.Name = "DirectoryPathTextBox";
            this.DirectoryPathTextBox.Size = new System.Drawing.Size(404, 20);
            this.DirectoryPathTextBox.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 69);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Результат выполнения обработки";
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(2, 84);
            this.ResultTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ReadOnly = true;
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(658, 280);
            this.ResultTextBox.TabIndex = 5;
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(580, 56);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessButton.TabIndex = 3;
            this.ProcessButton.Text = "&Обработать";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1363, 918);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox DirectoryPathTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button ChooseButton;
        private System.Windows.Forms.Button ProcessButton;
    }
}
