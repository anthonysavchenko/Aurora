using System.Drawing;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Form;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Wizard;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads
{
    /// <summary>
    /// Контроллер модуля
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.BUILDING_VALUES_UPLOADS)]
    public class ModuleController : CommonModuleController<AnyStartUpParams>
    {
        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        protected override void Initialize(AnyStartUpParams startUpParams)
        {
            MainViewSize = new Size(800, 670);
            MainViewLocation = MainViewLocation;
            base.Initialize(startUpParams);
        }

        /// <summary>
        /// Создание пункта меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(ModuleUIExtensionSiteNames.DEFAULT_WINDOW_HEADER,
                ApplicationUIExtensionSiteNames.Uploads,
                ModuleUIExtensionSiteNames.MENU_ITEM_NAME);
        }

        /// <summary>
        /// Добавление вьюх
        /// </summary>
        protected override void AddViews()
        {
            AddView<WizardView>(ModuleViewNames.WIZARD_VIEW);
            AddView<ListView>(ModuleViewNames.LIST_VIEW);
            AddView<ItemView>(ModuleViewNames.ITEM_VIEW);
            AddView<FormView>(ModuleViewNames.FORM_VIEW);
            AddView<TabbedView>(ModuleViewNames.TABBED_VIEW);
            AddView<LayoutView>(ModuleViewNames.LAYOUT_VIEW);
        }
    }
}
