using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        #region ProcessingPage

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        void ResetProgressBar(int maxValue);

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        void AddProgress();

        #endregion

        #region BuildingSelectPage

        DataTable Streets { set; }
        DataTable Buildings { set; }
        string StreetId { get; set; }
        string BuildingId { get; set; }
        DateTime Period { get; set; }

        #endregion

        #region CollectDataPage

        DataTable Items { get; set; }

        void ShowEditor();

        decimal CounterValue { set; }
        DateTime CollectDateTime { set; }

        #endregion

        #region FinishPage

        /// <summary>
        /// Итоговое количество обработанных записей
        /// </summary>
        int ResultCount { set; }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        int ResultErrorCount { set; } 

        #endregion

        /// <summary>
        /// Начинает работу мастера
        /// </summary>
        void StartWizard();

        /// <summary>
        /// Отображает страницу мастера
        /// </summary>
        /// <param name="page">Шаг</param>
        void SelectPage(WizardSteps page);

        /// <summary>
        /// Признак завершения работы мастера
        /// </summary>
        bool IsMasterCompleted { get; set; }

        /// <summary>
        /// Признак активного процесса обработки
        /// </summary>
        bool IsMasterInProgress { get; set; }
    }

    /// <summary>
    /// Шаги мастера
    /// </summary>
    public enum WizardSteps
    {
        BuildingSelectPage,
        CollectDataPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }
}