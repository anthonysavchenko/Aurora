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
            SaveFileDialog _saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data", 
                Title = "Открыть файл", 
                Filter = "Текстовый файл|*.txt", 
                FilterIndex = 1, 
                RestoreDirectory = true, 
                DefaultExt = "txt",
                FileName = "2540165515_40702810900100001650_UTF.txt"
            };

            View.FilePath = _saveFileDialog.ShowDialog() == DialogResult.OK 
                ? _saveFileDialog.FileName 
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
                    View.ShowMessage("Укажите файл для экспорта", "Ошибка экспорта");
                }
                else
                {
                    string _dirPath = Path.GetDirectoryName(View.FilePath);
                    string _postfix = View.IsSberbankFileFormat ? "UTF_Сбербанк" : "Примсоцбанк";
                    Encoding _encoding = View.IsSberbankFileFormat ? Encoding.UTF8 : Encoding.GetEncoding(1251);
                    DateTime _period = new DateTime(View.Period.Year, View.Period.Month, 1);

                    using (Entities _entities = new Entities())
                    {
                        _entities.CommandTimeout = 3600;

                        var _list = 
                            _entities.ChargeOpers
                                .Select(
                                    c =>
                                    new
                                    {
                                        CustomerID = c.Customers.ID,
                                        BuildingID = c.Customers.Buildings.ID,
                                        c.Customers.Buildings.BankDetailID,
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
                                                BuildingID = c.Customers.Buildings.ID,
                                                c.Customers.Buildings.BankDetailID,
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
                                            BuildingID = c.Customers.Buildings.ID,
                                            c.Customers.Buildings.BankDetailID,
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
                                                BuildingID = c.Customers.Buildings.ID,
                                                c.Customers.Buildings.BankDetailID,
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
                                                BuildingID = b.ChargeOpers.Customers.Buildings.ID,
                                                b.ChargeOpers.Customers.Buildings.BankDetailID,
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
                                                BuildingID = b.ChargeOpers.Customers.Buildings.ID,
                                                b.ChargeOpers.Customers.Buildings.BankDetailID,
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
                                                BuildingID = b.RechargeOpers.Customers.Buildings.ID,
                                                b.RechargeOpers.Customers.Buildings.BankDetailID,
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
                                                BuildingID = b.RechargeOpers.Customers.Buildings.ID,
                                                b.RechargeOpers.Customers.Buildings.BankDetailID,
                                                b.BenefitCorrectionOpers.ChargeCorrectionOpers.Period,
                                                Value = -1 * b.Value
                                            }))
                                .Concat(
                                    _entities.PaymentOperPoses
                                        .Select(p =>
                                            new
                                            {
                                                CustomerID = p.PaymentOpers.Customers.ID,
                                                BuildingID = p.PaymentOpers.Customers.Buildings.ID,
                                                p.PaymentOpers.Customers.Buildings.BankDetailID,
                                                p.Period,
                                                p.Value
                                            }))
                                .Concat(
                                    _entities.PaymentCorrectionOpers
                                        .Select(p =>
                                            new
                                            {
                                                CustomerID = p.PaymentOpers.Customers.ID,
                                                BuildingID = p.PaymentOpers.Customers.Buildings.ID,
                                                p.PaymentOpers.Customers.Buildings.BankDetailID,
                                                p.Period,
                                                p.Value
                                            }))
                                .Concat(
                                    _entities.OverpaymentOperPoses
                                        .Select(o =>
                                            new
                                            {
                                                CustomerID = o.OverpaymentOpers.Customers.ID,
                                                BuildingID = o.OverpaymentOpers.Customers.Buildings.ID,
                                                o.OverpaymentOpers.Customers.Buildings.BankDetailID,
                                                o.Period,
                                                o.Value
                                            }))
                                .Concat(
                                    _entities.OverpaymentCorrectionOpers
                                        .Select(o =>
                                            new
                                            {
                                                CustomerID = o.ChargeOpers.Customers.ID,
                                                BuildingID = o.ChargeOpers.Customers.Buildings.ID,
                                                o.ChargeOpers.Customers.Buildings.BankDetailID,
                                                o.Period,
                                                o.Value
                                            }))
                                .Where(c => c.Period <= _period)
                                .GroupBy(
                                    c =>
                                    new
                                    {
                                        c.CustomerID,
                                        c.BuildingID,
                                        c.BankDetailID,
                                    })
                                .Select(
                                    g =>
                                    new
                                    {
                                        g.Key.CustomerID,
                                        g.Key.BuildingID,
                                        g.Key.BankDetailID,
                                        Value = g.Sum(c => c.Value) 
                                    })
                            .ToList()
                            .GroupBy(r => r.BankDetailID)
                            .Select(gBankDetail =>
                                new
                                {
                                    BankDetailID = gBankDetail.Key.Value,
                                    Data = gBankDetail
                                        .Select(r => 
                                            new
                                            {
                                                r.CustomerID,
                                                r.BuildingID,
                                                Value = r.Value < 0 ? 0 : r.Value
                                            })
                                        .ToList()
                                })
                            .ToDictionary(r => r.BankDetailID, r => r.Data);

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

                        var _buildings = 
                            _entities.Buildings
                                .Select(b => 
                                    new
                                    {
                                        b.ID,
                                        StreetName = b.Streets.Name, 
                                        b.Number,
                                    })
                                .ToDictionary(b => b.ID);

                        var _bankDetails =
                            _entities.BankDetails
                                .Select(b =>
                                    new
                                    {
                                        b.ID,
                                        b.INN,
                                        b.Account
                                    })
                                .ToDictionary(b => b.ID);

                        foreach(var _pair in _list)
                        {
                            var _bd = _bankDetails[_pair.Key];
                            string _fileName = $"{_dirPath}\\{_bd.INN}_{_bd.Account}_{_postfix}.txt";
                            using (StreamWriter _file = new StreamWriter(_fileName, false, _encoding))
                            {
                                _file.AutoFlush = true;

                                if(!View.IsSberbankFileFormat)
                                {
                                    _file.WriteLine($"#FILESUM {_pair.Value.Sum(x => x.Value)}");
                                    _file.WriteLine("#TYPE 7");
                                    _file.WriteLine("#SERVICE 63350");
                                }

                                foreach (var _record in _pair.Value)
                                {
                                    var _customer = _customers[_record.CustomerID];
                                    var _building = _buildings[_record.BuildingID];

                                    string _owner = "Неизвестен";

                                    if (_customer.OwnerType == (int)Customer.OwnerTypes.PhysicalPerson)
                                    {
                                        _owner = _customer.PhysicalPersonFullName;
                                    }
                                    else if (_customer.OwnerType == (int)Customer.OwnerTypes.JuridicalPerson)
                                    {
                                        _owner = _customer.JuridicalPersonFullName;
                                    }

                                    if (View.IsSberbankFileFormat)
                                    {
                                        _file.WriteLine("{0}|{1}|{2}",
                                            _customer.Account,
                                            _owner,
                                            _record.Value.ToString().Replace(',', '.'));
                                    }
                                    else
                                    {
                                        _file.WriteLine(
                                            "{0};Владивосток,{1},{2},{3};{4};{5}",
                                            _owner, 
                                            _building.StreetName,
                                            _building.Number,
                                            _customer.Apartment,
                                            _customer.Account,
                                            _record.Value.ToString().Replace(',', '.'));
                                    }
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