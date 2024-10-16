﻿using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Services;

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

        [ServiceDependency]
        public IGisZhkhChargesExportService GisZhkhChargesExportService { get; set; }

        [ServiceDependency]
        public ICounterValuesExportService CounterValuesExportService { get; set; }

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
                            View.ResetProgress();
                            _next = View.WizardAction == WizardAction.ExportChargesForGisZhkh
                                ? WizardPages.ServiceMatchingWizardPage
                                : WizardPages.ProcessingPage;
                        }
                        break;
                    case WizardPages.ServiceMatchingWizardPage:
                        _next = WizardPages.ProcessingPage;
                        break;
                    case WizardPages.ProcessingPage:
                        _next = WizardPages.FinishPage;
                        break;
                }
            }
            else
            {
                _next = prevPage == WizardPages.ServiceMatchingWizardPage
                    ? WizardPages.FilePage
                    : WizardPages.ChooseMethodPage;
            }

            return _next;
        }

        private bool ValidateFilePage()
        {
            WizardAction _action = View.WizardAction;

            StringBuilder _msg = new StringBuilder();
            if ((_action == WizardAction.ExportChargesForBanks
                || _action == WizardAction.ExportChargesForGisZhkh
                || _action == WizardAction.ExportCustomersForGisZhkh)
                && string.IsNullOrEmpty(View.OutputPath))
            {
                _msg.AppendLine("- Укажите путь к папке для экспорта данных");
            }

            if ((_action == WizardAction.ExportBenefitData
                || _action == WizardAction.ExportChargesForGisZhkh)
                && string.IsNullOrEmpty(View.TemplatePath))
            {
                _msg.AppendLine("- Выберите файл шаблона");
            }

            if ((_action == WizardAction.ExportChargesForBanks || _action == WizardAction.ExportCounterValues)
                && View.Period == DateTime.MinValue)
            {
                _msg.AppendLine("- Выберите период");
            }

            if (_action == WizardAction.ExportChargesForBanks
                && !View.SbrfChecked
                && !View.PrimSocBankChecked
                && !View.PochtaBankChecked)
            {
                _msg.AppendLine("- Укажите формат");
            }

            if (_action == WizardAction.ExportCounterValues 
                && string.IsNullOrEmpty(View.CollectFormPath) 
                && string.IsNullOrEmpty(View.Form2Path))
            {
                _msg.AppendLine("- Укажите путь к файлам с формой ДЭК");
            }

            bool _result = _msg.Length == 0;
            if (!_result)
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
                    case WizardPages.FilePage:
                        View.OutputPath = string.Empty;
                        View.TemplatePath = string.Empty;
                        View.CollectFormPath = string.Empty;
                        View.Form2Path = string.Empty;
                        View.Period = ServerTime.GetPeriodInfo().LastCharged;
                        View.StartPeriod = new DateTime(2015, 7, 1);
                        View.SbrfChecked = true;
                        View.PrimSocBankChecked = false;
                        View.PochtaBankChecked = false;
                        View.GisZhkhOnlyNew = false;
                        break;
                    case WizardPages.ServiceMatchingWizardPage:
                        View.ServiceMatchingTableProgressBarVisible = true;
                        Application.DoEvents();

                        FillServiceMatchingTable();

                        View.ServiceMatchingTableProgressBarVisible = false;
                        Application.DoEvents();
                        break;
                    case WizardPages.ProcessingPage:
                        BackgroundWorker _worker = CreateBackgroundWorker();
                        _worker.RunWorkerAsync();
                        break;
                }
            }
        }

        private void FillServiceMatchingTable()
        {
            View.ClearServiceMatchingTable();

            List<string> _gisZhkhServices = GisZhkhChargesExportService.GetGisZhkhServices(View.TemplatePath);
            _gisZhkhServices.Insert(0, string.Empty);
            Dictionary<int, string> _services = GisZhkhChargesExportService.GetServices(View.Period);

            int _tabIndex = 1;
            foreach (KeyValuePair<int, string> _pair in _services)
            {
                string _selectedValue = _gisZhkhServices
                    .Where(s => s.ToLower().Replace(" ", "") == _pair.Value.ToLower().Replace(" ", ""))
                    .FirstOrDefault();

                View.AddRowToServiceMatchingTable(
                    _pair.Key,
                    _pair.Value,
                    _gisZhkhServices,
                    _selectedValue,
                    _tabIndex++);
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
                try
                {
                    switch (View.WizardAction)
                    {
                        case WizardAction.ExportBenefitData:
                            args.Result = BenefitExportService.Export(View.OutputPath, View.TemplatePath, View.StartPeriod, ((BackgroundWorker)sender).ReportProgress);
                            break;
                        case WizardAction.ExportChargesForBanks:
                            args.Result = ChargeExportService.Export(
                                View.OutputPath,
                                View.Period,
                                ServerTime.GetDateTimeInfo().Now,
                                GetChargeExportFormatList(),
                                ((BackgroundWorker)sender).ReportProgress);
                            break;
                        case WizardAction.ExportCustomersForGisZhkh:
                            args.Result = GisZhkhCustomerExportService.Export(View.OutputPath, View.GisZhkhOnlyNew, ((BackgroundWorker)sender).ReportProgress);
                            break;
                        case WizardAction.ExportChargesForGisZhkh:
                            args.Result = GisZhkhChargesExportService.Export(View.OutputPath, View.TemplatePath, View.Period, View.GetServiceMatchingDict(), ((BackgroundWorker)sender).ReportProgress);
                            break;
                        case WizardAction.ExportCounterValues:
                            args.Result = CounterValuesExportService.Export(View.CollectFormPath, View.Form2Path, View.Period, ((BackgroundWorker)sender).ReportProgress);
                            break;
                    }
                }
                catch (Exception _ex)
                {
                    Logger.SimpleWrite($"Ошибка экспорта: {_ex}");
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

            if (View.PochtaBankChecked)
            {
                _types.Add(ChargeExportFormatType.Pochtabank);
            }

            return _types;
        }
    }
}