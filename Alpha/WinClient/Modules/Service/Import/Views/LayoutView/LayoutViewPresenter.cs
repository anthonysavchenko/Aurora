using System.ComponentModel;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        private readonly IImportService _gisZhkhCustomersImportService = new GisZhkhCustomersImportService();
        private readonly IImportService _newCustomersImportService = new NewCustomersImportService();
        private readonly IImportService _customerPosesImportService = new CustomerPosesImportService();
       
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

        private BackgroundWorker CreateBackgroundWorker()
        {
            BackgroundWorker _worker = new BackgroundWorker { WorkerReportsProgress = true };
            _worker.ProgressChanged += (sender, args) =>
            {
                View.SetProgress(args.ProgressPercentage);
            };

            _worker.DoWork += (sender, args) =>
            {
                switch(View.WizardAction)
                {
                    case WizardAction.ImportNewCustomers:
                        args.Result = _newCustomersImportService.ProcessFile(View.FilePath, ((BackgroundWorker)sender).ReportProgress);
                        break;
                    case WizardAction.ImportCustomerPoses:
                        args.Result = _customerPosesImportService.ProcessFile(View.FilePath, ((BackgroundWorker)sender).ReportProgress);
                        break;
                    case WizardAction.ImportGisZhkhCustomerIDs:
                        args.Result = _gisZhkhCustomersImportService.ProcessFile(View.FilePath, ((BackgroundWorker)sender).ReportProgress);
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