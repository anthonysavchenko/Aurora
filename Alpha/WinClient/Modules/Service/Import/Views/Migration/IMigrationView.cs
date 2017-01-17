using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration
{
    public interface IMigrationView : IBaseView
    {
        /// <summary>
        /// Устанавливает текущее значение в прогресс баре
        /// </summary>
        /// <param name="value">Значение прогресс бара</param>
        void AddProgress(int value);

        /// <summary>
        /// Сбрасывает текущее значение шага
        /// </summary>
        /// <param name="maxValue">Максимальное значение интервала прогресс бара</param>
        void ResetProgressBar(int maxValue);

        /// <summary>
        /// Текст текущего состояния
        /// </summary>
        string StateText { set; }
    }
}