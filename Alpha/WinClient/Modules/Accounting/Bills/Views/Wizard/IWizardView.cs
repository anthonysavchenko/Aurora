using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        /// <summary>
        /// Тип квитанций
        /// </summary>
        ReceiptTypes ReceiptType { set; get; }

        /// <summary>
        /// Выбирать до месяца
        /// </summary>
        DateTime TillDate { set; get; }

        /// <summary>
        /// Минимальная сумма
        /// </summary>
        decimal MinValue { set; get; }

        /// <summary>
        /// Лицевой счет абонента
        /// </summary>
        string TotalBillAccount { set; get; }

        /// <summary>
        /// Конечный период квитанции по доплате
        /// </summary>
        DateTime TotalBillTillPeriod { set; get; }

        /// <summary>
        /// Сбрасывает текущее состояние процесса обработки
        /// </summary>
        /// <param name="maxValue">Количество шагов процесса</param>
        void ResetProgressBar(int maxValue);

        /// <summary>
        /// Обновляет состояние процесса обработки
        /// </summary>
        /// <param name="delta">Количество обработанных единиц</param>
        void AddProgress(int delta);

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

        /// <summary>
        /// Итоговое количество обработанных записей
        /// </summary>
        int ResultCount { set; }

        /// <summary>
        /// Итоговая сумма 
        /// </summary>
        decimal ResultValue { set; }

        /// <summary>
        /// Итоговое количество ошибок в процессе обработки
        /// </summary>
        int ResultErrorCount { set; }
    }

    /// <summary>
    /// Типы квитанций
    /// </summary>
    public enum ReceiptTypes
    {
        Debt,
        Total
    }

    /// <summary>
    /// Шаги мастера
    /// </summary>
    public enum WizardSteps
    {
        ChooseMethodPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }
}