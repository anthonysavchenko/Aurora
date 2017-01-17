namespace Taumis.Domain.Doc
{
    /// <summary>
    /// Базовый проводимый документ.
    /// </summary>
    public abstract class TransDoc : Doc, ITransDoc
    {
        #region ITransDoc Members

        private bool _transacted;
        /// <summary>
        /// Признак утвержденности документа
        /// </summary>
        public bool IsTransacted
        {
            get
            {
                Load();
                return _transacted;
            }
            set
            {
                Load();
                _transacted = value;
            }
        }

        #endregion
    }
}