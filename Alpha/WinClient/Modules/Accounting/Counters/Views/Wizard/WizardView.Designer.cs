
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
            this.accountGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.apartmentGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fullNameGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.counterNumGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.counterModelGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.prevValueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.valueGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collectDateGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.collectDateRepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.counterIdGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.errorsGroupBox = new System.Windows.Forms.GroupBox();
            this.errorMessageTextBox = new System.Windows.Forms.TextBox();
            this.finishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.TotalErrorCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalErrorCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedLabel = new DevExpress.XtraEditors.LabelControl();
            this.buildingSelectWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.periodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.buildingLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.streetLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.counterWizardControl)).BeginInit();
            this.counterWizardControl.SuspendLayout();
            this.processingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl.Properties)).BeginInit();
            this.collectDataWizardPage.SuspendLayout();
            this.rightTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterValueGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateRepositoryItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateRepositoryItem.CalendarTimeProperties)).BeginInit();
            this.errorsGroupBox.SuspendLayout();
            this.finishWizardPage.SuspendLayout();
            this.buildingSelectWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // counterWizardControl
            // 
            this.counterWizardControl.CancelText = "Отмена";
            this.counterWizardControl.Controls.Add(this.processingWizardPage);
            this.counterWizardControl.Controls.Add(this.collectDataWizardPage);
            this.counterWizardControl.Controls.Add(this.finishWizardPage);
            this.counterWizardControl.Controls.Add(this.buildingSelectWizardPage);
            this.counterWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterWizardControl.FinishText = "Завершить";
            this.counterWizardControl.Location = new System.Drawing.Point(0, 0);
            this.counterWizardControl.Name = "counterWizardControl";
            this.counterWizardControl.NextText = "&Далее>";
            this.counterWizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.buildingSelectWizardPage,
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
            this.rightTablePanel.Location = new System.Drawing.Point(0, 0);
            this.rightTablePanel.Name = "rightTablePanel";
            this.rightTablePanel.Size = new System.Drawing.Size(858, 497);
            this.rightTablePanel.TabIndex = 61;
            // 
            // counterValueGridControl
            // 
            this.counterValueGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterValueGridControl.Location = new System.Drawing.Point(0, 75);
            this.counterValueGridControl.MainView = this.counterValueGridView;
            this.counterValueGridControl.Name = "counterValueGridControl";
            this.counterValueGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.collectDateRepositoryItem});
            this.counterValueGridControl.Size = new System.Drawing.Size(858, 422);
            this.counterValueGridControl.TabIndex = 7;
            this.counterValueGridControl.TabStop = false;
            this.counterValueGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.counterValueGridView});
            // 
            // counterValueGridView
            // 
            this.counterValueGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.accountGridColumn,
            this.apartmentGridColumn,
            this.fullNameGridColumn,
            this.counterNumGridColumn,
            this.counterModelGridColumn,
            this.prevValueGridColumn,
            this.valueGridColumn,
            this.collectDateGridColumn,
            this.counterIdGridColumn});
            this.counterValueGridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.counterValueGridView.GridControl = this.counterValueGridControl;
            this.counterValueGridView.Name = "counterValueGridView";
            this.counterValueGridView.OptionsCustomization.AllowGroup = false;
            this.counterValueGridView.OptionsNavigation.EnterMoveNextColumn = true;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.counterValueGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.counterValueGridView.OptionsView.ShowGroupPanel = false;
            this.counterValueGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.PaymentsGridView_CustomDrawCell);
            this.counterValueGridView.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.counterValueGridView_ShowingEditor);
            this.counterValueGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.counterValueGridView_FocusedRowChanged);
            this.counterValueGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.counterValueGridView_CellValueChanged);
            // 
            // accountGridColumn
            // 
            this.accountGridColumn.Caption = "Лицевой счет";
            this.accountGridColumn.FieldName = "Account";
            this.accountGridColumn.Name = "accountGridColumn";
            this.accountGridColumn.OptionsColumn.AllowEdit = false;
            this.accountGridColumn.OptionsColumn.AllowFocus = false;
            this.accountGridColumn.Visible = true;
            this.accountGridColumn.VisibleIndex = 0;
            // 
            // apartmentGridColumn
            // 
            this.apartmentGridColumn.Caption = "Кв.";
            this.apartmentGridColumn.FieldName = "Apartment";
            this.apartmentGridColumn.Name = "apartmentGridColumn";
            this.apartmentGridColumn.OptionsColumn.AllowEdit = false;
            this.apartmentGridColumn.OptionsColumn.AllowFocus = false;
            this.apartmentGridColumn.Visible = true;
            this.apartmentGridColumn.VisibleIndex = 1;
            // 
            // fullNameGridColumn
            // 
            this.fullNameGridColumn.Caption = "ФИО";
            this.fullNameGridColumn.FieldName = "FullName";
            this.fullNameGridColumn.Name = "fullNameGridColumn";
            this.fullNameGridColumn.OptionsColumn.AllowEdit = false;
            this.fullNameGridColumn.OptionsColumn.AllowFocus = false;
            this.fullNameGridColumn.Visible = true;
            this.fullNameGridColumn.VisibleIndex = 2;
            // 
            // counterNumGridColumn
            // 
            this.counterNumGridColumn.Caption = "Номер ПУ";
            this.counterNumGridColumn.FieldName = "CounterNum";
            this.counterNumGridColumn.Name = "counterNumGridColumn";
            this.counterNumGridColumn.OptionsColumn.AllowEdit = false;
            this.counterNumGridColumn.OptionsColumn.AllowFocus = false;
            this.counterNumGridColumn.Visible = true;
            this.counterNumGridColumn.VisibleIndex = 3;
            // 
            // counterModelGridColumn
            // 
            this.counterModelGridColumn.Caption = "Модель ПУ";
            this.counterModelGridColumn.FieldName = "CounterModel";
            this.counterModelGridColumn.Name = "counterModelGridColumn";
            this.counterModelGridColumn.OptionsColumn.AllowEdit = false;
            this.counterModelGridColumn.OptionsColumn.AllowFocus = false;
            this.counterModelGridColumn.Visible = true;
            this.counterModelGridColumn.VisibleIndex = 4;
            // 
            // prevValueGridColumn
            // 
            this.prevValueGridColumn.Caption = "Пред. показание";
            this.prevValueGridColumn.FieldName = "PrevValue";
            this.prevValueGridColumn.Name = "prevValueGridColumn";
            this.prevValueGridColumn.OptionsColumn.AllowEdit = false;
            this.prevValueGridColumn.OptionsColumn.AllowFocus = false;
            this.prevValueGridColumn.Visible = true;
            this.prevValueGridColumn.VisibleIndex = 5;
            // 
            // valueGridColumn
            // 
            this.valueGridColumn.Caption = "Показание";
            this.valueGridColumn.DisplayFormat.FormatString = "{0:0.000}";
            this.valueGridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.valueGridColumn.FieldName = "Value";
            this.valueGridColumn.Name = "valueGridColumn";
            this.valueGridColumn.Visible = true;
            this.valueGridColumn.VisibleIndex = 6;
            // 
            // collectDateGridColumn
            // 
            this.collectDateGridColumn.Caption = "Дата внесения";
            this.collectDateGridColumn.ColumnEdit = this.collectDateRepositoryItem;
            this.collectDateGridColumn.FieldName = "CollectDate";
            this.collectDateGridColumn.Name = "collectDateGridColumn";
            this.collectDateGridColumn.Visible = true;
            this.collectDateGridColumn.VisibleIndex = 7;
            // 
            // collectDateRepositoryItem
            // 
            this.collectDateRepositoryItem.AutoHeight = false;
            this.collectDateRepositoryItem.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.collectDateRepositoryItem.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.collectDateRepositoryItem.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.collectDateRepositoryItem.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.collectDateRepositoryItem.Name = "collectDateRepositoryItem";
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
            this.errorsGroupBox.Size = new System.Drawing.Size(858, 75);
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
            this.errorMessageTextBox.Size = new System.Drawing.Size(848, 52);
            this.errorMessageTextBox.TabIndex = 43;
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
            // buildingSelectWizardPage
            // 
            this.buildingSelectWizardPage.Controls.Add(this.periodDateEdit);
            this.buildingSelectWizardPage.Controls.Add(this.buildingLookUpEdit);
            this.buildingSelectWizardPage.Controls.Add(this.streetLookUpEdit);
            this.buildingSelectWizardPage.Controls.Add(this.label4);
            this.buildingSelectWizardPage.Controls.Add(this.label3);
            this.buildingSelectWizardPage.Controls.Add(this.streetLabel);
            this.buildingSelectWizardPage.DescriptionText = "Выберите дом и период для внесения показаний индивидуальных приборов учета";
            this.buildingSelectWizardPage.Name = "buildingSelectWizardPage";
            this.buildingSelectWizardPage.Size = new System.Drawing.Size(858, 497);
            this.buildingSelectWizardPage.Text = "Выбор дома и периода";
            // 
            // periodDateEdit
            // 
            this.periodDateEdit.EditValue = null;
            this.periodDateEdit.Location = new System.Drawing.Point(83, 72);
            this.periodDateEdit.Name = "periodDateEdit";
            this.periodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.periodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.periodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.periodDateEdit.Properties.DisplayFormat.FormatString = "MM.yyyy";
            this.periodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.periodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.periodDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.periodDateEdit.Size = new System.Drawing.Size(201, 20);
            this.periodDateEdit.TabIndex = 32;
            // 
            // buildingLookUpEdit
            // 
            this.buildingLookUpEdit.Location = new System.Drawing.Point(326, 32);
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
            this.buildingLookUpEdit.TabIndex = 31;
            this.buildingLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            // 
            // streetLookUpEdit
            // 
            this.streetLookUpEdit.Location = new System.Drawing.Point(83, 32);
            this.streetLookUpEdit.Name = "streetLookUpEdit";
            this.streetLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.streetLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Название")});
            this.streetLookUpEdit.Properties.DisplayMember = "Name";
            this.streetLookUpEdit.Properties.NullText = "(все)";
            this.streetLookUpEdit.Properties.ValueMember = "ID";
            this.streetLookUpEdit.Size = new System.Drawing.Size(201, 20);
            this.streetLookUpEdit.TabIndex = 30;
            this.streetLookUpEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.filterLookUpEdit_ButtonClick);
            this.streetLookUpEdit.EditValueChanged += new System.EventHandler(this.streetLookUpEdit_EditValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Период";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Дом";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(38, 35);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(39, 13);
            this.streetLabel.TabIndex = 0;
            this.streetLabel.Text = "Улица";
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
            ((System.ComponentModel.ISupportInitialize)(this.collectDateRepositoryItem.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectDateRepositoryItem)).EndInit();
            this.errorsGroupBox.ResumeLayout(false);
            this.errorsGroupBox.PerformLayout();
            this.finishWizardPage.ResumeLayout(false);
            this.finishWizardPage.PerformLayout();
            this.buildingSelectWizardPage.ResumeLayout(false);
            this.buildingSelectWizardPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.streetLookUpEdit.Properties)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn counterNumGridColumn;
        private System.Windows.Forms.TextBox errorMessageTextBox;
        private System.Windows.Forms.GroupBox errorsGroupBox;
        private DevExpress.XtraWizard.WizardPage finishWizardPage;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountLabel;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedLabel;
        private System.Windows.Forms.Panel rightTablePanel;
        private DevExpress.XtraGrid.Columns.GridColumn counterIdGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn apartmentGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn counterModelGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn prevValueGridColumn;
        private DevExpress.XtraWizard.WizardPage buildingSelectWizardPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label streetLabel;
        private DevExpress.XtraEditors.LookUpEdit streetLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit buildingLookUpEdit;
        private DevExpress.XtraEditors.DateEdit periodDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit collectDateRepositoryItem;
        private DevExpress.XtraGrid.Columns.GridColumn fullNameGridColumn;
    }
}

