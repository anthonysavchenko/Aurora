using DevExpress.XtraWizard;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using DomBuilding = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomStreet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Street;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class WizardViewPresenter : BaseDomainPresenter<IWizardView>
    {
        private DateTime _suggestCollectDate;
        private int _processedCount;
        private int _errorsCount;

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        public void StartWizard()
        {
            View.IsMasterCompleted = false;
            View.IsMasterInProgress = false;

            View.Streets = GetList<DomStreet>();
            View.StreetId = string.Empty;
            View.BuildingId = string.Empty;
            View.Period = ServerTime.GetPeriodInfo().FirstUncharged;
            _suggestCollectDate = ServerTime.GetDateTimeInfo().Now.Date;

            View.ResultCount = 0;
            View.ResultErrorCount = 0;
            View.ResetProgressBar(1);

            View.SelectPage(WizardSteps.BuildingSelectPage);
        }

        /// <summary>
        /// Завершает работу мастера
        /// </summary>
        public void FinishWizard()
        {
            ITabbedView _tabbed = ((ITabbedView)WorkItem.SmartParts.Get(ModuleViewNames.TABBED_VIEW));
            _tabbed.SelectTab("tabList");
        }

        /// <summary>
        /// Обрабатывает изменение шага мастера
        /// </summary>
        /// <param name="prevPage">Предыдущая страница</param>
        /// <param name="page">Открываемая страница</param>
        /// <param name="direction">Назад / Далее</param>
        /// <returns>Следующая страница мастера</returns>
        public WizardSteps OnSelectedPageChanging(WizardSteps prevPage, WizardSteps page, Direction direction)
        {
            WizardSteps _next = WizardSteps.Unknown;

            if (direction == Direction.Forward)
            {
                switch (prevPage)
                {
                    case WizardSteps.BuildingSelectPage:
                        if (ValidateBuildingSelectPage(out string message))
                        {
                            FillDataGrid();
                            _next = WizardSteps.CollectDataPage;
                        }
                        else
                        {
                            View.ShowMessage(message, "Ошибка");
                        }
                        break;

                    case WizardSteps.CollectDataPage:
                        _next = WizardSteps.ProcessingPage;
                        break;

                    case WizardSteps.ProcessingPage:
                        _next = WizardSteps.FinishPage;
                        break;
                }
            }
            else
            {
                _next = prevPage == WizardSteps.FinishPage ? WizardSteps.BuildingSelectPage : page;
            }

            return _next;
        }

        /// <summary>
        /// Обрабатывает событие перехода на новую страницу
        /// </summary>
        /// <param name="page">Страница, на которую был осуществлен переход</param>
        /// <param name="prevPage">Страница предыдущего состояния</param>
        /// <param name="direction">Назад / Далее</param>
        public void OnSelectedPageChanged(WizardSteps prevPage, WizardSteps page, Direction direction)
        {
            if (direction == Direction.Forward)
            {
                switch (page)
                {
                    case WizardSteps.CollectDataPage:
                        View.ShowEditor();
                        break;

                    case WizardSteps.ProcessingPage:
                        Thread _thread = new Thread(Save);
                        _thread.Start();
                        break;
                }
            }
        }

        public void FillBuildingList()
        {
            View.Buildings = DataMapper<DomBuilding, IBuildingDataMapper>().GetBuildingsOnStreet(GetItem<DomStreet>(View.StreetId));
        }

        private bool ValidateBuildingSelectPage(out string message)
        {
            bool _valid = !string.IsNullOrEmpty(View.BuildingId);
            message = _valid ? string.Empty : "Не выбран дом";
            return _valid;
        }

        /// <summary>
        /// Сохраняет введенные данные
        /// </summary>
        private void Save()
        {
            View.IsMasterInProgress = true;
            int _customerID = int.Parse(UserHolder.User.ID);

            _processedCount = _errorsCount = 0;

            foreach (DataRow _row in View.Items.Rows)
            {
                decimal _counterValue = _row[WizardTableColumnNames.VALUE] != DBNull.Value
                    ? _row.Field<decimal>(WizardTableColumnNames.VALUE)
                    : 0;

                DateTime _collectDateTime = _row[WizardTableColumnNames.COLLECT_DATE] != DBNull.Value
                    ? _row.Field<DateTime>(WizardTableColumnNames.COLLECT_DATE)
                    : DateTime.MinValue;

                DateTime _period = View.Period;

                if (_counterValue <= 0 || _collectDateTime == DateTime.MinValue)
                {
                    continue;
                }

                using (var _db = new Entities())
                {
                    try
                    {
                        int _counterId = _row.Field<int>(WizardTableColumnNames.ID);

                        PrivateCounterValues _value = _db.PrivateCounterValues
                            .FirstOrDefault(x => x.Period == _period && x.PrivateCounters.ID == _counterId);

                        if (_value == null)
                        {
                            _value =
                                new PrivateCounterValues
                                {
                                    Period = _period,
                                    PrivateCounters = _db.PrivateCounters.First(x => x.ID == _counterId)
                                };
                            _db.PrivateCounterValues.AddObject(_value);
                        }

                        _value.CollectDate = _collectDateTime;
                        _value.Value = _counterValue;
                        _db.SaveChanges();
                        _processedCount++;
                    }
                    catch(Exception ex)
                    {
                        Logger.SimpleWrite(
                            $"Ошибка при сохранении показния ПУ(ID: {_row[WizardTableColumnNames.ID]}, Value{_counterValue}, CollectDate: {_collectDateTime}, Period: {_period}): {ex}");
                        _errorsCount++;
                    }
                }
            }

            View.ResultCount = _processedCount;
            View.ResultErrorCount = _errorsCount;
            View.IsMasterInProgress = false;
            View.IsMasterCompleted = true;

            View.SelectPage(WizardSteps.FinishPage);
        }

        /// <summary>
        /// Заполняет таблицу введенными данными
        /// </summary>
        private void FillDataGrid()
        {
            using (var _db = new Entities())
            {
                View.Items = _db.GetCounters(int.Parse(View.BuildingId), View.Period);
            }
        }

        public void SetSuggestedValue(string focusedCellFieldName, DataRow currentRow)
        {
            switch (focusedCellFieldName)
            {
                case WizardTableColumnNames.VALUE:
                    SetCounterValue(currentRow);
                    break;
                case WizardTableColumnNames.COLLECT_DATE:
                    if (currentRow[WizardTableColumnNames.COLLECT_DATE] == DBNull.Value)
                    {
                        View.CollectDateTime = _suggestCollectDate;
                    }
                    break;
            }
        }

        private void SetCounterValue(DataRow currentRow)
        {
            const decimal MONTH_NORM = 200;

            decimal _value = currentRow[WizardTableColumnNames.VALUE] != DBNull.Value 
                ? currentRow.Field<decimal>(WizardTableColumnNames.VALUE)
                : 0;

            if (_value <= 0)
            {
                decimal _lastValue = currentRow["PrevValue"] != DBNull.Value ? currentRow.Field<decimal>("PrevValue") : 0;

                if (_lastValue > 0)
                {
                    DateTime _currentPeriod = View.Period;
                    DateTime _prevPeriod = currentRow.Field<DateTime>("PrevPeriod");
                    int _mounthCount = (int)((_currentPeriod - _prevPeriod).TotalDays / 30);
                    _value = _mounthCount * MONTH_NORM + _lastValue;
                }
                else
                {
                    _value = MONTH_NORM;
                }
                View.CounterValue = _value;
            }
        }

        public void ValidateRow(DataRow dataRow)
        {
            decimal _value = dataRow[WizardTableColumnNames.VALUE] != DBNull.Value
                ? dataRow.Field<decimal>(WizardTableColumnNames.VALUE)
                : 0;
            decimal _prevValue = dataRow[WizardTableColumnNames.PREV_VALUE] != DBNull.Value
                ? dataRow.Field<decimal>(WizardTableColumnNames.PREV_VALUE)
                : 0;

            string _errorMessage = _value == 0 || _value >= _prevValue
                ? string.Empty
                : "- Введенное показание меньше, чем показание за прошлый период";

            DateTime _collectDate = dataRow[WizardTableColumnNames.COLLECT_DATE] != DBNull.Value
                ? dataRow.Field<DateTime>(WizardTableColumnNames.COLLECT_DATE)
                : DateTime.MinValue;
            DateTime _prevCollectDate = dataRow[WizardTableColumnNames.PREV_COLLECT_DATE] != DBNull.Value
                ? dataRow.Field<DateTime>(WizardTableColumnNames.PREV_COLLECT_DATE)
                : DateTime.MinValue;

            if (_collectDate == DateTime.MinValue || _collectDate < _prevCollectDate)
            {
                _errorMessage = $"{_errorMessage}\r\n- Дата сбора показаний меньше, чем за прошлый период";
            }

            dataRow[WizardTableColumnNames.ERROR_MESSAGE] = _errorMessage;
        }
    }
}