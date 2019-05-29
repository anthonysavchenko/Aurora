using Microsoft.Practices.CompositeUI;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Layout
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        [ServiceDependency]
        public IExcelService ExcelService { get; set; }

        [ServiceDependency]
        public IPublicPlaceServiceVolumesImportService PublicPlaceServiceVolumesImportService { get; set; }

        [ServiceDependency]
        public IChildrenOfWarBenefitImportService ChildrenOfWarBenefitImportService { get; set; }

        /// <summary>
        /// Обрабатывает активацию модуля
        /// </summary>
        public override void ActivateUseCase()
        {
            /* Не реализованно */
        }

        public override void OnViewReady()
        {
            using (var db = new Entities())
            {
                View.Streets = db.GetStreetsForComboBox();
            }
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
                        if (Validate(View.WizardAction))
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

        private bool Validate(WizardAction wizardAction)
        {
            bool _result = DataValidator.ValidateFilePath(View.FilePath, out string _errorMessage);
            if (_result)
            {
                switch (wizardAction)
                {
                    case WizardAction.ImportPublicPlaceServiceVolumes:
                    case WizardAction.ImportCounters:
                    case WizardAction.ImportElectricitySharedCounterVolumes:
                        _result = DataValidator.ValidateImportPeriod(View.Period, wizardAction);
                        break;
                    case WizardAction.ImportChildrenOfWarBenefit:
                        _result = DataValidator.ValidateAddress(View.BuildingId, out _errorMessage);
                        break;
                    default:
                        _result = true;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(_errorMessage))
            {
                View.ShowMessage(_errorMessage, "Ошибка");
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
                    FileName = "Импорт_СОД.xlsx"
                };

            if(_dialog.ShowDialog() == DialogResult.OK )
            {
                string _info = PublicPlaceServiceVolumesImportService.GenerateTemplate(_dialog.FileName)
                    ? "Шаблон успешно создан"
                    : "Не удалось создать шаблон";
                View.ShowMessage(_info, "Инфо");
            }
        }

        public void FillBuildingList()
        {
            using (Entities db = new Entities())
            {
                View.Buildings = db.GetBuildingsForComboBox(int.Parse(View.StreetId));
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
                        args.Result = new NewCustomersImportService(ExcelService).ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportCustomerPoses:
                        args.Result = new CustomerPosesImportService(ExcelService).ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportGisZhkhCustomerIDs:
                        args.Result = new GisZhkhCustomersImportService(ExcelService).ProcessFile(View.FilePath, _reportProgressAction);
                        break;
                    case WizardAction.ImportPublicPlaceServiceVolumes:
                        args.Result = PublicPlaceServiceVolumesImportService.ProcessFile(View.FilePath, _reportProgressAction, View.Period); 
                        break;
                    case WizardAction.ImportCounters:
                        args.Result = new PrivateCounterImportService(ExcelService).ProcessFile(View.FilePath, _reportProgressAction, View.Period);
                        break;
                    case WizardAction.ImportElectricitySharedCounterVolumes:
                        args.Result = new ElectricitySharedCounterVolumeImportService(ExcelService).ProcessFile(View.FilePath, _reportProgressAction, View.Period);
                        break;
                    case WizardAction.ImportChildrenOfWarBenefit:
                        args.Result = ChildrenOfWarBenefitImportService.ProcessFile(View.FilePath, _reportProgressAction, int.Parse(View.BuildingId));
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