using Taumis.Alpha.WinClient.Aurora.Library;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.PosListView;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.FINES)]
    public sealed class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавить вью
        /// </summary>
        protected override void AddViews()
        {
            AddView<PosListView>("PosListView");
            AddView<ListView>("ListView");
            AddView<ItemView>("ItemView");
            AddView<TabbedView>("TabbedView");
            AddView<LayoutView>("LayoutView");
        }

        /// <summary>
        /// Добавить пункт в меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(
                ModuleUIExtensionSiteNames.DEFAULT_WINDOW_HEADER,
                ApplicationUIExtensionSiteNames.Accounting,
                ModuleUIExtensionSiteNames.MENU_ITEM_NAME);
        }

        /// <summary>
        /// Добавить сервисы
        /// </summary>
        protected override void AddServices()
        {
            AddLocalService<UnitOfWork, IUnitOfWork>();
        }
    }
}