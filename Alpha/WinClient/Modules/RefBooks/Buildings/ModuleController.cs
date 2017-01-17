using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Counter;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.CounterValue;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.PublicPlaceViews;
using Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Tabbed;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings
{
    [UsecaseName(ApplicationUsecaseNames.BUILDINGS)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<ListView>("ListView");
            AddView<ItemView>("ItemView");
            AddView<CounterView>(ModuleViewNames.COUNTER_VIEW);
            AddView<CounterValueView>(ModuleViewNames.COUNTER_VALUE_VIEW);
            AddView<PublicPlaceView>(ModuleViewNames.PUBLIC_PLACE_VIEW);
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

        /// <summary>
        /// Добавить сервисы
        /// </summary>
        protected override void AddServices()
        {
            AddLocalService<BuildingUnitOfWork, IUnitOfWork>();
        }
    }
}