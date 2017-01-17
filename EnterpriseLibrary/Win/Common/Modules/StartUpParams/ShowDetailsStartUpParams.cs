namespace Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams
{
    /// <summary>
    /// Параметры запуска юз-кейса на деталях указанного элемента
    /// </summary>
    public class ShowDetailsStartUpParams<TDomain> : AnyStartUpParams
        where TDomain : DomainObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_domainObject"></param>
        public ShowDetailsStartUpParams(TDomain _domainObject)
        {
            DomainObject = _domainObject;
        }

        /// <summary>
        /// Объект домен
        /// </summary>
        public TDomain DomainObject
        {
            get;
            set;
        }
    }
}