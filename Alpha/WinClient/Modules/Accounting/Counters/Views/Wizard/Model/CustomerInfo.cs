namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Model
{
    public class CustomerInfo
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// ФИО абонента
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// ID абонента
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Квартира
        /// </summary>
        public string Apartment { get; set; }

        /// <summary>
        /// Площадь квартиры
        /// </summary>
        public decimal Area { get; set; }
    }
}
