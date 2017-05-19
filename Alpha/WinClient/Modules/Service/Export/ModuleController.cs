using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
{
    [UsecaseName(ApplicationUsecaseNames.EXPORT)]
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

        protected override void AddServices()
        {
            base.AddServices();
            AddLocalService<ChargeExportService, IChargeExportService>();
            AddLocalService<BenefitExportService, IBenefitExportService>();
            AddLocalService<GisZhkhCustomerExportService, IGisZhkhCustomerExportService>();
        }
    }
}