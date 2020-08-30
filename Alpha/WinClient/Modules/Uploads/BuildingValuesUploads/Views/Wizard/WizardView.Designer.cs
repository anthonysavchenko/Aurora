
namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Wizard
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
            this.WizardControl = new DevExpress.XtraWizard.WizardControl();
            this.ProcessingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.ProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.ChoosePathWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.MonthLabel = new DevExpress.XtraEditors.LabelControl();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.MonthDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathButtonEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.ErrorsCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.ErrorsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.BuildingValuesCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.BuuildingValuesCountLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.WizardControl)).BeginInit();
            this.WizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            this.ChoosePathWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathButtonEdit.Properties)).BeginInit();
            this.FinishWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardControl
            // 
            this.WizardControl.CancelText = "Отмена";
            this.WizardControl.Controls.Add(this.ProcessingWizardPage);
            this.WizardControl.Controls.Add(this.ChoosePathWizardPage);
            this.WizardControl.Controls.Add(this.FinishWizardPage);
            this.WizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardControl.FinishText = "Завершить";
            this.WizardControl.Location = new System.Drawing.Point(0, 0);
            this.WizardControl.Name = "WizardControl";
            this.WizardControl.NextText = "&Далее>";
            this.WizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.ChoosePathWizardPage,
            this.ProcessingWizardPage,
            this.FinishWizardPage});
            this.WizardControl.PreviousText = "< &Назад";
            this.WizardControl.Size = new System.Drawing.Size(854, 574);
            this.WizardControl.Text = "Мастер загрузки файлов";
            this.WizardControl.UseAcceptButton = false;
            this.WizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.PaymentWizardControl_SelectedPageChanged);
            this.WizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.PaymentWizardControl_SelectedPageChanging);
            this.WizardControl.CancelClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_CancelClick);
            this.WizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_FinishClick);
            // 
            // ProcessingWizardPage
            // 
            this.ProcessingWizardPage.AllowBack = false;
            this.ProcessingWizardPage.AllowCancel = false;
            this.ProcessingWizardPage.AllowNext = false;
            this.ProcessingWizardPage.Controls.Add(this.ProgressLabel);
            this.ProcessingWizardPage.Controls.Add(this.ProgressBarControl);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(822, 429);
            this.ProcessingWizardPage.Text = "Обработка данных";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressLabel.Location = new System.Drawing.Point(43, 35);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(725, 21);
            this.ProgressLabel.TabIndex = 2;
            this.ProgressLabel.Text = "Обработка данных...";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarControl.Location = new System.Drawing.Point(43, 59);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Properties.ShowTitle = true;
            this.ProgressBarControl.ShowProgressInTaskBar = true;
            this.ProgressBarControl.Size = new System.Drawing.Size(725, 26);
            this.ProgressBarControl.TabIndex = 0;
            // 
            // ChoosePathWizardPage
            // 
            this.ChoosePathWizardPage.Controls.Add(this.MonthLabel);
            this.ChoosePathWizardPage.Controls.Add(this.NoteTextBox);
            this.ChoosePathWizardPage.Controls.Add(this.MonthDateEdit);
            this.ChoosePathWizardPage.Controls.Add(this.NoteLabel);
            this.ChoosePathWizardPage.Controls.Add(this.PathLabel);
            this.ChoosePathWizardPage.Controls.Add(this.PathButtonEdit);
            this.ChoosePathWizardPage.DescriptionText = "Выберите файл, из которого будут загружаться показаниями ОДПУ. Укажите месяц, за " +
    "который будут учитываться данные (конкретное число месяца не имеет значения). Ко" +
    "мментарий можно указать по желанию.";
            this.ChoosePathWizardPage.Name = "ChoosePathWizardPage";
            this.ChoosePathWizardPage.Size = new System.Drawing.Size(822, 429);
            this.ChoosePathWizardPage.Text = "Загрузка файла с показаниями ОДПУ.";
            // 
            // MonthLabel
            // 
            this.MonthLabel.Location = new System.Drawing.Point(36, 57);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(223, 13);
            this.MonthLabel.TabIndex = 38;
            this.MonthLabel.Text = "Месяц, за который загружаются показания";
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteTextBox.Location = new System.Drawing.Point(36, 101);
            this.NoteTextBox.MaxLength = 250;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.Size = new System.Drawing.Size(739, 47);
            this.NoteTextBox.TabIndex = 10;
            // 
            // MonthDateEdit
            // 
            this.MonthDateEdit.EditValue = null;
            this.MonthDateEdit.Location = new System.Drawing.Point(265, 54);
            this.MonthDateEdit.Name = "MonthDateEdit";
            this.MonthDateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.MonthDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MonthDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MonthDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.MonthDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.MonthDateEdit.Properties.DisplayFormat.FormatString = "MM.yyyy";
            this.MonthDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.MonthDateEdit.Properties.EditFormat.FormatString = "MM.yyyy";
            this.MonthDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.MonthDateEdit.Properties.Mask.EditMask = "MM.yyyy";
            this.MonthDateEdit.Size = new System.Drawing.Size(140, 20);
            this.MonthDateEdit.TabIndex = 39;
            // 
            // NoteLabel
            // 
            this.NoteLabel.AutoSize = true;
            this.NoteLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoteLabel.Location = new System.Drawing.Point(33, 85);
            this.NoteLabel.Name = "NoteLabel";
            this.NoteLabel.Size = new System.Drawing.Size(77, 13);
            this.NoteLabel.TabIndex = 12;
            this.NoteLabel.Text = "Комментарий";
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.BackColor = System.Drawing.Color.Transparent;
            this.PathLabel.Location = new System.Drawing.Point(33, 31);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(152, 13);
            this.PathLabel.TabIndex = 39;
            this.PathLabel.Text = "Файл с показаниями ОДПУ";
            // 
            // PathButtonEdit
            // 
            this.PathButtonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathButtonEdit.Location = new System.Drawing.Point(265, 28);
            this.PathButtonEdit.Name = "PathButtonEdit";
            this.PathButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.PathButtonEdit.Size = new System.Drawing.Size(510, 20);
            this.PathButtonEdit.TabIndex = 38;
            this.PathButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FileOpenButtonEdit_ButtonClick);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.AllowBack = false;
            this.FinishWizardPage.AllowCancel = false;
            this.FinishWizardPage.Controls.Add(this.ErrorsCountLabel);
            this.FinishWizardPage.Controls.Add(this.ErrorsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.BuildingValuesCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.BuuildingValuesCountLabel);
            this.FinishWizardPage.DescriptionText = "Для окончания нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(822, 429);
            this.FinishWizardPage.Text = "Обработка данных завершена";
            // 
            // ErrorsCountLabel
            // 
            this.ErrorsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorsCountLabel.Location = new System.Drawing.Point(12, 32);
            this.ErrorsCountLabel.Name = "ErrorsCountLabel";
            this.ErrorsCountLabel.Size = new System.Drawing.Size(99, 13);
            this.ErrorsCountLabel.TabIndex = 10;
            this.ErrorsCountLabel.Text = "Ошибок произошло";
            // 
            // ErrorsCountValueLabel
            // 
            this.ErrorsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorsCountValueLabel.Location = new System.Drawing.Point(292, 32);
            this.ErrorsCountValueLabel.Name = "ErrorsCountValueLabel";
            this.ErrorsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.ErrorsCountValueLabel.TabIndex = 11;
            this.ErrorsCountValueLabel.Text = "0";
            // 
            // BuildingValuesCountValueLabel
            // 
            this.BuildingValuesCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuildingValuesCountValueLabel.Location = new System.Drawing.Point(292, 13);
            this.BuildingValuesCountValueLabel.Name = "BuildingValuesCountValueLabel";
            this.BuildingValuesCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.BuildingValuesCountValueLabel.TabIndex = 8;
            this.BuildingValuesCountValueLabel.Text = "0";
            // 
            // BuuildingValuesCountLabel
            // 
            this.BuuildingValuesCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuuildingValuesCountLabel.Location = new System.Drawing.Point(12, 13);
            this.BuuildingValuesCountLabel.Name = "BuuildingValuesCountLabel";
            this.BuuildingValuesCountLabel.Size = new System.Drawing.Size(274, 13);
            this.BuuildingValuesCountLabel.TabIndex = 9;
            this.BuuildingValuesCountLabel.Text = "Строк с показаниями ОДПУ распознано и сохранено";
            // 
            // WizardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WizardControl);
            this.Name = "WizardView";
            this.Size = new System.Drawing.Size(854, 574);
            ((System.ComponentModel.ISupportInitialize)(this.WizardControl)).EndInit();
            this.WizardControl.ResumeLayout(false);
            this.ProcessingWizardPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).EndInit();
            this.ChoosePathWizardPage.ResumeLayout(false);
            this.ChoosePathWizardPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PathButtonEdit.Properties)).EndInit();
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl WizardControl;
        private DevExpress.XtraWizard.WizardPage ProcessingWizardPage;
        private DevExpress.XtraEditors.ProgressBarControl ProgressBarControl;
        private DevExpress.XtraWizard.WizardPage ChoosePathWizardPage;
        private System.Windows.Forms.Label NoteLabel;
        private System.Windows.Forms.TextBox NoteTextBox;
        private DevExpress.XtraEditors.ButtonEdit PathButtonEdit;
        private System.Windows.Forms.Label PathLabel;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private DevExpress.XtraEditors.LabelControl ErrorsCountLabel;
        private DevExpress.XtraEditors.LabelControl ErrorsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl BuildingValuesCountValueLabel;
        private DevExpress.XtraEditors.LabelControl BuuildingValuesCountLabel;
        private DevExpress.XtraEditors.DateEdit MonthDateEdit;
        private DevExpress.XtraEditors.LabelControl MonthLabel;
        private System.Windows.Forms.Label ProgressLabel;
    }
}

