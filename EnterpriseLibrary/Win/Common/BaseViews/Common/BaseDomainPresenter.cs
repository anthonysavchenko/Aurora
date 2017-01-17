using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    /// <summary>
    /// Базовый презентер, работающий с доменом
    /// </summary>
    /// <typeparam name="TView">Тип вью</typeparam>
    public class BaseDomainPresenter<TView> : BasePresenter<TView>, IBaseDomainPresenter
        where TView : IBaseView
    {
        /// <summary>
        /// Сервис работы с доменами, умеющими работать с датамаппером
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainWithDataMapperHelperServ
        {
            set;
            private get;
        }

        /// <summary>
        /// Получить из БД объект домена
        /// </summary>
        /// <param name="_domType">Тип объекта домена</param>
        /// <param name="_id">id объекта</param>
        /// <returns>Объект домена</returns>
        public object GetItem(Type _domType, string _id)
        {
            return DomainWithDataMapperHelperServ.GetItem(_domType, _id);
        }

        /// <summary>
        /// Получить объект домена типа T по его ID
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <param name="_id">ID объекта</param>
        /// <returns>Объект домента типа Т</returns>
        public T GetItem<T>(string _id)
            where T : DomainObject
        {
            return DomainWithDataMapperHelperServ.GetItem<T>(_id);
        }

        /// <summary>
        /// Сохранить объект домена в БД
        /// </summary>
        /// <param name="_domain">Объект домена</param>
        /// <returns>true, если сохранение прошло успешно, иначе - false</returns>
        public bool UpdateItem(IDomainObject _domain)
        {
            return DomainWithDataMapperHelperServ.UpdateItem(_domain);
        }

        /// <summary>
        /// Получить преобразователь данных для объекта домена
        /// типа Т с интерфейсом T1
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <typeparam name="T1">Тип интерфейса преобразователя</typeparam>
        /// <returns>Преобразователь данных по типу интерфейса T1</returns>
        public T1 DataMapper<T, T1>()
            where T : DomainObject
            where T1 : IDataMapper
        {
            return DomainWithDataMapperHelperServ.DataMapper<T, T1>();
        }

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <returns>Преобразователь данных для типа T</returns>
        public IDataMapper DataMapper<T>()
            where T : DomainObject
        {
            return DomainWithDataMapperHelperServ.DataMapper<T>();
        }

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом по типу
        /// </summary>
        /// <param name="domType">Тип объекта домена</typeparam>
        /// <returns>Преобразователь данных для типа</returns>
        public IDataMapper DataMapper(Type domType)
        {
            return DomainWithDataMapperHelperServ.DataMapper(domType);
        }

        /// <summary>
        /// Получить полный список элементов для домена типа T
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList<T>()
            where T : DomainObject
        {
            return DomainWithDataMapperHelperServ.GetList<T>();
        }

        /// <summary>
        /// Получить список элементов для домена типа T с параметром
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList<T>(object _param)
            where T : DomainObject
        {
            return DomainWithDataMapperHelperServ.GetList<T>(_param);
        }

        /// <summary>
        /// Получить полный список элементов для домена типа T
        /// </summary>
        /// <param name="_type">Тип объекта домена</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList(Type _type)
        {
            return DomainWithDataMapperHelperServ.GetList(_type);
        }

        /// <summary>
        /// Получить полный список элементов для домена типа T с параметром
        /// </summary>
        /// <param name="_type">Тип объекта домена</param>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList(Type _type, object _param)
        {
            return DomainWithDataMapperHelperServ.GetList(_param, _type);
        }
    }
}