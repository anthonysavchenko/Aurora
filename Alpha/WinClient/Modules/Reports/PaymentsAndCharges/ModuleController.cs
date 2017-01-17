using Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.List;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.PAYMENTS_AND_CHARGES_REPORT)]
    public sealed class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        protected override void Initialize(EmptyStartUpParams startUpParams)
        {
            MainViewSize = new System.Drawing.Size(880, 600);
        }

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