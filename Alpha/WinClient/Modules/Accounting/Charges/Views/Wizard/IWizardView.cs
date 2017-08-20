using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard
{
    public interface IWizardView : IBaseView
    {
        /// <summary>
        /// Вид создаваемых начислений
        /// </summary>
        ChargeType ChargeType { get; }

        /// <summary>
        /// Период предстоящих начислений
        /// </summary>
        DateTime ChargingPeriod { set; }

        /// <summary>
        /// Имя файла с импортируемыми задолжнастями
        /// </summary>
        string DebtFileName { get; set; }

        /// <summary>
        /// Период внесения задолжностей
        /// </summary>
        DateTime DebtPeriod { get; }

        /// <summary>
        /// Строка целиком
        /// </summary>
        bool WholeWord { get; set; }

        /// <summary>
        /// Наименование улицы
        /// </summary>
        string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        string House { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        string Apartment { get; set; }

        /// <summary>
        /// Номер аккаунта
        /// </summary>
        string Account { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        string ZipCode { get; set; }

        /// <summary>
        /// Фильтр
        /// </summary>
        FilterType Filter { get; set; }

        /// <summary>
        /// Источник данных для таблицы с информацией о найденных абонентах
        /// </summary>
        DataTable FoundCustomers { get; set; }

        /// <summary>
        /// Источник данных для таблицы с выбранными абонентами
        /// </summary>
        DataTable SelectedCustomers { get; set; }

        /// <summary>
        /// Начальный период 
        /// </summary>
        DateTime SinceCorrectionPeriod { get; set; }

        /// <summary>
        /// Конечный период
        /// </summary>
        DateTime TillCorrectionPeriod { get; set; }

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
        void SelectPage(WizardPages page);

        /// <summary>
        /// Признак завершения работы мастера
        /// </summary>
        bool IsMasterCompleted { get; set; }

        /// <summary>
        /// Признак активного процесса обработки
        /// </summary>
        bool IsMasterInProgress { get; set; }

        /// <summary>
        /// Сбрасывает текущее состояние процесса резервного копирования
        /// </summary>
        void ResetBackupProgress();

        /// <summary>
        /// Устанавливает состояние процесса резервного копирования
        /// </summary>
        /// <param name="value">Текущее значение</param>
        void SetBackupProgress(int value);

        int? CorrectingServiceID { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        Service Service { get;}

        /// <summary>
        /// Услуги
        /// </summary>
        DataTable Services { set; }

        /// <summary>
        /// Период снятия начислений по услуге
        /// </summary>
        DateTime PercentCorrectionPeriod { get; set; }

        /// <summary>
        /// Источник данных для таблицы с абонентами, процентами снятия и днями
        /// </summary>
        DataTable CustomersWithPercents { get; set; }
    }

    /// <summary>
    /// Виды создаваемых начислений
    /// </summary>
    public enum ChargeType
    {
        // Создание ежемесячных
        Regular,
        // Корректировка начислений
        Correction,
        // Снятие начислений по одной из услуг
        PercentCorrection,
        // Внесение выявленной задолженности
        Debt,
    }

    /// <summary>
    /// Шаги мастера
    /// </summary>
    public enum WizardPages
    {
        ChooseMethodPage,
        CustomersPage,
        PercentPage,
        ChoosePeriodPage,
        BackupPage,
        ProcessingPage,
        FinishPage,
        Unknown
    }

    /// <summary>
    /// Тип фильтра в мастере
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// По адресу
        /// </summary>
        Address,

        /// <summary>
        /// По номеру счета
        /// </summary>
        Account,

        /// <summary>
        /// По почтовому индексу
        /// </summary>
        ZipCode,

        /// <summary>
        /// Все абоненты
        /// </summary>
        All
    }
}