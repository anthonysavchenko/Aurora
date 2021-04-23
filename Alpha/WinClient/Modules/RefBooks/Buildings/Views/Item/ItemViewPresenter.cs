using System;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, Building>
    {
        #region Overrides of BaseItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(Building _domItem)
        {
            View.Street = _domItem.Street;

            if (!string.IsNullOrEmpty(_domItem.Number)
                && _domItem.Number.Contains(", корп. "))
            {
                var substrings = _domItem.Number.Split(new string[] { ", корп. " }, StringSplitOptions.None);

                View.BuildingNumber = substrings[0];
                View.BuildingPartNumber = substrings[1];
            }
            else
            {
                View.BuildingNumber = _domItem.Number;
                View.BuildingPartNumber = string.Empty;
            }

            if (_domItem.IsNew)
            {
                View.RouteFormLastMonth = "Нет данных";
                View.CustomersCount = 0;
                View.CountersCount = 0;
                View.CalculationFormLastMonth = "Нет данных";
                View.BuildingContract = string.Empty;
            }
            else
            {
                var buildingID = int.Parse(_domItem.ID);
                var routeFormLastMonth = GetRouteFormLastMonth(buildingID);
                var calculationFormLastMonth = GetCalculationFormLastMonth(buildingID);

                if (routeFormLastMonth <= DateTime.MinValue)
                {
                    View.RouteFormLastMonth = "Нет данных";
                    View.CustomersCount = 0;
                    View.CountersCount = 0;
                }
                else
                {
                    View.RouteFormLastMonth = routeFormLastMonth.ToString("MM.yyyy");
                    View.CustomersCount = CountCustomers(buildingID, routeFormLastMonth);
                    View.CountersCount = CountCounters(buildingID, routeFormLastMonth);
                }

                if (calculationFormLastMonth <= DateTime.MinValue)
                {
                    View.CalculationFormLastMonth = "Нет данных";
                    View.BuildingContract = string.Empty;
                }
                else
                {
                    View.CalculationFormLastMonth = calculationFormLastMonth.ToString("MM.yyyy");

                    byte contract = GetContract(buildingID, calculationFormLastMonth);

                    View.BuildingContract =
                        (BuildingContract)contract == BuildingContract.Contract6784
                            ? BuildingContractNames.CONTRACT_6784
                            : (BuildingContract)contract == BuildingContract.Contract15297
                                ? BuildingContractNames.CONTRACT_15297
                                : string.Empty;
                }
            }

            View.NormCoefficient = _domItem.NormCoefficient;
            View.CollectiveSquare = _domItem.CollectiveSquare;
            View.IsArchived = _domItem.IsArchived;
            View.Note = _domItem.Note;

            ((ICounterView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VIEW]).RefreshList();
        }

        private DateTime GetRouteFormLastMonth(int buildingID)
        {
            using (var db = new Entities())
            {
                var lastMonth =
                    db.RouteFormValues
                        .Where(v => v.PrivateCounters.Customers.Buildings.ID == buildingID)
                        .Select(v => v.Month)
                        .DefaultIfEmpty(DateTime.MinValue)
                        .Max();

                return lastMonth;
            }
        }

        private DateTime GetCalculationFormLastMonth(int buildingID)
        {
            using (var db = new Entities())
            {
                var lastMonth =
                    db.BuildingCalculationValues
                        .Where(v => v.Buildings.ID == buildingID)
                        .Select(v => v.Month)
                        .DefaultIfEmpty(DateTime.MinValue)
                        .Max();

                return lastMonth;
            }
        }

        private int CountCustomers(int buildingID, DateTime lastMonth)
        {
            using (var db = new Entities())
            {
                var customersCount =
                    db.RouteFormValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == lastMonth)
                        .GroupBy(v => v.PrivateCounters.Customers.ID)
                        .Count();

                return customersCount;
            }
        }

        private int CountCounters(int buildingID, DateTime lastMonth)
        {
            using (var db = new Entities())
            {
                var countersCount =
                    db.RouteFormValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == lastMonth
                            && (PrivateCounterType)v.PrivateCounters.CounterType != PrivateCounterType.Norm)
                        .GroupBy(v => v.PrivateCounters.ID)
                        .Count();

                return countersCount;
            }
        }

        private byte GetContract(int buildingID, DateTime lastMonth)
        {
            using (var db = new Entities())
            {
                var contract =
                    db.BuildingCalculationValues
                        .FirstOrDefault(v =>
                            v.Buildings.ID == buildingID
                            && v.Month == lastMonth)
                        .Contract;

                return contract;
            }
        }

        /// <summary>
        /// Включить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        /// <remarks>Не реализована, может быть переписана в унаследованном классе</remarks>
        protected override void BindAdditionalViewsControls()
        {
            base.BindAdditionalViewsControls();
            ((ICounterView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VIEW])
                .BindActivate(OnAnyAttributeChangedEventHandler);
        }

        /// <summary>
        /// Выключить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        protected override void UnbindAdditionalViewsControls()
        {
            base.UnbindAdditionalViewsControls();
            ((ICounterView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VIEW))
                .BindDeactivate(OnAnyAttributeChangedEventHandler);
        }

        #endregion

        #region Overrides of BaseMainItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_updateMode">Режим изменения элемента</param>
        /// <returns>Признак успешности изменения</returns>
        protected override bool AddOrUpdateItem(Building _domItem, UpdateMode _updateMode)
        {
            UpdateItem(_domItem);
            return WorkItem.Services.Get<IUnitOfWork>().commit();
        }

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void FillDomainFromAllViews(Building _domItem)
        {
            _domItem.Street = View.Street;

            string building = View.BuildingNumber.Trim();
            string buildingPart = View.BuildingPartNumber.Trim();

            _domItem.Number =
                !string.IsNullOrEmpty(building) && !string.IsNullOrEmpty(buildingPart)
                    ? building + ", корп. " + buildingPart
                    : !string.IsNullOrEmpty(building)
                        ? building
                        : null;

            _domItem.NormCoefficient = View.NormCoefficient;
            _domItem.CollectiveSquare = View.CollectiveSquare;
            _domItem.IsArchived = View.IsArchived;
            _domItem.Note = View.Note;
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(Building _domItem, out string _errorMessage)
        {
            StringBuilder _error = new StringBuilder();

            if (string.IsNullOrEmpty(_domItem.Street))
            {
                _error.AppendLine("- Не указана улица");
            }

            if (string.IsNullOrEmpty(_domItem.Number))
            {
                _error.AppendLine("- Не указан номер дома");
            }

            if (!string.IsNullOrEmpty(_domItem.Number)
                && _domItem.Number.Count(c => c == ',') > 1)
            {
                _error.AppendLine("- При указании номера дома и корпуса нельзя использовать запятые");
            }

            if (!string.IsNullOrEmpty(_domItem.Number)
                && !string.IsNullOrEmpty(_domItem.Street)
                && (string)WorkItem.State[Params.EditItemStateName] == CommonEditItemStates.New)
            {
                using (Entities db = new Entities())
                {
                    if (db.Buildings
                            .Count(b =>
                                b.Street.Equals(_domItem.Street, StringComparison.OrdinalIgnoreCase)
                                && b.Number.Equals(_domItem.Number, StringComparison.OrdinalIgnoreCase)) > 0)
                    {
                        _error.AppendLine("- Дом с указанным адресом уже был создан ранее");
                    }
                }
            }
            else if (!string.IsNullOrEmpty(_domItem.Number)
                && !string.IsNullOrEmpty(_domItem.Street)
                && (string)WorkItem.State[Params.EditItemStateName] == CommonEditItemStates.Edit)
            {
                using (Entities db = new Entities())
                {
                    if (db.Buildings
                            .Count(b =>
                                b.Street.Equals(_domItem.Street, StringComparison.OrdinalIgnoreCase)
                                && b.Number.Equals(_domItem.Number, StringComparison.OrdinalIgnoreCase)
                                && b.ID.ToString() != _domItem.ID) > 0)
                    {
                        _error.AppendLine("- Дом с указанным адресом уже был создан ранее");
                    }
                }
            }

            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion
    }
}
