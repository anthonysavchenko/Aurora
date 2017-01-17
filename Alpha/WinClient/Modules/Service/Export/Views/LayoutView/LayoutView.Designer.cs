namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.FormatRadioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PeriodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.FileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.ExportOperationsButton = new System.Windows.Forms.Button();
            this.ImportCustomersBrowseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.benefitProgressBar = new System.Windows.Forms.ProgressBar();
            this.benefitExportBtn = new System.Windows.Forms.Button();
            this.benefitInputFileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.benefitInputSelectFileBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gisZhkhOnlyNewRadioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.gisZhkhExportButton = new System.Windows.Forms.Button();
            this.gisZhkhInputFileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.gisZhkhSelectInputFileButton = new System.Windows.Forms.Button();
            this.gisZhkhProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormatRadioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileTextEdit.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.benefitInputFileTextEdit.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhOnlyNewRadioGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhInputFileTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.FormatRadioGroup);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.PeriodDateEdit);
            this.groupBox3.Controls.Add(this.FileTextEdit);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ExportOperationsButton);
            this.groupBox3.Controls.Add(this.ImportCustomersBrowseButton);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(554, 152);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Экпорт начислений";
            // 
            // FormatRadioGroup
            // 
            this.FormatRadioGroup.EditValue = true;
            this.FormatRadioGroup.Location = new System.Drawing.Point(119, 84);
            this.FormatRadioGroup.Name = "FormatRadioGroup";
            this.FormatRadioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Сбербанк"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Примсоцбанк")});
            this.FormatRadioGroup.Size = new System.Drawing.Size(318, 24);
            this.FormatRadioGroup.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Формат";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Учетный период";
            // 
            // PeriodDateEdit
            // 
            this.PeriodDateEdit.EditValue = null;
            this.PeriodDateEdit.Location = new System.Drawing.Point(119, 58);
            this.PeriodDateEdit.Name = "PeriodDateEdit";
            this.PeriodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.PeriodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.PeriodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.PeriodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.PeriodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.PeriodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.PeriodDateEdit.Properties.EditFormat.FormatString = "y";
            this.PeriodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.PeriodDateEdit.Properties.Mask.EditMask = "y";
            this.PeriodDateEdit.Size = new System.Drawing.Size(131, 20);
            this.PeriodDateEdit.TabIndex = 36;
            // 
            // FileTextEdit
            // 
            this.FileTextEdit.Location = new System.Drawing.Point(119, 32);
            this.FileTextEdit.Name = "FileTextEdit";
            this.FileTextEdit.Properties.Mask.EditMask = "\\d+";
            this.FileTextEdit.Properties.ReadOnly = true;
            this.FileTextEdit.Size = new System.Drawing.Size(318, 20);
            this.FileTextEdit.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Файл для экспорта";
            // 
            // ExportOperationsButton
            // 
            this.ExportOperationsButton.Location = new System.Drawing.Point(119, 114);
            this.ExportOperationsButton.Name = "ExportOperationsButton";
            this.ExportOperationsButton.Size = new System.Drawing.Size(100, 23);
            this.ExportOperationsButton.TabIndex = 33;
            this.ExportOperationsButton.Text = "Экспортировать";
            this.ExportOperationsButton.UseVisualStyleBackColor = true;
            this.ExportOperationsButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportCustomersBrowseButton
            // 
            this.ImportCustomersBrowseButton.Location = new System.Drawing.Point(443, 30);
            this.ImportCustomersBrowseButton.Name = "ImportCustomersBrowseButton";
            this.ImportCustomersBrowseButton.Size = new System.Drawing.Size(100, 23);
            this.ImportCustomersBrowseButton.TabIndex = 34;
            this.ImportCustomersBrowseButton.Text = "Выбрать...";
            this.ImportCustomersBrowseButton.UseVisualStyleBackColor = true;
            this.ImportCustomersBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.benefitProgressBar);
            this.groupBox1.Controls.Add(this.benefitExportBtn);
            this.groupBox1.Controls.Add(this.benefitInputFileTextEdit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.benefitInputSelectFileBtn);
            this.groupBox1.Location = new System.Drawing.Point(3, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 78);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Экспорт данных о льготниках";
            // 
            // benefitProgressBar
            // 
            this.benefitProgressBar.Location = new System.Drawing.Point(226, 44);
            this.benefitProgressBar.Name = "benefitProgressBar";
            this.benefitProgressBar.Size = new System.Drawing.Size(317, 23);
            this.benefitProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.benefitProgressBar.TabIndex = 40;
            this.benefitProgressBar.Visible = false;
            // 
            // benefitExportBtn
            // 
            this.benefitExportBtn.Location = new System.Drawing.Point(119, 45);
            this.benefitExportBtn.Name = "benefitExportBtn";
            this.benefitExportBtn.Size = new System.Drawing.Size(100, 23);
            this.benefitExportBtn.TabIndex = 39;
            this.benefitExportBtn.Text = "Экспортировать";
            this.benefitExportBtn.UseVisualStyleBackColor = true;
            this.benefitExportBtn.Click += new System.EventHandler(this.benefitExportBtn_Click);
            // 
            // benefitInputFileTextEdit
            // 
            this.benefitInputFileTextEdit.Location = new System.Drawing.Point(119, 19);
            this.benefitInputFileTextEdit.Name = "benefitInputFileTextEdit";
            this.benefitInputFileTextEdit.Properties.Mask.EditMask = "\\d+";
            this.benefitInputFileTextEdit.Properties.ReadOnly = true;
            this.benefitInputFileTextEdit.Size = new System.Drawing.Size(318, 20);
            this.benefitInputFileTextEdit.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Файл c данными";
            // 
            // benefitInputSelectFileBtn
            // 
            this.benefitInputSelectFileBtn.Location = new System.Drawing.Point(443, 17);
            this.benefitInputSelectFileBtn.Name = "benefitInputSelectFileBtn";
            this.benefitInputSelectFileBtn.Size = new System.Drawing.Size(100, 23);
            this.benefitInputSelectFileBtn.TabIndex = 37;
            this.benefitInputSelectFileBtn.Text = "Выбрать...";
            this.benefitInputSelectFileBtn.UseVisualStyleBackColor = true;
            this.benefitInputSelectFileBtn.Click += new System.EventHandler(this.benefitInputSelectFileBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gisZhkhProgressBar);
            this.groupBox2.Controls.Add(this.gisZhkhOnlyNewRadioGroup);
            this.groupBox2.Controls.Add(this.gisZhkhExportButton);
            this.groupBox2.Controls.Add(this.gisZhkhInputFileTextEdit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.gisZhkhSelectInputFileButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 108);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Экспорт абонентов в ГИС ЖКХ";
            // 
            // gisZhkhOnlyNewRadioGroup
            // 
            this.gisZhkhOnlyNewRadioGroup.EditValue = true;
            this.gisZhkhOnlyNewRadioGroup.Location = new System.Drawing.Point(118, 47);
            this.gisZhkhOnlyNewRadioGroup.Name = "gisZhkhOnlyNewRadioGroup";
            this.gisZhkhOnlyNewRadioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Все"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Только новые")});
            this.gisZhkhOnlyNewRadioGroup.Size = new System.Drawing.Size(318, 24);
            this.gisZhkhOnlyNewRadioGroup.TabIndex = 40;
            // 
            // gisZhkhExportButton
            // 
            this.gisZhkhExportButton.Location = new System.Drawing.Point(118, 77);
            this.gisZhkhExportButton.Name = "gisZhkhExportButton";
            this.gisZhkhExportButton.Size = new System.Drawing.Size(100, 23);
            this.gisZhkhExportButton.TabIndex = 39;
            this.gisZhkhExportButton.Text = "Экспортировать";
            this.gisZhkhExportButton.UseVisualStyleBackColor = true;
            this.gisZhkhExportButton.Click += new System.EventHandler(this.gisZhkhExportButton_Click);
            // 
            // gisZhkhInputFileTextEdit
            // 
            this.gisZhkhInputFileTextEdit.Location = new System.Drawing.Point(119, 19);
            this.gisZhkhInputFileTextEdit.Name = "gisZhkhInputFileTextEdit";
            this.gisZhkhInputFileTextEdit.Properties.Mask.EditMask = "\\d+";
            this.gisZhkhInputFileTextEdit.Properties.ReadOnly = true;
            this.gisZhkhInputFileTextEdit.Size = new System.Drawing.Size(318, 20);
            this.gisZhkhInputFileTextEdit.TabIndex = 38;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Файл";
            // 
            // gisZhkhSelectInputFileButton
            // 
            this.gisZhkhSelectInputFileButton.Location = new System.Drawing.Point(443, 17);
            this.gisZhkhSelectInputFileButton.Name = "gisZhkhSelectInputFileButton";
            this.gisZhkhSelectInputFileButton.Size = new System.Drawing.Size(100, 23);
            this.gisZhkhSelectInputFileButton.TabIndex = 37;
            this.gisZhkhSelectInputFileButton.Text = "Выбрать...";
            this.gisZhkhSelectInputFileButton.UseVisualStyleBackColor = true;
            this.gisZhkhSelectInputFileButton.Click += new System.EventHandler(this.gisZhkhSelectInputFileButton_Click);
            // 
            // gisZhkhProgressBar
            // 
            this.gisZhkhProgressBar.Location = new System.Drawing.Point(224, 77);
            this.gisZhkhProgressBar.Name = "gisZhkhProgressBar";
            this.gisZhkhProgressBar.Size = new System.Drawing.Size(317, 23);
            this.gisZhkhProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.gisZhkhProgressBar.TabIndex = 41;
            this.gisZhkhProgressBar.Visible = false;
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1062, 577);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormatRadioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileTextEdit.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.benefitInputFileTextEdit.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhOnlyNewRadioGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhInputFileTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ExportOperationsButton;
        private System.Windows.Forms.Button ImportCustomersBrowseButton;
        private DevExpress.XtraEditors.TextEdit FileTextEdit;
        private DevExpress.XtraEditors.DateEdit PeriodDateEdit;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup FormatRadioGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit benefitInputFileTextEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button benefitInputSelectFileBtn;
        private System.Windows.Forms.Button benefitExportBtn;
        private System.Windows.Forms.ProgressBar benefitProgressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.RadioGroup gisZhkhOnlyNewRadioGroup;
        private System.Windows.Forms.Button gisZhkhExportButton;
        private DevExpress.XtraEditors.TextEdit gisZhkhInputFileTextEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button gisZhkhSelectInputFileButton;
        private System.Windows.Forms.ProgressBar gisZhkhProgressBar;
    }
}
