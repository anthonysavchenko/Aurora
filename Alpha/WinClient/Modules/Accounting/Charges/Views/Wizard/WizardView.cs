using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard
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
        /// Строка целиком
        /// </summary>
        public bool WholeWord
        {
            get
            {
                return WholeWordCheckBox.Checked;
            }
            set
            {
                WholeWordCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Наименование улицы
        /// </summary>
        public string Street
        {
            get
            {
                return streetTextEdit.Text;
            }
            set
            {
                streetTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string House
        {
            get
            {
                return houseTextEdit.Text;
            }
            set
            {
                houseTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment
        {
            get
            {
                return apartmentTextEdit.Text;
            }
            set
            {
                apartmentTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Номер аккаунта
        /// </summary>
        public string Account
        {
            get
            {
                return accountTextEdit.Text;
            }
            set
            {
                accountTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode
        {
            get
            {
                return zipCodeTextEdit.Text;
            }
            set
            {
                zipCodeTextEdit.Text = value;
            }
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        public FilterType Filter
        {
            get
            {
                FilterType _res = FilterType.All;

                if (addressRadioButton.Checked)
                {
                    _res = FilterType.Address;
                }

                if (accountRadioButton.Checked)
                {
                    _res = FilterType.Account;
                }

                if (zipCodeRadioButton.Checked)
                {
                    _res = FilterType.ZipCode;
                }

                return _res;
            }
            set
            {
                switch (value)
                {
                    case FilterType.Account:
                        accountRadioButton.Checked = true;
                        break;
                    case FilterType.Address:
                        addressRadioButton.Checked = true;
                        break;
                    case FilterType.ZipCode:
                        zipCodeRadioButton.Checked = true;
                        break;
                    case FilterType.All:
                        allRadioButton.Checked = true;
                        break;

                }
            }
        }

        /// <summary>
        /// Вид создаваемых начислений
        /// </summary>
        public ChargeType ChargeType
        {
            get
            {
                ChargeType _res = ChargeType.Regular;
                if (regularChargesRadioButton.Checked)
                {
                    _res = ChargeType.Regular;
                }
                else if (correctionChargesRadioButton.Checked)
                {
                    _res = ChargeType.Correction;
                }
                else if (PercentCorrectionRadioButton.Checked)
                {
                    _res = ChargeType.PercentCorrection;
                }
                else if (debtRadioButton.Checked)
                {
                    _res = ChargeType.Debt;
                }

                return _res;
            }
        }

        /// <summary>
        /// Период предстоящих начислений
        /// </summary>
        public DateTime ChargingPeriod
        {
            set
            {
                chargePeriodLabel.Text = string.Format("{0:MMMM yyyy} года", value);
                debtPeriodDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Имя файла с импортируемыми задолжнастями
        /// </summary>
        public string DebtFileName
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
        /// Период внесения задолжностей
        /// </summary>
        public DateTime DebtPeriod
        {
            get
            {
                DateTime _date = debtPeriodDateEdit.DateTime;
                return new DateTime(_date.Year, _date.Month, 1);
            }
        } 

        /// <summary>
        /// Таблица с информацией об абонентах
        /// </summary>
        public DataTable FoundCustomers
        {
            get
            {
                return (DataTable)foundCustomersGridControl.DataSource;
            }
            set
            {
                foundCustomersGridControl.DataSource = value;
                foundCustomersGridView.BestFitColumns();
            }
        }

        /// <summary>
        /// Источник данных для таблицы с выбранными абонентами
        /// </summary>
        public DataTable SelectedCustomers
        {
            get
            {
                return (DataTable)selectedCustomersGridControl.DataSource;
            }
            set
            {
                selectedCustomersGridControl.DataSource = value;
                selectedCustomersGridView.BestFitColumns();
            }
        }

        /// <summary>
        /// Начальный период 
        /// </summary>
        public DateTime SinceCorrectionPeriod
        {
            get
            {
                DateTime _dt = sinceCorrectionPeriodDateEdit.DateTime;
                return new DateTime(_dt.Year, _dt.Month, 1);
            }
            set
            {
                sinceCorrectionPeriodDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Конечный период
        /// </summary>
        public DateTime TillCorrectionPeriod
        {
            get
            {
                DateTime _dt = toCorrectionPeriodDateEdit.DateTime;
                return new DateTime(_dt.Year, _dt.Month, 1);
            }
            set
            {
                toCorrectionPeriodDateEdit.DateTime = value;
            }
        }

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

        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service
        {
            get
            {
                Service _res = null;

                if (ServiceLookUpEdit.ItemIndex != -1)
                {
                    string _id = ServiceLookUpEdit.GetColumnValue("ID").ToString();
                    _res = Presenter.GetItem<Service>(_id);
                }

                return _res;
            }
        }

        /// <summary>
        /// Услуги
        /// </summary>
        public DataTable Services
        {
            set
            {
                ServiceLookUpEdit.Properties.DataSource = value;
                ServiceLookUpEdit.Properties.ForceInitialize();
            }
        }

        /// <summary>
        /// Период снятия начислений по услуге
        /// </summary>
        public DateTime PercentCorrectionPeriod
        {
            get
            {
                DateTime _dt = PercentCorrectionPeriodDateEdit.DateTime;
                return new DateTime(_dt.Year, _dt.Month, 1);
            }
            set
            {
                PercentCorrectionPeriodDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Источник данных для таблицы с абонентами, процентами снятия и днями
        /// </summary>
        public DataTable CustomersWithPercents
        {
            get
            {
                return (DataTable)PercentsGridControl.DataSource;
            }
            set
            {
                PercentsGridControl.DataSource = value;
                PercentsGridView.BestFitColumns();
            }
        }

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        public void ResetProgressBar(int maxValue)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() =>
            {
                ProgressBarControl.Properties.Maximum = maxValue;
                ProgressBarControl.Properties.Minimum = 0;
                ProgressBarControl.EditValue = 0;
            }));
        }

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        /// <param name="delta">Количество обработанных единиц</param>
        public void AddProgress(int delta)
        {
            ProgressBarControl.Invoke(new MethodInvoker(() => ProgressBarControl.EditValue = (int)ProgressBarControl.EditValue + delta));
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
        /// Сбрасывает текущее состояние процесса резервного копирования
        /// </summary>
        public void ResetBackupProgress()
        {
            backupProgressBar.Invoke(new MethodInvoker(() =>
            {
                backupProgressBar.Properties.Maximum = 100;
                backupProgressBar.Properties.Minimum = 0;
                backupProgressBar.EditValue = 0;
            }));
        }

        /// <summary>
        /// Устанавливает состояние процесса резервного копирования
        /// </summary>
        /// <param name="value">Текущее значение</param>
        public void SetBackupProgress(int value)
        {
            backupProgressBar.Invoke(new MethodInvoker(() => backupProgressBar.EditValue = value));
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
                    PaymentWizardControl.SelectedPage = ChooseMethodWizardPage;
                    break;
                case WizardPages.CustomersPage:
                    PaymentWizardControl.SelectedPage = CustomersWizardPage;
                    break;
                case WizardPages.PercentPage:
                    PaymentWizardControl.SelectedPage = PercentWizardPage;
                    break;
                case WizardPages.ChoosePeriodPage:
                    PaymentWizardControl.SelectedPage = ChoosePeriodWizardPage;
                    break;
                case WizardPages.BackupPage:
                    PaymentWizardControl.SelectedPage = BackupWizardPage;
                    break;
                case WizardPages.ProcessingPage:
                    PaymentWizardControl.SelectedPage = ProcessingWizardPage;
                    break;
                case WizardPages.FinishPage:
                    PaymentWizardControl.SelectedPage = FinishWizardPage;
                    break;
            }
        }

        #endregion

        private WizardPages ConvertWizardPage(BaseWizardPage page)
        {
            switch (page.Name)
            {
                case "ChooseMethodWizardPage":
                    return WizardPages.ChooseMethodPage;
                case "CustomersWizardPage":
                    return WizardPages.CustomersPage;
                case "PercentWizardPage":
                    return WizardPages.PercentPage;
                case "ChoosePeriodWizardPage":
                    return WizardPages.ChoosePeriodPage;
                case "BackupWizardPage":
                    return WizardPages.BackupPage;
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
                case WizardPages.CustomersPage:
                    return CustomersWizardPage;
                case WizardPages.PercentPage:
                    return PercentWizardPage;
                case WizardPages.ChoosePeriodPage:
                    return ChoosePeriodWizardPage;
                case WizardPages.BackupPage:
                    return BackupWizardPage;
                case WizardPages.ProcessingPage:
                    return ProcessingWizardPage;
                case WizardPages.FinishPage:
                    return FinishWizardPage;
                default:
                    return null;
            }
        }

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

        private void AddressControl_Enter(object sender, EventArgs e)
        {
            addressRadioButton.Checked = true;
        }

        private void accountTextEdit_Enter(object sender, EventArgs e)
        {
            accountRadioButton.Checked = true;
        }

        private void zipCodeTextEdit_Enter(object sender, EventArgs e)
        {
            zipCodeRadioButton.Checked = true;
        }

        /// <summary>
        /// Обрабатывает событие попытки перейти на новую страницу мастера
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            BaseWizardPage _page =
                ConvertWizardPage(Presenter.OnSelectedPageChanging(ConvertWizardPage(e.PrevPage), e.Direction));

            if (_page != null)
            {
                e.Page = _page;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        private void PaymentWizardControl_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            Presenter.OnSelectedPageChanged(ConvertWizardPage(e.Page), e.Direction);
        }

        /// <summary>
        /// Обрабатывает поиск абонентов по фильтру
        /// </summary>
        private void searchButton_Click(object sender, EventArgs e)
        {
            Presenter.SetCustomers();
        }

        private void debtRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            fileGroupBox.Enabled = debtRadioButton.Checked;
        }

        private void fileOpenButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileOpenButtonEdit.Text = OpenFileDialog.FileName;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Presenter.AddToSelectedCustomers();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Presenter.DeleteFromSelectedCustomers();
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            Presenter.ClearSelectedCustomers();
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            Presenter.ChangeAllFoundCustomersSelect(true);
        }

        private void unselectAllButton_Click(object sender, EventArgs e)
        {
            Presenter.ChangeAllFoundCustomersSelect(false);
        }
    }
}