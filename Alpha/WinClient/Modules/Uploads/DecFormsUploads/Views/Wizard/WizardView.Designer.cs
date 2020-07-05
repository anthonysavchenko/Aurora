
namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsUploads.Views.Wizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardView));
            this.WizardControl = new DevExpress.XtraWizard.WizardControl();
            this.ProcessingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.ProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.ChooseDirectoryWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.MonthLabel = new DevExpress.XtraEditors.LabelControl();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.MonthDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.DirectoryButtonEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.FillFormsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.PrintFormsCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.ExceptionsCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.ExceptionsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.UnknownFilesCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.UnknownFilesCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.RouteFormsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.RouteFormsCountLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.WizardControl)).BeginInit();
            this.WizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            this.ChooseDirectoryWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryButtonEdit.Properties)).BeginInit();
            this.FinishWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // WizardControl
            // 
            this.WizardControl.CancelText = "Отмена";
            this.WizardControl.Controls.Add(this.ProcessingWizardPage);
            this.WizardControl.Controls.Add(this.ChooseDirectoryWizardPage);
            this.WizardControl.Controls.Add(this.FinishWizardPage);
            this.WizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WizardControl.FinishText = "Завершить";
            this.WizardControl.Location = new System.Drawing.Point(0, 0);
            this.WizardControl.Name = "WizardControl";
            this.WizardControl.NextText = "&Далее>";
            this.WizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.ChooseDirectoryWizardPage,
            this.ProcessingWizardPage,
            this.FinishWizardPage});
            this.WizardControl.PreviousText = "< &Назад";
            this.WizardControl.Size = new System.Drawing.Size(890, 642);
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
            this.ProcessingWizardPage.Controls.Add(this.ProgressBarControl);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(858, 497);
            this.ProcessingWizardPage.Text = "Обработка данных";
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarControl.Location = new System.Drawing.Point(43, 32);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Size = new System.Drawing.Size(761, 26);
            this.ProgressBarControl.TabIndex = 0;
            // 
            // ChooseDirectoryWizardPage
            // 
            this.ChooseDirectoryWizardPage.Controls.Add(this.MonthLabel);
            this.ChooseDirectoryWizardPage.Controls.Add(this.NoteTextBox);
            this.ChooseDirectoryWizardPage.Controls.Add(this.MonthDateEdit);
            this.ChooseDirectoryWizardPage.Controls.Add(this.NoteLabel);
            this.ChooseDirectoryWizardPage.Controls.Add(this.DirectoryLabel);
            this.ChooseDirectoryWizardPage.Controls.Add(this.DirectoryButtonEdit);
            this.ChooseDirectoryWizardPage.DescriptionText = resources.GetString("ChooseDirectoryWizardPage.DescriptionText");
            this.ChooseDirectoryWizardPage.Name = "ChooseDirectoryWizardPage";
            this.ChooseDirectoryWizardPage.Size = new System.Drawing.Size(858, 497);
            this.ChooseDirectoryWizardPage.Text = "Загрузка файлов в формате \"Маршрутный лист\" и \"Форма для заполнения\".";
            // 
            // MonthLabel
            // 
            this.MonthLabel.Location = new System.Drawing.Point(36, 57);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(204, 13);
            this.MonthLabel.TabIndex = 38;
            this.MonthLabel.Text = "Месяц, за который загружаются файлы";
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteTextBox.Location = new System.Drawing.Point(36, 101);
            this.NoteTextBox.MaxLength = 250;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.Size = new System.Drawing.Size(775, 47);
            this.NoteTextBox.TabIndex = 10;
            // 
            // MonthDateEdit
            // 
            this.MonthDateEdit.EditValue = null;
            this.MonthDateEdit.Location = new System.Drawing.Point(249, 54);
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
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.DirectoryLabel.Location = new System.Drawing.Point(33, 31);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(167, 13);
            this.DirectoryLabel.TabIndex = 39;
            this.DirectoryLabel.Text = "Папка с файлами для загрузки";
            // 
            // DirectoryButtonEdit
            // 
            this.DirectoryButtonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryButtonEdit.Location = new System.Drawing.Point(249, 28);
            this.DirectoryButtonEdit.Name = "DirectoryButtonEdit";
            this.DirectoryButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DirectoryButtonEdit.Size = new System.Drawing.Size(562, 20);
            this.DirectoryButtonEdit.TabIndex = 38;
            this.DirectoryButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FileOpenButtonEdit_ButtonClick);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.AllowBack = false;
            this.FinishWizardPage.AllowCancel = false;
            this.FinishWizardPage.Controls.Add(this.FillFormsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.PrintFormsCountLabel);
            this.FinishWizardPage.Controls.Add(this.ExceptionsCountLabel);
            this.FinishWizardPage.Controls.Add(this.ExceptionsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.UnknownFilesCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.UnknownFilesCountLabel);
            this.FinishWizardPage.Controls.Add(this.RouteFormsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.RouteFormsCountLabel);
            this.FinishWizardPage.DescriptionText = "Для окончания нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(858, 497);
            this.FinishWizardPage.Text = "Обработка данных завершена";
            // 
            // FillFormsCountValueLabel
            // 
            this.FillFormsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillFormsCountValueLabel.Location = new System.Drawing.Point(167, 32);
            this.FillFormsCountValueLabel.Name = "FillFormsCountValueLabel";
            this.FillFormsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.FillFormsCountValueLabel.TabIndex = 13;
            this.FillFormsCountValueLabel.Text = "0";
            // 
            // PrintFormsCountLabel
            // 
            this.PrintFormsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PrintFormsCountLabel.Location = new System.Drawing.Point(12, 32);
            this.PrintFormsCountLabel.Name = "PrintFormsCountLabel";
            this.PrintFormsCountLabel.Size = new System.Drawing.Size(115, 13);
            this.PrintFormsCountLabel.TabIndex = 14;
            this.PrintFormsCountLabel.Text = "Форм для заполнения";
            // 
            // ExceptionsCountLabel
            // 
            this.ExceptionsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExceptionsCountLabel.Location = new System.Drawing.Point(12, 70);
            this.ExceptionsCountLabel.Name = "ExceptionsCountLabel";
            this.ExceptionsCountLabel.Size = new System.Drawing.Size(113, 13);
            this.ExceptionsCountLabel.TabIndex = 10;
            this.ExceptionsCountLabel.Text = "Программных ошибок";
            // 
            // ExceptionsCountValueLabel
            // 
            this.ExceptionsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExceptionsCountValueLabel.Location = new System.Drawing.Point(167, 70);
            this.ExceptionsCountValueLabel.Name = "ExceptionsCountValueLabel";
            this.ExceptionsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.ExceptionsCountValueLabel.TabIndex = 11;
            this.ExceptionsCountValueLabel.Text = "0";
            // 
            // UnknownFilesCountValueLabel
            // 
            this.UnknownFilesCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnknownFilesCountValueLabel.Location = new System.Drawing.Point(167, 51);
            this.UnknownFilesCountValueLabel.Name = "UnknownFilesCountValueLabel";
            this.UnknownFilesCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.UnknownFilesCountValueLabel.TabIndex = 12;
            this.UnknownFilesCountValueLabel.Text = "0";
            // 
            // UnknownFilesCountLabel
            // 
            this.UnknownFilesCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnknownFilesCountLabel.Location = new System.Drawing.Point(12, 51);
            this.UnknownFilesCountLabel.Name = "UnknownFilesCountLabel";
            this.UnknownFilesCountLabel.Size = new System.Drawing.Size(149, 13);
            this.UnknownFilesCountLabel.TabIndex = 7;
            this.UnknownFilesCountLabel.Text = "Формат файла не определен";
            // 
            // RouteFormsCountValueLabel
            // 
            this.RouteFormsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RouteFormsCountValueLabel.Location = new System.Drawing.Point(167, 13);
            this.RouteFormsCountValueLabel.Name = "RouteFormsCountValueLabel";
            this.RouteFormsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.RouteFormsCountValueLabel.TabIndex = 8;
            this.RouteFormsCountValueLabel.Text = "0";
            // 
            // RouteFormsCountLabel
            // 
            this.RouteFormsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RouteFormsCountLabel.Location = new System.Drawing.Point(12, 13);
            this.RouteFormsCountLabel.Name = "RouteFormsCountLabel";
            this.RouteFormsCountLabel.Size = new System.Drawing.Size(102, 13);
            this.RouteFormsCountLabel.TabIndex = 9;
            this.RouteFormsCountLabel.Text = "Маршрутных листов";
            // 
            // WizardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WizardControl);
            this.Name = "WizardView";
            this.Size = new System.Drawing.Size(890, 642);
            ((System.ComponentModel.ISupportInitialize)(this.WizardControl)).EndInit();
            this.WizardControl.ResumeLayout(false);
            this.ProcessingWizardPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).EndInit();
            this.ChooseDirectoryWizardPage.ResumeLayout(false);
            this.ChooseDirectoryWizardPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryButtonEdit.Properties)).EndInit();
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl WizardControl;
        private DevExpress.XtraWizard.WizardPage ProcessingWizardPage;
        private DevExpress.XtraEditors.ProgressBarControl ProgressBarControl;
        private DevExpress.XtraWizard.WizardPage ChooseDirectoryWizardPage;
        private System.Windows.Forms.Label NoteLabel;
        private System.Windows.Forms.TextBox NoteTextBox;
        private DevExpress.XtraEditors.ButtonEdit DirectoryButtonEdit;
        private System.Windows.Forms.Label DirectoryLabel;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private DevExpress.XtraEditors.LabelControl ExceptionsCountLabel;
        private DevExpress.XtraEditors.LabelControl ExceptionsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl UnknownFilesCountValueLabel;
        private DevExpress.XtraEditors.LabelControl UnknownFilesCountLabel;
        private DevExpress.XtraEditors.LabelControl RouteFormsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl RouteFormsCountLabel;
        private DevExpress.XtraEditors.DateEdit MonthDateEdit;
        private DevExpress.XtraEditors.LabelControl MonthLabel;
        private DevExpress.XtraEditors.LabelControl FillFormsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl PrintFormsCountLabel;
    }
}

