
namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.DecFormsDownloads.Views.Wizard
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
            this.ChooseDirectoryWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.NoteLabel = new System.Windows.Forms.Label();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.DirectoryPathButtonEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.FilesCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.FilesCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.ErrorsCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.ErrorsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.EmailsCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.EmailsCountLabel = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.WizardControl)).BeginInit();
            this.WizardControl.SuspendLayout();
            this.ProcessingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            this.ChooseDirectoryWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryPathButtonEdit.Properties)).BeginInit();
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
            // ChooseDirectoryWizardPage
            // 
            this.ChooseDirectoryWizardPage.Controls.Add(this.NoteTextBox);
            this.ChooseDirectoryWizardPage.Controls.Add(this.NoteLabel);
            this.ChooseDirectoryWizardPage.Controls.Add(this.DirectoryLabel);
            this.ChooseDirectoryWizardPage.Controls.Add(this.DirectoryPathButtonEdit);
            this.ChooseDirectoryWizardPage.DescriptionText = "Выберите папку, в которую будут скачиваться файлы. Комментарий можно указать по ж" +
    "еланию.";
            this.ChooseDirectoryWizardPage.Name = "ChooseDirectoryWizardPage";
            this.ChooseDirectoryWizardPage.Size = new System.Drawing.Size(822, 429);
            this.ChooseDirectoryWizardPage.Text = "Скачивание файлов из почтового ящика.";
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoteTextBox.Location = new System.Drawing.Point(36, 74);
            this.NoteTextBox.MaxLength = 250;
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.Size = new System.Drawing.Size(739, 47);
            this.NoteTextBox.TabIndex = 10;
            // 
            // NoteLabel
            // 
            this.NoteLabel.AutoSize = true;
            this.NoteLabel.BackColor = System.Drawing.Color.Transparent;
            this.NoteLabel.Location = new System.Drawing.Point(33, 58);
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
            this.DirectoryLabel.Size = new System.Drawing.Size(163, 13);
            this.DirectoryLabel.TabIndex = 39;
            this.DirectoryLabel.Text = "Папка для скачивания файлов";
            // 
            // DirectoryPathButtonEdit
            // 
            this.DirectoryPathButtonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectoryPathButtonEdit.Location = new System.Drawing.Point(202, 28);
            this.DirectoryPathButtonEdit.Name = "DirectoryPathButtonEdit";
            this.DirectoryPathButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DirectoryPathButtonEdit.Properties.MaxLength = 200;
            this.DirectoryPathButtonEdit.Size = new System.Drawing.Size(573, 20);
            this.DirectoryPathButtonEdit.TabIndex = 38;
            this.DirectoryPathButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.FileOpenButtonEdit_ButtonClick);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.AllowBack = false;
            this.FinishWizardPage.AllowCancel = false;
            this.FinishWizardPage.Controls.Add(this.FilesCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.FilesCountLabel);
            this.FinishWizardPage.Controls.Add(this.ErrorsCountLabel);
            this.FinishWizardPage.Controls.Add(this.ErrorsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.EmailsCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.EmailsCountLabel);
            this.FinishWizardPage.DescriptionText = "Для окончания нажмите Завершить";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(822, 429);
            this.FinishWizardPage.Text = "Обработка данных завершена";
            // 
            // FilesCountValueLabel
            // 
            this.FilesCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FilesCountValueLabel.Location = new System.Drawing.Point(167, 32);
            this.FilesCountValueLabel.Name = "FilesCountValueLabel";
            this.FilesCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.FilesCountValueLabel.TabIndex = 13;
            this.FilesCountValueLabel.Text = "0";
            // 
            // FilesCountLabel
            // 
            this.FilesCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FilesCountLabel.Location = new System.Drawing.Point(12, 32);
            this.FilesCountLabel.Name = "FilesCountLabel";
            this.FilesCountLabel.Size = new System.Drawing.Size(85, 13);
            this.FilesCountLabel.TabIndex = 14;
            this.FilesCountLabel.Text = "Файлов скачано";
            // 
            // ErrorsCountLabel
            // 
            this.ErrorsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorsCountLabel.Location = new System.Drawing.Point(12, 51);
            this.ErrorsCountLabel.Name = "ErrorsCountLabel";
            this.ErrorsCountLabel.Size = new System.Drawing.Size(99, 13);
            this.ErrorsCountLabel.TabIndex = 10;
            this.ErrorsCountLabel.Text = "Ошибок произошло";
            // 
            // ErrorsCountValueLabel
            // 
            this.ErrorsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ErrorsCountValueLabel.Location = new System.Drawing.Point(167, 51);
            this.ErrorsCountValueLabel.Name = "ErrorsCountValueLabel";
            this.ErrorsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.ErrorsCountValueLabel.TabIndex = 11;
            this.ErrorsCountValueLabel.Text = "0";
            // 
            // EmailsCountValueLabel
            // 
            this.EmailsCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmailsCountValueLabel.Location = new System.Drawing.Point(167, 13);
            this.EmailsCountValueLabel.Name = "EmailsCountValueLabel";
            this.EmailsCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.EmailsCountValueLabel.TabIndex = 8;
            this.EmailsCountValueLabel.Text = "0";
            // 
            // EmailsCountLabel
            // 
            this.EmailsCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EmailsCountLabel.Location = new System.Drawing.Point(12, 13);
            this.EmailsCountLabel.Name = "EmailsCountLabel";
            this.EmailsCountLabel.Size = new System.Drawing.Size(113, 13);
            this.EmailsCountLabel.TabIndex = 9;
            this.EmailsCountLabel.Text = "Сообщений прочитано";
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
            this.ChooseDirectoryWizardPage.ResumeLayout(false);
            this.ChooseDirectoryWizardPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DirectoryPathButtonEdit.Properties)).EndInit();
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
        private DevExpress.XtraEditors.ButtonEdit DirectoryPathButtonEdit;
        private System.Windows.Forms.Label DirectoryLabel;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private DevExpress.XtraEditors.LabelControl ErrorsCountLabel;
        private DevExpress.XtraEditors.LabelControl ErrorsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl EmailsCountValueLabel;
        private DevExpress.XtraEditors.LabelControl EmailsCountLabel;
        private DevExpress.XtraEditors.LabelControl FilesCountValueLabel;
        private DevExpress.XtraEditors.LabelControl FilesCountLabel;
        private System.Windows.Forms.Label ProgressLabel;
    }
}

