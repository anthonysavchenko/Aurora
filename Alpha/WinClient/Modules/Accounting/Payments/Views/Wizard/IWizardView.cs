using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        #region ChooseMethodPage

        /// <summary>
        /// Посредники
        /// </summary>
        DataTable Intermediaries { set; }

        /// <summary>
        /// Метод внесения платежей
        /// </summary>
        ImportTypes ImportType { get; set; }

        /// <summary>
        /// Источник данных для ручного ввода
        /// </summary>
        ManualTypeSources ManualTypeSource { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        DateTime PaymentDate { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// Посредник
        /// </summary>
        Intermediary Intermediary { get; set; } 
        
        #endregion

        #region ProcessingPage

        /// <summary>
        /// Таблица с информацией текущаего ввода
        /// </summary>
        DataTable ProcessingData { get; set; }

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

        #region CheckDataPage

        /// <summary>
        /// Устанавливает фокус на поле с номером счета
        /// </summary>
        void SetAccountFocus();

        /// <summary>
        /// Устанавливает фокус на поле со штрих-кодом
        /// </summary>
        void SetBarcodeFocus();

        /// <summary>
        /// Признак доступность панели со штрих-кодом
        /// </summary>
        bool IsBarcodeEnabled { set; }

        /// <summary>
        /// Признак использования сканера штрих-кода
        /// </summary>
        bool IsUseScanner { get; set; }

        /// <summary>
        /// Штри-код в выбранной позиции
        /// </summary>
        string CurrentBarcode { get; set; }

        /// <summary>
        /// Лицевой счет в выбранной позиции
        /// </summary>
        string CurrentAccount { get; set; }

        /// <summary>
        /// Период  в выбранной позиции
        /// </summary>
        DateTime CurrentPeriod { set; get; }

        /// <summary>
        /// Сумма платежа в выбранной позиции
        /// </summary>
        decimal CurrentValue { set; get; }

        /// <summary>
        /// Собственник в выбранной позиции
        /// </summary>
        string CurrentOwner { get; set; }

        /// <summary>
        /// Выбранный посредник
        /// </summary>
        string CurrentIntermediary { get; set; }

        /// <summary>
        /// Улица в выбранной позиции
        /// </summary>
        string CurrentStreet { set; }

        /// <summary>
        /// Дом в выбранной позиции
        /// </summary>
        string CurrentHouse { set; }

        /// <summary>
        /// Квартира в выбранной позиции
        /// </summary>
        string CurrentApartment { set; }

        /// <summary>
        /// Площадь в выбранной позиции
        /// </summary>
        string CurrentSquare { set; }

        /// <summary>
        /// Сообщение о корректности данных в выбранной позиции
        /// </summary>
        string CurrentItemMessage { get; set; }

        /// <summary>
        /// Признак наличия ошибок в выбранной позиции
        /// </summary>
        bool CurrentItemHasError { set; }

        /// <summary>
        /// Признак отображения кнопки создания новой записи
        /// </summary>
        bool AddButtonVisible { set; }

        #endregion

        #region FinishPage

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
    /// Методы внесения платежей
    /// </summary>
    public enum ImportTypes
    {
        File,
        Manual
    }

    /// <summary>
    /// Источники данных для ручного ввода
    /// </summary>
    public enum ManualTypeSources
    {
        Bill,
        OtherPayments
    }

    /// <summary>
    /// Шаги мастера
    /// </summary>
    public enum WizardSteps
    {
        ChooseMethodPage,
        ProcessingPage,
        CheckDataPage,
        FinishPage,
        Unknown
    }
}