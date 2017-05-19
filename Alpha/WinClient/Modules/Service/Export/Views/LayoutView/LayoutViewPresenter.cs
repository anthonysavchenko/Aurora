using System;
using System.Text;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using System.Collections.Generic;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        [ServiceDependency]
        public IChargeExportService ChargeExportService { get; set; }

        [ServiceDependency]
        public IBenefitExportService BenefitExportService { get; set; }

        [ServiceDependency]
        public IGisZhkhCustomerExportService GisZhkhCustomerExportService { get; set; }
        
        /// <summary>
        /// Обрабатывает отображение вью
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
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
                switch (prevPage)
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
            StringBuilder _msg = new StringBuilder();
            if(string.IsNullOrEmpty(View.OutputPath))
            {
                _msg.AppendLine("- Укажите путь к папке для экспорта данных");
            }

            WizardAction _action = View.WizardAction;

            if((_action == WizardAction.ExportBenefitData 
                || _action == WizardAction.ExportChargesForGisZhkh 
                || _action == WizardAction.ExportCustomersForGisZhkh) 
               && string.IsNullOrEmpty(View.TemplatePath))
            {
                _msg.AppendLine("- Выберите файл шаблона");
            }

            if(_action == WizardAction.ExportChargesForBanks)
            {
                if(View.Period == DateTime.MinValue)
                {
                    _msg.AppendLine("- Выберите период");
                }

                if(!View.SbrfChecked && !View.PrimSocBankChecked)
                {
                    _msg.AppendLine("- Укажите формат");
                }
            }

            bool _result = _msg.Length == 0;
            if(!_result)
            {
                View.ShowMessage(_msg.ToString(), "Исправьте указанные ошибки");
            }

            return _result;
        }

        public void OnSelectedPageChanged(WizardPages page, bool forwardDirection)
        {
            if (forwardDirection)
            {
                switch (page)
                {
                    case WizardPages.ChooseMethodPage:
                        View.OutputPath = string.Empty;
                        View.TemplatePath = string.Empty;
                        View.Period = ServerTime.GetPeriodInfo().LastCharged;
                        View.StartPeriod = new DateTime(2015, 7, 1);
                        View.SbrfChecked = true;
                        View.PrimSocBankChecked = false;
                        View.GisZhkhOnlyNew = false;
                        break;
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
                switch (View.WizardAction)
                {
                    case WizardAction.ExportBenefitData:
                        args.Result = BenefitExportService.Export(View.OutputPath, View.TemplatePath, View.StartPeriod, ((BackgroundWorker)sender).ReportProgress);
                        break;
                    case WizardAction.ExportChargesForBanks:
                        args.Result = ChargeExportService.Export(View.OutputPath, View.Period, GetChargeExportFormatList(), ((BackgroundWorker)sender).ReportProgress);
                        break;
                    case WizardAction.ExportCustomersForGisZhkh:
                        args.Result = GisZhkhCustomerExportService.Export(View.OutputPath, View.TemplatePath, View.GisZhkhOnlyNew, ((BackgroundWorker)sender).ReportProgress);
                        break;
                }
            };

            _worker.RunWorkerCompleted += (sender, args) =>
            {
                View.ResultText = ((ExportResult)args.Result).Info;
                View.SelectPage(WizardPages.FinishPage);
            };
            return _worker;
        }

        private IEnumerable<ChargeExportFormatType> GetChargeExportFormatList()
        {
            List<ChargeExportFormatType> _types = new List<ChargeExportFormatType>();
            if (View.SbrfChecked)
            {
                _types.Add(ChargeExportFormatType.Sberbank);
            }
            if (View.PrimSocBankChecked)
            {
                _types.Add(ChargeExportFormatType.Primsocbank);
            }

            return _types;
        }
    }
}