using Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings.Views.Layout;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Settings
{
    [UsecaseName(ApplicationUsecaseNames.SETTINGS)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<LayoutView>("LayoutView");
        }

        /// <summary>
        /// Добавляет пункт в меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(
                ModuleUIExtensionSiteNames.DEFAULT_WINDOW_HEADER,
                ApplicationUIExtensionSiteNames.Services,
                ModuleUIExtensionSiteNames.MENU_ITEM_NAME);
        }
    }
}