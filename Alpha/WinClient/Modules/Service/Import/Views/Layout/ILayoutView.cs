using System;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Layout
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

        /// <summary>
        /// Учетный период
        /// </summary>
        DateTime Period { get; set; }

        DataTable Streets { set; }
        DataTable Buildings { set; }
        string StreetId { get; set; }
        string BuildingId { get; set; }

        void SetProgress(int percent);

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        void SelectPage(WizardPages page);

        string ResultText { set; }
    }
}