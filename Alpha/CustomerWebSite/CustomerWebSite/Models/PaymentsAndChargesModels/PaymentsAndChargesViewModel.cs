using System.Collections.Generic;

namespace CustomerWebSite.Models.PaymentsAndChargesModels
{
    /// <summary>
    /// Модель для отображения платежей и начислений
    /// </summary>
    public class PaymentsAndChargesViewModel
    {
        /// <summary>
        /// Модель для отображения годов обслуживания абонента
        /// </summary>
        public YearsViewModel YearsViewModel { get; set; }

        /// <summary>
        /// Список итого по периодам
        /// </summary>
        public List<PeriodTotal> PeriodTotals { get; set; }
    }
}