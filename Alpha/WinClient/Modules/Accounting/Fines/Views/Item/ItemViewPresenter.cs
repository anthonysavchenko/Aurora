using Microsoft.Practices.CompositeUI;
using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, FineDoc>
    {

        /// <summary>
        /// Единица работы
        /// </summary>
        [ServiceDependency]
        public IUnitOfWork UnitOfWork { get; set; }

        #region Overrides of BaseMainItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(FineDoc _domItem)
        {
            View.Period = _domItem.Period;
            ((IBaseSimpleListView)WorkItem.SmartParts.Get(ModuleViewNames.FINE_POS_LIST_VIEW)).RefreshList();
        }

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void FillDomainFromAllViews(FineDoc _domItem)
        {
            _domItem.Period = View.Period;
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(FineDoc _domItem, out string _errorMessage)
        {
            _errorMessage = _domItem.Period == DateTime.MinValue
                ? " Не указан период"
                : string.Empty;

            return _errorMessage.Length == 0;
        }

        protected override void BindAdditionalViewsControls()
        {
            base.BindAdditionalViewsControls();
            ((IPosListView)WorkItem.SmartParts.Get(ModuleViewNames.FINE_POS_LIST_VIEW)).BindActivate(OnAnyAttributeChangedEventHandler);
        }

        protected override bool AddOrUpdateItem(FineDoc _domItem, UpdateMode _updateMode)
        {
            return base.AddOrUpdateItem(_domItem, _updateMode) && UnitOfWork.commit();
        }

        #endregion
    }
}