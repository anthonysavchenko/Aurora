using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.View.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.View.List;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.View.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services
{
    [UsecaseName(ApplicationUsecaseNames.SERVICES)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<ListView>("ListView");
            AddView<ItemView>("ItemView");
            AddView<TabbedView>("TabbedView");
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