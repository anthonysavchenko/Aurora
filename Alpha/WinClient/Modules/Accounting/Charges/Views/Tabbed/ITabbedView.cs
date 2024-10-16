﻿using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface ITabbedView : IBaseTabbedView
    {
        /// <summary>
        /// Обновляет вкладки для создания нового элемента
        /// </summary>
        void RenewView();

        /// <summary>
        /// Показывает вкладку с мастером
        /// </summary>
        void ShowWizardTab();

        /// <summary>
        /// Скрывает вкладку c мастером
        /// </summary>
        void HideWizardTab();
    }
}
