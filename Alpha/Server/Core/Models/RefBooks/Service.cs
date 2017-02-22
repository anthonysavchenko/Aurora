namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class Service : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public byte ChargeRule { get; set; }
        /// <summary>
        /// Норматив
        /// </summary>
        public decimal? Norm { get; set; }

        /// <summary>
        /// Единица измерения норматива
        /// </summary>
        public string Measure { get; set; }

        public int ServiceTypeID { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}