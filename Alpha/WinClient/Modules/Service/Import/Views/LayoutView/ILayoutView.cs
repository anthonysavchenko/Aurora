using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Интерфейс вью формы
    /// </summary>
    public interface ILayoutView : IBaseLayoutView
    {
        WizardAction WizardAction { get; }

        /// <summary>
        /// Полное имя файла
        /// </summary>
        string FilePath { get; }

        void ResetProgress();
        void SetProgress(int percent);

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        void SelectPage(WizardPages page);

        string ResultText { set; }
    }
}