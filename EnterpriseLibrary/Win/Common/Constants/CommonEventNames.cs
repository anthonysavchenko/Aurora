namespace Taumis.EnterpriseLibrary.Win.Constants
{
    /// <summary>
    /// Константы наименований событий
    /// </summary>
    public static class CommonEventNames
    {
        /// <summary>
        /// Запущена команда "Создать".
        /// </summary>
        public const string CreateNewItemFired = "event://MainToolStrip/CreateNewCommandFired";

        /// <summary>
        /// Запущена команда "Сохранить".
        /// </summary>
        public const string SaveItemFired = "event://MainToolStrip/SaveItemFired";

        /// <summary>
        /// Запущена команда "Удалить".
        /// </summary>
        public const string DeleteItemFired = "event://MainToolStrip/DeleteItemFired";

        /// <summary>
        /// Запущена команда "Провести".
        /// </summary>
        public const string PostItemFired = "event://MainToolStrip/PostItemFired";

        /// <summary>
        /// Запущена команда "Отменить проведение".
        /// </summary>
        public const string UnPostItemFired = "event://MainToolStrip/UnPostItemFired";

        /// <summary>
        /// Запущена команда "Архивировать".
        /// </summary>
        public const string ArchivateItemFired = "event://MainToolStrip/ArchivateItemFired";

        /// <summary>
        /// Запущена команда "Разархивировать".
        /// </summary>
        public const string UnArchivateItemFired = "event://MainToolStrip/UnArchivateItemFired";

        /// <summary>
        /// Запущена команда "Обновить".
        /// </summary>
        public const string RefreshItemFired = "event://MainToolStrip/RefreshItemFired";

        /// <summary>
        /// Запущена команда "Обновить справочники".
        /// </summary>
        public const string RefreshRefBooksFired = "event://MainToolStrip/RefreshRefBooksFired";

        /// <summary>
        /// Изменённый элемент успешно сохранён.
        /// </summary>
        public const string ItemChanged = "event://ItemChanged";

        /// <summary>
        /// Событие запускается при экспорте активной таблицы в MS Excel.
        /// </summary>
        public const string ExportToExcelFired = "event://MainToolStrip/ExportToExcel";

        /// <summary>
        /// Запущена команда "Печать".
        /// </summary>
        public const string PrintItemFired = "event://MainToolStrip/PrintItemFired";

        /// <summary>
        /// Дана команда отобразить "бегущий" прогрессбар
        /// </summary>
        public const string ShowMarqueeProgressBarFired = "event://StatusBar/ShowMarqueeProgressBarFired";

        /// <summary>
        /// Дана команда убрать "бегущий" прогрессбар
        /// </summary>
        public const string HideMarqueeProgressBarFired = "event://StatusBar/HideMarqueeProgressBarFired";

        /// <summary>
        /// Дана команда на формирование отчета
        /// </summary>
        public const string GenerateReportFired = "event://Reporting/GenerateReportFired";

        /// <summary>
        /// Окончание формирования отчета
        /// </summary>
        public const string ReportGenerated = "event://Reporting/ReportGenerated";

        /// <summary>
        /// Дана команда на формирование диаграммной части отчета
        /// </summary>
        public const string GenerateDiagramFired = "event://Reporting/GenerateDeiagramFired";

        /// <summary>
        /// Окончание формирования диаграммной части отчета
        /// </summary>
        public const string DiagramGenerated = "event://Reporting/DiagramGenerated";

        /// <summary>
        /// Событие при отображении главной формы юзкейса
        /// </summary>
        public const string ON_MAIN_VIEW_SHOWN = "event://UsecaseBehaviour/OnMainViewShown";

        /// <summary>
        /// Дана команда заполнить таблицу на виде списка доменом
        /// </summary>
        public const string FILL_LIST_WITH_DOMAIN_FIRED = "event://UsecaseBehaviour/Update";
    }
}