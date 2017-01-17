using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Tabbed
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface ITabbedView : IBaseTabbedView
    {
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
