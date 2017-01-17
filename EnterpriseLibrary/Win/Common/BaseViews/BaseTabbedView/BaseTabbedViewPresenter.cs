using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView
{
    public class BaseTabbedViewPresenter<TView, TDomItem> : BaseDomainPresenter<TView>, IBaseTabbedViewPresenter
        where TDomItem : DomainObject, new()
        where TView : IBaseTabbedView
    {
        /// <summary>
        /// Конструктор с дефолтными параметрами работы BaseTabbedView
        /// </summary>
        [InjectionConstructor]
        public BaseTabbedViewPresenter()
        {
            Params = BaseTabbedViewConstants.BaseTabbedViewDefaultParams;
        }

        /// <summary>
        /// Конструктор со специфичными параметрами работы BaseTabbedView
        /// </summary>
        /// <param name="_params">Параметры работы BaseTabbedView</param>
        public BaseTabbedViewPresenter(BaseTabbedViewParams _params)
        {
            Params = _params;
        }

        /// <summary>
        /// Параметры работы BaseTabbedView
        /// </summary>
        protected readonly BaseTabbedViewParams Params;

        /// <summary>
        /// Название вкладки для отображения списка элементов
        /// </summary>
        protected const string LIST_TAB_PAGE_NAME = "tabList";

        /// <summary>
        /// Название вкладки для отображения деталей элемента
        /// </summary>
        protected const string DETAIL_TAB_PAGE_NAME = "tabDetail";

        /// <summary>
        /// Название вкладки для отображения истории изменения элементов
        /// </summary>
        protected const string HISTORY_TAB_PAGE_NAME = "tabHistory";

        /// <summary>
        /// ID элемента списка, выбранного перед началом процесса создания нового элемента
        /// </summary>
        private string SelectedItemIdBeforeNewItemCreation;

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();

            // При начальном открытии юзкейса всегда начинать с закладки "Список элементов"
            View.SelectTab(LIST_TAB_PAGE_NAME);
        }

        #region Подписка на глобальные события

        /// <summary>
        /// Обработчик глобального события "Создать новый"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.CreateNewItemFired, ThreadOption.UserInterface)]
        public void OnNewItemRun(object sender, EventArgs eventArgs)
        {
            // Отказаться от выполнения если юзкейс не активен
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            OnCreateNewItem();
        }

        /// <summary>
        /// Подписчик на событие "Изменён элемент списка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        [EventSubscription(CommonEventNames.ItemChanged, ThreadOption.UserInterface)]
        public void OnItemChangedFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            OnItemChanged();
        }

        /// <summary>
        /// Обработчик события, возникающего после отображения формы окна юзкейса
        /// </summary>
        /// <param name="sender">Контроллер модуля</param>
        /// <param name="e">Стартовые параметры</param>
        [EventSubscription(CommonEventNames.ON_MAIN_VIEW_SHOWN, ThreadOption.UserInterface)]
        public void OnMainViewShownEventHandler(object sender, EventArgs<AnyStartUpParams> e)
        {
            if (WorkItem.Status == WorkItemStatus.Inactive)
                return;

            OnMainViewShown(e.Data);
        }

        #endregion

        #region IBaseTabbedViewPresenter members

        /// <summary>
        /// Выполняет действия при входе на закладку
        /// </summary>
        /// <param name="_tabPageName">Имя закладки</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        public virtual void OnEnterTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;

            if (_tabPageName == LIST_TAB_PAGE_NAME)
            {
                CheckForItemModified(out _cancelAction);
                if (!_cancelAction)
                {
                    ManageCommandsForListTab();
                }
            }
            else
            {
                if (((string)WorkItem.State[Params.LeavingTabNameStateName]) == LIST_TAB_PAGE_NAME)
                {
                    PrepareDomainEditing(out _cancelAction);
                }
                if (!_cancelAction)
                {
                    ManageCommandsForNotListTab();
                }
            }
        }

        /// <summary>
        /// Выполняет действия при выходе с закладки
        /// </summary>
        /// <param name="_tabPageName">Имя закладки, с которой осуществляется выход</param>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        public virtual void OnLeaveTabPage(string _tabPageName, out bool _cancelAction)
        {
            _cancelAction = false;

            // _cancelAction оставлено на будущее: вдруг появится какой-то код

            if (!_cancelAction)
            {
                // Запоминаем имя покидаемой вкладки
                WorkItem.State[Params.LeavingTabNameStateName] = _tabPageName;
            }
        }

        /// <summary>
        /// Изменить доступ к глобальным командам для закладок видов детализации
        /// </summary>
        public virtual void ManageCommandsForNotListTab()
        {
            // Разрешается команда "Сохранить"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Enabled;

            // Разрешается команда "Обновить справочники"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Enabled;

            // Запрещается команда "Обновить список"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Создать"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Удалить"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Провести"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.PostItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Отменить проведение"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnpostItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Архивировать"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ArchiveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Разархивировать"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.UnarchiveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Копировать в Excel"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Disabled;
        }

        /// <summary>
        /// Изменить доступ к глобальным командам для закладки вида списка
        /// </summary>
        public virtual void ManageCommandsForListTab()
        {
            // Запрещается команда "Сохранить"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.SaveItem].Status =
                CommandStatus.Disabled;

            // Запрещается команда "Обновить справочники"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshRefBooks].Status =
                CommandStatus.Disabled;

            // Разрешается команда "Обновить список элементов"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.RefreshItem].Status =
                CommandStatus.Enabled;

            // Разрешается команда "Создать"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.CreateNewItem].Status =
                CommandStatus.Enabled;

            // Разрешается команда "Удалить"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.DeleteItem].Status =
                CommandStatus.Enabled;

            // Запрещается команда "Копировать в Excel"
            WorkItem.RootWorkItem.Commands[CommonCommandNames.ExportToExcel].Status =
                CommandStatus.Enabled;

            ((IBaseListView)WorkItem.SmartParts.Get(Params.ListViewNameStateName)).UpdateGlobalButtonsForCurrentItem();
        }

        #endregion

        #region Для наследников

        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected virtual TDomItem CreateNewItem()
        {
            return new TDomItem() { ID = Guid.NewGuid().ToString() };
        }

        /// <summary>
        /// Проверяет измененность элемента при выходе из редактирования
        /// </summary>
        /// <param name="_cancelAction">Признак отмены действия выхода с закладки</param>
        protected void CheckForItemModified(out bool _cancelAction)
        {
            _cancelAction = false;

            // Если статус текущего - модифицированный
            if ((string)WorkItem.State[CommonStateNames.ItemState] == CommonItemStates.Modified)
            {
                DialogResult _dr =
                    MessageBox.Show(
                        "Документ не сохранён, желаете сохранить?",
                        "Подтверждение сохранения", MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                // Если сохранение подтверждено
                if (_dr == DialogResult.Yes)
                {
                    // Сбрасываем признак успешного сохранения
                    WorkItem.State[CommonStateNames.IsSaveSucceeded] = false;

                    // Запустить глобальную команду "Сохранить"
                    WorkItem.RootWorkItem.Commands.Get(CommonCommandNames.SaveItem).Execute();

                    // Если сохранение не успешно, запрещаем выход с текущей закладки
                    if (!(bool)WorkItem.State[CommonStateNames.IsSaveSucceeded])
                    {
                        _cancelAction = true;
                    }
                }
                // Если от сохранения отказались - выход без сохранения
                else if (_dr == DialogResult.No)
                {
                    // Очистка вида. Переключение на режим редактирования
                    EditCompletion((WorkItem.State[Params.EditItemStateName].ToString() == CommonEditItemStates.New));
                }
                // Если воздержались от действий ("Отмена")
                else
                {
                    // Остаёмся в текущем режиме на текущей закладке
                    _cancelAction = true;
                }
            }
        }

        /// <summary>
        /// Выполняет действия для создания нового объекта
        /// </summary>
        protected virtual void OnCreateNewItem()
        {
            // Текущий режим обработки элемента - "создание нового"
            WorkItem.State[Params.EditItemStateName] = CommonEditItemStates.New;
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.Modified;

            // Сохраняем id выбранного элемента списка для возможного возвращения к нему, в случае отказа от сохранения нового элемента
            SelectedItemIdBeforeNewItemCreation = (string)WorkItem.State[Params.CurrentItemIdStateName];

            // Перейти на закладку "Детали"
            View.SelectTab(DETAIL_TAB_PAGE_NAME);
        }

        /// <summary>
        /// Выполняет действия при изменении элемента
        /// </summary>
        protected virtual void OnItemChanged()
        {
            // Вновь отображаем редактируемый элемент, только если не ушли на список
            if (View.CurrentTab != LIST_TAB_PAGE_NAME)
            {
                ShowCurrentDomainToItemView();
            }
        }

        /// <summary>
        /// Выполняет действия после отображения формы главного окна юзкейса
        /// </summary>
        /// <param name="startUpParams">Параметры запуска юзкейса</param>
        protected virtual void OnMainViewShown(AnyStartUpParams startUpParams)
        {
            if (startUpParams is ShowDetailsStartUpParams<TDomItem> &&
                View.ContainsTab(DETAIL_TAB_PAGE_NAME))
            {
                var _showDetailsStartUpParams =
                    (ShowDetailsStartUpParams<TDomItem>)startUpParams;

                WorkItem.EventTopics[CommonEventNames.FILL_LIST_WITH_DOMAIN_FIRED]
                    .Fire(this, new EventArgs<TDomItem>(_showDetailsStartUpParams.DomainObject), WorkItem, PublicationScope.WorkItem);

                ShowTab(_showDetailsStartUpParams.DomainObject, DETAIL_TAB_PAGE_NAME);
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// По окончании редактирования элемента
        /// </summary>
        /// <param name="_isNewItemCreateMode">Признак завершения редактирования
        /// в режиме создания нового элемента</param>
        private void EditCompletion(bool _isNewItemCreateMode)
        {
            // Текущий режим обработки - редактирование
            WorkItem.State[Params.EditItemStateName] = CommonEditItemStates.Edit;

            // Текущий статус элемента - не модифицирован
            WorkItem.State[CommonStateNames.ItemState] = CommonItemStates.NotChanged;

            if (_isNewItemCreateMode)
            {
                // Восстанавливаем id выбранного элемента списка. Визуально он и так выбран
                WorkItem.State[Params.CurrentItemIdStateName] = SelectedItemIdBeforeNewItemCreation;
            }

            // Изменить заголовок окна
            WorkItem.Controller.MainViewTitle = String.Format(
                "{0} {1}",
                WorkItem.Controller.DefaultMainViewTitle,
                ((IBaseListView)WorkItem.SmartParts.Get(Params.ListViewNameStateName)).GetCurrentItemShortName());
        }

        /// <summary>
        /// Отображает текущий домен на вью деталей
        /// </summary>
        private void ShowCurrentDomainToItemView()
        {
            IBaseItemView _iv = (IBaseItemView)WorkItem.SmartParts.Get(Params.ItemViewNameStateName);

            _iv.BindDeactivate();

            _iv.ShowDomainToView();

            _iv.BindActivate();
        }

        /// <summary>
        /// Выполняет подготовительные действия перед началом редактирования домена
        /// </summary>
        /// <param name="_cancelAction">Признак отмены действия</param>
        protected virtual void PrepareDomainEditing(out bool _cancelAction)
        {
            _cancelAction = false;

            switch ((string)WorkItem.State[Params.EditItemStateName])
            {
                case CommonEditItemStates.Edit:
                    string _curId = (string)WorkItem.State[Params.CurrentItemIdStateName];

                    if (String.IsNullOrEmpty(_curId))
                    {
                        _cancelAction = true;
                    }
                    else
                    {
                        WorkItem.State[Params.CurrentItemStateName] = GetItem<TDomItem>(_curId);
                    }
                    break;

                case CommonEditItemStates.New:
                    TDomItem _domItem = CreateNewItem();

                    WorkItem.State[Params.CurrentItemStateName] = _domItem;
                    WorkItem.State[Params.CurrentItemIdStateName] = _domItem.ID;

                    WorkItem.Controller.MainViewTitle = String.Format(
                        "{0} *(создание нового)",
                        WorkItem.Controller.DefaultMainViewTitle);
                    break;
            }

            if (!_cancelAction)
            {
                ShowCurrentDomainToItemView();
            }
        }

        /// <summary>
        /// Отображает содержимое элемента на указанной вкладке
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_tabPageName">Имя вкладки, которую нужно отобразить</param>
        /// <remarks>Предполагается, что _tabPageName отлична от LIST_TAB_PAGE_NAME</remarks>
        private void ShowTab(TDomItem _domItem, string _tabPageName)
        {
            IBaseListView _listView = WorkItem.SmartParts.Get<IBaseListView>(Params.ListViewNameStateName);

            if (View.CurrentTab == LIST_TAB_PAGE_NAME)
            {
                _listView.LocateToId(_domItem.ID, true);
                View.SelectTab(_tabPageName);
            }
            else
            {
                string _lastTabPageName = View.CurrentTab;
                bool _cancelAction;
                OnLeaveTabPage(_lastTabPageName, out _cancelAction);
                OnEnterTabPage(LIST_TAB_PAGE_NAME, out _cancelAction);
                if (!_cancelAction)
                {
                    _listView.LocateToId(_domItem.ID, true);
                    OnLeaveTabPage(LIST_TAB_PAGE_NAME, out _cancelAction);
                    OnEnterTabPage(_tabPageName, out _cancelAction);
                    if (_lastTabPageName != _tabPageName)
                    {
                        View.SelectTab(_tabPageName);
                    }
                }
            }
        }

        #endregion
    }
}