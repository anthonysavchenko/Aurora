using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
    /// <summary>
    /// Контроллер модуля
    /// </summary>
    /// <typeparam name="TStartUpParams">Тип параметров запуска юзкейза</typeparam>
    public abstract class CommonModuleController<TStartUpParams> : ICommonModuleController
        where TStartUpParams : AnyStartUpParams
    {
        #region Закрытые константы и переменные

        /// <summary>
        /// Имя слота стэйта юзкейза с параметрами запуска
        /// </summary>
        private const string START_UP_PARAMS_STATE_NAME = "state://CommonModule/StartUpParams";

        /// <summary>
        /// Команда запуска юзкейза
        /// </summary>
        private const string RUN_USECASE_COMMAND = "command://CommonModule/RunUsecaseCommand";

        /// <summary>
        /// Событие запуска из меню
        /// </summary>
        private const string ACTIVATING_EVENT = "Click";

        /// <summary>
        /// Абсцисса окна по умолчанию
        /// </summary>
        private const int DEFAULT_LOCATION_X = 50;

        /// <summary>
        /// Ордината окна по умолчанию
        /// </summary>
        private const int DEFAULT_LOCATION_Y = 50;

        /// <summary>
        /// Ширина окна по умолчанию
        /// </summary>
        private const int DEFAULT_WIDTH = 800;

        /// <summary>
        /// Высота окна по умолчанию
        /// </summary>
        private const int DEFAULT_HEIGHT = 600;

        /// <summary>
        /// Индекс первого узла дерева меню
        /// </summary>
        private const int FIRST_MENU_INDEX = 0;

        /// <summary>
        /// Индекс шаблонного типа параметров запуска юзкейза
        /// </summary>
        private const int START_UP_PARAMS_GENERIC_INDEX = 0;

        /// <summary>
        /// Декримент для дерева меню, обозначающий пункт юзкейза
        /// </summary>
        private const int DECRIMENT_MENU_PATH = 1;

        /// <summary>
        /// Количество элементов в пустом дереве меню
        /// </summary>
        private const int EMPTY_MENU_PATH_LIST_COUNT = 0;

        /// <summary>
        /// Параметры основного окна
        /// </summary>
        private WindowSmartPartInfo mainViewInfo;

        /// <summary>
        /// Ветка дерева меню
        /// </summary>
        private List<string> menuPathList;

        /// <summary>
        /// Основное вью
        /// </summary>
        /// <remarks>
        /// Заменить UserControl на BaseView как только все вью в юзкейзах будут от него унаследованы
        /// для того, чтобы жестко связать базовый контроллер модуля с базовыми вью
        /// </remarks>
        private UserControl mainView;

        #endregion

        #region Защищенные методы

        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        /// <remarks>Не реализована, может быть перегружена в унаследованном классе</remarks>
        protected virtual void Initialize(TStartUpParams startUpParams)
        {
            /* Не реализованно */
        }

        /// <summary>
        /// Добавить сервисы
        /// </summary>
        /// <remarks>Не реализована, может быть перегружена в унаследованном классе</remarks>
        protected virtual void AddServices()
        {
            /* Не реализованно */
        }

        /// <summary>
        /// Добавить вью
        /// </summary>
        /// <remarks>Не реализована, должна быть перегружена в унаследованном классе</remarks>
        protected abstract void AddViews();

        /// <summary>
        /// Добавить пункт в меню
        /// </summary>
        /// <remarks>Не реализована, должна быть перегружена в унаследованном классе</remarks>
        protected abstract void AddMenu();

        /// <summary>
        /// Проинициализировать переменную стэйта юзкейза новым значением
        /// </summary>
        /// <param name="stateKey">Идентификатор переменной стэйта</param>
        /// <param name="stateValue">Значение переменной стэйта</param>
        protected void AddState(string stateKey, object stateValue)
        {
            WorkItem.State[stateKey] = stateValue;
        }

        /// <summary>
        /// Получить переменную стэйта юзкейза
        /// </summary>
        /// <param name="stateKey">Идентификатор переменной стэйта</param>
        protected TObjectType GetState<TObjectType>(string stateKey)
        {
            return (TObjectType)WorkItem.State[stateKey];
        }

        /// <summary>
        /// Добавить локальный сервис
        /// </summary>
        /// <typeparam name="TService">Тип сервиса</typeparam>
        /// <typeparam name="TServiceRegisterAs">Тип регистрации сервиса</typeparam>
        protected void AddLocalService<TService, TServiceRegisterAs>() where TService : TServiceRegisterAs
        {
            WorkItem.Services.AddNew<TService, TServiceRegisterAs>();
        }

        /// <summary>
        /// Получить локальный сервис
        /// </summary>
        /// <typeparam name="TServiceRegisterAs">Тип регистрации сервиса</typeparam>
        /// <returns>Локальный сервис</returns>
        protected TServiceRegisterAs GetLocalService<TServiceRegisterAs>()
        {
            return WorkItem.Services.Get<TServiceRegisterAs>(true);
        }

        /// <summary>
        /// Добавить вью
        /// </summary>
        /// <typeparam name="TView">Тип вью</typeparam>
        /// <param name="viewName">Название вью</param>
        /// <remarks>
        /// Заменить UserControl на BaseView как только все вью в юзкейзах будут от него унаследованы
        /// для того, чтобы жестко связать базовый контроллер модуля с базовыми вью
        /// </remarks>
        protected void AddView<TView>(string viewName)
            where TView : UserControl
        {
            mainView = WorkItem.SmartParts.AddNew<TView>(viewName);
        }

        /// <summary>
        /// Добавить пункт меню
        /// </summary>
        /// <param name="defaultTitle">Заголовок основного окна по умолчанию</param>
        /// <param name="menuPath">Ветка дерева меню</param>
        protected void AddTitleAndMenuItem(string defaultTitle, params string[] menuPath)
        {
            DefaultMainViewTitle = defaultTitle;

            mainViewInfo.Title = DefaultMainViewTitle;
            mainViewInfo.Description = DefaultMainViewTitle;

            if (null != menuPath && EMPTY_MENU_PATH_LIST_COUNT != menuPath.Length)
            {
                menuPathList.AddRange(menuPath);

                ToolStripMenuItem menuItem = new ToolStripMenuItem(menuPathList[menuPathList.Count - DECRIMENT_MENU_PATH]);
                MenuExtendService menuService = WorkItem.RootWorkItem.Services.Get<MenuExtendService>();
                string[] menuSubstring = new string[menuPathList.Count - DECRIMENT_MENU_PATH];

                menuPathList.CopyTo(FIRST_MENU_INDEX, menuSubstring, FIRST_MENU_INDEX, menuPathList.Count - DECRIMENT_MENU_PATH);
                WorkItem.Commands[RUN_USECASE_COMMAND].AddInvoker(menuItem, ACTIVATING_EVENT);
                menuService.Run(menuItem, menuSubstring);
            }
        }

        #endregion

        #region Открытые свойства

        /// <summary>
        /// Юзкейз
        /// </summary>
        [ServiceDependency]
        public CommonControlledWorkItem WorkItem
        {
            set;
            private get;
        }

        /// <summary>
        /// Заголовок основного окна по умолчанию
        /// </summary>
        public string DefaultMainViewTitle
        {
            private set;
            get;
        }

        /// <summary>
        /// Заголовок основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает заголовок в зависимости от того, запущен ли юзкейз,
        /// сохраняя текущие координаты и размер окна.
        /// </remarks>
        public string MainViewTitle
        {
            set
            {
                mainViewInfo.Title = value;
                mainViewInfo.Description = value;
                if (null != mainView)
                {
                    mainViewInfo.Location = mainView.ParentForm.Location;
                    mainViewInfo.Width = mainView.ParentForm.Size.Width;
                    mainViewInfo.Height = mainView.ParentForm.Size.Height;
                    WorkItem.RootWorkItem.Workspaces.Get(CommonWorkspaceNames.MDIWindows).ApplySmartPartInfo(mainView, mainViewInfo);
                }
            }
        }

        /// <summary>
        /// Координаты основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает координаты основного окна в зависимости от того,
        /// запущен ли юзкейз, сохраняя текущий заголовок и размер окна.
        /// get возвращет текущие или заданные координаты окна в зависимости от того,
        /// запущен ли юзкейз.
        /// </remarks>
        public Point MainViewLocation
        {
            set
            {
                mainViewInfo.Location = value;
                if (null != mainView && null != mainView.ParentForm)
                {
                    mainViewInfo.Width = mainView.ParentForm.Size.Width;
                    mainViewInfo.Height = mainView.ParentForm.Size.Height;
                    WorkItem.RootWorkItem.Workspaces.Get(CommonWorkspaceNames.MDIWindows).ApplySmartPartInfo(mainView, mainViewInfo);
                }
            }
            get
            {
                Point value = new Point();

                if (null != mainView && null != mainView.ParentForm)
                {
                    value = mainView.ParentForm.Location;
                }
                else
                {
                    value = mainViewInfo.Location;
                }

                return value;
            }
        }

        /// <summary>
        /// Размер основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает размер основного окна в зависимости от того,
        /// запущен ли юзкейз, сохраняя текущий заголовок и координаты окна.
        /// get возвращет текущий или заданный размер окна в зависимости от того,
        /// запущен ли юзкейз.
        /// </remarks>
        public Size MainViewSize
        {
            set
            {
                mainViewInfo.Width = value.Width;
                mainViewInfo.Height = value.Height;
                if (null != mainView && null != mainView.ParentForm)
                {
                    mainViewInfo.Location = mainView.ParentForm.Location;
                    mainViewInfo.Location = mainView.ParentForm.Location;
                    WorkItem.RootWorkItem.Workspaces.Get(CommonWorkspaceNames.MDIWindows).ApplySmartPartInfo(mainView, mainViewInfo);
                }
            }
            get
            {
                Size value = new Size();

                if (null != mainView && null != mainView.ParentForm)
                {
                    value = mainView.ParentForm.Size;
                }
                else
                {
                    value.Height = mainViewInfo.Height;
                    value.Width = mainViewInfo.Width;
                }

                return value;
            }
        }

        #endregion

        #region Открытые методы

        /// <summary>
        /// Конструктор
        /// </summary>
        public CommonModuleController()
        {
            mainViewInfo = new WindowSmartPartInfo()
            {
                Title = DefaultMainViewTitle,
                Description = DefaultMainViewTitle,
                Location = new Point(DEFAULT_LOCATION_X, DEFAULT_LOCATION_Y),
                Width = DEFAULT_WIDTH,
                Height = DEFAULT_HEIGHT
            };
            menuPathList = new List<string>();
            mainView = null;
        }

        /// <summary>
        /// Запустить юзкейз
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="eventArgs">Аргументы события</param>
        [CommandHandler(RUN_USECASE_COMMAND)]
        public virtual void Run(object sender, System.EventArgs eventArgs)
        {
            try
            {
                TStartUpParams parameters = (TStartUpParams)WorkItem.State[START_UP_PARAMS_STATE_NAME];
                Initialize(parameters);
                WorkItem.State[START_UP_PARAMS_STATE_NAME] = null;

                if (null == mainView)
                {
                    AddServices();
                    AddViews();
                }

                WorkItem.RootWorkItem.Workspaces[CommonWorkspaceNames.MDIWindows].Show(mainView, mainViewInfo);

                WorkItem.EventTopics[CommonEventNames.ON_MAIN_VIEW_SHOWN]
                    .Fire(this, new EventArgs<AnyStartUpParams>(parameters), WorkItem, PublicationScope.WorkItem);
            }
            catch (Exception anyException)
            {
                throw new UsecaseException("Ошибка запуска", anyException);
            }
        }

        /// <summary>
        /// Загрузить юзкейз
        /// </summary>
        public void Load()
        {
            try
            {
                AddMenu();
            }
            catch (Exception anyException)
            {
                throw new UsecaseException("Ошибка загрузки", anyException);
            }
        }

        /// <summary>
        /// Запустить юзкейз, найденный в загруженных юзкейзах по имени
        /// </summary>
        /// <param name="usecaseName">Имя юзкейза</param>
        /// <param name="parameters">Параметры запуска юзкейза</param>
        public void RunUsecase(string usecaseName, object parameters)
        {
            WorkItem item = WorkItem.RootWorkItem.WorkItems.Get(usecaseName);

            if (null != item && typeof(CommonControlledWorkItem).IsAssignableFrom(item.GetType()))
            {
                CommonControlledWorkItem controlledWorkItem = (CommonControlledWorkItem)item;
                if (controlledWorkItem.Controller.GetType().BaseType.IsGenericType &&
                    controlledWorkItem.Controller.GetType().BaseType.GetGenericArguments()[START_UP_PARAMS_GENERIC_INDEX].IsInstanceOfType(parameters))
                {
                    controlledWorkItem.State[START_UP_PARAMS_STATE_NAME] = parameters;
                    controlledWorkItem.Commands[RUN_USECASE_COMMAND].Execute();
                }
                else
                {
                    throw new UsecaseException("Параметры запуска юзкейза не соответствуют указанным в контроллере модуля");
                }
            }
            else
            {
                throw new UsecaseException("Вызываемый юзкейз не найден или он не унаследован от базового");
            }
        }

        #endregion

    }
}
