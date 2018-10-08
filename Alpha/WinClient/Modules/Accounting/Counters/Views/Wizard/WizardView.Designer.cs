
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
{
    partial class WizardView
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
            if (disposing)
            {
                if (components != null)
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
            this.counterWizardControl = new DevExpress.XtraWizard.WizardControl();
            this.processingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.progressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.collectDataWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.rightTablePanel = new System.Windows.Forms.Panel();
            this.counterValueGridControl = new DevExpress.XtraGrid.GridControl();
            this.counterValueGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.numberGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.accountGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.counterGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collectDateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.periodGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hasErrorGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.counterIdGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.errorsGroupBox = new System.Windows.Forms.GroupBox();
            this.errorMessageTextBox = new System.Windows.Forms.TextBox();
            this.leftInputPanel = new System.Windows.Forms.Panel();
            this.customerGroupBox = new System.Windows.Forms.GroupBox();
            this.accountLabel = new System.Windows.Forms.Label();
            this.ownerLabel = new System.Windows.Forms.Label();
            this.ownerValueLabel = new System.Windows.Forms.Label();
            this.accountTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.addNewButton = new DevExpress.XtraEditors.SimpleButton();
            this.addressGroupBox = new System.Windows.Forms.GroupBox();
            this.squareValuelabel = new System.Windows.Forms.Label();
            this.squareLabel = new System.Windows.Forms.Label();
            this.apartmentLabel = new System.Windows.Forms.Label();
            this.houseLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.apartmentValueLabel = new System.Windows.Forms.Label();
            this.houseValueLabel = new System.Windows.Forms.Label();
            this.streetValueLabel = new System.Windows.Forms.Label();
            this.deleteItemButton = new DevExpress.XtraEditors.SimpleButton();
            this.counterInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.prevValueLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.counterLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.counterModelLabel = new System.Windows.Forms.Label();
            this.couterValueLabel = new System.Windows.Forms.Label();
            this.periodLabel = new System.Windows.Forms.Label();
            this.intermediaryLabel = new System.Windows.Forms.Label();
            this.collectDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.valueTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.finishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.TotalErrorCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalErrorCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.counterWizardControl)).BeginInit();
            this.counterWizardControl.SuspendLayout();
            this.processingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            this.collectDataWizardPage.SuspendLayout();
            this.rightTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).BeginInit();
            this.errorsGroupBox.SuspendLayout();
            this.leftInputPanel.SuspendLayout();
            this.customerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountTextEdit.Properties)).BeginInit();
            this.addressGroupBox.SuspendLayout();
            this.counterInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueTextEdit.Properties)).BeginInit();
            this.finishWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // counterWizardControl
            // 
            this.counterWizardControl.CancelText = "Отмена";
            this.counterWizardControl.Controls.Add(this.processingWizardPage);
            this.counterWizardControl.Controls.Add(this.collectDataWizardPage);
            this.counterWizardControl.Controls.Add(this.finishWizardPage);
            this.counterWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterWizardControl.FinishText = "Завершить";
            this.counterWizardControl.Location = new System.Drawing.Point(0, 0);
            this.counterWizardControl.Name = "counterWizardControl";
            this.counterWizardControl.NextText = "&Далее>";
            this.counterWizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.collectDataWizardPage,
            this.processingWizardPage,
            this.finishWizardPage});
            this.counterWizardControl.PreviousText = "< &Назад";
            this.counterWizardControl.Size = new System.Drawing.Size(890, 642);
            this.counterWizardControl.Text = "Мастер внесение показаний приборов учета";
            this.counterWizardControl.UseAcceptButton = false;
            this.counterWizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.PaymentWizardControl_SelectedPageChanged);
            this.counterWizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.PaymentWizardControl_SelectedPageChanging);
            this.counterWizardControl.CancelClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_CancelClick);
            this.counterWizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_FinishClick);
            // 
            // processingWizardPage
            // 
            this.processingWizardPage.AllowBack = false;
            this.processingWizardPage.AllowCancel = false;
            this.processingWizardPage.AllowNext = false;
            this.processingWizardPage.Controls.Add(this.progressBarControl);
            this.processingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.processingWizardPage.Name = "processingWizardPage";
            this.processingWizardPage.Size = new System.Drawing.Size(858, 497);
            this.processingWizardPage.Text = "Обработка данных";
            // 
            // progressBarControl
            // 
            this.progressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl.Location = new System.Drawing.Point(0, 32);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Size = new System.Drawing.Size(855, 26);
            this.progressBarControl.TabIndex = 0;
            // 
            // collectDataWizardPage
            // 
            this.collectDataWizardPage.AutoScroll = true;
            this.collectDataWizardPage.Controls.Add(this.rightTablePanel);
            this.collectDataWizardPage.Controls.Add(this.leftInputPanel);
            this.collectDataWizardPage.DescriptionText = "Внесите показания прибора учета по абоненту. Проверьте корректность внесенных дан" +
    "ных и исправьте имеющиеся ошибки. Для окончательного сохранения данных нажмите \"" +
    "Далее\".";
            this.collectDataWizardPage.Name = "collectDataWizardPage";
            this.collectDataWizardPage.Size = new System.Drawing.Size(858, 497);
            this.collectDataWizardPage.Text = "Внесение показаний";
            // 
            // rightTablePanel
            // 
            this.rightTablePanel.Controls.Add(this.counterValueGridControl);
            this.rightTablePanel.Controls.Add(this.errorsGroupBox);
            this.rightTablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightTablePanel.Location = new System.Drawing.Point(351, 0);
            this.rightTablePanel.Name = "rightTablePanel";
            this.rightTablePanel.Size = new System.Drawing.Size(507, 497);
            this.rightTablePanel.TabIndex = 61;
            // 
            // counterValueGridControl
            // 
            this.counterValueGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterValueGridControl.Location = new System.Drawing.Point(0, 75);
            this.counterValueGridControl.MainView = this.counterValueGridView;
            this.counterValueGridControl.Name = "counterValueGridControl";
            this.counterValueGridControl.Size = new System.Drawing.Size(507, 422);
            this.counterValueGridControl.TabIndex = 7;
            this.counterValueGridControl.TabStop = false;
            this.counterValueGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.counterValueGridView});
            // 
            // counterValueGridView
            // 
            this.counterValueGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.numberGridColumn,
            this.accountGridColumn,
            this.counterGridColumn,
            this.collectDateGridColumn,
            this.periodGridColumn,
            this.valueGridColumn,
            this.hasErrorGridColumn,
            this.counterIdGridColumn});
            this.counterValueGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.counterValueGridView.GridControl = this.counterValueGridControl;
            this.counterValueGridView.Name = "counterValueGridView";
            this.counterValueGridView.OptionsBehavior.Editable = false;
            this.counterValueGridView.OptionsCustomization.AllowGroup = false;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.counterValueGridView.OptionsSelection.MultiSelect = true;
            this.counterValueGridView.OptionsView.ShowGroupPanel = false;
            this.counterValueGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.PaymentsGridView_CustomDrawCell);
            this.counterValueGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.PaymentsGridView_FocusedRowChanged);
            this.counterValueGridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.PaymentsGridView_CustomColumnDisplayText);
            // 
            // numberGridColumn
            // 
            this.numberGridColumn.Caption = "№";
            this.numberGridColumn.FieldName = "ID";
            this.numberGridColumn.Name = "numberGridColumn";
            this.numberGridColumn.OptionsColumn.AllowEdit = false;
            this.numberGridColumn.Visible = true;
            this.numberGridColumn.VisibleIndex = 0;
            // 
            // accountGridColumn
            // 
            this.accountGridColumn.Caption = "Лицевой счет";
            this.accountGridColumn.FieldName = "Account";
            this.accountGridColumn.Name = "accountGridColumn";
            this.accountGridColumn.OptionsColumn.AllowEdit = false;
            this.accountGridColumn.Visible = true;
            this.accountGridColumn.VisibleIndex = 1;
            // 
            // counterGridColumn
            // 
            this.counterGridColumn.Caption = "Прибор учета";
            this.counterGridColumn.FieldName = "Counter";
            this.counterGridColumn.Name = "counterGridColumn";
            this.counterGridColumn.OptionsColumn.AllowEdit = false;
            this.counterGridColumn.Visible = true;
            this.counterGridColumn.VisibleIndex = 2;
            // 
            // collectDateGridColumn
            // 
            this.collectDateGridColumn.Caption = "Дата внесения";
            this.collectDateGridColumn.FieldName = "CollectDate";
            this.collectDateGridColumn.Name = "collectDateGridColumn";
            this.collectDateGridColumn.OptionsColumn.AllowEdit = false;
            this.collectDateGridColumn.Visible = true;
            this.collectDateGridColumn.VisibleIndex = 3;
            // 
            // periodGridColumn
            // 
            this.periodGridColumn.Caption = "Период";
            this.periodGridColumn.FieldName = "Period";
            this.periodGridColumn.Name = "periodGridColumn";
            this.periodGridColumn.Visible = true;
            this.periodGridColumn.VisibleIndex = 4;
            // 
            // valueGridColumn
            // 
            this.valueGridColumn.Caption = "Показание";
            this.valueGridColumn.DisplayFormat.FormatString = "{0:0.000}";
            this.valueGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueGridColumn.FieldName = "Value";
            this.valueGridColumn.Name = "valueGridColumn";
            this.valueGridColumn.OptionsColumn.AllowEdit = false;
            this.valueGridColumn.Visible = true;
            this.valueGridColumn.VisibleIndex = 5;
            // 
            // hasErrorGridColumn
            // 
            this.hasErrorGridColumn.Caption = "Ошибка";
            this.hasErrorGridColumn.FieldName = "HasError";
            this.hasErrorGridColumn.Name = "hasErrorGridColumn";
            this.hasErrorGridColumn.OptionsColumn.AllowEdit = false;
            this.hasErrorGridColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            // 
            // counterIdGridColumn
            // 
            this.counterIdGridColumn.Caption = "CounterId";
            this.counterIdGridColumn.FieldName = "CounterId";
            this.counterIdGridColumn.Name = "counterIdGridColumn";
            // 
            // errorsGroupBox
            // 
            this.errorsGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.errorsGroupBox.Controls.Add(this.errorMessageTextBox);
            this.errorsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.errorsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.errorsGroupBox.Name = "errorsGroupBox";
            this.errorsGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.errorsGroupBox.Size = new System.Drawing.Size(507, 75);
            this.errorsGroupBox.TabIndex = 47;
            this.errorsGroupBox.TabStop = false;
            this.errorsGroupBox.Text = "Сообщения";
            // 
            // errorMessageTextBox
            // 
            this.errorMessageTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.errorMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorMessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorMessageTextBox.Location = new System.Drawing.Point(5, 18);
            this.errorMessageTextBox.Multiline = true;
            this.errorMessageTextBox.Name = "errorMessageTextBox";
            this.errorMessageTextBox.ReadOnly = true;
            this.errorMessageTextBox.Size = new System.Drawing.Size(497, 52);
            this.errorMessageTextBox.TabIndex = 43;
            // 
            // leftInputPanel
            // 
            this.leftInputPanel.AutoScroll = true;
            this.leftInputPanel.Controls.Add(this.customerGroupBox);
            this.leftInputPanel.Controls.Add(this.addNewButton);
            this.leftInputPanel.Controls.Add(this.addressGroupBox);
            this.leftInputPanel.Controls.Add(this.deleteItemButton);
            this.leftInputPanel.Controls.Add(this.counterInfoGroupBox);
            this.leftInputPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftInputPanel.Location = new System.Drawing.Point(0, 0);
            this.leftInputPanel.Name = "leftInputPanel";
            this.leftInputPanel.Size = new System.Drawing.Size(351, 497);
            this.leftInputPanel.TabIndex = 60;
            // 
            // customerGroupBox
            // 
            this.customerGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.customerGroupBox.Controls.Add(this.accountLabel);
            this.customerGroupBox.Controls.Add(this.ownerLabel);
            this.customerGroupBox.Controls.Add(this.ownerValueLabel);
            this.customerGroupBox.Controls.Add(this.accountTextEdit);
            this.customerGroupBox.Location = new System.Drawing.Point(3, 3);
            this.customerGroupBox.Name = "customerGroupBox";
            this.customerGroupBox.Size = new System.Drawing.Size(343, 84);
            this.customerGroupBox.TabIndex = 2;
            this.customerGroupBox.TabStop = false;
            this.customerGroupBox.Text = "Абонент";
            // 
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(17, 28);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(76, 13);
            this.accountLabel.TabIndex = 2;
            this.accountLabel.Text = "Лицевой счет";
            // 
            // ownerLabel
            // 
            this.ownerLabel.AutoSize = true;
            this.ownerLabel.Location = new System.Drawing.Point(17, 54);
            this.ownerLabel.Name = "ownerLabel";
            this.ownerLabel.Size = new System.Drawing.Size(73, 13);
            this.ownerLabel.TabIndex = 2;
            this.ownerLabel.Text = "Собственник";
            // 
            // ownerValueLabel
            // 
            this.ownerValueLabel.AutoSize = true;
            this.ownerValueLabel.Location = new System.Drawing.Point(116, 54);
            this.ownerValueLabel.Name = "ownerValueLabel";
            this.ownerValueLabel.Size = new System.Drawing.Size(131, 13);
            this.ownerValueLabel.TabIndex = 2;
            this.ownerValueLabel.Text = "Фамилия Имя Отчество";
            // 
            // accountTextEdit
            // 
            this.accountTextEdit.EditValue = "";
            this.accountTextEdit.Location = new System.Drawing.Point(119, 25);
            this.accountTextEdit.Name = "accountTextEdit";
            this.accountTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.accountTextEdit.Properties.Mask.EditMask = "EG-\\d\\d\\d\\d-\\d\\d\\d-\\d";
            this.accountTextEdit.Properties.Mask.IgnoreMaskBlank = false;
            this.accountTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.accountTextEdit.Properties.Mask.PlaceHolder = '0';
            this.accountTextEdit.Size = new System.Drawing.Size(212, 20);
            this.accountTextEdit.TabIndex = 1;
            this.accountTextEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyControl_KeyDown);
            // 
            // addNewButton
            // 
            this.addNewButton.Location = new System.Drawing.Point(187, 402);
            this.addNewButton.Name = "addNewButton";
            this.addNewButton.Size = new System.Drawing.Size(77, 23);
            this.addNewButton.TabIndex = 5;
            this.addNewButton.Text = "Добавить";
            this.addNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // addressGroupBox
            // 
            this.addressGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.addressGroupBox.Controls.Add(this.squareValuelabel);
            this.addressGroupBox.Controls.Add(this.squareLabel);
            this.addressGroupBox.Controls.Add(this.apartmentLabel);
            this.addressGroupBox.Controls.Add(this.houseLabel);
            this.addressGroupBox.Controls.Add(this.streetLabel);
            this.addressGroupBox.Controls.Add(this.apartmentValueLabel);
            this.addressGroupBox.Controls.Add(this.houseValueLabel);
            this.addressGroupBox.Controls.Add(this.streetValueLabel);
            this.addressGroupBox.Location = new System.Drawing.Point(3, 93);
            this.addressGroupBox.Name = "addressGroupBox";
            this.addressGroupBox.Size = new System.Drawing.Size(343, 134);
            this.addressGroupBox.TabIndex = 3;
            this.addressGroupBox.TabStop = false;
            this.addressGroupBox.Text = "Адрес";
            // 
            // squareValuelabel
            // 
            this.squareValuelabel.AutoSize = true;
            this.squareValuelabel.Location = new System.Drawing.Point(116, 101);
            this.squareValuelabel.Name = "squareValuelabel";
            this.squareValuelabel.Size = new System.Drawing.Size(45, 13);
            this.squareValuelabel.TabIndex = 3;
            this.squareValuelabel.Text = "0 кв. м.";
            // 
            // squareLabel
            // 
            this.squareLabel.AutoSize = true;
            this.squareLabel.Location = new System.Drawing.Point(17, 101);
            this.squareLabel.Name = "squareLabel";
            this.squareLabel.Size = new System.Drawing.Size(90, 13);
            this.squareLabel.TabIndex = 3;
            this.squareLabel.Text = "Общая площадь";
            // 
            // apartmentLabel
            // 
            this.apartmentLabel.AutoSize = true;
            this.apartmentLabel.Location = new System.Drawing.Point(17, 75);
            this.apartmentLabel.Name = "apartmentLabel";
            this.apartmentLabel.Size = new System.Drawing.Size(55, 13);
            this.apartmentLabel.TabIndex = 3;
            this.apartmentLabel.Text = "Квартира";
            // 
            // houseLabel
            // 
            this.houseLabel.AutoSize = true;
            this.houseLabel.Location = new System.Drawing.Point(17, 49);
            this.houseLabel.Name = "houseLabel";
            this.houseLabel.Size = new System.Drawing.Size(30, 13);
            this.houseLabel.TabIndex = 3;
            this.houseLabel.Text = "Дом";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(17, 23);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(39, 13);
            this.streetLabel.TabIndex = 3;
            this.streetLabel.Text = "Улица";
            // 
            // apartmentValueLabel
            // 
            this.apartmentValueLabel.AutoSize = true;
            this.apartmentValueLabel.Location = new System.Drawing.Point(116, 75);
            this.apartmentValueLabel.Name = "apartmentValueLabel";
            this.apartmentValueLabel.Size = new System.Drawing.Size(13, 13);
            this.apartmentValueLabel.TabIndex = 3;
            this.apartmentValueLabel.Text = "0";
            // 
            // houseValueLabel
            // 
            this.houseValueLabel.AutoSize = true;
            this.houseValueLabel.Location = new System.Drawing.Point(116, 49);
            this.houseValueLabel.Name = "houseValueLabel";
            this.houseValueLabel.Size = new System.Drawing.Size(13, 13);
            this.houseValueLabel.TabIndex = 3;
            this.houseValueLabel.Text = "0";
            // 
            // streetValueLabel
            // 
            this.streetValueLabel.AutoSize = true;
            this.streetValueLabel.Location = new System.Drawing.Point(116, 23);
            this.streetValueLabel.Name = "streetValueLabel";
            this.streetValueLabel.Size = new System.Drawing.Size(36, 13);
            this.streetValueLabel.TabIndex = 3;
            this.streetValueLabel.Text = "улица";
            // 
            // deleteItemButton
            // 
            this.deleteItemButton.Location = new System.Drawing.Point(270, 402);
            this.deleteItemButton.Name = "deleteItemButton";
            this.deleteItemButton.Size = new System.Drawing.Size(76, 23);
            this.deleteItemButton.TabIndex = 6;
            this.deleteItemButton.Text = "Удалить";
            this.deleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            // 
            // counterInfoGroupBox
            // 
            this.counterInfoGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.counterInfoGroupBox.Controls.Add(this.prevValueLabel);
            this.counterInfoGroupBox.Controls.Add(this.label2);
            this.counterInfoGroupBox.Controls.Add(this.label1);
            this.counterInfoGroupBox.Controls.Add(this.counterLookUpEdit);
            this.counterInfoGroupBox.Controls.Add(this.counterModelLabel);
            this.counterInfoGroupBox.Controls.Add(this.couterValueLabel);
            this.counterInfoGroupBox.Controls.Add(this.periodLabel);
            this.counterInfoGroupBox.Controls.Add(this.intermediaryLabel);
            this.counterInfoGroupBox.Controls.Add(this.collectDateEdit);
            this.counterInfoGroupBox.Controls.Add(this.valueTextEdit);
            this.counterInfoGroupBox.Location = new System.Drawing.Point(3, 233);
            this.counterInfoGroupBox.Name = "counterInfoGroupBox";
            this.counterInfoGroupBox.Size = new System.Drawing.Size(343, 163);
            this.counterInfoGroupBox.TabIndex = 3;
            this.counterInfoGroupBox.TabStop = false;
            this.counterInfoGroupBox.Text = "Прибор учета";
            // 
            // prevValueLabel
            // 
            this.prevValueLabel.AutoSize = true;
            this.prevValueLabel.Location = new System.Drawing.Point(119, 108);
            this.prevValueLabel.Name = "prevValueLabel";
            this.prevValueLabel.Size = new System.Drawing.Size(13, 13);
            this.prevValueLabel.TabIndex = 7;
            this.prevValueLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Пред . показание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Модель";
            // 
            // counterLookUpEdit
            // 
            this.counterLookUpEdit.Location = new System.Drawing.Point(119, 27);
            this.counterLookUpEdit.Name = "counterLookUpEdit";
            this.counterLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.counterLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Number", "Номер"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Модель")});
            this.counterLookUpEdit.Properties.DisplayMember = "Number";
            this.counterLookUpEdit.Properties.NullText = "<Введите значение>";
            this.counterLookUpEdit.Properties.ValueMember = "ID";
            this.counterLookUpEdit.Properties.EditValueChanged += new System.EventHandler(this.counterLookUpEdit_Properties_EditValueChanged);
            this.counterLookUpEdit.Size = new System.Drawing.Size(212, 20);
            this.counterLookUpEdit.TabIndex = 2;
            this.counterLookUpEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyControl_KeyDown);
            this.counterLookUpEdit.Leave += new System.EventHandler(this.AnyControl_Leave);
            // 
            // counterModelLabel
            // 
            this.counterModelLabel.AutoSize = true;
            this.counterModelLabel.Location = new System.Drawing.Point(119, 56);
            this.counterModelLabel.Name = "counterModelLabel";
            this.counterModelLabel.Size = new System.Drawing.Size(10, 13);
            this.counterModelLabel.TabIndex = 3;
            this.counterModelLabel.Text = "-";
            // 
            // couterValueLabel
            // 
            this.couterValueLabel.AutoSize = true;
            this.couterValueLabel.Location = new System.Drawing.Point(17, 134);
            this.couterValueLabel.Name = "couterValueLabel";
            this.couterValueLabel.Size = new System.Drawing.Size(63, 13);
            this.couterValueLabel.TabIndex = 4;
            this.couterValueLabel.Text = "Показание";
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Location = new System.Drawing.Point(17, 81);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(71, 13);
            this.periodLabel.TabIndex = 3;
            this.periodLabel.Text = "Дата снятия";
            // 
            // intermediaryLabel
            // 
            this.intermediaryLabel.AutoSize = true;
            this.intermediaryLabel.Location = new System.Drawing.Point(17, 30);
            this.intermediaryLabel.Name = "intermediaryLabel";
            this.intermediaryLabel.Size = new System.Drawing.Size(41, 13);
            this.intermediaryLabel.TabIndex = 3;
            this.intermediaryLabel.Text = "Номер";
            // 
            // collectDateEdit
            // 
            this.collectDateEdit.EditValue = null;
            this.collectDateEdit.Location = new System.Drawing.Point(119, 78);
            this.collectDateEdit.Name = "collectDateEdit";
            this.collectDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.collectDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.collectDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.collectDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.collectDateEdit.Properties.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.collectDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.collectDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.collectDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.collectDateEdit.Properties.Mask.EditMask = "dd.MM.yyyy";
            this.collectDateEdit.Size = new System.Drawing.Size(212, 20);
            this.collectDateEdit.TabIndex = 3;
            this.collectDateEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyControl_KeyDown);
            this.collectDateEdit.Leave += new System.EventHandler(this.AnyControl_Leave);
            // 
            // valueTextEdit
            // 
            this.valueTextEdit.EditValue = "";
            this.valueTextEdit.Location = new System.Drawing.Point(119, 131);
            this.valueTextEdit.Name = "valueTextEdit";
            this.valueTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.valueTextEdit.Properties.Mask.EditMask = "n";
            this.valueTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.valueTextEdit.Size = new System.Drawing.Size(212, 20);
            this.valueTextEdit.TabIndex = 4;
            this.valueTextEdit.Enter += new System.EventHandler(this.ValueTextEdit_Enter);
            this.valueTextEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnyControl_KeyDown);
            this.valueTextEdit.Leave += new System.EventHandler(this.AnyControl_Leave);
            // 
            // finishWizardPage
            // 
            this.finishWizardPage.AllowBack = false;
            this.finishWizardPage.AllowCancel = false;
            this.finishWizardPage.Controls.Add(this.TotalErrorCountLabel);
            this.finishWizardPage.Controls.Add(this.TotalErrorCountValueLabel);
            this.finishWizardPage.Controls.Add(this.TotalProcessedValueLabel);
            this.finishWizardPage.Controls.Add(this.TotalProcessedLabel);
            this.finishWizardPage.DescriptionText = "Для окончания нажмите Завершить";
            this.finishWizardPage.Name = "finishWizardPage";
            this.finishWizardPage.Size = new System.Drawing.Size(858, 497);
            this.finishWizardPage.Text = "Обработка данных завершена";
            // 
            // TotalErrorCountLabel
            // 
            this.TotalErrorCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalErrorCountLabel.Location = new System.Drawing.Point(12, 46);
            this.TotalErrorCountLabel.Name = "TotalErrorCountLabel";
            this.TotalErrorCountLabel.Size = new System.Drawing.Size(40, 13);
            this.TotalErrorCountLabel.TabIndex = 10;
            this.TotalErrorCountLabel.Text = "Ошибок";
            // 
            // TotalErrorCountValueLabel
            // 
            this.TotalErrorCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalErrorCountValueLabel.Location = new System.Drawing.Point(113, 46);
            this.TotalErrorCountValueLabel.Name = "TotalErrorCountValueLabel";
            this.TotalErrorCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.TotalErrorCountValueLabel.TabIndex = 11;
            this.TotalErrorCountValueLabel.Text = "0";
            // 
            // TotalProcessedValueLabel
            // 
            this.TotalProcessedValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalProcessedValueLabel.Location = new System.Drawing.Point(113, 13);
            this.TotalProcessedValueLabel.Name = "TotalProcessedValueLabel";
            this.TotalProcessedValueLabel.Size = new System.Drawing.Size(6, 13);
            this.TotalProcessedValueLabel.TabIndex = 8;
            this.TotalProcessedValueLabel.Text = "0";
            // 
            // TotalProcessedLabel
            // 
            this.TotalProcessedLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalProcessedLabel.Location = new System.Drawing.Point(12, 13);
            this.TotalProcessedLabel.Name = "TotalProcessedLabel";
            this.TotalProcessedLabel.Size = new System.Drawing.Size(92, 13);
            this.TotalProcessedLabel.TabIndex = 9;
            this.TotalProcessedLabel.Text = "Всего обработано";
            // 
            // WizardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.counterWizardControl);
            this.Name = "WizardView";
            this.Size = new System.Drawing.Size(890, 642);
            ((System.ComponentModel.ISupportInitialize)(this.counterWizardControl)).EndInit();
            this.counterWizardControl.ResumeLayout(false);
            this.processingWizardPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).EndInit();
            this.collectDataWizardPage.ResumeLayout(false);
            this.rightTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).EndInit();
            this.errorsGroupBox.ResumeLayout(false);
            this.errorsGroupBox.PerformLayout();
            this.leftInputPanel.ResumeLayout(false);
            this.customerGroupBox.ResumeLayout(false);
            this.customerGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountTextEdit.Properties)).EndInit();
            this.addressGroupBox.ResumeLayout(false);
            this.addressGroupBox.PerformLayout();
            this.counterInfoGroupBox.ResumeLayout(false);
            this.counterInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueTextEdit.Properties)).EndInit();
            this.finishWizardPage.ResumeLayout(false);
            this.finishWizardPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl counterWizardControl;
        private DevExpress.XtraWizard.WizardPage processingWizardPage;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl;
        private DevExpress.XtraWizard.WizardPage collectDataWizardPage;
        private DevExpress.XtraGrid.GridControl counterValueGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView counterValueGridView;
        private DevExpress.XtraGrid.Columns.GridColumn accountGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn collectDateGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn valueGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn counterGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn hasErrorGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn numberGridColumn;
        private System.Windows.Forms.TextBox errorMessageTextBox;
        private System.Windows.Forms.GroupBox errorsGroupBox;
        private DevExpress.XtraWizard.WizardPage finishWizardPage;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountLabel;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedLabel;
        private DevExpress.XtraEditors.SimpleButton addNewButton;
        private System.Windows.Forms.GroupBox customerGroupBox;
        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.Label ownerLabel;
        private System.Windows.Forms.Label ownerValueLabel;
        private DevExpress.XtraEditors.TextEdit accountTextEdit;
        private System.Windows.Forms.GroupBox counterInfoGroupBox;
        private System.Windows.Forms.Label counterModelLabel;
        private System.Windows.Forms.Label couterValueLabel;
        private System.Windows.Forms.Label periodLabel;
        private System.Windows.Forms.Label intermediaryLabel;
        private DevExpress.XtraEditors.DateEdit collectDateEdit;
        private DevExpress.XtraEditors.TextEdit valueTextEdit;
        private System.Windows.Forms.GroupBox addressGroupBox;
        private System.Windows.Forms.Label squareValuelabel;
        private System.Windows.Forms.Label squareLabel;
        private System.Windows.Forms.Label apartmentLabel;
        private System.Windows.Forms.Label houseLabel;
        private System.Windows.Forms.Label streetLabel;
        private System.Windows.Forms.Label apartmentValueLabel;
        private System.Windows.Forms.Label houseValueLabel;
        private System.Windows.Forms.Label streetValueLabel;
        private DevExpress.XtraEditors.SimpleButton deleteItemButton;
        private System.Windows.Forms.Panel rightTablePanel;
        private System.Windows.Forms.Panel leftInputPanel;
        private DevExpress.XtraGrid.Columns.GridColumn periodGridColumn;
        private DevExpress.XtraEditors.LookUpEdit counterLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn counterIdGridColumn;
        private System.Windows.Forms.Label prevValueLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

