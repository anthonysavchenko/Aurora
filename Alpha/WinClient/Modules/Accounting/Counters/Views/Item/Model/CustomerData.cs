namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model
{
    public class CustomerData
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Собственник
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Квартира
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area { get; set; }
    }
}
