namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums
{
    /// <summary>
    /// Виды импорта
    /// </summary>
    public enum WizardAction
    {
        /// <summary>
        /// Импортировать новых абонентов
        /// </summary>
        ImportNewCustomers,

        /// <summary>
        /// Импортировать услуги по домам
        /// </summary>
        ImportCustomerPoses,

        /// <summary>
        /// Импортировать ID абонентов из ГИС ЖКХ
        /// </summary>
        ImportGisZhkhCustomerIDs
    }
}