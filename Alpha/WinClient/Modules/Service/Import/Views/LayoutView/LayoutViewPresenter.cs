using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        private readonly IImportService _gisZhkhCustomersImportService;
        private readonly IImportService _newCustomersImportService;
        private readonly IImportService _customerPosesImportService;
        private readonly IImportService _privateCounterImportService;
        private readonly IImportService _buildingConsumptionImportService;
        private readonly IImportService _electricitySharedCounterVolumeImportService;
        private readonly IImportService _publicPlaceServiceVolumesImportService;

        [InjectionConstructor]
        public LayoutViewPresenter(
            [ServiceDependency]IExcelService excelService,
            [ServiceDependency]IServerTimeService serverTimeService)
        {
            _gisZhkhCustomersImportService = new GisZhkhCustomersImportService(excelService);
            _newCustomersImportService = new NewCustomersImportService(excelService);
            _customerPosesImportService = new CustomerPosesImportService(excelService);
            _buildingConsumptionImportService = new BuildingConsumptionImportService(excelService);
            _privateCounterImportService = new PrivateCounterImportService(excelService);
            _electricitySharedCounterVolumeImportService = new ElectricitySharedCounterVolumeImportService(excelService);
            _publicPlaceServiceVolumesImportService = new PublicPlaceServiceVolumesImportService(excelService, serverTimeService);
        }

        /// <summary>
        /// Обрабатывает активацию модуля
        /// </summary>
        public override void ActivateUseCase()
        {
            /* Не реализованно */
        }

        public WizardPages OnSelectingPageChanging(WizardPages prevPage, bool forwardDirection)
        {
            WizardPages _next = WizardPages.Unknown;

            if (forwardDirection)
            {
                switch(prevPage)
                {
                    case WizardPages.ChooseMethodPage:
                        _next = WizardPages.FilePage;
                        View.Period = ServerTime.GetPeriodInfo().FirstUncharged;
                        break;

                    case WizardPages.FilePage:
                        if (ValidateFilePage())
                        {
                            _next = WizardPages.ProcessingPage;
                        }
                        break;

                    case WizardPages.ProcessingPage:
                        _next = WizardPages.FinishPage;
                        break;
                }
            }
            else
            {
                _next = WizardPages.ChooseMethodPage;
            }

            return _next;
        }

        private bool ValidateFilePage()
        {
            bool _result = !string.IsNullOrEmpty(View.FilePath);
            if(!_result)
            {
                View.ShowMessage("Выберите файл для импорта данных", "Ошибка");
            }

            _result = ValidateImportPeriod(View.Period);

            return _result;
        }

        private bool ValidateImportPeriod(DateTime period)
        {
            bool _result;

            using (Entities _db = new Entities())
            {
                _result = View.WizardAction == WizardAction.ImportPublicPlaceServiceVolumes 
                    ? _db.PublicPlaceServiceVolumes.Any(x => x.Period == period)
                    : View.WizardAction == WizardAction.ImportElectricitySharedCounterVolumes 
                        ? _db.ElectricitySharedCounterVolumes.Any(x => x.Period == period)
                        : _db.PrivateCounterValues.Any(x => x.Period == period);
            }

            if(_result)
            {
                DialogResult _answer = MessageBox.Show(
                        $"Данные за {period:MMMM yyyy} уже импортированы, при новом импорте они будут перезаписаны. Продолжить?",
                        $"Импорт данных за {period:MMMM yyyy}",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                _result = _answer == DialogResult.Yes;
            }
            else
            {
                _result = true;
            }

            return _result;
        }

        public void OnSelectedPageChanged(WizardPages page, bool forwardDirection)
        {
            if (forwardDirection)
            {
                switch(page)
                {
                    case WizardPages.ProcessingPage:
                        BackgroundWorker _worker = CreateBackgroundWorker();
                        _worker.RunWorkerAsync();
                        break;
                }
            }
        }

        public void GenerateImportPublicPlaceServiceVolumeTemplate()
        {
            SaveFileDialog _dialog =
                new SaveFileDialog
                {
                    Filter = "Excel 2007 (*.xlsx)|*.xlsx",
                    RestoreDirectory = true,
                    FileName =
                        $"{ServerTime.GetDateTimeInfo().Now:yyyy.MM.dd HH.mm} " +
                        $"{AppInfo.PRODUCT_NAME} " +
                        "Импорт показаний ОДПУ для начислений.xlsx",
                };

            if(_dialog.ShowDialog() == DialogResult.OK )
            {
                string _info = _publicPlaceServiceVolumesImportService.GenerateImportTemplate(_dialog.FileName)
                    ? "Шаблон успешно создан"
                    : "Не удалось создать шаблон";
                View.ShowMessage(_info, "Инфо");
            }
        }

        public void GenerateBuildingConsumptionImportTemplate()
        {
            SaveFileDialog _dialog =
                new SaveFileDialog
                {
                    Filter = "Excel 2007 (*.xlsx)|*.xlsx",
                    RestoreDirectory = true,
                    FileName =
                        $"{ServerTime.GetDateTimeInfo().Now:yyyy.MM.dd HH.mm} " +
                        $"{AppInfo.PRODUCT_NAME} " +
                        "Импорт справочных данных по общедомовым объемам.xlsx",
                };

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                string _info = _buildingConsumptionImportService.GenerateImportTemplate(_dialog.FileName)
                    ? "Шаблон успешно создан"
                    : "Не удалось создать шаблон";
                View.ShowMessage(_info, "Инфо");
            }
        }

        public void GenerateImportNewCustomersTemplate()
        {
            SaveFileDialog _dialog =
                new SaveFileDialog
                {
                    Filter = "Excel 2007 (*.xlsx)|*.xlsx",
                    RestoreDirectory = true,
                    FileName =
                        $"{ServerTime.GetDateTimeInfo().Now:yyyy.MM.dd HH.mm} " +
                        $"{AppInfo.PRODUCT_NAME} " +
                        "Импорт новых абонентов.xlsx"
                };

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                string _info = _newCustomersImportService.GenerateImportTemplate(_dialog.FileName)
                    ? "Шаблон успешно создан"
                    : "Не удалось создать шаблон";
                View.ShowMessage(_info, "Инфо");
            }
        }

        private BackgroundWorker CreateBackgroundWorker()
        {
            BackgroundWorker _worker = new BackgroundWorker { WorkerReportsProgress = true };
            _worker.ProgressChanged += (sender, args) =>
            {
                View.SetProgress(args.ProgressPercentage);
            };

            _worker.DoWork += (sender, args) =>
            {
                Action<int> _reportProgressAction = ((BackgroundWorker)sender).ReportProgress;
                switch (View.WizardAction)
                {
                    case WizardAction.ImportNewCustomers:
                        args.Result = _newCustomersImportService.ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportCustomerPoses:
                        args.Result = _customerPosesImportService.ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportGisZhkhCustomerIDs:
                        args.Result = _gisZhkhCustomersImportService.ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportPublicPlaceServiceVolumes:
                        args.Result = _publicPlaceServiceVolumesImportService.ProcessFile(View.FilePath, _reportProgressAction, View.Period); 
                        break;
                    case WizardAction.ImportCounters:
                        args.Result = _privateCounterImportService.ProcessFile(View.FilePath, _reportProgressAction, View.Period);
                        break;
                    case WizardAction.ImportElectricitySharedCounterVolumes:
                        args.Result = _electricitySharedCounterVolumeImportService.ProcessFile(View.FilePath, _reportProgressAction, View.Period);
                        break;
                    case WizardAction.ImportBuildingConsumptionInfo:
                        args.Result = _buildingConsumptionImportService.ProcessFile(View.FilePath, _reportProgressAction, View.Period);
                        break;
                }
            };

            _worker.RunWorkerCompleted += (sender, args) =>
            {
                View.ResultText = (string)args.Result;
                View.SelectPage(WizardPages.FinishPage);
            };
            return _worker;
        }
    }
}