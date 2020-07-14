namespace Taumis.Infrastructure.Interface.Constants
{
    /// <summary>
    /// Имена юзкейзов
    /// </summary>
    public static class ApplicationUsecaseNames
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public const string USERS = "usecase://RefBook/Users";

        /// <summary>
        /// Настройки
        /// </summary>
        public const string SETTINGS = "usecase://Services/Settings";

        /// <summary>
        /// Обработка
        /// </summary>
        public const string PROCESSING = "usecase://Services/Processing";

        /// <summary>
        /// Печатная форма различий в файлах
        /// </summary>
        public const string FILL_FORM_AND_PRINT_FORM_DIFF = "usecase://Report/FillFormAndPrintFormDiff";

        /// <summary>
        /// Скачивания форм ДЭК.
        /// </summary>
        public const string DEC_FORMS_DOWNLOADS = "usecase://Uploads/DecFormsDownloads";

        /// <summary>
        /// Загрузки форм ДЭК.
        /// </summary>
        public const string DEC_FORMS_UPLOADS = "usecase://Uploads/DecFormsUploads";

        /// <summary>
        /// Маршрутные листы.
        /// </summary>
        public const string ROUTE_FORMS= "usecase://Uploads/RouteForms";

        /// <summary>
        /// Формы для заполнения.
        /// </summary>
        public const string FILL_FORMS = "usecase://Uploads/FillForms";

        /// <summary>
        /// Объемы потребления по ИПУ.
        /// </summary>
        public const string PRIVATE_COUNTERS_VOLUMES = "usecase://Reports/RrivateCountersVolumes";
    }
}