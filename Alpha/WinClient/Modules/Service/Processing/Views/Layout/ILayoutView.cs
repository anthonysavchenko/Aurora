using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout
{
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Путь к директории для переименования файлов.
        /// </summary>
        string DirectoryPath { get; set; }

        /// <summary>
        /// Результат выполнения обработки.
        /// </summary>
        string Result { set; }

        /// <summary>
        /// Очищает данные.
        /// </summary>
        void ClearView();
    }
}