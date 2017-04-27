﻿using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Вью формы
    /// </summary>
    [SmartPart]
    public partial class LayoutView : BaseLayoutView, ILayoutView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public LayoutView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Презентер
        /// </summary>
        /// <value>Презентер</value>
        [CreateNew]
        public new LayoutViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (LayoutViewPresenter)base.Presenter;
            }
        }

        public WizardAction WizardAction
        {
            get
            {
                WizardAction _action = WizardAction.ImportNewCustomers;

                if (importCustomerPosesRadioButton.Checked)
                {
                    _action = WizardAction.ImportCustomerPoses;
                }
                else if(importGisZhkhCustomerIDsRadioButton.Checked)
                {
                    _action = WizardAction.ImportGisZhkhCustomerIDs;
                }

                return _action;
            }
        }

        /// <summary>
        /// Полное имя файла
        /// </summary>
        public string FilePath
        {
            get
            {
                return filePathTextEdit.Text;
            }
        }

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        public void SelectPage(WizardPages page)
        {
            switch (page)
            {
                case WizardPages.ChooseMethodPage:
                    ImportWizardControl.SelectedPage = ChooseMethodWizardPage;
                    break;
                case WizardPages.FilePage:
                    ImportWizardControl.SelectedPage = FileWizardPage;
                    break;
                case WizardPages.ProcessingPage:
                    ImportWizardControl.SelectedPage = ProcessingWizardPage;
                    break;
                case WizardPages.FinishPage:
                    ImportWizardControl.SelectedPage = FinishWizardPage;
                    break;
            }
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data",
                Title = "Открыть файл",
                Filter = "Книга Microsoft Excel (*.xls;*.xlsx)|*.xls;*.xlsx",
                FilterIndex = 0,
                DefaultExt = "xlsx",
                RestoreDirectory = true,
            };

            filePathTextEdit.Text = _openFileDialog.ShowDialog() == DialogResult.OK
                ? _openFileDialog.FileName
                : string.Empty;
        }

        private WizardPages ConvertWizardPage(BaseWizardPage page)
        {
            switch (page.Name)
            {
                case "ChooseMethodWizardPage":
                    return WizardPages.ChooseMethodPage;
                case "FileWizardPage":
                    return WizardPages.FilePage;
                case "ProcessingWizardPage":
                    return WizardPages.ProcessingPage;
                case "FinishWizardPage":
                    return WizardPages.FinishPage;
                default:
                    return WizardPages.Unknown;
            }
        }

        private BaseWizardPage ConvertWizardPage(WizardPages page)
        {
            switch (page)
            {
                case WizardPages.ChooseMethodPage:
                    return ChooseMethodWizardPage;
                case WizardPages.FilePage:
                    return FileWizardPage;
                case WizardPages.ProcessingPage:
                    return ProcessingWizardPage;
                case WizardPages.FinishPage:
                    return FinishWizardPage;
                default:
                    return null;
            }
        }

        private void ImportWizardControl_SelectedPageChanged(object sender, WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(ConvertWizardPage(e.Page), e.Direction == Direction.Forward);
        }

        private void ImportWizardControl_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
        {
            BaseWizardPage _page = ConvertWizardPage(
                Presenter.OnSelectingPageChanging(ConvertWizardPage(e.PrevPage), e.Direction == Direction.Forward));

            e.Cancel = _page == null;
            if(!e.Cancel)
            {
                e.Page = _page;
            }
        }

        public void ResetProgress()
        {
            throw new NotImplementedException();
        }

        public void SetProgress(int value)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.EditValue = value;
                progressProcentLabel.Text = $"Обработано {value}%";
            }));
        }

        public string ResultText
        {
            set
            {
                resultTextBox.Text = value;
            }
        }

        private void CleanUp()
        {
            filePathTextEdit.Text = string.Empty;
            resultTextBox.Text = string.Empty;
            ProgressBarControl.EditValue = 0;
            progressProcentLabel.Text = "Обработано 0%";
        }

        private void ImportWizardControl_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CleanUp();
            SelectPage(WizardPages.ChooseMethodPage);
        }

        private void ImportWizardControl_CustomizeCommandButtons(object sender, CustomizeCommandButtonsEventArgs e)
        {
            e.CancelButton.Visible = false;
        }
    }
}