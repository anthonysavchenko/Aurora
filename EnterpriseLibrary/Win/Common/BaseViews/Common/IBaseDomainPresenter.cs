using System;


namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    /// <summary>
    /// Интерфейс базового презентера, работающего с доменом
    /// </summary>
    public interface IBaseDomainPresenter : IBasePresenter
    {
        /// <summary>
        /// Получить из БД объект домена
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <param name="_id">id объекта</param>
        /// <returns>Объект домена</returns>
        object GetItem(Type domType, string _id);

        /// <summary>
        /// Получить объект домена типа T по его ID
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <param name="_id">ID объекта</param>
        /// <returns>Объект домента типа Т</returns>
        T GetItem<T>(string _id)
            where T : DomainObject;
    }
}