using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets.Views.List;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Streets
{
    [UsecaseName(ApplicationUsecaseNames.STREETS)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<ListView>("ListView");
            AddView<LayoutView>("LayoutView");
        }

        /// <summary>
        /// Добавляет пункт в меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(
                ModuleUIExtensionSiteNames.DEFAULT_WINDOW_HEADER,
                ApplicationUIExtensionSiteNames.RefBooks,
                ModuleUIExtensionSiteNames.MENU_ITEM_NAME);
        }
    }
}