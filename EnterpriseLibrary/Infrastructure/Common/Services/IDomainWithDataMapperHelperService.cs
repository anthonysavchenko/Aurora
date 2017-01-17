using System;
using System.Data;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Интерфейс доступа к сервису работы с доменами, умеющими работать с датамаппером
    /// </summary>
    public interface IDomainWithDataMapperHelperService
    {
        /// <summary>
        /// Получить из БД объект домена
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <param name="_id">id объекта</param>
        /// <returns>Объект домена</returns>
        object GetItem(Type domType, string _id);

        /// <summary>
        /// Получить объект домена типа TDomain по его ID
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <param name="_id">ID объекта</param>
        /// <returns>Объект домента типа TDomain</returns>
        TDomain GetItem<TDomain>(string _id);

        /// <summary>
        /// Сохранить объект домена в БД
        /// </summary>
        /// <param name="_domain">Объект домена</param>
        /// <returns>true, если сохранение прошло успешно, иначе - false</returns>
        bool UpdateItem(IDomainObject _domain);

        /// <summary>
        /// Удалить объект домена в БД
        /// </summary>
        /// <param name="_domain">Объект домена</param>
        /// <returns>true, если удаление прошло успешно, иначе - false</returns>
        bool DeleteItem(IDomainObject _domain);

        /// <summary>
        /// Получить преобразователь данных для объекта домена
        /// типа TDomain с интерфейсом TDataMapperInterface
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <typeparam name="TDataMapperInterface">Тип интерфейса преобразователя</typeparam>
        /// <returns>Преобразователь данных по типу интерфейса TDataMapperInterface</returns>
        TDataMapperInterface DataMapper<TDomain, TDataMapperInterface>();

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <returns>Преобразователь данных для типа TDomain</returns>
        IDataMapper DataMapper<TDomain>();

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Преобразователь данных для типа domType</returns>
        IDataMapper DataMapper(Type domType);

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <returns>Таблица данных (DataTable)</returns>
        DataTable GetList<TDomain>(object _param);

        /// <summary>
        /// Получить список элементов для домена типа domType
        /// </summary>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Таблица данных (DataTable)</returns>
        DataTable GetList(object _param, Type domType);

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <returns>Таблица данных (DataTable)</returns>
        DataTable GetList<TDomain>();

        /// <summary>
        /// Получить список элементов для домена типа domType
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Таблица данных (DataTable)</returns>
        DataTable GetList(Type domType);
    }
}