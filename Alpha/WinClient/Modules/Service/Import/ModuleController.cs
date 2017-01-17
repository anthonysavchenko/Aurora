using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    [UsecaseName(ApplicationUsecaseNames.IMPORT)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавляет виды
        /// </summary>
        protected override void AddViews()
        {
            AddView<MigrationView>("MigrationView");
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

        /// <summary>
        /// Добавить сервисы
        /// </summary>
        protected override void AddServices()
        {
            AddLocalService<PaymentDistributionService, PaymentDistributionService>();
        }
    }
}