
namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Wizard
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
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ChooseMethodWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.totalGroupBox = new System.Windows.Forms.GroupBox();
            this.TotalBillTillPeriodDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.AccountTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.debtGroupBox = new System.Windows.Forms.GroupBox();
            this.MonthYearDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.ValueTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalBillRadioButton = new System.Windows.Forms.RadioButton();
            this.DebtRadioButton = new System.Windows.Forms.RadioButton();
            this.ProcessingWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.ProgressBarControl = new DevExpress.XtraEditors.ProgressBarControl();
            this.PaymentWizardControl = new DevExpress.XtraWizard.WizardControl();
            this.FinishWizardPage = new DevExpress.XtraWizard.WizardPage();
            this.TotalErrorCountLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalErrorCountValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalAmountLabelValue = new DevExpress.XtraEditors.LabelControl();
            this.TotalAmountLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedValueLabel = new DevExpress.XtraEditors.LabelControl();
            this.TotalProcessedLabel = new DevExpress.XtraEditors.LabelControl();
            this.ChooseMethodWizardPage.SuspendLayout();
            this.totalGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TotalBillTillPeriodDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalBillTillPeriodDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTextEdit.Properties)).BeginInit();
            this.debtGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthYearDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthYearDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTextEdit.Properties)).BeginInit();
            this.ProcessingWizardPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentWizardControl)).BeginInit();
            this.PaymentWizardControl.SuspendLayout();
            this.FinishWizardPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Title = "Открыть файл";
            // 
            // ChooseMethodWizardPage
            // 
            this.ChooseMethodWizardPage.Controls.Add(this.totalGroupBox);
            this.ChooseMethodWizardPage.Controls.Add(this.debtGroupBox);
            this.ChooseMethodWizardPage.Controls.Add(this.TotalBillRadioButton);
            this.ChooseMethodWizardPage.Controls.Add(this.DebtRadioButton);
            this.ChooseMethodWizardPage.DescriptionText = "Выберите тип создаваемых квитанций";
            this.ChooseMethodWizardPage.Name = "ChooseMethodWizardPage";
            this.ChooseMethodWizardPage.Size = new System.Drawing.Size(781, 366);
            this.ChooseMethodWizardPage.Text = "Печать квитанций";
            // 
            // totalGroupBox
            // 
            this.totalGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.totalGroupBox.Controls.Add(this.TotalBillTillPeriodDateEdit);
            this.totalGroupBox.Controls.Add(this.label3);
            this.totalGroupBox.Controls.Add(this.AccountTextEdit);
            this.totalGroupBox.Controls.Add(this.label4);
            this.totalGroupBox.Enabled = false;
            this.totalGroupBox.Location = new System.Drawing.Point(22, 133);
            this.totalGroupBox.Name = "totalGroupBox";
            this.totalGroupBox.Size = new System.Drawing.Size(383, 79);
            this.totalGroupBox.TabIndex = 7;
            this.totalGroupBox.TabStop = false;
            // 
            // TotalBillTillPeriodDateEdit
            // 
            this.TotalBillTillPeriodDateEdit.EditValue = null;
            this.TotalBillTillPeriodDateEdit.Location = new System.Drawing.Point(141, 45);
            this.TotalBillTillPeriodDateEdit.Name = "TotalBillTillPeriodDateEdit";
            this.TotalBillTillPeriodDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TotalBillTillPeriodDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.TotalBillTillPeriodDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.TotalBillTillPeriodDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.TotalBillTillPeriodDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.TotalBillTillPeriodDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TotalBillTillPeriodDateEdit.Properties.EditFormat.FormatString = "y";
            this.TotalBillTillPeriodDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TotalBillTillPeriodDateEdit.Properties.Mask.EditMask = "y";
            this.TotalBillTillPeriodDateEdit.Size = new System.Drawing.Size(212, 20);
            this.TotalBillTillPeriodDateEdit.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(19, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Выбирать до месяца:";
            // 
            // AccountTextEdit
            // 
            this.AccountTextEdit.EditValue = "";
            this.AccountTextEdit.Location = new System.Drawing.Point(141, 19);
            this.AccountTextEdit.Name = "AccountTextEdit";
            this.AccountTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.AccountTextEdit.Properties.Mask.EditMask = "\\d\\d\\d\\d-\\d\\d\\d-\\d";
            this.AccountTextEdit.Properties.Mask.IgnoreMaskBlank = false;
            this.AccountTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.AccountTextEdit.Properties.Mask.PlaceHolder = '0';
            this.AccountTextEdit.Size = new System.Drawing.Size(212, 20);
            this.AccountTextEdit.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(19, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Лицевой счет:";
            // 
            // debtGroupBox
            // 
            this.debtGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.debtGroupBox.Controls.Add(this.MonthYearDateEdit);
            this.debtGroupBox.Controls.Add(this.ValueTextEdit);
            this.debtGroupBox.Controls.Add(this.label1);
            this.debtGroupBox.Controls.Add(this.label2);
            this.debtGroupBox.Location = new System.Drawing.Point(22, 26);
            this.debtGroupBox.Name = "debtGroupBox";
            this.debtGroupBox.Size = new System.Drawing.Size(383, 78);
            this.debtGroupBox.TabIndex = 6;
            this.debtGroupBox.TabStop = false;
            // 
            // MonthYearDateEdit
            // 
            this.MonthYearDateEdit.EditValue = null;
            this.MonthYearDateEdit.Location = new System.Drawing.Point(141, 17);
            this.MonthYearDateEdit.Name = "MonthYearDateEdit";
            this.MonthYearDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.MonthYearDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.MonthYearDateEdit.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.MonthYearDateEdit.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.MonthYearDateEdit.Properties.DisplayFormat.FormatString = "y";
            this.MonthYearDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.MonthYearDateEdit.Properties.EditFormat.FormatString = "y";
            this.MonthYearDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.MonthYearDateEdit.Properties.Mask.EditMask = "y";
            this.MonthYearDateEdit.Size = new System.Drawing.Size(212, 20);
            this.MonthYearDateEdit.TabIndex = 3;
            // 
            // ValueTextEdit
            // 
            this.ValueTextEdit.EditValue = "";
            this.ValueTextEdit.Location = new System.Drawing.Point(141, 43);
            this.ValueTextEdit.Name = "ValueTextEdit";
            this.ValueTextEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.ValueTextEdit.Properties.DisplayFormat.FormatString = "n2";
            this.ValueTextEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ValueTextEdit.Properties.EditFormat.FormatString = "n2";
            this.ValueTextEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ValueTextEdit.Properties.Mask.EditMask = "n02";
            this.ValueTextEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ValueTextEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.ValueTextEdit.Size = new System.Drawing.Size(212, 20);
            this.ValueTextEdit.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выбирать до месяца:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(19, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Минимальная сумма:";
            // 
            // TotalBillRadioButton
            // 
            this.TotalBillRadioButton.AutoSize = true;
            this.TotalBillRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.TotalBillRadioButton.Location = new System.Drawing.Point(3, 110);
            this.TotalBillRadioButton.Name = "TotalBillRadioButton";
            this.TotalBillRadioButton.Size = new System.Drawing.Size(400, 17);
            this.TotalBillRadioButton.TabIndex = 0;
            this.TotalBillRadioButton.Text = "Создать квитанцию о доплате со справкой об отсутствии задолженности";
            this.TotalBillRadioButton.UseVisualStyleBackColor = false;
            this.TotalBillRadioButton.CheckedChanged += new System.EventHandler(this.BillTyppes_CheckedChanged);
            // 
            // DebtRadioButton
            // 
            this.DebtRadioButton.AutoSize = true;
            this.DebtRadioButton.BackColor = System.Drawing.Color.Transparent;
            this.DebtRadioButton.Checked = true;
            this.DebtRadioButton.Location = new System.Drawing.Point(3, 3);
            this.DebtRadioButton.Name = "DebtRadioButton";
            this.DebtRadioButton.Size = new System.Drawing.Size(175, 17);
            this.DebtRadioButton.TabIndex = 0;
            this.DebtRadioButton.TabStop = true;
            this.DebtRadioButton.Text = "Создать долговые квитанции";
            this.DebtRadioButton.UseVisualStyleBackColor = false;
            this.DebtRadioButton.CheckedChanged += new System.EventHandler(this.BillTyppes_CheckedChanged);
            // 
            // ProcessingWizardPage
            // 
            this.ProcessingWizardPage.AllowBack = false;
            this.ProcessingWizardPage.AllowCancel = false;
            this.ProcessingWizardPage.AllowNext = false;
            this.ProcessingWizardPage.Controls.Add(this.ProgressBarControl);
            this.ProcessingWizardPage.DescriptionText = "Дождитесь окончания обработки данных...";
            this.ProcessingWizardPage.Name = "ProcessingWizardPage";
            this.ProcessingWizardPage.Size = new System.Drawing.Size(781, 366);
            this.ProcessingWizardPage.Text = "Обработка данных";
            // 
            // ProgressBarControl
            // 
            this.ProgressBarControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBarControl.Location = new System.Drawing.Point(0, 32);
            this.ProgressBarControl.Name = "ProgressBarControl";
            this.ProgressBarControl.Size = new System.Drawing.Size(778, 26);
            this.ProgressBarControl.TabIndex = 0;
            // 
            // PaymentWizardControl
            // 
            this.PaymentWizardControl.CancelText = "Отмена";
            this.PaymentWizardControl.Controls.Add(this.ProcessingWizardPage);
            this.PaymentWizardControl.Controls.Add(this.ChooseMethodWizardPage);
            this.PaymentWizardControl.Controls.Add(this.FinishWizardPage);
            this.PaymentWizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaymentWizardControl.FinishText = "Завершить";
            this.PaymentWizardControl.Location = new System.Drawing.Point(0, 0);
            this.PaymentWizardControl.Name = "PaymentWizardControl";
            this.PaymentWizardControl.NextText = "&Далее>";
            this.PaymentWizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.ChooseMethodWizardPage,
            this.ProcessingWizardPage,
            this.FinishWizardPage});
            this.PaymentWizardControl.PreviousText = "< &Назад";
            this.PaymentWizardControl.Size = new System.Drawing.Size(813, 511);
            this.PaymentWizardControl.Text = "Мастер внесение платежей";
            this.PaymentWizardControl.UseAcceptButton = false;
            this.PaymentWizardControl.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.PaymentWizardControl_SelectedPageChanged);
            this.PaymentWizardControl.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.PaymentWizardControl_SelectedPageChanging);
            this.PaymentWizardControl.CancelClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_CancelClick);
            this.PaymentWizardControl.FinishClick += new System.ComponentModel.CancelEventHandler(this.PaymentWizardControl_FinishClick);
            // 
            // FinishWizardPage
            // 
            this.FinishWizardPage.AllowBack = false;
            this.FinishWizardPage.AllowCancel = false;
            this.FinishWizardPage.Controls.Add(this.TotalErrorCountLabel);
            this.FinishWizardPage.Controls.Add(this.TotalErrorCountValueLabel);
            this.FinishWizardPage.Controls.Add(this.TotalAmountLabelValue);
            this.FinishWizardPage.Controls.Add(this.TotalAmountLabel);
            this.FinishWizardPage.Controls.Add(this.TotalProcessedValueLabel);
            this.FinishWizardPage.Controls.Add(this.TotalProcessedLabel);
            this.FinishWizardPage.DescriptionText = "Для окончания работы с мастером нажмите Завершить.";
            this.FinishWizardPage.Name = "FinishWizardPage";
            this.FinishWizardPage.Size = new System.Drawing.Size(781, 366);
            this.FinishWizardPage.Text = "Обработка данных завершена";
            // 
            // TotalErrorCountLabel
            // 
            this.TotalErrorCountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalErrorCountLabel.Location = new System.Drawing.Point(12, 76);
            this.TotalErrorCountLabel.Name = "TotalErrorCountLabel";
            this.TotalErrorCountLabel.Size = new System.Drawing.Size(43, 13);
            this.TotalErrorCountLabel.TabIndex = 4;
            this.TotalErrorCountLabel.Text = "Ошибок:";
            // 
            // TotalErrorCountValueLabel
            // 
            this.TotalErrorCountValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalErrorCountValueLabel.Location = new System.Drawing.Point(113, 76);
            this.TotalErrorCountValueLabel.Name = "TotalErrorCountValueLabel";
            this.TotalErrorCountValueLabel.Size = new System.Drawing.Size(6, 13);
            this.TotalErrorCountValueLabel.TabIndex = 5;
            this.TotalErrorCountValueLabel.Text = "0";
            // 
            // TotalAmountLabelValue
            // 
            this.TotalAmountLabelValue.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalAmountLabelValue.Location = new System.Drawing.Point(113, 43);
            this.TotalAmountLabelValue.Name = "TotalAmountLabelValue";
            this.TotalAmountLabelValue.Size = new System.Drawing.Size(6, 13);
            this.TotalAmountLabelValue.TabIndex = 6;
            this.TotalAmountLabelValue.Text = "0";
            // 
            // TotalAmountLabel
            // 
            this.TotalAmountLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalAmountLabel.Location = new System.Drawing.Point(12, 43);
            this.TotalAmountLabel.Name = "TotalAmountLabel";
            this.TotalAmountLabel.Size = new System.Drawing.Size(89, 13);
            this.TotalAmountLabel.TabIndex = 1;
            this.TotalAmountLabel.Text = "На общую сумму:";
            // 
            // TotalProcessedValueLabel
            // 
            this.TotalProcessedValueLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalProcessedValueLabel.Location = new System.Drawing.Point(113, 13);
            this.TotalProcessedValueLabel.Name = "TotalProcessedValueLabel";
            this.TotalProcessedValueLabel.Size = new System.Drawing.Size(6, 13);
            this.TotalProcessedValueLabel.TabIndex = 2;
            this.TotalProcessedValueLabel.Text = "0";
            // 
            // TotalProcessedLabel
            // 
            this.TotalProcessedLabel.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TotalProcessedLabel.Location = new System.Drawing.Point(12, 13);
            this.TotalProcessedLabel.Name = "TotalProcessedLabel";
            this.TotalProcessedLabel.Size = new System.Drawing.Size(95, 13);
            this.TotalProcessedLabel.TabIndex = 3;
            this.TotalProcessedLabel.Text = "Всего обработано:";
            // 
            // WizardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PaymentWizardControl);
            this.Name = "WizardView";
            this.Size = new System.Drawing.Size(813, 511);
            this.ChooseMethodWizardPage.ResumeLayout(false);
            this.ChooseMethodWizardPage.PerformLayout();
            this.totalGroupBox.ResumeLayout(false);
            this.totalGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TotalBillTillPeriodDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalBillTillPeriodDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTextEdit.Properties)).EndInit();
            this.debtGroupBox.ResumeLayout(false);
            this.debtGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthYearDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthYearDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueTextEdit.Properties)).EndInit();
            this.ProcessingWizardPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentWizardControl)).EndInit();
            this.PaymentWizardControl.ResumeLayout(false);
            this.FinishWizardPage.ResumeLayout(false);
            this.FinishWizardPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private DevExpress.XtraWizard.WizardPage ChooseMethodWizardPage;
        private System.Windows.Forms.GroupBox debtGroupBox;
        private DevExpress.XtraEditors.DateEdit MonthYearDateEdit;
        private DevExpress.XtraEditors.TextEdit ValueTextEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton TotalBillRadioButton;
        private System.Windows.Forms.RadioButton DebtRadioButton;
        private DevExpress.XtraWizard.WizardPage ProcessingWizardPage;
        private DevExpress.XtraEditors.ProgressBarControl ProgressBarControl;
        private DevExpress.XtraWizard.WizardControl PaymentWizardControl;
        private DevExpress.XtraWizard.WizardPage FinishWizardPage;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountLabel;
        private DevExpress.XtraEditors.LabelControl TotalErrorCountValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalAmountLabelValue;
        private DevExpress.XtraEditors.LabelControl TotalAmountLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedValueLabel;
        private DevExpress.XtraEditors.LabelControl TotalProcessedLabel;
        private System.Windows.Forms.GroupBox totalGroupBox;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit AccountTextEdit;
        private DevExpress.XtraEditors.DateEdit TotalBillTillPeriodDateEdit;
        private System.Windows.Forms.Label label3;
    }
}

