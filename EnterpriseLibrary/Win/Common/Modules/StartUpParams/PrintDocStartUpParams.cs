namespace Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams
{
    /// <summary>
    /// Параметры запуска модуля для печати документа
    /// </summary>
    public class PrintDocStartUpParams : AnyStartUpParams
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="docId">Идентификатор документа</param>
        public PrintDocStartUpParams(string docId)
        {
            DocId = docId;
        }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public string DocId
        {
            private set;
            get;
        }
    }
}
