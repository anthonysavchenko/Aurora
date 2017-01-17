using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.Counter;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.CounterValue;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views.PaymentsAndCharges;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.CUSTOMERS)]
    public sealed class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавить вью
        /// </summary>
        protected override void AddViews()
        {
            AddView<PaymentsAndChargesView>(ModuleViewNames.PAYMENTS_AND_CHARGES_VIEW);
            AddView<CustomerPosListView>(ModuleViewNames.CUSTOMER_POS_VIEW);
            AddView<CounterView>(ModuleViewNames.COUNTER_VIEW);
            AddView<CounterValueView>(ModuleViewNames.COUNTER_VALUE_VIEW);
            AddView<ResidentsListView>(ModuleViewNames.RESIDENTS_LIST_VIEW);
            AddView<ItemView>("ItemView");
            AddView<ListView>("ListView");
            AddView<TabbedView>("TabbedView");
            AddView<TopView>("TopView");
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
            AddLocalService<PaymentDistributionService, PaymentDistributionService>();
            AddLocalService<CustomersUnitOfWork, IUnitOfWork>();
        }
    }
}