using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard
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

        #region ChooseMethodPage

        /// <summary>
        /// Посредники
        /// </summary>
        public DataTable Intermediaries
        {
            set
            {
                intermediaryLookUpEdit.Properties.DataSource = value;
                intermediaryLookUpEdit.Properties.ForceInitialize();
            }
        }

        /// <summary>
        /// Метод внесения платежей
        /// </summary>
        public ImportTypes ImportType
        {
            get
            {
                return fromFileRadioButton.Checked ? ImportTypes.File : ImportTypes.Manual;
            }
            set
            {
                fromFileRadioButton.Checked = (value == ImportTypes.File);
                manualRadioButton.Checked = (value == ImportTypes.Manual);
            }
        }

        /// <summary>
        /// Источник данных для ручного ввода
        /// </summary>
        public ManualTypeSources ManualTypeSource
        {
            get
            {
                return billRadioButton.Checked ? ManualTypeSources.Bill : ManualTypeSources.OtherPayments;
            }
            set
            {
                billRadioButton.Checked = (value == ManualTypeSources.Bill);
                otherPaymentsRadioButton.Checked = (value == ManualTypeSources.OtherPayments);
            }
        }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName
        {
            get
            {
                return fileOpenButtonEdit.Text;
            }
            set
            {
                fileOpenButtonEdit.Text = value;
            }
        }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime PaymentDate
        {
            get
            {
                return paymentDateEdit.DateTime;
            }
            set
            {
                paymentDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get
            {
                return commentTextBox.Text;
            }
            set
            {
                commentTextBox.Text = value;
            }
        }

        /// <summary>
        /// Посредник
        /// </summary>
        public Intermediary Intermediary
        {
            get
            {
                Intermediary _res = null;

                if (intermediaryLookUpEdit.ItemIndex != -1)
                {
                    string _id = intermediaryLookUpEdit.GetColumnValue("ID").ToString();
                    _res = Presenter.GetItem<Intermediary>(_id);
                }

                return _res;
            }
            set
            {
                if (value != null)
                {
                    ((System.ComponentModel.ISupportInitialize)(intermediaryLookUpEdit.Properties)).BeginInit();
                    intermediaryLookUpEdit.Properties.ValueMember = "ID";
                    intermediaryLookUpEdit.EditValue = value.ID;
                    ((System.ComponentModel.ISupportInitialize)(intermediaryLookUpEdit.Properties)).EndInit();
                }
                else
                {
                    intermediaryLookUpEdit.EditValue = null;
                }
            }
        }

        #endregion

        #region ProcessingPage

        /// <summary>
        /// Таблица с информацией текущаего ввода
        /// </summary>
        public DataTable ProcessingData
        {
            get
            {
                return (DataTable)PaymentsGridControl.DataSource;
            }
            set
            {
                PaymentsGridControl.Invoke(new MethodInvoker(() => PaymentsGridControl.DataSource = value));
                //PaymentsGridView.BestFitColumns();

                if (value != null)
                {
                    FillPaymentsTotalLabels();
                }
            }
        }

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        public void ResetProgressBar(int maxValue)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.Properties.Maximum = maxValue));
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.Properties.Minimum = 0));
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.Properties.Step = 1));
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.EditValue = 0));
        }

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        public void AddProgress()
        {
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.PerformStep()));
        }

        #endregion

        #region CheckDataPage

        /// <summary>
        /// Устанавливает фокус на поле с номером счета
        /// </summary>
        public void SetAccountFocus()
        {
            AccountTextEdit.Focus();
        }

        /// <summary>
        /// Устанавливает фокус на поле со штрих-кодом
        /// </summary>
        public void SetBarcodeFocus()
        {
            BarcodeTextEdit.Focus();
        }

        /// <summary>
        /// Признак отображения панели со штрих-кодом
        /// </summary>
        public bool IsBarcodeEnabled
        {
            set
            {
                BarcodeTextEdit.Enabled = value;
                useScannerCheckBox.Enabled = value;
            }
        }

        /// <summary>
        /// Признак использования сканера штрих-кода
        /// </summary>
        public bool IsUseScanner
        {
            get
            {
                return useScannerCheckBox.Checked;
            }
            set
            {
                BarcodeTextEdit.Enabled = value;
                useScannerCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Штрих-код в выбранной позиции
        /// </summary>
        public string CurrentBarcode
        {
            get
            {
                return BarcodeTextEdit.Text;
            }
            set
            {
                BarcodeTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Лицевой счет в выбранной позиции
        /// </summary>
        public string CurrentAccount
        {
            get
            {
                return AccountTextEdit.Text;
            }
            set
            {
                AccountTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Период  в выбранной позиции
        /// </summary>
        public DateTime CurrentPeriod
        {
            get
            {
                return new DateTime(MonthYearDateEdit.DateTime.Year, MonthYearDateEdit.DateTime.Month, 1, 0, 0, 0);
            }
            set
            {
                MonthYearDateEdit.EditValue = value;
            }
        }

        /// <summary>
        /// Сумма платежа в выбранной позиции
        /// </summary>
        public decimal CurrentValue
        {
            get
            {
                decimal _res = 0;
                if (!String.IsNullOrEmpty(ValueTextEdit.Text))
                {
                    Decimal.TryParse(ValueTextEdit.Text.Replace(".", ","), out _res);
                }
                return _res;
            }
            set
            {
                ValueTextEdit.Text = value.ToString();
            }
        }

        /// <summary>
        /// Собственник в выбранной позиции
        /// </summary>
        public string CurrentOwner
        {
            get
            {
                return ownerValueLabel.Text;
            }
            set
            {
                ownerValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Выбранный посредник
        /// </summary>
        public string CurrentIntermediary
        {
            get
            {
                return paymentOperIntermediaryLabel.Text;
            }
            set
            {
                paymentOperIntermediaryLabel.Text = value;
            }
        }

        /// <summary>
        /// Улица в выбранной позиции
        /// </summary>
        public string CurrentStreet
        {
            set
            {
                streetValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Дом в выбранной позиции
        /// </summary>
        public string CurrentHouse
        {
            set
            {
                houseValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Квартира в выбранной позиции
        /// </summary>
        public string CurrentApartment
        {
            set
            {
                apartmentValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Площадь в выбранной позиции
        /// </summary>
        public string CurrentSquare
        {
            set
            {
                squareValuelabel.Text = value;
            }
        }

        /// <summary>
        /// Сообщение о корректности данных в выбранной позиции
        /// </summary>
        public string CurrentItemMessage
        {
            get
            {
                return ErrorMessageTextBox.Text.Trim();
            }
            set
            {
                ErrorMessageTextBox.Text = value;
            }
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
                    ErrorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                }
                else
                {
                    ErrorMessageTextBox.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                }
            }
        }

        /// <summary>
        /// Признак отображения кнопки создания новой записи
        /// </summary>
        public bool AddButtonVisible
        {
            set
            {
                AddNewButton.Visible = value;
            }
        }

        #endregion

        #region FinishPage

        /// <summary>
        /// Итоговое количество обработанных записей
        /// </summary>
        public int ResultCount
        {
            set
            {
                TotalProcessedValueLabel.Invoke(new MethodInvoker(() => TotalProcessedValueLabel.Text = value.ToString()));
            }
        }

        /// <summary>
        /// Итоговая сумма 
        /// </summary>
        public decimal ResultValue
        {
            set
            {
                TotalAmountLabelValue.Invoke(new MethodInvoker(() => TotalAmountLabelValue.Text = value.ToString("C2")));
            }
        }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        public int ResultErrorCount
        {
            set
            {
                TotalErrorCountValueLabel.Invoke(new MethodInvoker(() => TotalErrorCountValueLabel.Text = value.ToString()));
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
                case WizardSteps.ChooseMethodPage:
                    _wizPage = ChooseMethodWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    _wizPage = ProcessingWizardPage;
                    break;
                case WizardSteps.CheckDataPage:
                    _wizPage = CheckDataWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    _wizPage = FinishWizardPage;
                    break;
            }

            if (_wizPage != null)
            {
                PaymentWizardControl.Invoke(new MethodInvoker(() => PaymentWizardControl.SelectedPage = _wizPage));
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
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.Title = "Открыть файл";
            _openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|Книга Microsoft Excel 2007 (*.xlsx)|*.xlsx";
            _openFileDialog.FilterIndex = 1;
            _openFileDialog.RestoreDirectory = true;
            _openFileDialog.DefaultExt = "txt";

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileOpenButtonEdit.Text = _openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Обрабатывает событие попытки перейти на новую страницу мастера
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            switch (Presenter.OnSelectedPageChanging(e.PrevPage, e.Page, e.Direction))
            {
                case WizardSteps.ChooseMethodPage:
                    e.Page = ChooseMethodWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    e.Page = ProcessingWizardPage;
                    break;
                case WizardSteps.CheckDataPage:
                    e.Page = CheckDataWizardPage;
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
                Presenter.OnProcesingDataRowChanged(Convert.ToInt32(PaymentsGridView.GetDataRow(e.FocusedRowHandle)["ID"]));
            }
        }

        /// <summary>
        /// Обрабатывает покидание любого контрола ввода
        /// </summary>
        private void AnyControl_Leave(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "AccountTextEdit")
            {
                Presenter.SetCustomer(CurrentAccount);
            }
            ReValidateCurrentItem();
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
                    case "BarcodeTextEdit":
                        FillDataFromBarcode();
                        Presenter.SetCustomer(CurrentAccount);
                        ValueTextEdit.Focus();
                        break;
                    case "AccountTextEdit":
                        MonthYearDateEdit.Focus();
                        break;
                    case "MonthYearDateEdit":
                        ValueTextEdit.Focus();
                        break;
                    case "ValueTextEdit":
                        if (ImportType == ImportTypes.File)
                        {
                            AccountTextEdit.Focus();
                        }
                        else
                        {
                            AddNewButton.Focus();
                        }
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
            if (CurrentValue == 0)
            {
                CurrentValue = Presenter.GetChargeValueByAccountAndPeriod(CurrentAccount, CurrentPeriod);
            }
        }

        /// <summary>
        /// Обработка прорисовки строк таблицы с введенными данными
        /// </summary>
        private void PaymentsGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if ((bool)PaymentsGridView.GetRowCellValue(e.RowHandle, "HasError") == true)
            {
                e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            }
        }

        /// <summary>
        /// Обработка создания новой записи
        /// </summary>
        private void AddNewButton_Click(object sender, EventArgs e)
        {
            Presenter.CreateNewPayment();
            PaymentsGridView.FocusedRowHandle = PaymentsGridView.RowCount - 1;
            PaymentsGridView.ClearSelection();
            PaymentsGridView.SelectRow(PaymentsGridView.RowCount - 1);
            FillSelectedPaymentsTotalLabels();
            FillPaymentsTotalLabels();

                if (IsUseScanner)
                {
                    BarcodeTextEdit.Focus();
                }
                else
                {
                    AccountTextEdit.Focus();
                }
            }

        /// <summary>
        /// Обрабатывает нажатие на кнопку "Удалить"
        /// </summary>
        private void DeleteItemButton_Click(object sender, EventArgs e)
        {
            Presenter.DeletePayments(PaymentsGridView.GetSelectedRows().Select(handle => Convert.ToInt32(PaymentsGridView.GetDataRow(handle)["ID"])).ToList());
            PaymentsGridView.FocusedRowHandle = 0;
            Presenter.OnProcesingDataRowChanged(Convert.ToInt32(PaymentsGridView.GetDataRow(0)["ID"]));
            PaymentsGridView.ClearSelection();
            PaymentsGridView.SelectRow(0);
            FillSelectedPaymentsTotalLabels();
            FillPaymentsTotalLabels();

            if (IsUseScanner)
            {
                BarcodeTextEdit.Focus();
            }
            else
            {
                AccountTextEdit.Focus();
            }
        }

        /// <summary>
        /// Обрабатывает изменение признака использования сканера штрих-кода
        /// </summary>
        private void useScannerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            IsUseScanner = useScannerCheckBox.Checked;
        }

        /// <summary>
        /// Обрабатывает изменение радио кнопки "Загрузить из файла"
        /// </summary>
        private void fromFileRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fileGroupBox.Enabled = fromFileRadioButton.Checked;
            if (fromFileRadioButton.Checked)
            {
                billRadioButton.Checked = true;
            }
            else
            {
                fileOpenButtonEdit.Text = string.Empty;
            }
        }

        /// <summary>
        /// Обрабатывает изменение радио кнопки "Внести данные вручную"
        /// </summary>
        private void manualRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            manualGroupBox.Enabled = manualRadioButton.Checked;
        }

        /// <summary>
        /// Обрабатывает изменение радио кнопки "Квитанция"
        /// </summary>
        private void billRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            intermediaryLookUpEdit.Enabled = billRadioButton.Checked;
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
        private void ReValidateCurrentItem()
        {
            bool _hasErrors = !Presenter.ReValidateCurrentItem();

            int _currentRow = PaymentsGridView.FocusedRowHandle;

            bool _valueChanged = (decimal)PaymentsGridView.GetRowCellValue(_currentRow, "Value") != CurrentValue;

            PaymentsGridView.SetRowCellValue(_currentRow, "Account", CurrentAccount);
            PaymentsGridView.SetRowCellValue(_currentRow, "Period", string.Format("{0:00}.{1:00}", CurrentPeriod.Month, CurrentPeriod.Year));
            PaymentsGridView.SetRowCellValue(_currentRow, "Value", CurrentValue);
            PaymentsGridView.SetRowCellValue(_currentRow, "Owner", CurrentOwner);
            PaymentsGridView.SetRowCellValue(_currentRow, "HasError", _hasErrors);

            PaymentsGridView.RefreshData();

            if (_valueChanged)
            {
                FillSelectedPaymentsTotalLabels();
                FillPaymentsTotalLabels();
            }
        }

        /// <summary>
        /// Заполняет информацию о платеже по штрих-коду
        /// </summary>
        private void FillDataFromBarcode()
        {
            string _barcode = CurrentBarcode;

            if (_barcode.Length == 19 || _barcode.Length == 20)
            {
                try
                {
                    CurrentAccount = String.Format("{0}-{1}-{2}", _barcode.Substring(3, 4), _barcode.Substring(7, 3), _barcode.Substring(10, 1));
                    CurrentPeriod = new DateTime(Int32.Parse(_barcode.Substring(11, 4)), Int32.Parse(_barcode.Substring(15, 2)), 1);
                }
                catch
                {
                    ShowMessage("Некорретный штрих-код", "Ошибка ввода");
                }
            }
        }

        /// <summary>
        /// Заполняет итоговые поля по выбранным строкам
        /// </summary>
        private void FillSelectedPaymentsTotalLabels()
        {
            selectedPaymentsCountLabel.Text = PaymentsGridView.SelectedRowsCount.ToString();

            decimal _sum = 0;
            int[] _selectedRows = PaymentsGridView.GetSelectedRows();

            for (int i = 0; i < _selectedRows.Length; i++)
            {
                _sum += (decimal)PaymentsGridView.GetDataRow(_selectedRows[i])["Value"];
            }

            selectedPaymentsSumLabel.Text = _sum.ToString("0.00");
        }

        /// <summary>
        /// Заполняет итоговые поля по всем строкам
        /// </summary>
        private void FillPaymentsTotalLabels()
        {
            paymentsCountLabel.Invoke(new MethodInvoker(() => paymentsCountLabel.Text = ((DataTable)PaymentsGridControl.DataSource).Rows.Count.ToString()));
            paymentsSumLabel.Invoke(new MethodInvoker(() => paymentsSumLabel.Text = ((DataTable)PaymentsGridControl.DataSource).AsEnumerable().Sum(r => (decimal)r["Value"]).ToString("0.00")));
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
    }
}