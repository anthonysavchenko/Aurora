using System;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, Building>
    {
        /// <summary>
        /// Обновляет все справочники
        /// </summary>
        protected override void RefreshRefBooks()
        {
            View.Streets = GetList<Street>();
            View.BankDetailsSource = GetList<BankDetail>();
        }

        #region Overrides of BaseItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(Building _domItem)
        {
            View.Street = _domItem.Street;
            View.Number = _domItem.Number;
            View.ZipCode = _domItem.ZipCode;
            View.FloorCount = _domItem.FloorCount;
            View.EntranceCount = _domItem.EntranceCount;
            View.Note = _domItem.Note;
            View.FiasID = _domItem.FiasID;
            View.NonResidentialPlaceArea = _domItem.NonResidentialPlaceArea;
            View.BankDetail = _domItem.BankDetail;

            if (_domItem.IsNew)
            {
                View.Area = 0;
                View.HeatedArea = 0;
                View.ResindentsCount = 0;
            }
            else
            {
                DateTime _period = ServerTime.GetPeriodInfo().FirstUncharged;
                int _id = int.Parse(_domItem.ID);
                using (Entities _entities = new Entities())
                {
                    View.Area = 
                        _entities.Customers
                            .Where(c => 
                                c.Buildings.ID == _id &&
                                _entities.CustomerPoses.Any(p => p.Customers.ID == c.ID && p.Till >= _period))
                            .Sum(c => (decimal?)c.Square) ?? 0;

                    View.HeatedArea =
                        _entities.Customers
                            .Where(c =>
                                c.Buildings.ID == _id &&
                                _entities.CustomerPoses.Any(p => p.Customers.ID == c.ID && p.Till >= _period))
                            .Sum(c => (decimal?)c.HeatedArea) ?? 0;

                    View.ResindentsCount =
                        _entities.Residents
                            .Count(r =>
                                r.Customers.Buildings.ID == _id &&
                                r.Customers.CustomerPoses.Any(p => p.Till >= _period));
                }
            }

            WorkItem.State[ModuleStateNames.COMMON_COUNTER] = null;

            ICounterValueView _counterValueView = (ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW];
            _counterValueView.NavigationButtonsEnabled = false;

            ((ICounterView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VIEW]).RefreshList();
            if (_domItem.CommonCounters.Count == 0)
            {
                _counterValueView.RefreshList();
            }

            ((IPublicPlaceView)WorkItem.SmartParts[ModuleViewNames.PUBLIC_PLACE_VIEW]).RefreshList();

        }

        /// <summary>
        /// Включить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        /// <remarks>Не реализована, может быть переписана в унаследованном классе</remarks>
        protected override void BindAdditionalViewsControls()
        {
            base.BindAdditionalViewsControls();
            ((ICounterView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VIEW]).BindActivate(OnAnyAttributeChangedEventHandler);
            ((ICounterValueView)WorkItem.SmartParts[ModuleViewNames.COUNTER_VALUE_VIEW]).BindActivate(OnAnyAttributeChangedEventHandler);
            ((IPublicPlaceView)WorkItem.SmartParts[ModuleViewNames.PUBLIC_PLACE_VIEW]).BindActivate(OnAnyAttributeChangedEventHandler);
        }

        /// <summary>
        /// Выключить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        protected override void UnbindAdditionalViewsControls()
        {
            base.UnbindAdditionalViewsControls();
            ((ICounterView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
            ((ICounterValueView)WorkItem.SmartParts.Get(ModuleViewNames.COUNTER_VALUE_VIEW)).BindDeactivate(OnAnyAttributeChangedEventHandler);
            ((IPublicPlaceView)WorkItem.SmartParts[ModuleViewNames.PUBLIC_PLACE_VIEW]).BindDeactivate(OnAnyAttributeChangedEventHandler);
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
            _domItem.Number = View.Number.Trim();
            _domItem.ZipCode = View.ZipCode.Trim();
            _domItem.FloorCount = View.FloorCount;
            _domItem.EntranceCount = View.EntranceCount;
            _domItem.Note = View.Note;
            _domItem.FiasID = View.FiasID;
            _domItem.NonResidentialPlaceArea = View.NonResidentialPlaceArea;
            _domItem.BankDetail = View.BankDetail;
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

            if (string.IsNullOrEmpty(_domItem.Number))
            {
                _error.AppendLine("- Не указан номер дома");
            }

            if (string.IsNullOrEmpty(_domItem.ZipCode))
            {
                _error.AppendLine("- Не указан почтовый индекс");
            }

            if (_domItem.BankDetail == null)
            {
                _error.AppendLine("- Не указаны банковские реквизиты");
            }

            if (_domItem.Street == null)
            {
                _error.AppendLine("- Не указана улица");
            }
            else
            {
                using (Entities _entities = new Entities())
                {
                    if (_entities.Buildings
                            .Any(
                                b =>
                                b.Streets.Name == _domItem.Street.Name &&
                                b.Number == _domItem.Number) &&
                        (string)WorkItem.State[Params.EditItemStateName] == CommonEditItemStates.New)
                    {
                        _error.AppendLine(" - Дом с указанным номером на указанной улице уже существует");
                    }
                }
            }

            if (!_domItem.IsNew)
            {
                int _buildingID = int.Parse(_domItem.ID);
                int _maxFloor;

                using (Entities _entities = new Entities())
                {
                    _maxFloor = _entities.Customers.Where(c => c.Buildings.ID == _buildingID).Max(c => c.Floor);
                }

                if (_domItem.FloorCount < _maxFloor)
                {
                    _error.AppendLine("- Количество этажей в доме меньше, чем этажи, указанные у некоторых абонентов этого дома");
                }
            }

            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion
    }
}