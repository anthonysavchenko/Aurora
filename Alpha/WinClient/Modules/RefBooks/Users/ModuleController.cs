using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users
{
    [UsecaseName(ApplicationUsecaseNames.USERS)]
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