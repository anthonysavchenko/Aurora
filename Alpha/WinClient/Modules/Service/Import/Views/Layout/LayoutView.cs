using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Layout
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
                else if (importGisZhkhCustomerIDsRadioButton.Checked)
                {
                    _action = WizardAction.ImportGisZhkhCustomerIDs;
                }
                else if (importPublicPlaceServiceVolumesRadioButton.Checked)
                {
                    _action = WizardAction.ImportPublicPlaceServiceVolumes;
                }
                else if (importCounterRadioButton.Checked)
                {
                    _action = WizardAction.ImportCounters;
                }
                else if (importElectricityVolumesRadioButton.Checked)
                {
                    _action = WizardAction.ImportElectricitySharedCounterVolumes;
                }
                else if (importChildrenOfWarBenefitRadioButton.Checked)
                {
                    _action = WizardAction.ImportChildrenOfWarBenefit;
                }

                return _action;
            }
        }

        /// <summary>
        /// Полное имя файла
        /// </summary>
        public string FilePath => filePathTextEdit.Text;

        /// <summary>
        /// Учетный период
        /// </summary>
        public DateTime Period
        {
            get
            {
                DateTime _date = periodDateEdit.DateTime.Date;
                return new DateTime(_date.Year, _date.Month, 1);
            }
            set => periodDateEdit.DateTime = value;
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
            set => resultTextBox.Text = value;
        }

        public DataTable Streets { set => streetLookUpEdit.Properties.DataSource = value; }

        public DataTable Buildings { set => buildingLookUpEdit.Properties.DataSource = value; }

        public string StreetId
        {
            get => (string)streetLookUpEdit.EditValue;
            set => streetLookUpEdit.EditValue = value;
        }

        public string BuildingId
        {
            get => (string)buildingLookUpEdit.EditValue;
            set => buildingLookUpEdit.EditValue = value;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data",
                Title = "Открыть файл",
                Filter = "Книга Microsoft Excel (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                DefaultExt =  "xlsx",
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

        private void ManageFilePageControlsVisibility()
        {
            switch (WizardAction)
            {
                case WizardAction.ImportPublicPlaceServiceVolumes:
                case WizardAction.ImportElectricitySharedCounterVolumes:
                case WizardAction.ImportCounters:
                    filePanel.Visible = true;
                    periodPanel.Visible = true;
                    addressPanel.Visible = false;
                    break;
                case WizardAction.ImportChildrenOfWarBenefit:
                    filePanel.Visible = true;
                    periodPanel.Visible = false;
                    addressPanel.Visible = true;
                    break;
                default:
                    filePanel.Visible = true;
                    periodPanel.Visible = false;
                    addressPanel.Visible = false;
                    break;
            }
        }

        private void ImportWizardControl_SelectedPageChanged(object sender, WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(ConvertWizardPage(e.Page), e.Direction == Direction.Forward);
        }

        private void ImportWizardControl_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
        {
            WizardPages _nextPage = Presenter.OnSelectingPageChanging(ConvertWizardPage(e.PrevPage), e.Direction == Direction.Forward);
            BaseWizardPage _page = ConvertWizardPage(_nextPage);

            e.Cancel = _page == null;
            if (!e.Cancel)
            {
                if (_nextPage == WizardPages.FilePage)
                {
                    ManageFilePageControlsVisibility();
                }
                e.Page = _page;
            }
        }

        private void CleanUp()
        {
            filePathTextEdit.Text = string.Empty;
            resultTextBox.Text = string.Empty;
            ProgressBarControl.EditValue = 0;
            progressProcentLabel.Text = "Обработано 0%";
            BuildingId = string.Empty;
            buildingLookUpEdit.Properties.DataSource = null;
            StreetId = string.Empty;
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

        private void importPublicPlaceServiceVolumeTemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Presenter.GenerateImportPublicPlaceServiceVolumeTemplate();
        }

        private void filterLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        private void streetLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }
    }
}