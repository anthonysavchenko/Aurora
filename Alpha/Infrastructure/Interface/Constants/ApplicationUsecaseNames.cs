namespace Taumis.Infrastructure.Interface.Constants
{
    /// <summary>
    /// Имена юзкейзов
    /// </summary>
    public static class ApplicationUsecaseNames
    {
        /// <summary>
        /// Абоненты
        /// </summary>
        public const string CUSTOMERS = "usecase://Doc/Customers";

        /// <summary>
        /// Типы услуг
        /// </summary>
        public const string SERVICE_TYPES = "usecase://RefBook/ServiceTypes";

        /// <summary>
        /// Услуги
        /// </summary>
        public const string SERVICES = "usecase://RefBook/Services";

        /// <summary>
        /// Подрядчики
        /// </summary>
        public const string CONTRACTORS = "usecase://RefBook/Contractors";

        /// <summary>
        /// Начислния
        /// </summary>
        public const string CHARGES = "usecase://Oper/Charges";

        /// <summary>
        /// Печать квитанции за месяц
        /// </summary>
        public const string REGULAR_BILL = "usecase://Report/RegularBill";

        /// <summary>
        /// Печать долговой квитанции
        /// </summary>
        public const string DEBT_BILL = "usecase://Report/DebtBill";

        /// <summary>
        /// Платежи
        /// </summary>
        public const string PAYMENTS = "usecase://Oper/Payments";

        /// <summary>
        /// Посредники
        /// </summary>
        public const string INTERMEDIARIES = "usecase://RefBook/Intermediaries";

        /// <summary>
        /// Платежи и начисления
        /// </summary>
        public const string PAYMENTS_AND_CHARGES_REPORT = "usecase://Report/PaymentsAndChargesReport";
        
        /// <summary>
        /// Платежи
        /// </summary>
        public const string PAYMENTS_REPORT = "usecase://Report/PaymentsReport";

        /// <summary>
        /// Начисления
        /// </summary>
        public const string CHARGES_REPORT = "usecase://Report/ChargesReport";

        /// <summary>
        /// Типы льгот
        /// </summary>
        public const string BENEFIT_TYPES = "usecase://RefBook/BenefitTypes";

        /// <summary>
        /// Должники
        /// </summary>
        public const string DEBT_AGENCY_REPORT = "usecase://Report/DebtAgencyReport";

        /// <summary>
        /// Должники
        /// </summary>
        public const string DEBTORS_REPORT = "usecase://Report/DebtorsReport";

        /// <summary>
        /// Льготы
        /// </summary>
        public const string BENEFITS_REPORT = "usecase://Report/BenefitsReport";

        /// <summary>
        /// Долги и льготы по СОД
        /// </summary>
        public const string PUBLIC_PLACE_DEBT_AND_FINE_REPORT = "usecase://Report/PublicPlaceDebtAndFineReport";

        /// <summary>
        /// Льготы
        /// </summary>
        public const string PRE_CHARGE_REPORT = "usecase://Report/PreChargeReport";

        /// <summary>
        /// Улицы
        /// </summary>
        public const string STREETS = "usecase://RefBook/Streets";

        /// <summary>
        /// Приборы учета
        /// </summary>
        public const string COUNTERS = "usecase://Accounting/Counters";

        /// <summary>
        /// Улицы
        /// </summary>
        public const string BUILDINGS = "usecase://RefBook/Buildings";

        /// <summary>
        /// Импорт
        /// </summary>
        public const string IMPORT = "usecase://Service/Import";

        /// <summary>
        /// Экспорт
        /// </summary>
        public const string EXPORT = "usecase://Service/Export";

        /// <summary>
        /// Квитанции
        /// </summary>
        public const string BILLS = "usecase://Doc/Bills";

        /// <summary>
        /// Печатная форма квитанции за период
        /// </summary>
        public const string TOTAL_BILL = "usecase://Report/TotalBill";

        /// <summary>
        /// Печатная форма акта взаиморасчета
        /// </summary>
        public const string MUTUAL_SETTLEMENT = "usecase://Report/MutualSettlement";

        /// <summary>
        /// Пользователи
        /// </summary>
        public const string USERS = "usecase://RefBook/Users";

        /// <summary>
        /// Настройки
        /// </summary>
        public const string SETTINGS = "usecase://Services/Settings";

        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public const string BANK_DETAILS = "usecase://RefBook/BankDetails";
        
        /// <summary>
        /// Участки сбора показаний приборов учета
        /// </summary>
        public const string COUNTER_VALUE_COLLECT_DISTRICTS = "usecase://RefBook/CounterValueCollectDistrict";

        /// <summary>
        /// Печатная форма сбора показаний приборов учета
        /// </summary>
        public const string COUNTER_VALUE_COLLECT_FORM = "usecase://Report/CounterValueCollectForm";

        /// <summary>
        /// Обработка
        /// </summary>
        public const string PROCESSING = "usecase://Services/Processing";
    }
}