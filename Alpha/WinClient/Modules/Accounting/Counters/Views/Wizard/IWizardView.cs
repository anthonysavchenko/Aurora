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

        #region CollectDataPage

        DataTable Counters { get; set; }
        DataTable Items { get; set; }

        void SetAccountFocus();

        string Account { get; set; }
        string CustomerName { set; }
        string Street { set; }
        string Building { set; }
        string Apartment { set; }
        string Area { set; }
        decimal PrevCounterValue { set; }
        decimal CounterValue { get; set; }
        int CounterId { get; set; }
        string CounterModel { set; }
        DateTime CollectDate { get; set; }

        /// <summary>
        /// Сообщение о корректности данных в выбранной позиции
        /// </summary>
        string CurrentItemMessage { get; set; }

        /// <summary>
        /// Признак наличия ошибок в выбранной позиции
        /// </summary>
        bool CurrentItemHasError { set; }

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
        CollectDataPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }
}