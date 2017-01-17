using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
{
    /// <summary>
    /// Интерфейс вью формы
    /// </summary>
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Полное имя файла
        /// </summary>
        string FilePath { set; get; }

        /// <summary>
        /// Учетный период
        /// </summary>
        DateTime Period { set; get; }

        /// <summary>
        /// Формат файла
        /// </summary>
        bool IsSberbankFileFormat { set; get; }

        string BenefitInputFilePath { get; }

        /// <summary>
        /// Флаг экспорта данных ГИС ЖКХ: "только новые" / "все" абоненты
        /// </summary>
        bool GisZhkhOnlyNew { get; set; }

        /// <summary>
        /// Путь к файлу шаблона
        /// </summary>
        string GisZhkhInputFilePath { get; }

        void ShowBenefitProgressBar();
        void HideBenefitProgressBar();

        void ShowGisZhkhProgressBar();
        void HideGisZhkhProgressBar();
    }
}