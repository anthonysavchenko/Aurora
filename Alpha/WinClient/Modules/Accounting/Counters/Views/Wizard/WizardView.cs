using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
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
            set
            {
                counterValueGridControl.Invoke(new MethodInvoker(() => counterValueGridControl.DataSource = value));
                //PaymentsGridView.BestFitColumns();

                if (value != null)
                {
                    FillTotalLabels();
                }
            }
        }
        
        public DataTable Counters
        {
            set
            {
                counterLookUpEdit.Properties.DataSource = value;
                counterLookUpEdit.Properties.ForceInitialize();
                if (value != null && value.Rows.Count > 0)
                {
                    counterLookUpEdit.EditValue = value.Rows[0]["ID"];
                }
            }
        }

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

        #region CollectDataPage

        /// <summary>
        /// Устанавливает фокус на поле с номером счета
        /// </summary>
        public void SetAccountFocus()
        {
            accountTextEdit.Focus();
        }

        /// <summary>
        /// Лицевой счет в выбранной позиции
        /// </summary>
        public string Account
        {
            get => accountTextEdit.Text;
            set => accountTextEdit.Text = value;
        }

        /// <summary>
        /// ФИО собственника
        /// </summary>
        public string CustomerName { set => ownerValueLabel.Text = value; }

        /// <summary>
        /// Улица в выбранной позиции
        /// </summary>
        public string Street { set => streetValueLabel.Text = value; }

        /// <summary>
        /// Дом в выбранной позиции
        /// </summary>
        public string Building { set => houseValueLabel.Text = value; }

        /// <summary>
        /// Квартира в выбранной позиции
        /// </summary>
        public string Apartment { set => apartmentValueLabel.Text = value; }

        /// <summary>
        /// Площадь в выбранной позиции
        /// </summary>
        public string Area { set => squareValuelabel.Text = value; }

        /// <summary>
        /// ID прибора учета
        /// </summary>
        public int CounterId
        {
            get
            {
                return counterLookUpEdit.ItemIndex != -1
                    ? (int)counterLookUpEdit.GetColumnValue("ID")
                    : 0;
            }
            set
            {
                if (value > 0)
                {
                    ((System.ComponentModel.ISupportInitialize)(counterLookUpEdit.Properties)).BeginInit();
                    counterLookUpEdit.Properties.ValueMember = "ID";
                    counterLookUpEdit.EditValue = value;
                    ((System.ComponentModel.ISupportInitialize)(counterLookUpEdit.Properties)).EndInit();
                }
                else
                {
                    counterLookUpEdit.EditValue = null;
                }
            }
        }

        private string CounterNumber => counterLookUpEdit.ItemIndex != -1
            ? counterLookUpEdit.GetColumnValue("Number").ToString()
            : string.Empty;

        private string CounterModel => counterLookUpEdit.ItemIndex != -1
            ? counterLookUpEdit.GetColumnValue("Model").ToString()
            : string.Empty;

        /// <summary>
        /// Сумма платежа в выбранной позиции
        /// </summary>
        public decimal CounterValue
        {
            get
            {
                decimal _res = 0;
                if (!string.IsNullOrEmpty(valueTextEdit.Text))
                {
                    decimal.TryParse(valueTextEdit.Text.Replace(".", ","), out _res);
                }
                return _res;
            }
            set => valueTextEdit.Text = value.ToString();
        }

        /// <summary>
        /// Период  в выбранной позиции
        /// </summary>
        public DateTime CollectDate
        {
            get => collectDateEdit.DateTime.Date;
            set => collectDateEdit.EditValue = value;
        }

        /// <summary>
        /// Сообщение о корректности данных в выбранной позиции
        /// </summary>
        public string CurrentItemMessage
        {
            get => errorMessageTextBox.Text.Trim();
            set => errorMessageTextBox.Text = value;
        }

        /// <summary>
        /// Признак наличия ошибок в выбранной позиции
        /// </summary>
        public bool CurrentItemHasError
        {
            set
            {
                if (value)
                {
                    errorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                }
                else
                {
                    errorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }
        }

        #endregion

        #region FinishPage

        /// <summary>
        /// Итоговое количество обработанных записей
        /// </summary>
        public int ResultCount { set => TotalProcessedValueLabel.Invoke(new MethodInvoker(() => TotalProcessedValueLabel.Text = value.ToString())); }

        /// <summary>
        /// Итоговая сумма 
        /// </summary>
        public decimal ResultValue { set => TotalAmountLabelValue.Invoke(new MethodInvoker(() => TotalAmountLabelValue.Text = value.ToString("C2"))); }

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
            switch (Presenter.OnSelectedPageChanging(e.PrevPage, e.Page, e.Direction))
            {
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
        private void PaymentWizardControl_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(e.Page, e.PrevPage, e.Direction);
        }

        /// <summary>
        /// Обрабатывает событие выбора строки в таблице с обработанными данными
        /// </summary>
        private void PaymentsGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                Presenter.OnProcesingDataRowChanged(Convert.ToInt32(counterValueGridView.GetDataRow(e.FocusedRowHandle)["ID"]));
            }
        }

        /// <summary>
        /// Обрабатывает покидание любого контрола ввода
        /// </summary>
        private void AnyControl_Leave(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "accountTextEdit")
            {
                Presenter.SetCustomer(Account);
                counterValueGridView.FocusedRowHandle = counterValueGridView.RowCount - 1;
                counterValueGridView.ClearSelection();
                counterValueGridView.SelectRow(counterValueGridView.RowCount - 1);
                FillSelectedPaymentsTotalLabels();
                FillTotalLabels();
            }
            ValidateCurrentItem();
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки Enter в любом конроле
        /// </summary>
        private void AnyControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Приходится перебирать контролы, т.к. мастер по умолчанию забирает на себя приоритет
                switch (((Control)sender).Name)
                {
                    case "accountTextEdit":
                        counterLookUpEdit.Focus();
                        break;
                    case "counterLookUpEdit":
                        collectDateEdit.Focus();
                        break;
                    case "collectDateEdit":
                        valueTextEdit.Focus();
                        break;
                    case "valueTextEdit":
                        addNewButton.Focus();
                        break;
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Обрабатывает получение фокуса текстовым полем с суммой платежа
        /// </summary>
        private void ValueTextEdit_Enter(object sender, EventArgs e)
        {
            if (CounterValue == 0)
            {
                CounterValue = Presenter.GetSuggestedValue();
            }
        }

        /// <summary>
        /// Обработка прорисовки строк таблицы с введенными данными
        /// </summary>
        private void PaymentsGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if ((bool)counterValueGridView.GetRowCellValue(e.RowHandle, "HasError") == true)
            {
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            }
        }

        /// <summary>
        /// Обработка создания новой записи
        /// </summary>
        private void AddNewButton_Click(object sender, EventArgs e)
        {
            accountTextEdit.Focus();
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку "Удалить"
        /// </summary>
        private void DeleteItemButton_Click(object sender, EventArgs e)
        {
            Presenter.DeleteItems(counterValueGridView.GetSelectedRows().Select(handle => Convert.ToInt32(counterValueGridView.GetDataRow(handle)["ID"])).ToList());
            counterValueGridView.FocusedRowHandle = 0;
            Presenter.OnProcesingDataRowChanged(Convert.ToInt32(counterValueGridView.GetDataRow(0)["ID"]));
            counterValueGridView.ClearSelection();
            counterValueGridView.SelectRow(0);
            FillSelectedPaymentsTotalLabels();
            FillTotalLabels();

            accountTextEdit.Focus();
        }

        /// <summary>
        /// Обрабатывает выделение строк в таблице
        /// </summary>
        private void PaymentsGridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            FillSelectedPaymentsTotalLabels();
        }

        #endregion

        /// <summary>
        /// Обработка изменения значения любого введенного поля платежа
        /// </summary>
        private void ValidateCurrentItem()
        {
            bool _hasErrors = !Presenter.ValidateCurrentItem();

            int _currentRow = counterValueGridView.FocusedRowHandle;

            bool _valueChanged = (decimal)counterValueGridView.GetRowCellValue(_currentRow, "Value") != CounterValue;

            counterValueGridView.SetRowCellValue(_currentRow, "Account", Account);
            counterValueGridView.SetRowCellValue(_currentRow, "CollectDate", CollectDate.ToString("dd.MM.yyyy"));
            counterValueGridView.SetRowCellValue(_currentRow, "Period", CollectDate.ToString("MM.yyyy"));
            counterValueGridView.SetRowCellValue(_currentRow, "Counter", CounterNumber);
            counterValueGridView.SetRowCellValue(_currentRow, "Value", CounterValue);
            counterValueGridView.SetRowCellValue(_currentRow, "HasError", _hasErrors);

            counterValueGridView.RefreshData();

            if (_valueChanged)
            {
                FillSelectedPaymentsTotalLabels();
                FillTotalLabels();
            }
        }


        /// <summary>
        /// Заполняет итоговые поля по выбранным строкам
        /// </summary>
        private void FillSelectedPaymentsTotalLabels()
        {

            decimal _sum = 0;
            int[] _selectedRows = counterValueGridView.GetSelectedRows();

            for (int i = 0; i < _selectedRows.Length; i++)
            {
                _sum += (decimal)counterValueGridView.GetDataRow(_selectedRows[i])["Value"];
            }
        }

        /// <summary>
        /// Заполняет итоговые поля по всем строкам
        /// </summary>
        private void FillTotalLabels()
        {
            //paymentsCountLabel.Invoke(new MethodInvoker(() => paymentsCountLabel.Text = ((DataTable)counterValueGridControl.DataSource).Rows.Count.ToString()));
        }

        /// <summary>
        /// Заполняет номер строки
        /// </summary>
        private void PaymentsGridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "NumberGridColumn")
            {
                e.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void counterLookUpEdit_Properties_EditValueChanged(object sender, EventArgs e)
        {
            counterModelLabel.Text = CounterModel;
        }
    }
}