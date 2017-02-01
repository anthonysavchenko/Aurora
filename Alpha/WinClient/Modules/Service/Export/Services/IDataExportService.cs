namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    /// <summary>
    /// Интерфейс сервиса экспорта данных
    /// </summary>
    public interface IDataExportService
    {
        /// <summary>
        /// Производит экспорт данных в файл
        /// </summary>
        /// <param name="inputFile">
        ///     Имя результирующего файла либо шаблона, на основании которого будут созданы 
        ///     файлы с экспортируемыми данными
        /// </param>
        /// <param name="additionalParams">Дополнительные параметры</param>
        /// <returns>Результат экспорта</returns>
        bool ProcessFile(string inputFile, params object[] additionalParams);
    }
}
