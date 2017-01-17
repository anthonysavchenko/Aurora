using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Wizard
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
        /// <summary>
        /// Тип квитанций
        /// </summary>
        public ReceiptTypes ReceiptType
        {
            set
            {
                if (value == ReceiptTypes.Debt)
                {
                    DebtRadioButton.Checked = true;
                }
                else if (value == ReceiptTypes.Total)
                {
                    TotalBillRadioButton.Checked = true;
                }
            }
            get
            {
                ReceiptTypes _res = ReceiptTypes.Debt;

                if (DebtRadioButton.Checked)
                {
                    _res = ReceiptTypes.Debt;
                }
                if (TotalBillRadioButton.Checked)
                {
                    _res = ReceiptTypes.Total;
                }

                return _res;
            }
        }

        /// <summary>
        /// Выбирать до месяца
        /// </summary>
        public DateTime TillDate
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
        /// Минимальная сумма
        /// </summary>
        public decimal MinValue
        {
            get
            {
                decimal _res = 0;
                if (!String.IsNullOrEmpty(ValueTextEdit.Text))
                {
                    Decimal.TryParse(ValueTextEdit.Text, out _res);
                }
                return _res;
            }
            set
            {
                ValueTextEdit.Text = value.ToString("n2");
            }
        }

        /// <summary>
        /// Лицевой счет абонента
        /// </summary>
        public string TotalBillAccount
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
        /// Конечный период квитанции по доплате
        /// </summary>
        public DateTime TotalBillTillPeriod
        {
            get
            {
                return new DateTime(TotalBillTillPeriodDateEdit.DateTime.Year, TotalBillTillPeriodDateEdit.DateTime.Month, 1, 0, 0, 0);
            }
            set
            {
                TotalBillTillPeriodDateEdit.EditValue = value;
            }
        }

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        public void ResetProgressBar(int maxValue)
        {
            ProgressBarControl.Properties.Maximum = maxValue;
            ProgressBarControl.Properties.Minimum = 0;
            ProgressBarControl.EditValue = 0;
        }

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        /// <param name="delta">Количество обработанных единиц</param>
        public void AddProgress(int delta)
        {
            ProgressBarControl.EditValue = (int)ProgressBarControl.EditValue + delta;
        }

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
        /// Итоговое количество обработанных записей
        /// </summary>
        public int ResultCount
        {
            set
            {
                TotalProcessedValueLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Итоговая сумма 
        /// </summary>
        public decimal ResultValue
        {
            set
            {
                TotalAmountLabelValue.Text = value.ToString("C2");
            }
        }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        public int ResultErrorCount
        {
            set
            {
                TotalErrorCountValueLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        public void SelectPage(WizardSteps page)
        {
            switch (page)
            {
                case WizardSteps.ChooseMethodPage:
                    PaymentWizardControl.SelectedPage = ChooseMethodWizardPage;
                    break;
                case WizardSteps.ProcessingPage:
                    PaymentWizardControl.SelectedPage = ProcessingWizardPage;
                    break;
                case WizardSteps.FinishPage:
                    PaymentWizardControl.SelectedPage = FinishWizardPage;
                    break;
            }
        }

        #endregion

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
        private void PaymentWizardControl_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            switch(Presenter.OnSelectedPageChanging(e.PrevPage, e.Page, e.Direction))
            {
                case WizardSteps.ChooseMethodPage:
                    e.Page = ChooseMethodWizardPage;
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
        private void PaymentWizardControl_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(e.Page, e.PrevPage, e.Direction);
        }

        /// <summary>
        /// Обрабатывает событие изменения выбора вида создаваемых квитанций
        /// </summary>
        private void BillTyppes_CheckedChanged(object sender, EventArgs e)
        {
            debtGroupBox.Enabled = DebtRadioButton.Checked;
            totalGroupBox.Enabled = TotalBillRadioButton.Checked;
        }
    }
}