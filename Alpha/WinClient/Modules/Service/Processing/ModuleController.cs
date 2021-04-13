﻿using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;
using Taumis.EnterpriseLibrary.Win.Modules.CommonModule;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing
{
    [UsecaseName(ApplicationUsecaseNames.PROCESSING)]
    public class ModuleController : CommonModuleController<EmptyStartUpParams>
    {
        protected override void Initialize(EmptyStartUpParams startUpParams)
        {
            base.Initialize(startUpParams);
            MainViewSize = new System.Drawing.Size(1000, 600);
        }
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
    }
}