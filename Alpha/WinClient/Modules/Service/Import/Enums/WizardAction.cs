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
        ImportGisZhkhCustomerIDs,

        /// <summary>
        /// Импортировать данные по потребленным объемам коммунального ресурса при содержании общедомового имущества за период
        /// </summary>
        ImportPublicPlaceServiceVolumes,

        /// <summary>
        /// Имортировать данные приборов учета из файла Excel
        /// </summary>
        ImportCounters,

        /// <summary>
        /// Импортировать объемы потребеления электроэнергии по ОДПУ
        /// </summary>
        ImportElectricitySharedCounterVolumes,

        /// <summary>
        /// Импортировать данные для учета льгот детям войны
        /// </summary>
        ImportChildrenOfWarBenefitData
    }
}