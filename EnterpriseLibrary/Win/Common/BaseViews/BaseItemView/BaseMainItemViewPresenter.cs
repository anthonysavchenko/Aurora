using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    public abstract class BaseMainItemViewPresenter<TView, TDomItem> : BaseItemViewPresenter<TView, TDomItem>
        where TDomItem : DomainObject
        where TView : IBaseItemView
    {
        /// <summary>
        /// Конструктор с дефолтными параметрами работы BaseMainItemView
        /// </summary>
        public BaseMainItemViewPresenter()
        {
        }

        /// <summary>
        /// Конструктор со специфичными параметрами работы BaseMainItemView
        /// </summary>
        /// <param name="_params">Параметры работы BaseMainItemTabbedView</param>
        public BaseMainItemViewPresenter(BaseItemViewParams _params)
            : base(_params)
        {
        }

        /// <summary>
        /// Режим изменения элемента в БД
        /// </summary>
        protected enum UpdateMode
        {
            /// <summary>
            /// Создание нового элемента
            /// </summary>
            UpdateModeNew,

            /// <summary>
            /// Изменение существующего элемента
            /// </summary>
            UpdateModeEdit
        }

        /// <summary>
        /// Подписка на глобальную команду "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.SaveItemFired, ThreadOption.UserInterface)]
        public void OnSaveItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (this.WorkItem.Status == WorkItemStatus.Inactive) return;

            OnSaveItem();
        }


        #region Для наследников

        /// <summary>
        /// Производит сохранение элемента в БД
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_updateMode">Режим изменения элемента</param>
        /// <returns>Признак успешности изменения</returns>
        protected virtual bool AddOrUpdateItem(TDomItem _domItem, UpdateMode _updateMode)
        {
            return UpdateItem(_domItem);
        }

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected abstract void FillDomainFromAllViews(TDomItem _domItem);

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected virtual bool CheckPreSaveConditions(TDomItem _domItem, out string _errorMessage)
        {
            _errorMessage = "";

            return true;
        }

        /// <summary>
        /// Выполняет действия при неуспешности сохранения
        /// </summary>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        protected virtual void OnSaveFailed(out string _errorMessage)
        {
            _errorMessage = "Произошла ошибка при сохранении.";
        }

        /// <summary>
        /// Выполняет действия для сохранения элемента
        /// </summary>
        protected virtual void OnSaveItem()
        {
            TDomItem _di = (TDomItem)this.WorkItem.State[Params.CurrentItemStateName];

            // Преобразовать данные всех "видов (Views)" в атрибуты объекта домена
            FillDomainFromAllViews(_di);

            // Если текущий элемент претерпел изменения
            if ((string)this.WorkItem.State[CommonStateNames.ItemState] == CommonItemStates.Modified)
            {
                string _errorMessage;
                bool _isSaveAllowed = CheckPreSaveConditions(_di, out _errorMessage);
                if (!_isSaveAllowed)
                {
                    MessageBox.Show(_errorMessage);
                    return;
                }

                UpdateMode _updateMode =
                    ((string)this.WorkItem.State[Params.EditItemStateName] == CommonEditItemStates.Edit) ?
                        UpdateMode.UpdateModeEdit : UpdateMode.UpdateModeNew;

                // Сохранить элемент в базе данных
                if (AddOrUpdateItem(_di, _updateMode))
                {
                    // Обновляем id текущего элемента и полностью переподнимаем сам элемент (в частности, для обновления ключей внутренних словарей)
                    this.WorkItem.State[Params.CurrentItemIdStateName] = _di.ID;
                    this.WorkItem.State[Params.CurrentItemStateName] = GetItem<TDomItem>(_di.ID);

                    // Изменить текущее состояние на "не изменён"
                    this.WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.NotChanged;

                    // Изменить текущий режим обработки на редактирование
                    this.WorkItem.State[Params.EditItemStateName] = CommonEditItemStates.Edit;

                    // Отмечаем успешное сохранение
                    WorkItem.State[CommonStateNames.IsSaveSucceeded] = true;

                    OnItemChanged(EventArgs.Empty);
                }
                else
                {
                    string _errorMessageOnSaveFailed;
                    OnSaveFailed(out _errorMessageOnSaveFailed);
                    MessageBox.Show(_errorMessageOnSaveFailed);
                }
            }
        }

        #endregion

        #region Публикация событий

        /// <summary>
        /// Измененный элемент успешно сохранен
        /// </summary>
        [EventPublication(CommonEventNames.ItemChanged, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs> ItemChanged;

        /// <summary>
        /// Измененный элемент успешно сохранен
        /// </summary>
        /// <param name="eventArgs"></param>
        public virtual void OnItemChanged(EventArgs eventArgs)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, eventArgs);
            }
        }

        #endregion
    }
}