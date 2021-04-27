﻿using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Wizard
{
    [SmartPart]
    public partial class WizardView : BaseView, IWizardView
    {
        public WizardView()
        {
            InitializeComponent();
        }

        [CreateNew]
        public new WizardViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }

            get
            {
                return (WizardViewPresenter)base.Presenter;
            }
        }

        #region IWizardView Members

        #region ChooseDirectoryPage

        /// <summary>
        /// Полное имя папки
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                return PathButtonEdit.Text;
            }
            set
            {
                PathButtonEdit.Text = value;
            }
        }

        /// <summary>
        /// Месяц
        /// </summary>
        public DateTime Month
        {
            get
            {
                return new DateTime(MonthDateEdit.DateTime.Year, MonthDateEdit.DateTime.Month, 1);
            }
            set
            {
                MonthDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Note
        {
            get
            {
                return NoteTextBox.Text;
            }
            set
            {
                NoteTextBox.Text = value;
            }
        }

        #endregion

        #region ProcessingPage

        public void SetInitialProgress(string label)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.EditValue = 0;
                ProgressLabel.Text = label;
            }));
        }

        public void SetProgress(string label, int value)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.EditValue = value;
                ProgressLabel.Text = label;
            }));
        }

        #endregion

        #region FinishPage

        public string Result
        {
            set
            {
                ResultValueLabel.Invoke(
                    new MethodInvoker(() => ResultValueLabel.Text = value.ToString()));
            }
        }

        public int FilesWithNoErrors
        {
            set
            {
                FilesWithNoErrorsValueLabel.Invoke(
                    new MethodInvoker(() => FilesWithNoErrorsValueLabel.Text = value.ToString()));
            }
        }

        public int FilesWithErrors
        {
            set
            {
                FilesWithErrorsValueLabel.Invoke(
                    new MethodInvoker(() => FilesWithErrorsValueLabel.Text = value.ToString()));
            }
        }

        public int BuildingsWithNoErrors
        {
            set
            {
                BuildingsWithNoErrorsValueLabel.Invoke(
                    new MethodInvoker(() => BuildingsWithNoErrorsValueLabel.Text = value.ToString()));
            }
        }

        public int BuildingsWithErrors
        {
            set
            {
                BuildingsWithErrorsValueLabel.Invoke(
                    new MethodInvoker(() => BuildingsWithErrorsValueLabel.Text = value.ToString()));
            }
        }

        #endregion

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        public void StartWizard()
        {
            Presenter.StartWizard();
        }

        /// <summary>
        /// Признак завершения работы мастера
        /// </summary>
        public bool IsMasterCompleted { get; set; }

        /// <summary>
        /// Признак активного процесса обработки
        /// </summary>
        public bool IsMasterInProgress { get; set; }

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        public void SelectPage(WizardSteps page)
        {
            WizardPage _wizPage = null;

            switch (page)
            {
                case WizardSteps.ChoosePathPage:
                    _wizPage = ChoosePathWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    _wizPage = ProcessingWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    _wizPage = FinishWizardPage;
                    break;
            }

            if (_wizPage != null)
            {
                WizardControl.Invoke(new MethodInvoker(() => WizardControl.SelectedPage = _wizPage));
            }
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Обрабатывает нажатие кнопки Отмена
        /// </summary>
        private void PaymentWizardControl_CancelClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Presenter.FinishWizard();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки Завершить
        /// </summary>
        private void PaymentWizardControl_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Presenter.FinishWizard();
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку "..."
        /// </summary>
        private void FileOpenButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();

            _dialog.Description = "Выберите папку для загрузки файлов из нее.";
            _dialog.ShowNewFolderButton = false;
            _dialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                PathButtonEdit.Text = _dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Обрабатывает событие попытки перейти на новую страницу мастера
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            switch (Presenter.OnSelectedPageChanging(e.PrevPage, e.Page, e.Direction))
            {
                case WizardSteps.ChoosePathPage:
                    e.Page = ChoosePathWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    e.Page = ProcessingWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    e.Page = FinishWizardPage;
                    break;
                case WizardSteps.Unknown:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanged(object sender, WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(e.Page, e.PrevPage, e.Direction);
        }

        #endregion

    }
}
