using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.List;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.DEBTORS_REPORT)]
    public sealed class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Добавить вью
        /// </summary>
        protected override void AddViews()
        {
            AddView<ListView>("ListView");
            AddView<StatusView>("StatusView");
            AddView<LayoutView>("LayoutView");
        }

        /// <summary>
        /// Добавить пункт в меню
        /// </summary>
        protected override void AddMenu()
        {
            AddTitleAndMenuItem(
                ModuleUIExtensionSiteNames.TITLE,
                ApplicationUIExtensionSiteNames.Reports,
                ModuleUIExtensionSiteNames.TITLE);
        }
    }
}