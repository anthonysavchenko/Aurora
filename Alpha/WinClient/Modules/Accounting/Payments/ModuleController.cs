using System.Drawing;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Item;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Payment;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Tabbed;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments
{
    /// <summary>
    /// Контроллер модуля
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.PAYMENTS)]
    public class ModuleController : CommonModuleController<AnyStartUpParams>
    {
        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        protected override void Initialize(AnyStartUpParams startUpParams)
        {
            MainViewSize = new Size(800, 670);
            MainViewLocation = MainViewLocation;
            base.Initialize(startUpParams);
        }

        /// <summary>
        /// Создание пункта меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(ModuleUIExtensionSiteNames.DEFAULT_WINDOW_HEADER,
                ApplicationUIExtensionSiteNames.Accounting,
                ModuleUIExtensionSiteNames.MENU_ITEM_NAME);
        }

        /// <summary>
        /// Добавление вьюх
        /// </summary>
        protected override void AddViews()
        {
            AddView<WizardView>(ModuleViewNames.WIZARD_VIEW);
            AddView<PaymentView>(ModuleViewNames.PAYMENT_VIEW);
            AddView<ListView>(ModuleViewNames.LIST_VIEW);
            AddView<ItemView>(ModuleViewNames.ITEM_VIEW);
            AddView<TabbedView>(ModuleViewNames.TABBED_VIEW);
            AddView<LayoutView>(ModuleViewNames.LAYOUT_VIEW);
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        protected override void AddServices()
        {
            AddLocalService<PaymentDistributionService, PaymentDistributionService>();
        }
    }
}