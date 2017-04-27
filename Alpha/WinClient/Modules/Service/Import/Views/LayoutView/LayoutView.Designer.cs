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
            this.ImportWizardControl = new DevExpress.XtraWizard.WizardControl();
            this.ProcessingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.progressProcentLabel = new System.Windows.Forms.Label();
            this.ChooseMethodWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.importNewCustomersRadioButton = new System.Windows.Forms.RadioButton();
            this.importGisZhkhCustomerIDsRadioButton = new System.Windows.Forms.RadioButton();
            this.importCustomerPosesRadioButton = new System.Windows.Forms.RadioButton();
            this.FileWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.label7 = new System.Windows.Forms.Label();
            this.filePathTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImportWizardControl)).BeginInit();
            this.ImportWizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            this.ChooseMethodWizardPage.SuspendLayout();
            this.FileWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePathTextEdit.Properties)).BeginInit();
            this.FinishWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportWizardControl
            // 
            this.ImportWizardControl.CancelText = "Отмена";
            this.ImportWizardControl.Controls.Add(this.ProcessingWizardPage);
            this.ImportWizardControl.Controls.Add(this.ChooseMethodWizardPage);
            this.ImportWizardControl.Controls.Add(this.FileWizardPage);
            this.ImportWizardControl.Controls.Add(this.FinishWizardPage);
            this.ImportWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportWizardControl.FinishText = "Завершить";
            this.ImportWizardControl.Location = new System.Drawing.Point(0, 0);
            this.ImportWizardControl.Name = "ImportWizardControl";
            this.ImportWizardControl.NextText = "&Далее>";
            this.ImportWizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.ChooseMethodWizardPage,
            this.FileWizardPage,
            this.ProcessingWizardPage,
            this.FinishWizardPage});
            this.ImportWizardControl.PreviousText = "< &Назад";
            this.ImportWizardControl.Size = new System.Drawing.Size(1464, 879);
            this.ImportWizardControl.Text = "Мастер импорта данных";
            this.ImportWizardControl.UseAcceptButton = false;
            this.ImportWizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.ImportWizardControl_SelectedPageChanged);
            this.ImportWizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.ImportWizardControl_SelectedPageChanging);
            this.ImportWizardControl.CancelClick += new System.ComponentModel.CancelEventHandler(this.ImportWizardControl_FinishClick);
            this.ImportWizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.ImportWizardControl_FinishClick);
            // 
            // ProcessingWizardPage
            // 
            this.ProcessingWizardPage.AllowBack = false;
            this.ProcessingWizardPage.AllowCancel = false;
            this.ProcessingWizardPage.AllowNext = false;
            this.ProcessingWizardPage.Controls.Add(this.panel1);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(1432, 734);
            this.ProcessingWizardPage.Text = "Обработка данных";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ProgressBarControl);
            this.panel1.Controls.Add(this.progressProcentLabel);
            this.panel1.Location = new System.Drawing.Point(0, 329);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1432, 55);
            this.panel1.TabIndex = 2;
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressBarControl.Location = new System.Drawing.Point(0, 0);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Size = new System.Drawing.Size(1432, 34);
            this.ProgressBarControl.TabIndex = 0;
            // 
            // progressProcentLabel
            // 
            this.progressProcentLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressProcentLabel.Location = new System.Drawing.Point(0, 34);
            this.progressProcentLabel.Name = "progressProcentLabel";
            this.progressProcentLabel.Size = new System.Drawing.Size(1432, 21);
            this.progressProcentLabel.TabIndex = 1;
            this.progressProcentLabel.Text = "Обработано 0%";
            this.progressProcentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChooseMethodWizardPage
            // 
            this.ChooseMethodWizardPage.Controls.Add(this.importNewCustomersRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importGisZhkhCustomerIDsRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importCustomerPosesRadioButton);
            this.ChooseMethodWizardPage.DescriptionText = "Выберите действие";
            this.ChooseMethodWizardPage.Name = "ChooseMethodWizardPage";
            this.ChooseMethodWizardPage.Size = new System.Drawing.Size(1432, 734);
            this.ChooseMethodWizardPage.Text = "Мастер импорта данных";
            // 
            // importNewCustomersRadioButton
            // 
            this.importNewCustomersRadioButton.AutoSize = true;
            this.importNewCustomersRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importNewCustomersRadioButton.Checked = true;
            this.importNewCustomersRadioButton.Location = new System.Drawing.Point(23, 21);
            this.importNewCustomersRadioButton.Name = "importNewCustomersRadioButton";
            this.importNewCustomersRadioButton.Size = new System.Drawing.Size(195, 17);
            this.importNewCustomersRadioButton.TabIndex = 1;
            this.importNewCustomersRadioButton.TabStop = true;
            this.importNewCustomersRadioButton.Text = "Импортировать новых абонентов";
            this.importNewCustomersRadioButton.UseVisualStyleBackColor = false;
            // 
            // importGisZhkhCustomerIDsRadioButton
            // 
            this.importGisZhkhCustomerIDsRadioButton.AutoSize = true;
            this.importGisZhkhCustomerIDsRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importGisZhkhCustomerIDsRadioButton.Location = new System.Drawing.Point(23, 67);
            this.importGisZhkhCustomerIDsRadioButton.Name = "importGisZhkhCustomerIDsRadioButton";
            this.importGisZhkhCustomerIDsRadioButton.Size = new System.Drawing.Size(317, 17);
            this.importGisZhkhCustomerIDsRadioButton.TabIndex = 42;
            this.importGisZhkhCustomerIDsRadioButton.Text = "Импортировать идентификаторы абонентов из ГИС ЖКХ";
            this.importGisZhkhCustomerIDsRadioButton.UseVisualStyleBackColor = false;
            // 
            // importCustomerPosesRadioButton
            // 
            this.importCustomerPosesRadioButton.AutoSize = true;
            this.importCustomerPosesRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importCustomerPosesRadioButton.Location = new System.Drawing.Point(23, 44);
            this.importCustomerPosesRadioButton.Name = "importCustomerPosesRadioButton";
            this.importCustomerPosesRadioButton.Size = new System.Drawing.Size(254, 17);
            this.importCustomerPosesRadioButton.TabIndex = 2;
            this.importCustomerPosesRadioButton.Text = "Импортировать услуги и тарифы по адресам";
            this.importCustomerPosesRadioButton.UseVisualStyleBackColor = false;
            // 
            // FileWizardPage
            // 
            this.FileWizardPage.Controls.Add(this.label7);
            this.FileWizardPage.Controls.Add(this.filePathTextEdit);
            this.FileWizardPage.Controls.Add(this.selectFileButton);
            this.FileWizardPage.DescriptionText = "Выберите файл, из которого будут импортированны данные";
            this.FileWizardPage.Name = "FileWizardPage";
            this.FileWizardPage.Size = new System.Drawing.Size(1432, 734);
            this.FileWizardPage.Text = "Выбор файла";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Файл";
            // 
            // filePathTextEdit
            // 
            this.filePathTextEdit.Location = new System.Drawing.Point(70, 23);
            this.filePathTextEdit.Name = "filePathTextEdit";
            this.filePathTextEdit.Properties.Mask.EditMask = "\\d+";
            this.filePathTextEdit.Properties.ReadOnly = true;
            this.filePathTextEdit.Size = new System.Drawing.Size(431, 20);
            this.filePathTextEdit.TabIndex = 36;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Location = new System.Drawing.Point(507, 21);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(100, 23);
            this.selectFileButton.TabIndex = 37;
            this.selectFileButton.Text = "Выбрать...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.Controls.Add(this.resultTextBox);
            this.FinishWizardPage.DescriptionText = "Для окончания работы с мастером нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(1432, 734);
            this.FinishWizardPage.Text = "Обработка данных завершена";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTextBox.Location = new System.Drawing.Point(0, 0);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(1432, 734);
            this.resultTextBox.TabIndex = 16;
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.ImportWizardControl);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1464, 879);
            ((System.ComponentModel.ISupportInitialize)(this.ImportWizardControl)).EndInit();
            this.ImportWizardControl.ResumeLayout(false);
            this.ProcessingWizardPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).EndInit();
            this.ChooseMethodWizardPage.ResumeLayout(false);
            this.ChooseMethodWizardPage.PerformLayout();
            this.FileWizardPage.ResumeLayout(false);
            this.FileWizardPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePathTextEdit.Properties)).EndInit();
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraWizard.WizardControl ImportWizardControl;
        private DevExpress.XtraWizard.WizardPage ProcessingWizardPage;
        private DevExpress.XtraEditors.ProgressBarControl ProgressBarControl;
        private DevExpress.XtraWizard.WizardPage ChooseMethodWizardPage;
        private System.Windows.Forms.RadioButton importNewCustomersRadioButton;
        private System.Windows.Forms.RadioButton importGisZhkhCustomerIDsRadioButton;
        private System.Windows.Forms.RadioButton importCustomerPosesRadioButton;
        private DevExpress.XtraWizard.WizardPage FileWizardPage;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit filePathTextEdit;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.Label progressProcentLabel;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox resultTextBox;
    }
}
