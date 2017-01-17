using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        private const string BENEFIT_PROCESSING_START = "event://Export/Benefit/Start";
        private const string BENEFIT_PROCESSING_COMPLETED = "event://Export/Benefit/Completed";
        private const string GISZHKH_PROCESSING_START = "event://Export/GisZhkh/Start";
        private const string GISZHKH_PROCESSING_COMPLETED = "event://Export/GisZhkh/Completed";

        private IBenefitDataExportService _srv = new BenefitDataExportService();
        private IGisZhkhDataExportService _gisZhkhDataExportService = new GisZhkhDataExportService();

        /// <summary>
        /// Обрабатывает отображение вью
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.Period = ServerTime.GetPeriodInfo().LastCharged;
        }

        /// <summary>
        /// Обрабатывает активацию модуля
        /// </summary>
        public override void ActivateUseCase()
        {
            /* Не реализованно */
        }

        /// <summary>
        /// Открывает диалог открытия файла
        /// </summary>
        /// <returns></returns>
        public void FindFile()
        {
            FolderBrowserDialog _dialog =
                new FolderBrowserDialog
                {
                    ShowNewFolderButton = true,
                    Description = @"Выберите папку для экспорта начислений",
                };

            View.FilePath = _dialog.ShowDialog() == DialogResult.OK
                ? _dialog.SelectedPath
                : string.Empty;
        }

        /// <summary>
        /// Экспортирует начисления
        /// </summary>
        public void ExportFile()
        {
            try
            {
                if (String.IsNullOrEmpty(View.FilePath))
                {
                    View.ShowMessage("Укажите папку для экспорта", "Ошибка экспорта");
                }
                else
                {
                    DateTime _period = new DateTime(View.Period.Year, View.Period.Month, 1);

                    using (Entities _entities = new Entities())
                    {
                        _entities.CommandTimeout = 3600;

                        #region Запросы

                        var _byBuildings =
                            _entities.ChargeOpers
                                .Select(
                                    c =>
                                        new
                                        {
                                            CustomerID = c.Customers.ID,
                                            BuildingAccount = c.Customers.Buildings.BankDetails.Account,
                                            c.ChargeSets.Period,
                                            c.Value
                                        })
                                .Concat(
                                    _entities.ChargeOpers
                                        .Where(c => c.ChargeCorrectionOpers != null)
                                        .Select(
                                            c =>
                                                new
                                                {
                                                    CustomerID = c.Customers.ID,
                                                    BuildingAccount = c.Customers.Buildings.BankDetails.Account,
                                                    c.ChargeCorrectionOpers.Period,
                                                    Value = -1 * c.Value
                                                }))
                                .Concat(
                                    _entities.RechargeOpers
                                        .Select(
                                            c =>
                                                new
                                                {
                                                    CustomerID = c.Customers.ID,
                                                    BuildingAccount = c.Customers.Buildings.BankDetails.Account,
                                                    c.RechargeSets.Period,
                                                    c.Value
                                                }))
                                .Concat(
                                    _entities.RechargeOpers
                                        .Where(c => c.ChildChargeCorrectionOpers != null)
                                        .Select(
                                            c =>
                                                new
                                                {
                                                    CustomerID = c.Customers.ID,
                                                    BuildingAccount = c.Customers.Buildings.BankDetails.Account,
                                                    c.ChildChargeCorrectionOpers.Period,
                                                    Value = -1 * c.Value
                                                }))
                                .Concat(
                                    _entities.BenefitOpers
                                        .Select(
                                            b =>
                                                new
                                                {
                                                    CustomerID = b.ChargeOpers.Customers.ID,
                                                    BuildingAccount = b.ChargeOpers.Customers.Buildings.BankDetails.Account,
                                                    b.ChargeOpers.ChargeSets.Period,
                                                    b.Value
                                                }))
                                .Concat(
                                    _entities.BenefitOpers
                                        .Where(b => b.BenefitCorrectionOpers != null)
                                        .Select(
                                            b =>
                                                new
                                                {
                                                    CustomerID = b.ChargeOpers.Customers.ID,
                                                    BuildingAccount = b.ChargeOpers.Customers.Buildings.BankDetails.Account,
                                                    b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                                    Value = -1 * b.Value
                                                }))
                                .Concat(
                                    _entities.RebenefitOpers
                                        .Select(
                                            b =>
                                                new
                                                {
                                                    CustomerID = b.RechargeOpers.Customers.ID,
                                                    BuildingAccount = b.RechargeOpers.Customers.Buildings.BankDetails.Account,
                                                    b.RechargeOpers.RechargeSets.Period,
                                                    b.Value
                                                }))
                                .Concat(
                                    _entities.RebenefitOpers
                                        .Where(b => b.BenefitCorrectionOpers != null)
                                        .Select(
                                            b =>
                                                new
                                                {
                                                    CustomerID = b.RechargeOpers.Customers.ID,
                                                    BuildingAccount = b.RechargeOpers.Customers.Buildings.BankDetails.Account,
                                                    b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                                    Value = -1 * b.Value
                                                }))
                                .Concat(
                                    _entities.PaymentOperPoses
                                        .Select(p =>
                                            new
                                            {
                                                CustomerID = p.PaymentOpers.Customers.ID,
                                                BuildingAccount = p.PaymentOpers.Customers.Buildings.BankDetails.Account,
                                                p.Period,
                                                p.Value
                                            }))
                                .Concat(
                                    _entities.PaymentCorrectionOpers
                                        .Select(p =>
                                            new
                                            {
                                                CustomerID = p.PaymentOpers.Customers.ID,
                                                BuildingAccount = p.PaymentOpers.Customers.Buildings.BankDetails.Account,
                                                p.Period,
                                                p.Value
                                            }))
                                .Concat(
                                    _entities.OverpaymentOperPoses
                                        .Select(o =>
                                            new
                                            {
                                                CustomerID = o.OverpaymentOpers.Customers.ID,
                                                BuildingAccount = o.OverpaymentOpers.Customers.Buildings.BankDetails.Account,
                                                o.Period,
                                                o.Value
                                            }))
                                .Concat(
                                    _entities.OverpaymentCorrectionOpers
                                        .Select(o =>
                                            new
                                            {
                                                CustomerID = o.ChargeOpers.Customers.ID,
                                                BuildingAccount = o.ChargeOpers.Customers.Buildings.BankDetails.Account,
                                                o.Period,
                                                o.Value
                                            }))
                                .Where(c => c.Period <= _period)
                                .GroupBy(c => c.BuildingAccount)
                                .Select(
                                    g =>
                                        new
                                        {
                                            BuildingAccount = g.Key,
                                            ByCustomers = g
                                                .GroupBy(c => c.CustomerID)
                                                .Select(byCustomer =>
                                                    new
                                                    {
                                                        CustomerID = byCustomer.Key,
                                                        Value = byCustomer.Sum(c => c.Value)
                                                    })
                                        })
                                .ToList();

                        var _customers =
                            _entities.Customers
                                .Select(c =>
                                    new
                                    {
                                        c.ID,
                                        c.OwnerType,
                                        c.Account,
                                        c.PhysicalPersonFullName,
                                        c.JuridicalPersonFullName,
                                        c.Apartment,
                                    })
                                .ToDictionary(c => c.ID);

                        #endregion

                        foreach (var _byBuilding in _byBuildings)
                        {
                            string _filePath = string.Format("{0}\\2540165515_{1}_UTF.txt", View.FilePath, _byBuilding.BuildingAccount);

                            using (StreamWriter _file = new StreamWriter(_filePath, false, Encoding.UTF8))
                            {
                                _file.AutoFlush = true;

                                foreach (var _byCustomer in _byBuilding.ByCustomers)
                                {
                                    var _customer = _customers[_byCustomer.CustomerID];

                                    string _owner = "Неизвестен";

                                    if (_customer.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson)
                                    {
                                        _owner = _customer.PhysicalPersonFullName;
                                    }
                                    else if (_customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson)
                                    {
                                        _owner = _customer.JuridicalPersonFullName;
                                    }

                                    _file.WriteLine("{0}|{1}|{2}|{3}|0.00|{3}",
                                        _customer.Account.Replace("EG-", string.Empty),
                                        _owner,
                                        _period.ToString("MM.yyyy"),
                                        _byCustomer.Value < 0 ? "0" : _byCustomer.Value.ToString().Replace(',', '.'));
                                }
                            }
                        }
                    }
                    View.ShowMessage("Операция выполнена успешно", "Экспорт");
                }
            }
            catch (Exception _ex)
            {
                Logger.SimpleWrite(String.Format("Export error: {0} {1}", _ex, _ex.InnerException != null ? _ex.InnerException.ToString() : String.Empty));
                View.ShowMessage("Произошла ошибка. Операция не выполнена", "Экспорт");
            }
        }

        public void BenefitExport()
        {
            if (string.IsNullOrEmpty(View.BenefitInputFilePath))
            {
                View.ShowMessage("Укажите файл для экспорта", "Ошибка экспорта");
            }
            else
            {
                View.ShowBenefitProgressBar();
                BenefitProcessingStart(this, EventArgs.Empty);
            }
        }

        [EventPublication(BENEFIT_PROCESSING_START, PublicationScope.WorkItem)]
        public event EventHandler BenefitProcessingStart;

        [EventSubscription(BENEFIT_PROCESSING_START, ThreadOption.Background)]
        public void OnBenefitProcessingStart(object sender, EventArgs e)
        {
            string _message = _srv.ProcessFile(View.BenefitInputFilePath);
            BenefitProcessingCompleted(this, new EventArgs<string>(_message));
        }

        [EventPublication(BENEFIT_PROCESSING_COMPLETED, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> BenefitProcessingCompleted;

        [EventSubscription(BENEFIT_PROCESSING_COMPLETED, ThreadOption.UserInterface)]
        public void OnGenerateReportFired(object sender, EventArgs<string> e)
        {
            View.HideBenefitProgressBar();
            View.ShowMessage(e.Data, "Экспорт");
        }

        public void GisZhkhExport()
        {
            if (string.IsNullOrEmpty(View.GisZhkhInputFilePath))
            {
                View.ShowMessage("Укажите файл с шаблоном для экспорта данных в ГИС ЖКХ", "Ошибка экспорта");
            }
            else
            {
                View.ShowGisZhkhProgressBar();
                GisZhkhProcessingStart(this, EventArgs.Empty);
            }
        }

        [EventPublication(GISZHKH_PROCESSING_START, PublicationScope.WorkItem)]
        public event EventHandler GisZhkhProcessingStart;

        [EventSubscription(GISZHKH_PROCESSING_START, ThreadOption.Background)]
        public void OnGisZhkhProcessingStart(object sender, EventArgs e)
        {
            string _message = _gisZhkhDataExportService.ProcessFile(View.GisZhkhInputFilePath, View.GisZhkhOnlyNew);
            GisZhkhProcessingCompleted(this, new EventArgs<string>(_message));
        }

        [EventPublication(GISZHKH_PROCESSING_COMPLETED, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> GisZhkhProcessingCompleted;

        [EventSubscription(GISZHKH_PROCESSING_COMPLETED, ThreadOption.UserInterface)]
        public void OnGisZhkhProcessingCompleted(object sender, EventArgs<string> e)
        {
            View.HideGisZhkhProgressBar();
            View.ShowMessage(e.Data, "Экспорт");
        }
    }
}