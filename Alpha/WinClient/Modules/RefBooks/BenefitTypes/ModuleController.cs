using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes
{
    [UsecaseName(ApplicationUsecaseNames.BENEFIT_TYPES)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<ItemView>("ItemView");
            AddView<ListView>("ListView");
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