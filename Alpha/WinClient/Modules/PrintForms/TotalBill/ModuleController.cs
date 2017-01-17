using Taumis.Alpha.WinClient.Aurora.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill.Views.Report;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.TotalBill
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.TOTAL_BILL)]
    public sealed class ModuleController : CommonModuleController<PrintItemsStartUpParams>
    {
        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        protected override void Initialize(PrintItemsStartUpParams startUpParams)
        {
            AddState(ModuleStateNames.START_UP_PARAMS_BILL_IDS, startUpParams.Data);
            MainViewSize = new System.Drawing.Size(900, 600);
        }

        /// <summary>
        /// Добавить вью
        /// </summary>
        protected override void AddViews()
        {
            AddView<ReportView>("ReportView");
            AddView<StatusView>("StatusView");
            AddView<LayoutView>("LayoutView");
        }

        /// <summary>
        /// Добавить меню
        /// </summary>
        protected override void AddMenu()
        {
        }

        /// <summary>
        /// Добавить сервисы
        /// </summary>
        protected override void AddServices()
        {
            AddLocalService<BillService, IBillService>();
        }
    }
}