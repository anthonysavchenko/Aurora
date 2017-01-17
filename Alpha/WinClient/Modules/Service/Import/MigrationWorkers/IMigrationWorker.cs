namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.MigrationWorkers
{
    public interface IMigrationWorker
    {
        /// <summary>
        /// Возвращает общее количество данных, подлежащих миграции
        /// </summary>
        int GetTotalCount();

        /// <summary>
        /// Запускает миграцию
        /// </summary>
        void StartMigration();
    }
}