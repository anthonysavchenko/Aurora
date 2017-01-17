namespace CustomerWebSite.Models
{
    public class YearsViewModel
    {
        /// <summary>
        /// Список годов обслуживания клиента
        /// </summary>
        public int[] Years { get; set; }

        /// <summary>
        /// Текущий год
        /// </summary>
        public int CurrentYear { get; set; }

        /// <summary>
        /// Текущий лицевой счет
        /// </summary>
        public string Account { get; set; }
    }
}