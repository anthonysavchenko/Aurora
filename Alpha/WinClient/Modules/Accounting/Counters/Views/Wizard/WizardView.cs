using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
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
            set => base.Presenter = value;
            get => (WizardViewPresenter)base.Presenter;
        }

        #region IWizardView Members

        #region ProcessingPage

        /// <summary>
        /// Таблица с информацией текущаего ввода
        /// </summary>
        public DataTable Items
        {
            get => (DataTable)counterValueGridControl.DataSource;
            set => counterValueGridControl.Invoke(new MethodInvoker(() => counterValueGridControl.DataSource = value));
        }

        public void ShowEditor()
        {
            counterValueGridView.Focus();
            counterValueGridView.FocusedRowHandle = 0;
            counterValueGridView.FocusedColumn = counterValueGridView.Columns["Value"];
            counterValueGridView.ShowEditor();
        }

        public decimal CounterValue { set => counterValueGridView.SetFocusedRowCellValue(WizardTableColumnNames.VALUE, value); }

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        public void ResetProgressBar(int maxValue)
        {
            progressBarControl.Invoke(new MethodInvoker(() => progressBarControl.Properties.Maximum = maxValue));
            progressBarControl.Invoke(new MethodInvoker(() => progressBarControl.Properties.Minimum = 0));
            progressBarControl.Invoke(new MethodInvoker(() => progressBarControl.Properties.Step = 1));
            progressBarControl.Invoke(new MethodInvoker(() => progressBarControl.EditValue = 0));
        }

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        public void AddProgress()
        {
            progressBarControl.Invoke(new MethodInvoker(() => progressBarControl.PerformStep()));
        }

        #endregion

        #region BuildingSelectPage

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

        public DateTime Period
        {
            get
            {
                DateTime _temp = DateTime.MinValue;
                periodDateEdit.Invoke(new MethodInvoker(() => _temp = periodDateEdit.DateTime));
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                periodDateEdit.DateTime = value;
            }
        }

        public DateTime CollectDate
        {
            get => collectDateEdit.DateTime.Date;
            set => collectDateEdit.DateTime = value;
        }

        #region Обработчики событий

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

        #endregion
        
        #endregion

        #region FinishPage

        /// <summary>
        /// Итоговое количество обработанных записей
        /// </summary>
        public int ResultCount { set => TotalProcessedValueLabel.Invoke(new MethodInvoker(() => TotalProcessedValueLabel.Text = value.ToString())); }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        public int ResultErrorCount { set => TotalErrorCountValueLabel.Invoke(new MethodInvoker(() => TotalErrorCountValueLabel.Text = value.ToString())); }

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
                case WizardSteps.BuildingSelectPage:
                    _wizPage = buildingSelectWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    _wizPage = processingWizardPage;
                    break;
                case WizardSteps.CollectDataPage:
                    _wizPage = collectDataWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    _wizPage = finishWizardPage;
                    break;
            }

            if (_wizPage != null)
            {
                counterWizardControl.Invoke(new MethodInvoker(() => counterWizardControl.SelectedPage = _wizPage));
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
        /// Обрабатывает событие попытки перейти на новую страницу мастера
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
        {
            switch (Presenter.OnSelectedPageChanging(ConverWizardStep(e.PrevPage.Name), ConverWizardStep(e.Page.Name), e.Direction))
            {
                case WizardSteps.BuildingSelectPage:
                    e.Page = buildingSelectWizardPage;
                    break;
                case WizardSteps.CollectDataPage:
                    e.Page = collectDataWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    e.Page = processingWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    e.Page = finishWizardPage;
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
            Presenter.OnSelectedPageChanged(
                ConverWizardStep(e.PrevPage?.Name ?? string.Empty), ConverWizardStep(e.Page?.Name ?? string.Empty), e.Direction);
        }

        /// <summary>
        /// Обработка прорисовки строк таблицы с введенными данными
        /// </summary>
        private void PaymentsGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DataRow _row = counterValueGridView.GetDataRow(e.RowHandle);
            if (!string.IsNullOrEmpty(_row[WizardTableColumnNames.ERROR_MESSAGE].ToString()))
            {
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            }
        }

        #endregion

        private WizardSteps ConverWizardStep(string pageName)
        {
            switch(pageName)
            {
                case "buildingSelectWizardPage":
                    return WizardSteps.BuildingSelectPage;
                case "collectDataWizardPage":
                    return WizardSteps.CollectDataPage;
                case "processingWizardPage":
                    return WizardSteps.ProcessingPage;
                case "finishWizardPage":
                    return WizardSteps.FinishPage;
                default:
                    return WizardSteps.Unknown;
            }
        }

        private void counterValueGridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            counterValueGridView.SelectAll();
        }

        private void counterValueGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                DataRow _row = counterValueGridView.GetDataRow(e.FocusedRowHandle);
                SetError(_row[WizardTableColumnNames.ERROR_MESSAGE].ToString());
            }
        }

        private void counterValueGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow _row = counterValueGridView.GetFocusedDataRow();
            Presenter.ValidateRow(_row);
            SetError(_row[WizardTableColumnNames.ERROR_MESSAGE].ToString());
        }

        private void SetError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                errorMessageTextBox.Text = "Данные корректны";
            }
            else
            {
                errorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                errorMessageTextBox.Text = errorMessage;
            }
        }
    }
}