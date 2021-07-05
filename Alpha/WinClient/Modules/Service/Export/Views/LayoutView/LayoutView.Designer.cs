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
            this.ExportWizardControl = new DevExpress.XtraWizard.WizardControl();
            this.ProcessingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.ProgressBarControl = new System.Windows.Forms.ProgressBar();
            this.progressProcentLabel = new System.Windows.Forms.Label();
            this.ChooseMethodWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.exportChargesForGizZhkhRadioBtn = new System.Windows.Forms.RadioButton();
            this.exportBenefitRadioBtn = new System.Windows.Forms.RadioButton();
            this.exportCustomersForGisZhkhRadioBtn = new System.Windows.Forms.RadioButton();
            this.exportChargesForBanksRadioBtn = new System.Windows.Forms.RadioButton();
            this.FileWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.tblIncludeWithoutGIDID = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.IncludeWithoutGISIDCheckBox = new System.Windows.Forms.CheckBox();
            this.tblBenefitExportInfo = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.startPeriodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tblGizZhkhInfo = new System.Windows.Forms.TableLayoutPanel();
            this.onlyNewRadioBtn = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.allRadioBtn = new System.Windows.Forms.RadioButton();
            this.tblBankExportInfo = new System.Windows.Forms.TableLayoutPanel();
            this.chkPrimSocBankFormat = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSbrfFormat = new System.Windows.Forms.CheckBox();
            this.tblPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tblOutputPath = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectExportPath = new System.Windows.Forms.Button();
            this.outputPathTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.tblTemplate = new System.Windows.Forms.TableLayoutPanel();
            this.templatePathTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectTemplate = new System.Windows.Forms.Button();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.ServiceMatchingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.serviceMatchingTableProgressBarPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tblServiceMatching = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ExportWizardControl)).BeginInit();
            this.ExportWizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            this.ChooseMethodWizardPage.SuspendLayout();
            this.FileWizardPage.SuspendLayout();
            this.tblIncludeWithoutGIDID.SuspendLayout();
            this.tblBenefitExportInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startPeriodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPeriodDateEdit.Properties)).BeginInit();
            this.tblGizZhkhInfo.SuspendLayout();
            this.tblBankExportInfo.SuspendLayout();
            this.tblPeriod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
            this.tblOutputPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputPathTextEdit.Properties)).BeginInit();
            this.tblTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.templatePathTextEdit.Properties)).BeginInit();
            this.FinishWizardPage.SuspendLayout();
            this.ServiceMatchingWizardPage.SuspendLayout();
            this.serviceMatchingTableProgressBarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportWizardControl
            // 
            this.ExportWizardControl.CancelText = "Отмена";
            this.ExportWizardControl.Controls.Add(this.ProcessingWizardPage);
            this.ExportWizardControl.Controls.Add(this.ChooseMethodWizardPage);
            this.ExportWizardControl.Controls.Add(this.FileWizardPage);
            this.ExportWizardControl.Controls.Add(this.FinishWizardPage);
            this.ExportWizardControl.Controls.Add(this.ServiceMatchingWizardPage);
            this.ExportWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExportWizardControl.FinishText = "Завершить";
            this.ExportWizardControl.Location = new System.Drawing.Point(0, 0);
            this.ExportWizardControl.Name = "ExportWizardControl";
            this.ExportWizardControl.NextText = "&Далее>";
            this.ExportWizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.ChooseMethodWizardPage,
            this.FileWizardPage,
            this.ServiceMatchingWizardPage,
            this.ProcessingWizardPage,
            this.FinishWizardPage});
            this.ExportWizardControl.PreviousText = "< &Назад";
            this.ExportWizardControl.Size = new System.Drawing.Size(1041, 1758);
            this.ExportWizardControl.Text = "Мастер экспорта данных";
            this.ExportWizardControl.UseAcceptButton = false;
            this.ExportWizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.ExportWizardControl_SelectedPageChanged);
            this.ExportWizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.ExportWizardControl_SelectedPageChanging);
            this.ExportWizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.ExportWizardControl_FinishClick);
            this.ExportWizardControl.CustomizeCommandButtons += new DevExpress.XtraWizard.WizardCustomizeCommandButtonsEventHandler(this.ExportWizardControl_CustomizeCommandButtons);
            // 
            // ProcessingWizardPage
            // 
            this.ProcessingWizardPage.AllowBack = false;
            this.ProcessingWizardPage.AllowCancel = false;
            this.ProcessingWizardPage.AllowNext = false;
            this.ProcessingWizardPage.Controls.Add(this.ProgressBarControl);
            this.ProcessingWizardPage.Controls.Add(this.progressProcentLabel);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания экспорта данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(1009, 1613);
            this.ProcessingWizardPage.Text = "Экспорт данных";
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarControl.Location = new System.Drawing.Point(3, 795);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Size = new System.Drawing.Size(1003, 23);
            this.ProgressBarControl.Step = 0;
            this.ProgressBarControl.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressBarControl.TabIndex = 2;
            // 
            // progressProcentLabel
            // 
            this.progressProcentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressProcentLabel.Location = new System.Drawing.Point(3, 821);
            this.progressProcentLabel.Name = "progressProcentLabel";
            this.progressProcentLabel.Size = new System.Drawing.Size(1003, 18);
            this.progressProcentLabel.TabIndex = 1;
            this.progressProcentLabel.Text = "Выполнено 0%";
            this.progressProcentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChooseMethodWizardPage
            // 
            this.ChooseMethodWizardPage.Controls.Add(this.exportChargesForGizZhkhRadioBtn);
            this.ChooseMethodWizardPage.Controls.Add(this.exportBenefitRadioBtn);
            this.ChooseMethodWizardPage.Controls.Add(this.exportCustomersForGisZhkhRadioBtn);
            this.ChooseMethodWizardPage.Controls.Add(this.exportChargesForBanksRadioBtn);
            this.ChooseMethodWizardPage.DescriptionText = "Выберите действие";
            this.ChooseMethodWizardPage.Name = "ChooseMethodWizardPage";
            this.ChooseMethodWizardPage.Size = new System.Drawing.Size(1009, 1613);
            this.ChooseMethodWizardPage.Text = "Мастер экспорта данных";
            // 
            // exportChargesForGizZhkhRadioBtn
            // 
            this.exportChargesForGizZhkhRadioBtn.AutoSize = true;
            this.exportChargesForGizZhkhRadioBtn.BackColor = System.Drawing.Color.Transparent;
            this.exportChargesForGizZhkhRadioBtn.Location = new System.Drawing.Point(23, 90);
            this.exportChargesForGizZhkhRadioBtn.Name = "exportChargesForGizZhkhRadioBtn";
            this.exportChargesForGizZhkhRadioBtn.Size = new System.Drawing.Size(190, 17);
            this.exportChargesForGizZhkhRadioBtn.TabIndex = 4;
            this.exportChargesForGizZhkhRadioBtn.Text = "Экспорт начислений в ГИС ЖКХ";
            this.exportChargesForGizZhkhRadioBtn.UseVisualStyleBackColor = false;
            // 
            // exportBenefitRadioBtn
            // 
            this.exportBenefitRadioBtn.AutoSize = true;
            this.exportBenefitRadioBtn.BackColor = System.Drawing.Color.Transparent;
            this.exportBenefitRadioBtn.Checked = true;
            this.exportBenefitRadioBtn.Location = new System.Drawing.Point(23, 21);
            this.exportBenefitRadioBtn.Name = "exportBenefitRadioBtn";
            this.exportBenefitRadioBtn.Size = new System.Drawing.Size(176, 17);
            this.exportBenefitRadioBtn.TabIndex = 1;
            this.exportBenefitRadioBtn.TabStop = true;
            this.exportBenefitRadioBtn.Text = "Экспорт данных о льготниках";
            this.exportBenefitRadioBtn.UseVisualStyleBackColor = false;
            // 
            // exportCustomersForGisZhkhRadioBtn
            // 
            this.exportCustomersForGisZhkhRadioBtn.AutoSize = true;
            this.exportCustomersForGisZhkhRadioBtn.BackColor = System.Drawing.Color.Transparent;
            this.exportCustomersForGisZhkhRadioBtn.Location = new System.Drawing.Point(23, 67);
            this.exportCustomersForGisZhkhRadioBtn.Name = "exportCustomersForGisZhkhRadioBtn";
            this.exportCustomersForGisZhkhRadioBtn.Size = new System.Drawing.Size(184, 17);
            this.exportCustomersForGisZhkhRadioBtn.TabIndex = 3;
            this.exportCustomersForGisZhkhRadioBtn.Text = "Экспорт абонентов в ГИС ЖКХ";
            this.exportCustomersForGisZhkhRadioBtn.UseVisualStyleBackColor = false;
            // 
            // exportChargesForBanksRadioBtn
            // 
            this.exportChargesForBanksRadioBtn.AutoSize = true;
            this.exportChargesForBanksRadioBtn.BackColor = System.Drawing.Color.Transparent;
            this.exportChargesForBanksRadioBtn.Location = new System.Drawing.Point(23, 44);
            this.exportChargesForBanksRadioBtn.Name = "exportChargesForBanksRadioBtn";
            this.exportChargesForBanksRadioBtn.Size = new System.Drawing.Size(189, 17);
            this.exportChargesForBanksRadioBtn.TabIndex = 2;
            this.exportChargesForBanksRadioBtn.Text = "Экспорт начислений для банков";
            this.exportChargesForBanksRadioBtn.UseVisualStyleBackColor = false;
            // 
            // FileWizardPage
            // 
            this.FileWizardPage.Controls.Add(this.tblIncludeWithoutGIDID);
            this.FileWizardPage.Controls.Add(this.tblBenefitExportInfo);
            this.FileWizardPage.Controls.Add(this.tblGizZhkhInfo);
            this.FileWizardPage.Controls.Add(this.tblBankExportInfo);
            this.FileWizardPage.Controls.Add(this.tblPeriod);
            this.FileWizardPage.Controls.Add(this.tblOutputPath);
            this.FileWizardPage.Controls.Add(this.tblTemplate);
            this.FileWizardPage.DescriptionText = "Выберите шаблон и путь для экспорта данных";
            this.FileWizardPage.Name = "FileWizardPage";
            this.FileWizardPage.Size = new System.Drawing.Size(1009, 1613);
            this.FileWizardPage.Text = "Выбор файла";
            // 
            // tblIncludeWithoutGIDID
            // 
            this.tblIncludeWithoutGIDID.ColumnCount = 2;
            this.tblIncludeWithoutGIDID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblIncludeWithoutGIDID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblIncludeWithoutGIDID.Controls.Add(this.label6, 0, 0);
            this.tblIncludeWithoutGIDID.Controls.Add(this.IncludeWithoutGISIDCheckBox, 1, 0);
            this.tblIncludeWithoutGIDID.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblIncludeWithoutGIDID.Location = new System.Drawing.Point(0, 207);
            this.tblIncludeWithoutGIDID.Name = "tblIncludeWithoutGIDID";
            this.tblIncludeWithoutGIDID.RowCount = 1;
            this.tblIncludeWithoutGIDID.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblIncludeWithoutGIDID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblIncludeWithoutGIDID.Size = new System.Drawing.Size(1009, 27);
            this.tblIncludeWithoutGIDID.TabIndex = 51;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "ЕЛС ГИС ЖКХ";
            // 
            // IncludeWithoutGISIDCheckBox
            // 
            this.IncludeWithoutGISIDCheckBox.AutoSize = true;
            this.IncludeWithoutGISIDCheckBox.Checked = true;
            this.IncludeWithoutGISIDCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeWithoutGISIDCheckBox.Location = new System.Drawing.Point(153, 3);
            this.IncludeWithoutGISIDCheckBox.Name = "IncludeWithoutGISIDCheckBox";
            this.IncludeWithoutGISIDCheckBox.Size = new System.Drawing.Size(215, 17);
            this.IncludeWithoutGISIDCheckBox.TabIndex = 6;
            this.IncludeWithoutGISIDCheckBox.Text = "Включить в файл абонентов без ЕЛС";
            this.IncludeWithoutGISIDCheckBox.UseVisualStyleBackColor = true;
            // 
            // tblBenefitExportInfo
            // 
            this.tblBenefitExportInfo.ColumnCount = 2;
            this.tblBenefitExportInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblBenefitExportInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBenefitExportInfo.Controls.Add(this.label5, 0, 0);
            this.tblBenefitExportInfo.Controls.Add(this.startPeriodDateEdit, 1, 0);
            this.tblBenefitExportInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblBenefitExportInfo.Location = new System.Drawing.Point(0, 180);
            this.tblBenefitExportInfo.Name = "tblBenefitExportInfo";
            this.tblBenefitExportInfo.RowCount = 1;
            this.tblBenefitExportInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblBenefitExportInfo.Size = new System.Drawing.Size(1009, 27);
            this.tblBenefitExportInfo.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Начальный период";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // startPeriodDateEdit
            // 
            this.startPeriodDateEdit.EditValue = null;
            this.startPeriodDateEdit.Location = new System.Drawing.Point(153, 3);
            this.startPeriodDateEdit.Name = "startPeriodDateEdit";
            this.startPeriodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.startPeriodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.startPeriodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.startPeriodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.startPeriodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.startPeriodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.startPeriodDateEdit.Properties.EditFormat.FormatString = "y";
            this.startPeriodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.startPeriodDateEdit.Properties.Mask.EditMask = "y";
            this.startPeriodDateEdit.Size = new System.Drawing.Size(131, 20);
            this.startPeriodDateEdit.TabIndex = 5;
            // 
            // tblGizZhkhInfo
            // 
            this.tblGizZhkhInfo.ColumnCount = 2;
            this.tblGizZhkhInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblGizZhkhInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGizZhkhInfo.Controls.Add(this.onlyNewRadioBtn, 1, 1);
            this.tblGizZhkhInfo.Controls.Add(this.label4, 0, 0);
            this.tblGizZhkhInfo.Controls.Add(this.allRadioBtn, 1, 0);
            this.tblGizZhkhInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblGizZhkhInfo.Location = new System.Drawing.Point(0, 133);
            this.tblGizZhkhInfo.Name = "tblGizZhkhInfo";
            this.tblGizZhkhInfo.RowCount = 2;
            this.tblGizZhkhInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGizZhkhInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGizZhkhInfo.Size = new System.Drawing.Size(1009, 47);
            this.tblGizZhkhInfo.TabIndex = 47;
            // 
            // onlyNewRadioBtn
            // 
            this.onlyNewRadioBtn.AutoSize = true;
            this.onlyNewRadioBtn.Location = new System.Drawing.Point(153, 26);
            this.onlyNewRadioBtn.Name = "onlyNewRadioBtn";
            this.onlyNewRadioBtn.Size = new System.Drawing.Size(96, 17);
            this.onlyNewRadioBtn.TabIndex = 9;
            this.onlyNewRadioBtn.Text = "Только новых";
            this.onlyNewRadioBtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Экспорт абонентов";
            // 
            // allRadioBtn
            // 
            this.allRadioBtn.AutoSize = true;
            this.allRadioBtn.Checked = true;
            this.allRadioBtn.Location = new System.Drawing.Point(153, 3);
            this.allRadioBtn.Name = "allRadioBtn";
            this.allRadioBtn.Size = new System.Drawing.Size(49, 17);
            this.allRadioBtn.TabIndex = 8;
            this.allRadioBtn.TabStop = true;
            this.allRadioBtn.Text = "Всех";
            this.allRadioBtn.UseVisualStyleBackColor = true;
            // 
            // tblBankExportInfo
            // 
            this.tblBankExportInfo.ColumnCount = 2;
            this.tblBankExportInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblBankExportInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBankExportInfo.Controls.Add(this.chkPrimSocBankFormat, 1, 1);
            this.tblBankExportInfo.Controls.Add(this.label3, 0, 0);
            this.tblBankExportInfo.Controls.Add(this.chkSbrfFormat, 1, 0);
            this.tblBankExportInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblBankExportInfo.Location = new System.Drawing.Point(0, 87);
            this.tblBankExportInfo.Name = "tblBankExportInfo";
            this.tblBankExportInfo.RowCount = 2;
            this.tblBankExportInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblBankExportInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblBankExportInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblBankExportInfo.Size = new System.Drawing.Size(1009, 46);
            this.tblBankExportInfo.TabIndex = 46;
            // 
            // chkPrimSocBankFormat
            // 
            this.chkPrimSocBankFormat.AutoSize = true;
            this.chkPrimSocBankFormat.Location = new System.Drawing.Point(153, 26);
            this.chkPrimSocBankFormat.Name = "chkPrimSocBankFormat";
            this.chkPrimSocBankFormat.Size = new System.Drawing.Size(96, 17);
            this.chkPrimSocBankFormat.TabIndex = 7;
            this.chkPrimSocBankFormat.Text = "Примсоцбанк";
            this.chkPrimSocBankFormat.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Формат";
            // 
            // chkSbrfFormat
            // 
            this.chkSbrfFormat.AutoSize = true;
            this.chkSbrfFormat.Checked = true;
            this.chkSbrfFormat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSbrfFormat.Location = new System.Drawing.Point(153, 3);
            this.chkSbrfFormat.Name = "chkSbrfFormat";
            this.chkSbrfFormat.Size = new System.Drawing.Size(75, 17);
            this.chkSbrfFormat.TabIndex = 6;
            this.chkSbrfFormat.Text = "Сбербанк";
            this.chkSbrfFormat.UseVisualStyleBackColor = true;
            // 
            // tblPeriod
            // 
            this.tblPeriod.ColumnCount = 2;
            this.tblPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPeriod.Controls.Add(this.label1, 0, 0);
            this.tblPeriod.Controls.Add(this.periodDateEdit, 1, 0);
            this.tblPeriod.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblPeriod.Location = new System.Drawing.Point(0, 60);
            this.tblPeriod.Name = "tblPeriod";
            this.tblPeriod.RowCount = 1;
            this.tblPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblPeriod.Size = new System.Drawing.Size(1009, 27);
            this.tblPeriod.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Учетный период";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(153, 3);
            this.periodDateEdit.Name = "periodDateEdit";
            this.periodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.periodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.periodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.periodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.EditFormat.FormatString = "y";
            this.periodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.Mask.EditMask = "y";
            this.periodDateEdit.Size = new System.Drawing.Size(131, 20);
            this.periodDateEdit.TabIndex = 6;
            // 
            // tblOutputPath
            // 
            this.tblOutputPath.ColumnCount = 3;
            this.tblOutputPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblOutputPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOutputPath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblOutputPath.Controls.Add(this.btnSelectExportPath, 2, 0);
            this.tblOutputPath.Controls.Add(this.outputPathTextEdit, 1, 0);
            this.tblOutputPath.Controls.Add(this.label8, 0, 0);
            this.tblOutputPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblOutputPath.Location = new System.Drawing.Point(0, 30);
            this.tblOutputPath.Name = "tblOutputPath";
            this.tblOutputPath.RowCount = 1;
            this.tblOutputPath.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblOutputPath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblOutputPath.Size = new System.Drawing.Size(1009, 30);
            this.tblOutputPath.TabIndex = 49;
            // 
            // btnSelectExportPath
            // 
            this.btnSelectExportPath.Location = new System.Drawing.Point(912, 3);
            this.btnSelectExportPath.Name = "btnSelectExportPath";
            this.btnSelectExportPath.Size = new System.Drawing.Size(94, 23);
            this.btnSelectExportPath.TabIndex = 4;
            this.btnSelectExportPath.Text = "Выбрать...";
            this.btnSelectExportPath.UseVisualStyleBackColor = true;
            this.btnSelectExportPath.Click += new System.EventHandler(this.btnSelectExportPath_Click);
            // 
            // outputPathTextEdit
            // 
            this.outputPathTextEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPathTextEdit.Location = new System.Drawing.Point(153, 5);
            this.outputPathTextEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.outputPathTextEdit.Name = "outputPathTextEdit";
            this.outputPathTextEdit.Properties.Mask.EditMask = "\\d+";
            this.outputPathTextEdit.Properties.ReadOnly = true;
            this.outputPathTextEdit.Size = new System.Drawing.Size(753, 20);
            this.outputPathTextEdit.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(89, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Экспорт в";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblTemplate
            // 
            this.tblTemplate.ColumnCount = 3;
            this.tblTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblTemplate.Controls.Add(this.templatePathTextEdit, 1, 0);
            this.tblTemplate.Controls.Add(this.label7, 0, 0);
            this.tblTemplate.Controls.Add(this.btnSelectTemplate, 2, 0);
            this.tblTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblTemplate.Location = new System.Drawing.Point(0, 0);
            this.tblTemplate.Name = "tblTemplate";
            this.tblTemplate.RowCount = 1;
            this.tblTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblTemplate.Size = new System.Drawing.Size(1009, 30);
            this.tblTemplate.TabIndex = 45;
            // 
            // templatePathTextEdit
            // 
            this.templatePathTextEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatePathTextEdit.Location = new System.Drawing.Point(153, 5);
            this.templatePathTextEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.templatePathTextEdit.Name = "templatePathTextEdit";
            this.templatePathTextEdit.Properties.Mask.EditMask = "\\d+";
            this.templatePathTextEdit.Properties.ReadOnly = true;
            this.templatePathTextEdit.Size = new System.Drawing.Size(753, 20);
            this.templatePathTextEdit.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Шаблон";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelectTemplate
            // 
            this.btnSelectTemplate.Location = new System.Drawing.Point(912, 3);
            this.btnSelectTemplate.Name = "btnSelectTemplate";
            this.btnSelectTemplate.Size = new System.Drawing.Size(94, 23);
            this.btnSelectTemplate.TabIndex = 2;
            this.btnSelectTemplate.Text = "Выбрать...";
            this.btnSelectTemplate.UseVisualStyleBackColor = true;
            this.btnSelectTemplate.Click += new System.EventHandler(this.btnSelectTemplate_Click);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.Controls.Add(this.resultTextBox);
            this.FinishWizardPage.DescriptionText = "Для окончания работы с мастером нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(1009, 1613);
            this.FinishWizardPage.Text = "Экспорт данных завершен";
            // 
            // resultTextBox
            // 
            this.resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTextBox.Location = new System.Drawing.Point(0, 0);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultTextBox.Size = new System.Drawing.Size(1009, 1613);
            this.resultTextBox.TabIndex = 16;
            // 
            // ServiceMatchingWizardPage
            // 
            this.ServiceMatchingWizardPage.Controls.Add(this.serviceMatchingTableProgressBarPanel);
            this.ServiceMatchingWizardPage.Controls.Add(this.tblServiceMatching);
            this.ServiceMatchingWizardPage.DescriptionText = "Сопоставьте оказанные за период виды услуг с услугами ГИС ЖКХ из шаблона";
            this.ServiceMatchingWizardPage.Name = "ServiceMatchingWizardPage";
            this.ServiceMatchingWizardPage.Size = new System.Drawing.Size(1009, 1613);
            this.ServiceMatchingWizardPage.Text = "Экспорт начислений для ГИС ЖКХ";
            // 
            // serviceMatchingTableProgressBarPanel
            // 
            this.serviceMatchingTableProgressBarPanel.Controls.Add(this.label2);
            this.serviceMatchingTableProgressBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceMatchingTableProgressBarPanel.Location = new System.Drawing.Point(0, 0);
            this.serviceMatchingTableProgressBarPanel.Name = "serviceMatchingTableProgressBarPanel";
            this.serviceMatchingTableProgressBarPanel.Size = new System.Drawing.Size(1009, 1613);
            this.serviceMatchingTableProgressBarPanel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 795);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1003, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Загрузка данных...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblServiceMatching
            // 
            this.tblServiceMatching.AutoScroll = true;
            this.tblServiceMatching.ColumnCount = 2;
            this.tblServiceMatching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblServiceMatching.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblServiceMatching.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblServiceMatching.Location = new System.Drawing.Point(0, 0);
            this.tblServiceMatching.Name = "tblServiceMatching";
            this.tblServiceMatching.RowCount = 2;
            this.tblServiceMatching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblServiceMatching.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblServiceMatching.Size = new System.Drawing.Size(1009, 1613);
            this.tblServiceMatching.TabIndex = 2;
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ExportWizardControl);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1041, 1758);
            ((System.ComponentModel.ISupportInitialize)(this.ExportWizardControl)).EndInit();
            this.ExportWizardControl.ResumeLayout(false);
            this.ProcessingWizardPage.ResumeLayout(false);
            this.ChooseMethodWizardPage.ResumeLayout(false);
            this.ChooseMethodWizardPage.PerformLayout();
            this.FileWizardPage.ResumeLayout(false);
            this.tblIncludeWithoutGIDID.ResumeLayout(false);
            this.tblIncludeWithoutGIDID.PerformLayout();
            this.tblBenefitExportInfo.ResumeLayout(false);
            this.tblBenefitExportInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startPeriodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPeriodDateEdit.Properties)).EndInit();
            this.tblGizZhkhInfo.ResumeLayout(false);
            this.tblGizZhkhInfo.PerformLayout();
            this.tblBankExportInfo.ResumeLayout(false);
            this.tblBankExportInfo.PerformLayout();
            this.tblPeriod.ResumeLayout(false);
            this.tblPeriod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            this.tblOutputPath.ResumeLayout(false);
            this.tblOutputPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputPathTextEdit.Properties)).EndInit();
            this.tblTemplate.ResumeLayout(false);
            this.tblTemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.templatePathTextEdit.Properties)).EndInit();
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.ServiceMatchingWizardPage.ResumeLayout(false);
            this.serviceMatchingTableProgressBarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraWizard.WizardControl ExportWizardControl;
        private DevExpress.XtraWizard.WizardPage ProcessingWizardPage;
        private System.Windows.Forms.Label progressProcentLabel;
        private DevExpress.XtraWizard.WizardPage ChooseMethodWizardPage;
        private System.Windows.Forms.RadioButton exportBenefitRadioBtn;
        private System.Windows.Forms.RadioButton exportCustomersForGisZhkhRadioBtn;
        private System.Windows.Forms.RadioButton exportChargesForBanksRadioBtn;
        private DevExpress.XtraWizard.WizardPage FileWizardPage;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.RadioButton exportChargesForGizZhkhRadioBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton onlyNewRadioBtn;
        private System.Windows.Forms.RadioButton allRadioBtn;
        private System.Windows.Forms.CheckBox chkPrimSocBankFormat;
        private System.Windows.Forms.CheckBox chkSbrfFormat;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit templatePathTextEdit;
        private System.Windows.Forms.Button btnSelectTemplate;
        private System.Windows.Forms.TableLayoutPanel tblGizZhkhInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblBankExportInfo;
        private System.Windows.Forms.TableLayoutPanel tblTemplate;
        private System.Windows.Forms.TableLayoutPanel tblBenefitExportInfo;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit startPeriodDateEdit;
        private System.Windows.Forms.TableLayoutPanel tblOutputPath;
        private System.Windows.Forms.Button btnSelectExportPath;
        private DevExpress.XtraEditors.TextEdit outputPathTextEdit;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraWizard.WizardPage ServiceMatchingWizardPage;
        private System.Windows.Forms.TableLayoutPanel tblServiceMatching;
        private System.Windows.Forms.TableLayoutPanel tblPeriod;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
        private System.Windows.Forms.Panel serviceMatchingTableProgressBarPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar ProgressBarControl;
        private System.Windows.Forms.TableLayoutPanel tblIncludeWithoutGIDID;
        private System.Windows.Forms.CheckBox IncludeWithoutGISIDCheckBox;
        private System.Windows.Forms.Label label6;
    }
}
