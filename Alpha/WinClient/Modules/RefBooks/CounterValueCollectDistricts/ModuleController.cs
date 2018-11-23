using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts.Views.List;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.CounterValueCollectDistricts
{
    [UsecaseName(ApplicationUsecaseNames.COUNTER_VALUE_COLLECT_DISTRICTS)]
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