using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    public abstract class BaseItemViewPresenter<TView, TDomItem> : BaseDomainPresenter<TView>, IBaseItemViewPresenter
        where TDomItem : DomainObject
        where TView : IBaseItemView
    {
        /// <summary>
        /// Конструктор с дефолтными параметрами работы BaseItemView
        /// </summary>
        public BaseItemViewPresenter()
        {
            Params = BaseItemViewConstants.BaseItemViewDefaultParams;
        }

        /// <summary>
        /// Конструктор со специфичными параметрами работы BaseItemView
        /// </summary>
        /// <param name="_params">Параметры работы BaseItemTabbedView</param>
        public BaseItemViewPresenter(BaseItemViewParams _params)
        {
            Params = _params;
        }

        /// <summary>
        /// Параметры работы BaseItemView
        /// </summary>
        protected readonly BaseItemViewParams Params;

        #region IBaseItemViewPresenter members

        /// <summary>
        /// Обработка загрузки вида.
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            // Обновляем справочники вида
            RefreshRefBooks();
        }

        /// <summary>
        /// Подписка на глобальную команду "Обновить справочники".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.RefreshRefBooksFired, ThreadOption.UserInterface)]
        public void OnRefreshRefBooksFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (this.WorkItem.Status == WorkItemStatus.Inactive) return;

            // Обновляем списки всех справочников на форме.
            RefreshRefBooks();
        }

        /// <summary>
        /// Отключить общий обработчик изменений
        /// </summary>
        public void UnBindChangeHandlers(Control.ControlCollection _coll)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().UnBind(_coll, OnAnyAttributeChangedEventHandler);
            UnbindAdditionalViewsControls();
        }

        /// <summary>
        /// Подключить общий обработчик изменений
        /// </summary>
        public void BindChangeHandlers(Control.ControlCollection _coll)
        {
            WorkItem.RootWorkItem.Services.Get<IChangeEventHandlerService>().Bind(_coll, OnAnyAttributeChangedEventHandler);
            BindAdditionalViewsControls();
        }

        /// <summary>
        /// Отобразить домен текущего элемента списка на виде
        /// </summary>
        public void ShowDomainToView()
        {
            ShowDomainOnAllViews((TDomItem)WorkItem.State[Params.CurrentItemStateName]);
        }

        #endregion

        #region Для наследников

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected abstract void ShowDomainOnAllViews(TDomItem _domItem);

        /// <summary>
        /// Выполняет действия при изменении значения какого-либо контрола на виде
        /// </summary>
        protected virtual void OnAnyAttributeChanged()
        {
            // Если статус документа - модифицирован, то выходим.
            //if ((string)WorkItem.State[CommonStateNames.ItemState] == CommonItemStates.Modified) return;

            // Изменить состояние.
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.Modified;
            WorkItem.Controller.MainViewTitle = String.Format(
                "{0} {1} {2}",
                WorkItem.Controller.DefaultMainViewTitle,
                ((IBaseListView)WorkItem.SmartParts.Get("ListView")).GetCurrentItemShortName(),
                "(изменён)");
        }

        /// <summary>
        /// Включить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        /// <remarks>Не реализована, может быть переписана в унаследованном классе</remarks>
        protected virtual void BindAdditionalViewsControls()
        {
            /* Не реализована */
        }

        /// <summary>
        /// Выключить отслеживание изменений элементов управления на дополнительных вью
        /// </summary>
        /// <remarks>Не реализована, может быть переписана в унаследованном классе</remarks>
        protected virtual void UnbindAdditionalViewsControls()
        {
            /* Не реализована */
        }

        #endregion

        #region Private members

        /// <summary>
        /// Обработчик события изменения значения любого атрибута вида
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnAnyAttributeChangedEventHandler(object sender, EventArgs e)
        {
            OnAnyAttributeChanged();
        }

        /// <summary>
        /// Обновляет все справочники
        /// </summary>
        protected virtual void RefreshRefBooks()
        {
        }
        #endregion
    }
}