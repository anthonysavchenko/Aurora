namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Layout
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
            this.importChildrenOfWarBenefitRadioButton = new System.Windows.Forms.RadioButton();
            this.importElectricityVolumesRadioButton = new System.Windows.Forms.RadioButton();
            this.importCounterRadioButton = new System.Windows.Forms.RadioButton();
            this.importPublicPlaceServiceVolumeTemplate = new System.Windows.Forms.LinkLabel();
            this.importPublicPlaceServiceVolumesRadioButton = new System.Windows.Forms.RadioButton();
            this.importNewCustomersRadioButton = new System.Windows.Forms.RadioButton();
            this.importGisZhkhCustomerIDsRadioButton = new System.Windows.Forms.RadioButton();
            this.importCustomerPosesRadioButton = new System.Windows.Forms.RadioButton();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.FileWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.addressPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buildingLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.streetLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.periodPanel = new System.Windows.Forms.TableLayoutPanel();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.filePanel = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.filePathTextEdit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ImportWizardControl)).BeginInit();
            this.ImportWizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            this.ChooseMethodWizardPage.SuspendLayout();
            this.FinishWizardPage.SuspendLayout();
            this.FileWizardPage.SuspendLayout();
            this.addressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).BeginInit();
            this.periodPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
            this.filePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePathTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportWizardControl
            // 
            this.ImportWizardControl.CancelText = "Отмена";
            this.ImportWizardControl.Controls.Add(this.ProcessingWizardPage);
            this.ImportWizardControl.Controls.Add(this.ChooseMethodWizardPage);
            this.ImportWizardControl.Controls.Add(this.FinishWizardPage);
            this.ImportWizardControl.Controls.Add(this.FileWizardPage);
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
            this.ImportWizardControl.Size = new System.Drawing.Size(1451, 893);
            this.ImportWizardControl.Text = "Мастер импорта данных";
            this.ImportWizardControl.UseAcceptButton = false;
            this.ImportWizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.ImportWizardControl_SelectedPageChanged);
            this.ImportWizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.ImportWizardControl_SelectedPageChanging);
            this.ImportWizardControl.CancelClick += new System.ComponentModel.CancelEventHandler(this.ImportWizardControl_FinishClick);
            this.ImportWizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.ImportWizardControl_FinishClick);
            this.ImportWizardControl.CustomizeCommandButtons += new DevExpress.XtraWizard.WizardCustomizeCommandButtonsEventHandler(this.ImportWizardControl_CustomizeCommandButtons);
            // 
            // ProcessingWizardPage
            // 
            this.ProcessingWizardPage.AllowBack = false;
            this.ProcessingWizardPage.AllowCancel = false;
            this.ProcessingWizardPage.AllowNext = false;
            this.ProcessingWizardPage.Controls.Add(this.panel1);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(1419, 748);
            this.ProcessingWizardPage.Text = "Обработка данных";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ProgressBarControl);
            this.panel1.Controls.Add(this.progressProcentLabel);
            this.panel1.Location = new System.Drawing.Point(0, 336);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1419, 55);
            this.panel1.TabIndex = 2;
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProgressBarControl.Location = new System.Drawing.Point(0, 0);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Properties.Step = 1;
            this.ProgressBarControl.ShowProgressInTaskBar = true;
            this.ProgressBarControl.Size = new System.Drawing.Size(1419, 34);
            this.ProgressBarControl.TabIndex = 0;
            // 
            // progressProcentLabel
            // 
            this.progressProcentLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressProcentLabel.Location = new System.Drawing.Point(0, 34);
            this.progressProcentLabel.Name = "progressProcentLabel";
            this.progressProcentLabel.Size = new System.Drawing.Size(1419, 21);
            this.progressProcentLabel.TabIndex = 1;
            this.progressProcentLabel.Text = "Загрузка данных...";
            this.progressProcentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChooseMethodWizardPage
            // 
            this.ChooseMethodWizardPage.Controls.Add(this.importChildrenOfWarBenefitRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importElectricityVolumesRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importCounterRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importPublicPlaceServiceVolumeTemplate);
            this.ChooseMethodWizardPage.Controls.Add(this.importPublicPlaceServiceVolumesRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importNewCustomersRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importGisZhkhCustomerIDsRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.importCustomerPosesRadioButton);
            this.ChooseMethodWizardPage.DescriptionText = "Выберите действие";
            this.ChooseMethodWizardPage.Name = "ChooseMethodWizardPage";
            this.ChooseMethodWizardPage.Size = new System.Drawing.Size(1419, 748);
            this.ChooseMethodWizardPage.Text = "Мастер импорта данных";
            // 
            // importChildrenOfWarBenefitRadioButton
            // 
            this.importChildrenOfWarBenefitRadioButton.AutoSize = true;
            this.importChildrenOfWarBenefitRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importChildrenOfWarBenefitRadioButton.Location = new System.Drawing.Point(23, 159);
            this.importChildrenOfWarBenefitRadioButton.Name = "importChildrenOfWarBenefitRadioButton";
            this.importChildrenOfWarBenefitRadioButton.Size = new System.Drawing.Size(377, 17);
            this.importChildrenOfWarBenefitRadioButton.TabIndex = 46;
            this.importChildrenOfWarBenefitRadioButton.Text = "Импортировать данные л/с для учета льгот категории \"Дети войны\"";
            this.importChildrenOfWarBenefitRadioButton.UseVisualStyleBackColor = false;
            // 
            // importElectricityVolumesRadioButton
            // 
            this.importElectricityVolumesRadioButton.AutoSize = true;
            this.importElectricityVolumesRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importElectricityVolumesRadioButton.Location = new System.Drawing.Point(23, 136);
            this.importElectricityVolumesRadioButton.Name = "importElectricityVolumesRadioButton";
            this.importElectricityVolumesRadioButton.Size = new System.Drawing.Size(316, 17);
            this.importElectricityVolumesRadioButton.TabIndex = 45;
            this.importElectricityVolumesRadioButton.Text = "Импорт объемов потребелния электроэнергии по ОДПУ";
            this.importElectricityVolumesRadioButton.UseVisualStyleBackColor = false;
            // 
            // importCounterRadioButton
            // 
            this.importCounterRadioButton.AutoSize = true;
            this.importCounterRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importCounterRadioButton.Location = new System.Drawing.Point(23, 113);
            this.importCounterRadioButton.Name = "importCounterRadioButton";
            this.importCounterRadioButton.Size = new System.Drawing.Size(145, 17);
            this.importCounterRadioButton.TabIndex = 5;
            this.importCounterRadioButton.Text = "Импорт приборов учета";
            this.importCounterRadioButton.UseVisualStyleBackColor = false;
            // 
            // importPublicPlaceServiceVolumeTemplate
            // 
            this.importPublicPlaceServiceVolumeTemplate.AutoSize = true;
            this.importPublicPlaceServiceVolumeTemplate.Location = new System.Drawing.Point(628, 92);
            this.importPublicPlaceServiceVolumeTemplate.Name = "importPublicPlaceServiceVolumeTemplate";
            this.importPublicPlaceServiceVolumeTemplate.Size = new System.Drawing.Size(46, 13);
            this.importPublicPlaceServiceVolumeTemplate.TabIndex = 44;
            this.importPublicPlaceServiceVolumeTemplate.TabStop = true;
            this.importPublicPlaceServiceVolumeTemplate.Text = "Шаблон";
            this.importPublicPlaceServiceVolumeTemplate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.importPublicPlaceServiceVolumeTemplate_LinkClicked);
            // 
            // importPublicPlaceServiceVolumesRadioButton
            // 
            this.importPublicPlaceServiceVolumesRadioButton.AutoSize = true;
            this.importPublicPlaceServiceVolumesRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importPublicPlaceServiceVolumesRadioButton.Location = new System.Drawing.Point(23, 90);
            this.importPublicPlaceServiceVolumesRadioButton.Name = "importPublicPlaceServiceVolumesRadioButton";
            this.importPublicPlaceServiceVolumesRadioButton.Size = new System.Drawing.Size(599, 17);
            this.importPublicPlaceServiceVolumesRadioButton.TabIndex = 4;
            this.importPublicPlaceServiceVolumesRadioButton.Text = "Импорт данных по потребленным объемам коммунального ресурса при содержании общедо" +
    "мового имущества";
            this.importPublicPlaceServiceVolumesRadioButton.UseVisualStyleBackColor = false;
            // 
            // importNewCustomersRadioButton
            // 
            this.importNewCustomersRadioButton.AutoSize = true;
            this.importNewCustomersRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importNewCustomersRadioButton.Checked = true;
            this.importNewCustomersRadioButton.Location = new System.Drawing.Point(23, 21);
            this.importNewCustomersRadioButton.Name = "importNewCustomersRadioButton";
            this.importNewCustomersRadioButton.Size = new System.Drawing.Size(154, 17);
            this.importNewCustomersRadioButton.TabIndex = 1;
            this.importNewCustomersRadioButton.TabStop = true;
            this.importNewCustomersRadioButton.Text = "Импорт новых абонентов";
            this.importNewCustomersRadioButton.UseVisualStyleBackColor = false;
            // 
            // importGisZhkhCustomerIDsRadioButton
            // 
            this.importGisZhkhCustomerIDsRadioButton.AutoSize = true;
            this.importGisZhkhCustomerIDsRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importGisZhkhCustomerIDsRadioButton.Location = new System.Drawing.Point(23, 67);
            this.importGisZhkhCustomerIDsRadioButton.Name = "importGisZhkhCustomerIDsRadioButton";
            this.importGisZhkhCustomerIDsRadioButton.Size = new System.Drawing.Size(280, 17);
            this.importGisZhkhCustomerIDsRadioButton.TabIndex = 3;
            this.importGisZhkhCustomerIDsRadioButton.Text = "Импорт идентификаторов абонентов из ГИС ЖКХ";
            this.importGisZhkhCustomerIDsRadioButton.UseVisualStyleBackColor = false;
            // 
            // importCustomerPosesRadioButton
            // 
            this.importCustomerPosesRadioButton.AutoSize = true;
            this.importCustomerPosesRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.importCustomerPosesRadioButton.Location = new System.Drawing.Point(23, 44);
            this.importCustomerPosesRadioButton.Name = "importCustomerPosesRadioButton";
            this.importCustomerPosesRadioButton.Size = new System.Drawing.Size(211, 17);
            this.importCustomerPosesRadioButton.TabIndex = 2;
            this.importCustomerPosesRadioButton.Text = "Импорт услуг и тарифов по адресам";
            this.importCustomerPosesRadioButton.UseVisualStyleBackColor = false;
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.Controls.Add(this.resultTextBox);
            this.FinishWizardPage.DescriptionText = "Для окончания работы с мастером нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(1419, 748);
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
            this.resultTextBox.Size = new System.Drawing.Size(1419, 748);
            this.resultTextBox.TabIndex = 16;
            // 
            // FileWizardPage
            // 
            this.FileWizardPage.Controls.Add(this.addressPanel);
            this.FileWizardPage.Controls.Add(this.periodPanel);
            this.FileWizardPage.Controls.Add(this.filePanel);
            this.FileWizardPage.DescriptionText = "Выберите файл, из которого будут импортированны данные";
            this.FileWizardPage.Name = "FileWizardPage";
            this.FileWizardPage.Size = new System.Drawing.Size(1419, 748);
            this.FileWizardPage.Text = "Выбор файла";
            // 
            // addressPanel
            // 
            this.addressPanel.ColumnCount = 4;
            this.addressPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.addressPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.addressPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.addressPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.addressPanel.Controls.Add(this.buildingLookUpEdit, 3, 0);
            this.addressPanel.Controls.Add(this.label2, 0, 0);
            this.addressPanel.Controls.Add(this.label3, 2, 0);
            this.addressPanel.Controls.Add(this.streetLookUpEdit, 1, 0);
            this.addressPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.addressPanel.Location = new System.Drawing.Point(0, 56);
            this.addressPanel.Name = "addressPanel";
            this.addressPanel.RowCount = 1;
            this.addressPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.addressPanel.Size = new System.Drawing.Size(1419, 28);
            this.addressPanel.TabIndex = 40;
            // 
            // buildingLookUpEdit
            // 
            this.buildingLookUpEdit.Location = new System.Drawing.Point(467, 3);
            this.buildingLookUpEdit.Name = "buildingLookUpEdit";
            this.buildingLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.buildingLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер дома")});
            this.buildingLookUpEdit.Properties.DisplayMember = "Number";
            this.buildingLookUpEdit.Properties.NullText = "(все)";
            this.buildingLookUpEdit.Properties.ValueMember = "ID";
            this.buildingLookUpEdit.Size = new System.Drawing.Size(102, 20);
            this.buildingLookUpEdit.TabIndex = 41;
            this.buildingLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Улица:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Дом:";
            // 
            // streetLookUpEdit
            // 
            this.streetLookUpEdit.Location = new System.Drawing.Point(203, 3);
            this.streetLookUpEdit.Name = "streetLookUpEdit";
            this.streetLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.streetLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.streetLookUpEdit.Properties.DisplayMember = "Name";
            this.streetLookUpEdit.Properties.NullText = "(все)";
            this.streetLookUpEdit.Properties.ValueMember = "ID";
            this.streetLookUpEdit.Size = new System.Drawing.Size(214, 20);
            this.streetLookUpEdit.TabIndex = 36;
            this.streetLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            this.streetLookUpEdit.EditValueChanged += new System.EventHandler(this.streetLookUpEdit_EditValueChanged);
            // 
            // periodPanel
            // 
            this.periodPanel.ColumnCount = 3;
            this.periodPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.periodPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.periodPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.periodPanel.Controls.Add(this.periodDateEdit, 0, 0);
            this.periodPanel.Controls.Add(this.label1, 0, 0);
            this.periodPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.periodPanel.Location = new System.Drawing.Point(0, 28);
            this.periodPanel.Name = "periodPanel";
            this.periodPanel.RowCount = 1;
            this.periodPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.periodPanel.Size = new System.Drawing.Size(1419, 28);
            this.periodPanel.TabIndex = 39;
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(203, 3);
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
            this.periodDateEdit.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Учетный период:";
            // 
            // filePanel
            // 
            this.filePanel.ColumnCount = 3;
            this.filePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.filePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.filePanel.Controls.Add(this.label7, 0, 0);
            this.filePanel.Controls.Add(this.selectFileButton, 2, 0);
            this.filePanel.Controls.Add(this.filePathTextEdit, 1, 0);
            this.filePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filePanel.Location = new System.Drawing.Point(0, 0);
            this.filePanel.Name = "filePanel";
            this.filePanel.RowCount = 1;
            this.filePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.filePanel.Size = new System.Drawing.Size(1419, 28);
            this.filePanel.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(158, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Файл:";
            // 
            // selectFileButton
            // 
            this.selectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFileButton.Location = new System.Drawing.Point(1302, 3);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(114, 22);
            this.selectFileButton.TabIndex = 37;
            this.selectFileButton.Text = "Выбрать...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // filePathTextEdit
            // 
            this.filePathTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTextEdit.Location = new System.Drawing.Point(203, 4);
            this.filePathTextEdit.Name = "filePathTextEdit";
            this.filePathTextEdit.Properties.Mask.EditMask = "\\d+";
            this.filePathTextEdit.Properties.ReadOnly = true;
            this.filePathTextEdit.Size = new System.Drawing.Size(1093, 20);
            this.filePathTextEdit.TabIndex = 36;
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.ImportWizardControl);
            this.Name = "LayoutView";
            this.Size = new System.Drawing.Size(1451, 893);
            ((System.ComponentModel.ISupportInitialize)(this.ImportWizardControl)).EndInit();
            this.ImportWizardControl.ResumeLayout(false);
            this.ProcessingWizardPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).EndInit();
            this.ChooseMethodWizardPage.ResumeLayout(false);
            this.ChooseMethodWizardPage.PerformLayout();
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.FileWizardPage.ResumeLayout(false);
            this.addressPanel.ResumeLayout(false);
            this.addressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).EndInit();
            this.periodPanel.ResumeLayout(false);
            this.periodPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            this.filePanel.ResumeLayout(false);
            this.filePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filePathTextEdit.Properties)).EndInit();
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
        private System.Windows.Forms.RadioButton importPublicPlaceServiceVolumesRadioButton;
        private System.Windows.Forms.TableLayoutPanel filePanel;
        private System.Windows.Forms.TableLayoutPanel periodPanel;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
        private System.Windows.Forms.LinkLabel importPublicPlaceServiceVolumeTemplate;
        private System.Windows.Forms.RadioButton importCounterRadioButton;
        private System.Windows.Forms.RadioButton importElectricityVolumesRadioButton;
        private System.Windows.Forms.RadioButton importChildrenOfWarBenefitRadioButton;
        private System.Windows.Forms.TableLayoutPanel addressPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LookUpEdit streetLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit buildingLookUpEdit;
    }
}
