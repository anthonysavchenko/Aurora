namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums
{
    /// <summary>
    /// Виды импорта
    /// </summary>
    public enum WizardAction
    {
        /// <summary>
        /// Экспорт данных о льготниках
        /// </summary>
        ExportBenefitData,

        /// <summary>
        /// Экспорт начислений для банков
        /// </summary>
        ExportChargesForBanks,

        /// <summary>
        /// Экспорт данных об абонентов для импорта в ГИС ЖКХ
        /// </summary>
        ExportCustomersForGisZhkh,

        /// <summary>
        /// Экспорт начислений в файл для импорта в ГИС ЖКХ
        /// </summary>
        ExportChargesForGisZhkh,

        /// <summary>
        /// Экспорт показаний приборов учета в форму ДЭК
        /// </summary>
        ExportCounterValues
    }
}