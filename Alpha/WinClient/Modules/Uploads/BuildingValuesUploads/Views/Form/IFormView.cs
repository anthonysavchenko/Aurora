using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Form
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IFormView : IBaseView
    {
        /// <summary>
        /// Источник данных для таблицы
        /// </summary>
        DataTable Rows { set; }

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        void ShowDomainOnView();
    }
}