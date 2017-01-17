using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement.Views.Report;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.MutualSettlement
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.MUTUAL_SETTLEMENT)]
    public sealed class ModuleController : CommonModuleController<PrintDocStartUpParams>
    {
        /// <summary>
        /// Проинициализировать юзкейз
        /// </summary>
        /// <param name="startUpParams">
        /// Параметры запуска юзкейза. null, если модуль запущен без параметров
        /// </param>
        protected override void Initialize(PrintDocStartUpParams startUpParams)
        {
            AddState(ModuleStateNames.START_UP_PARAMS_CUSTOMER_ID, startUpParams.DocId);
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
    }
}