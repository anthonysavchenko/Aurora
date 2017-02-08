namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    /// <summary>
    /// Общедомовое имущество
    /// </summary>
    public class PublicPlace : Entity
    {
        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// ID дома
        /// </summary>
        public int BuildingID { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public Building Building { get; set; }

        /// <summary>
        /// ID услуги
        /// </summary>
        public int ServiceID { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public Service Service { get; set; }
    }
}
