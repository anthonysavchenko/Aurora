﻿using Taumis.Alpha.WinClient.Aurora.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Layout;
using Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Report;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm
{
    /// <summary>
    /// Контроллер прецедента
    /// </summary>
    [UsecaseName(ApplicationUsecaseNames.COUNTER_VALUE_COLLECT_FORM)]
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
            AddState(ModuleStateNames.DISTRICT_IDS, startUpParams.Data);
            MainViewSize = new System.Drawing.Size(1300, 600);
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
        /// Добавить пункт в меню
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