using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Library.Services
{
    /// <summary>
    /// Интерфейс доступа к сервису работы с доменами, умеющими работать с датамаппером
    /// </summary>
    public class DomainWithDataMapperHelperService
    {
        /// <summary>
        /// Сервис доступа к слою преобразователей данных
        /// </summary>
        virtual public IDataMapperService DatMapServ
        {
            set;
            private get;
        }

        /// <summary>
        /// Получить из БД объект домена
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <param name="_id">id объекта</param>
        /// <returns>Объект домена</returns>
        public object GetItem(Type domType, string _id)
        {
            return DataMapper(domType).find(_id);
        }

        /// <summary>
        /// Получить объект домена типа TDomain по его ID
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <param name="_id">ID объекта</param>
        /// <returns>Объект домента типа TDomain</returns>
        public TDomain GetItem<TDomain>(string _id)
        {
            return (TDomain)DataMapper<TDomain>().find(_id);
        }

        /// <summary>
        /// Сохранить объект домена в БД
        /// </summary>
        /// <param name="_domain">Объект домена</param>
        /// <returns>true, если сохранение прошло успешно, иначе - false</returns>
        public bool UpdateItem(IDomainObject _domain)
        {
            return DataMapper(_domain.GetType()).update(_domain);
        }

        /// <summary>
        /// Удалить объект домена в БД
        /// </summary>
        /// <param name="_domain">Объект домена</param>
        /// <returns>true, если удаление прошло успешно, иначе - false</returns>
        public bool DeleteItem(IDomainObject _domain)
        {
            return DataMapper(_domain.GetType()).delete(_domain.ID);
        }

        /// <summary>
        /// Получить преобразователь данных для объекта домена
        /// типа TDomain с интерфейсом TDataMapperInterface
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <typeparam name="TDataMapperInterface">Тип интерфейса преобразователя</typeparam>
        /// <returns>Преобразователь данных по типу интерфейса TDataMapperInterface</returns>
        public TDataMapperInterface DataMapper<TDomain, TDataMapperInterface>()
        {
            return (TDataMapperInterface)DatMapServ.get(typeof(TDomain));
        }

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <returns>Преобразователь данных для типа TDomain</returns>
        public IDataMapper DataMapper<TDomain>()
        {
            return DatMapServ.get(typeof(TDomain));
        }

        /// <summary>
        /// Получить преобразователь данных со стандартным интерфейсом
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Преобразователь данных для типа domType</returns>
        public IDataMapper DataMapper(Type domType)
        {
            return DatMapServ.get(domType);
        }

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList<TDomain>(object _param)
        {
            return GetList(_param, typeof(TDomain));
        }

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <param name="_param">Параметр запроса. Если параметр null, то метод вернет полный список.</param>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList(object _param, Type domType)
        {
            DataTable _res = null;

            if (_param == null)
            {
                _res = (DataTable)DataMapper(domType).doLoad();
            }
            else
            {
                _res = (DataTable)DataMapper(domType).doLoad(_param);
            }

            return _res;
        }

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <typeparam name="TDomain">Тип объекта домена</typeparam>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList<TDomain>()
        {
            return GetList<TDomain>(null);
        }

        /// <summary>
        /// Получить список элементов для домена типа TDomain
        /// </summary>
        /// <param name="domType">Тип объекта домена</param>
        /// <returns>Таблица данных (DataTable)</returns>
        public DataTable GetList(Type domType)
        {
            return GetList(null, domType);
        }
    }
}