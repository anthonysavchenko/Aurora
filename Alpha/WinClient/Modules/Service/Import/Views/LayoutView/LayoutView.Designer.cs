namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
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
            this.ImportButton = new System.Windows.Forms.Button();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.FileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.FileLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.IsPrivateCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.RateSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.ContractorLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ServiceLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.IsPrivateLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AddServicesForCustomersButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ImportCustomersImportButton = new System.Windows.Forms.Button();
            this.ImportCustomersFileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.ImportCustomersBrowseButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.smartPartPlaceholder1 = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gisZhkhProgressBar = new System.Windows.Forms.ProgressBar();
            this.gisZhkhImportButton = new System.Windows.Forms.Button();
            this.gisZhkhInputFileTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.gisZhkhSelectInputFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FileTextEdit.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsPrivateCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceLookUpEdit.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImportCustomersFileTextEdit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhInputFileTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(443, 77);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(100, 23);
            this.ImportButton.TabIndex = 33;
            this.ImportButton.Text = "Импортировать";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(443, 48);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(100, 23);
            this.BrowseButton.TabIndex = 34;
            this.BrowseButton.Text = "Выбрать...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // FileTextEdit
            // 
            this.FileTextEdit.Location = new System.Drawing.Point(6, 50);
            this.FileTextEdit.Name = "FileTextEdit";
            this.FileTextEdit.Properties.Mask.EditMask = "\\d+";
            this.FileTextEdit.Properties.ReadOnly = true;
            this.FileTextEdit.Size = new System.Drawing.Size(431, 20);
            this.FileTextEdit.TabIndex = 32;
            // 
            // FileLabel
            // 
            this.FileLabel.AutoSize = true;
            this.FileLabel.Location = new System.Drawing.Point(6, 35);
            this.FileLabel.Name = "FileLabel";
            this.FileLabel.Size = new System.Drawing.Size(228, 13);
            this.FileLabel.TabIndex = 31;
            this.FileLabel.Text = "Файл с таблицей адресов, услуг и тарифов";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FileLabel);
            this.groupBox1.Controls.Add(this.ImportButton);
            this.groupBox1.Controls.Add(this.FileTextEdit);
            this.groupBox1.Controls.Add(this.BrowseButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 118);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление услуг и тарифов по адресам";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.IsPrivateCheckEdit);
            this.groupBox2.Controls.Add(this.RateSpinEdit);
            this.groupBox2.Controls.Add(this.ContractorLookUpEdit);
            this.groupBox2.Controls.Add(this.ServiceLookUpEdit);
            this.groupBox2.Controls.Add(this.IsPrivateLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.AddServicesForCustomersButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 174);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Добавление услуг и тарифов всем абонентам";
            // 
            // IsPrivateCheckEdit
            // 
            this.IsPrivateCheckEdit.Location = new System.Drawing.Point(229, 110);
            this.IsPrivateCheckEdit.Name = "IsPrivateCheckEdit";
            this.IsPrivateCheckEdit.Properties.Caption = "";
            this.IsPrivateCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.IsPrivateCheckEdit.TabIndex = 36;
            // 
            // RateSpinEdit
            // 
            this.RateSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.RateSpinEdit.Location = new System.Drawing.Point(231, 84);
            this.RateSpinEdit.Name = "RateSpinEdit";
            this.RateSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.RateSpinEdit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RateSpinEdit.Properties.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.RateSpinEdit.Size = new System.Drawing.Size(63, 20);
            this.RateSpinEdit.TabIndex = 35;
            // 
            // ContractorLookUpEdit
            // 
            this.ContractorLookUpEdit.Location = new System.Drawing.Point(231, 58);
            this.ContractorLookUpEdit.Name = "ContractorLookUpEdit";
            this.ContractorLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ContractorLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.ContractorLookUpEdit.Properties.DisplayMember = "Name";
            this.ContractorLookUpEdit.Properties.NullText = "<Введите значение>";
            this.ContractorLookUpEdit.Properties.ValueMember = "ID";
            this.ContractorLookUpEdit.Size = new System.Drawing.Size(312, 20);
            this.ContractorLookUpEdit.TabIndex = 34;
            // 
            // ServiceLookUpEdit
            // 
            this.ServiceLookUpEdit.Location = new System.Drawing.Point(231, 32);
            this.ServiceLookUpEdit.Name = "ServiceLookUpEdit";
            this.ServiceLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ServiceLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.ServiceLookUpEdit.Properties.DisplayMember = "Name";
            this.ServiceLookUpEdit.Properties.NullText = "<Введите значение>";
            this.ServiceLookUpEdit.Properties.ValueMember = "ID";
            this.ServiceLookUpEdit.Size = new System.Drawing.Size(312, 20);
            this.ServiceLookUpEdit.TabIndex = 34;
            // 
            // IsPrivateLabel
            // 
            this.IsPrivateLabel.AutoSize = true;
            this.IsPrivateLabel.Location = new System.Drawing.Point(6, 113);
            this.IsPrivateLabel.Name = "IsPrivateLabel";
            this.IsPrivateLabel.Size = new System.Drawing.Size(197, 13);
            this.IsPrivateLabel.TabIndex = 31;
            this.IsPrivateLabel.Text = "Только для квартир в собственности";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Тариф";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Подрядчик";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Услуга";
            // 
            // AddServicesForCustomersButton
            // 
            this.AddServicesForCustomersButton.Location = new System.Drawing.Point(231, 135);
            this.AddServicesForCustomersButton.Name = "AddServicesForCustomersButton";
            this.AddServicesForCustomersButton.Size = new System.Drawing.Size(100, 23);
            this.AddServicesForCustomersButton.TabIndex = 33;
            this.AddServicesForCustomersButton.Text = "Добавить";
            this.AddServicesForCustomersButton.UseVisualStyleBackColor = true;
            this.AddServicesForCustomersButton.Click += new System.EventHandler(this.AddServicesForCustomersButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ImportCustomersImportButton);
            this.groupBox3.Controls.Add(this.ImportCustomersFileTextEdit);
            this.groupBox3.Controls.Add(this.ImportCustomersBrowseButton);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(554, 119);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Добавление абонентов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Файл с таблицей абонентов";
            // 
            // ImportCustomersImportButton
            // 
            this.ImportCustomersImportButton.Location = new System.Drawing.Point(443, 77);
            this.ImportCustomersImportButton.Name = "ImportCustomersImportButton";
            this.ImportCustomersImportButton.Size = new System.Drawing.Size(100, 23);
            this.ImportCustomersImportButton.TabIndex = 33;
            this.ImportCustomersImportButton.Text = "Импортировать";
            this.ImportCustomersImportButton.UseVisualStyleBackColor = true;
            this.ImportCustomersImportButton.Click += new System.EventHandler(this.ImportCustomersImportButton_Click);
            // 
            // ImportCustomersFileTextEdit
            // 
            this.ImportCustomersFileTextEdit.Location = new System.Drawing.Point(6, 50);
            this.ImportCustomersFileTextEdit.Name = "ImportCustomersFileTextEdit";
            this.ImportCustomersFileTextEdit.Properties.Mask.EditMask = "\\d+";
            this.ImportCustomersFileTextEdit.Properties.ReadOnly = true;
            this.ImportCustomersFileTextEdit.Size = new System.Drawing.Size(431, 20);
            this.ImportCustomersFileTextEdit.TabIndex = 32;
            // 
            // ImportCustomersBrowseButton
            // 
            this.ImportCustomersBrowseButton.Location = new System.Drawing.Point(443, 48);
            this.ImportCustomersBrowseButton.Name = "ImportCustomersBrowseButton";
            this.ImportCustomersBrowseButton.Size = new System.Drawing.Size(100, 23);
            this.ImportCustomersBrowseButton.TabIndex = 34;
            this.ImportCustomersBrowseButton.Text = "Выбрать...";
            this.ImportCustomersBrowseButton.UseVisualStyleBackColor = true;
            this.ImportCustomersBrowseButton.Click += new System.EventHandler(this.ImportCustomersBrowseButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 432);
            this.panel1.TabIndex = 38;
            // 
            // smartPartPlaceholder1
            // 
            this.smartPartPlaceholder1.BackColor = System.Drawing.Color.Transparent;
            this.smartPartPlaceholder1.Location = new System.Drawing.Point(0, 438);
            this.smartPartPlaceholder1.Name = "smartPartPlaceholder1";
            this.smartPartPlaceholder1.Size = new System.Drawing.Size(560, 80);
            this.smartPartPlaceholder1.SmartPartName = "MigrationView";
            this.smartPartPlaceholder1.TabIndex = 40;
            this.smartPartPlaceholder1.Text = "smartPartPlaceholder1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gisZhkhProgressBar);
            this.groupBox4.Controls.Add(this.gisZhkhImportButton);
            this.groupBox4.Controls.Add(this.gisZhkhInputFileTextEdit);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.gisZhkhSelectInputFileButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 524);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(554, 81);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Импорт абонентов из ГИС ЖКХ";
            // 
            // gisZhkhProgressBar
            // 
            this.gisZhkhProgressBar.Location = new System.Drawing.Point(225, 45);
            this.gisZhkhProgressBar.Name = "gisZhkhProgressBar";
            this.gisZhkhProgressBar.Size = new System.Drawing.Size(317, 23);
            this.gisZhkhProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.gisZhkhProgressBar.TabIndex = 41;
            this.gisZhkhProgressBar.Visible = false;
            // 
            // gisZhkhImportButton
            // 
            this.gisZhkhImportButton.Location = new System.Drawing.Point(119, 45);
            this.gisZhkhImportButton.Name = "gisZhkhImportButton";
            this.gisZhkhImportButton.Size = new System.Drawing.Size(100, 23);
            this.gisZhkhImportButton.TabIndex = 39;
            this.gisZhkhImportButton.Text = "Импортировать";
            this.gisZhkhImportButton.UseVisualStyleBackColor = true;
            this.gisZhkhImportButton.Click += new System.EventHandler(this.gisZhkhImportButton_Click);
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
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.smartPartPlaceholder1);
            this.Controls.Add(this.panel1);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(980, 577);
            ((System.ComponentModel.ISupportInitialize)(this.FileTextEdit.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IsPrivateCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractorLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceLookUpEdit.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImportCustomersFileTextEdit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gisZhkhInputFileTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button BrowseButton;
        private DevExpress.XtraEditors.TextEdit FileTextEdit;
        private System.Windows.Forms.Label FileLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddServicesForCustomersButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ImportCustomersImportButton;
        private System.Windows.Forms.Button ImportCustomersBrowseButton;
        private DevExpress.XtraEditors.LookUpEdit ServiceLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit ContractorLookUpEdit;
        private DevExpress.XtraEditors.SpinEdit RateSpinEdit;
        private System.Windows.Forms.Label IsPrivateLabel;
        private DevExpress.XtraEditors.CheckEdit IsPrivateCheckEdit;
        private DevExpress.XtraEditors.TextEdit ImportCustomersFileTextEdit;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder smartPartPlaceholder1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ProgressBar gisZhkhProgressBar;
        private System.Windows.Forms.Button gisZhkhImportButton;
        private DevExpress.XtraEditors.TextEdit gisZhkhInputFileTextEdit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button gisZhkhSelectInputFileButton;
    }
}
