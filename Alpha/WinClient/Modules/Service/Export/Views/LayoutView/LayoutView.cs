using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;
using DevExpress.XtraWizard;
using System.Collections.Generic;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
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
            set => base.Presenter = value;
            get => (LayoutViewPresenter)base.Presenter;
        }

        /// <summary>
        /// Файл с шаблоном
        /// </summary>
        public string TemplatePath
        {
            set => templatePathTextEdit.Text = value;
            get => templatePathTextEdit.Text;
        }

        /// <summary>
        /// Путь для сохранения файлов с экспортируемыми данными
        /// </summary>
        public string OutputPath
        {
            set => outputPathTextEdit.Text = value;
            get => outputPathTextEdit.Text;
        }

        /// <summary>
        /// Учетный период, за который будут экспортированы начисления
        /// </summary>
        public DateTime Period
        {
            set => periodDateEdit.DateTime = value;
            get => periodDateEdit.DateTime;
        }

        /// <summary>
        /// Учетный период, от которого будут выбираться данные льготников
        /// </summary>
        public DateTime StartPeriod
        {
            get => startPeriodDateEdit.DateTime;
            set => startPeriodDateEdit.DateTime = value;
        }

        /// <summary>
        /// Флаг экспорта данных ГИС ЖКХ: "только новые" / "все" абоненты
        /// </summary>
        public bool GisZhkhOnlyNew
        {
            get => onlyNewRadioBtn.Checked;
            set => onlyNewRadioBtn.Checked = value;
        }

        /// <summary>
        /// Выбранное действие мастера экспорта данных
        /// </summary>
        public WizardAction WizardAction
        {
            get
            {
                WizardAction _action = WizardAction.ExportBenefitData;

                if (exportChargesForBanksRadioBtn.Checked)
                {
                    _action = WizardAction.ExportChargesForBanks;
                }
                else if (exportCustomersForGisZhkhRadioBtn.Checked)
                {
                    _action = WizardAction.ExportCustomersForGisZhkh;
                }
                else if(exportChargesForGizZhkhRadioBtn.Checked)
                {
                    _action = WizardAction.ExportChargesForGisZhkh;
                }

                return _action;
            }
        }

        /// <summary>
        /// Выбран формат сбербанка
        /// </summary>
        public bool SbrfChecked
        {
            get => chkSbrfFormat.Checked;
            set => chkSbrfFormat.Checked = value;
        }

        /// <summary>
        /// Выбран формат примсоцбанка
        /// </summary>
        public bool PrimSocBankChecked
        {
            get => chkPrimSocBankFormat.Checked;
            set => chkPrimSocBankFormat.Checked = value;
        }

        /// <summary>
        /// Информация о результате экспорта
        /// </summary>
        public string ResultText { set => resultTextBox.Text = value; }

        /// <summary>
        /// Открывает указанную старницу мастера эскпорта
        /// </summary>
        /// <param name="page">Страница</param>
        public void SelectPage(WizardPages page)
        {
            switch (page)
            {
                case WizardPages.ChooseMethodPage:
                    ExportWizardControl.SelectedPage = ChooseMethodWizardPage;
                    break;
                case WizardPages.FilePage:
                    ExportWizardControl.SelectedPage = FileWizardPage;
                    break;
                case WizardPages.ServiceMatchingWizardPage:
                    ExportWizardControl.SelectedPage = ServiceMatchingWizardPage;
                    break;
                case WizardPages.ProcessingPage:
                    ExportWizardControl.SelectedPage = ProcessingWizardPage;
                    break;
                case WizardPages.FinishPage:
                    ExportWizardControl.SelectedPage = FinishWizardPage;
                    break;
            }
        }

        /// <summary>
        /// Устанавливает значение прогресс-бара
        /// </summary>
        /// <param name="percent">Значение в процентах</param>
        public void SetProgress(int percent)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.Value = percent;
                progressProcentLabel.Text = $"Выполнено {percent}%";
            }));
        }

        public void ResetProgress()
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.Value = 0;
                progressProcentLabel.Text = $"Загрузка даннных...";
            }));
        }

        public bool ServiceMatchingTableProgressBarVisible
        {
            set
            {
                serviceMatchingTableProgressBarPanel.Visible = value;
                tblServiceMatching.Visible = !value;
            }
        }

        public void ClearServiceMatchingTable()
        {
            tblServiceMatching.Controls.Clear();
            tblServiceMatching.RowCount = 0;
            tblServiceMatching.RowStyles.Clear();
        }

        public void AddRowToServiceMatchingTable(
            int serviceTypeID, 
            string serviceTypeName, 
            List<string> matchingValues, 
            string selectedValue, 
            int tabIndex)
        {
            tblServiceMatching.RowCount++;
            tblServiceMatching.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Label _lbl =
                new Label()
                {
                    Text = serviceTypeName,
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                    TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                    Tag = serviceTypeID
                };
            ComboBox _cb =
                new ComboBox
                {
                    Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                };

            foreach (string _value in matchingValues)
            {
                _cb.Items.Add(_value);
            }

            if (!string.IsNullOrEmpty(selectedValue))
            {
                _cb.SelectedIndex = _cb.FindStringExact(selectedValue);
            }

            _cb.TabIndex = tabIndex;

            tblServiceMatching.Controls.Add(_lbl, 0, tblServiceMatching.RowCount);
            tblServiceMatching.Controls.Add(_cb, 1, tblServiceMatching.RowCount);
        }

        public Dictionary<int, string> GetServiceMatchingDict()
        {
            Dictionary<int, List<Control>> _controlByRow = new Dictionary<int, List<Control>>();

            foreach (Control _control in tblServiceMatching.Controls)
            {
                int _row = tblServiceMatching.GetRow(_control);
                if (_controlByRow.ContainsKey(_row))
                {
                    _controlByRow[_row].Add(_control);
                }
                else
                {
                    _controlByRow.Add(_row, new List<Control> { _control });
                }
            }

            Dictionary<int, string> _result = new Dictionary<int, string>();

            foreach (List<Control> _cList in _controlByRow.Values)
            {
                int _serviceID = 0;
                string _gisZhkhSeviceName = string.Empty;

                foreach(Control _c in _cList)
                {
                    ComboBox _cb = _c as ComboBox;
                    if(_cb != null)
                    {
                        _cb.Invoke(new MethodInvoker(() => _gisZhkhSeviceName = _cb.SelectedItem?.ToString() ?? string.Empty));
                    }
                    else
                    {
                        _c.Invoke(new MethodInvoker(() => _serviceID = (int)_c.Tag));
                    }
                }

                if (_serviceID > 0 && !string.IsNullOrEmpty(_gisZhkhSeviceName))
                {
                    _result.Add(_serviceID, _gisZhkhSeviceName);
                }
            }

            return _result;
        }

        private WizardPages ConvertWizardPage(BaseWizardPage page)
        {
            switch (page.Name)
            {
                case "ChooseMethodWizardPage":
                    return WizardPages.ChooseMethodPage;
                case "FileWizardPage":
                    return WizardPages.FilePage;
                case "ServiceMatchingWizardPage":
                    return WizardPages.ServiceMatchingWizardPage;
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
                case WizardPages.ServiceMatchingWizardPage:
                    return ServiceMatchingWizardPage;
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
                case WizardAction.ExportChargesForBanks:
                    tblPeriod.Visible = true;
                    tblBankExportInfo.Visible = true;
                    tblGizZhkhInfo.Visible = false;
                    tblBenefitExportInfo.Visible = false;
                    tblOutputPath.Visible = true;
                    tblTemplate.Visible = false;
                    break;
                case WizardAction.ExportCustomersForGisZhkh:
                    tblPeriod.Visible = false;
                    tblBankExportInfo.Visible = false;
                    tblGizZhkhInfo.Visible = true;
                    tblBenefitExportInfo.Visible = false;
                    tblOutputPath.Visible = true;
                    tblTemplate.Visible = false;
                    break;
                case WizardAction.ExportBenefitData:
                    tblPeriod.Visible = false;
                    tblBankExportInfo.Visible = false;
                    tblGizZhkhInfo.Visible = false;
                    tblBenefitExportInfo.Visible = true;
                    tblOutputPath.Visible = true;
                    tblTemplate.Visible = true;
                    break;
                case WizardAction.ExportChargesForGisZhkh:
                    tblPeriod.Visible = true;
                    tblBankExportInfo.Visible = false;
                    tblGizZhkhInfo.Visible = false;
                    tblBenefitExportInfo.Visible = false;
                    tblOutputPath.Visible = true;
                    tblTemplate.Visible = true;
                    break;
                default:
                    tblPeriod.Visible = false;
                    tblBankExportInfo.Visible = false;
                    tblGizZhkhInfo.Visible = false;
                    tblBenefitExportInfo.Visible = false;
                    tblOutputPath.Visible = true;
                    tblTemplate.Visible = true;
                    break;
            }
        }

        #region Event Handlers

        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data",
                Title = "Открыть файл",
                Filter = "Книга Microsoft Excel (*.xlsx)|*.xlsx",
                FilterIndex = 0,
                DefaultExt = "xlsx",
                RestoreDirectory = true,
            };

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                templatePathTextEdit.Text = _openFileDialog.FileName;
            }
        }

        private void btnSelectExportPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();
            if(_dialog.ShowDialog() == DialogResult.OK)
            {
                outputPathTextEdit.Text = _dialog.SelectedPath;
            }
        }

        private void ExportWizardControl_SelectedPageChanged(object sender, WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(ConvertWizardPage(e.Page), e.Direction == Direction.Forward);
        }

        private void ExportWizardControl_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
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

        private void ExportWizardControl_FinishClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SelectPage(WizardPages.ChooseMethodPage);
        }

        private void ExportWizardControl_CustomizeCommandButtons(object sender, CustomizeCommandButtonsEventArgs e)
        {
            e.CancelButton.Visible = false;
        }

        #endregion
    }
}