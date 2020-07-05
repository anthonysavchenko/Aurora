using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.RouteForms.Views.Tabbed
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
    }
}
