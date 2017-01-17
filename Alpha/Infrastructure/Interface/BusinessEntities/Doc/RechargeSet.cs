namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc
{
    /// <summary>
    /// Набор дополнительных начислений
    /// </summary>
    public class RechargeSet : ChargeSet
    {
        private string _comment;
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get
            {
                Load();
                return _comment;
            }
            set
            {
                Load();
                _comment = value;
            }
        }
    }
}