using System;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects.DataClasses;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider
{
    /// <summary>
    /// Базовый реобразователь данных
    /// </summary>
    public abstract class BaseDataMapper<TDomEntity, TDBEntity> : IDataMapper
        where TDomEntity : IDomainObject
        where TDBEntity : EntityObject
    {
        /// <summary>
        /// Таймаут для запросов к БД
        /// </summary>
        protected const int DEFAULT_TIMEOUT = 30000;

        /// <summary>
        /// Стандартная строка SQL-запроса
        /// </summary>
        /// <returns>Строка SQL-запроса</returns>
        protected virtual string selectAllStatement()
        {
            throw new NotImplementedException("Не переопределен запрос по умолчанию.");
        }

        /// <summary>
        /// Получить ID по значению атрибута с уникальным значением.
        /// </summary>
        /// <param name="_sql">Текст SQL - запроса</param>
        /// <returns>ID искомого объекта</returns>
        protected string GetIdByFieldValue(string _sql)
        {
            DataTable _table = ExecuteQuery(_sql);

            return _table.Rows.Count != 0 ? _table.Rows[0]["ID"].ToString() : string.Empty;
        }

        #region IDataMapper Members

        /// <summary>
        /// Удалить элемент
        /// </summary>
        /// <param name="id">ID объекта домена</param>
        /// <returns>Результат удаления</returns>
        public virtual bool delete(string id)
        {
            int _id;
            bool _result = int.TryParse(id, out _id);
            if (_result)
            {
                string _typeName = typeof(TDBEntity).Name;
                EntityKey _key =
                    new EntityKey($"Entities.{_typeName}", "ID", int.Parse(id));
                try
                {
                    using (Entities _entities = new Entities())
                    {
                        var _entity = _entities.GetObjectByKey(_key);
                        _entities.DeleteObject(_entity);
                        _entities.SaveChanges();
                    }
                }
                catch(Exception _ex)
                {
                    Logger.SimpleWrite($"Delete domain error: type - {_typeName}, id = {id}. Exception: {_ex}");
                    _result = false;
                }
            }

            return _result;
        }

        /// <summary>
        /// Обновить элемент
        /// </summary>
        /// <param name="_domainObject">Объект домена</param>
        /// <returns>Результат обновления</returns>
        public virtual bool update(IDomainObject _domainObject)
        {
            bool _result = true;

            if (_domainObject.IsAllowedToSave())
            {
                try
                {
                    BusinessToService((TDomEntity)_domainObject);
                    _domainObject.SetLoadedStatusAfterSave();
                }
                catch (Exception _ex)
                {
                    Logger.SimpleWrite($"Update domain error: type - {typeof(TDBEntity).Name}, id = {_domainObject.ID}. Exception: {_ex}");
                    throw _ex;
                }
            }

            return _result;
        }

        /// <summary>
        /// Загрузить набор данных
        /// </summary>
        /// <param name="_param">Параметры</param>
        /// <returns>Набор данных (DataTable)</returns>
        public virtual object doLoad(object _param)
        {
            throw new Exception("Не переопределен: object doLoad(object param)");
        }

        /// <summary>
        /// Загрузить набор данных (без параметра)
        /// </summary>
        /// <returns>Набор данных (DataTable)</returns>
        public virtual object doLoad()
        {
            return ExecuteQuery(selectAllStatement());
        }

        /// <summary>
        /// Поиск и загрузка объекта (на основе фиктивного). Возвращает реальный объект
        /// </summary>
        /// <param name="_obj">Фиктивный объект домена</param>
        /// <returns>Найденный в БД объект, преобразованный в вид домена
        /// или null, если объект не найден.</returns>
        public virtual IDomainObject findReal(IDomainObject _obj)
        {
            // TODO: Сначала проверить в коллекции объектов кэша (Identity Map, 216).
            return ServiceToBusiness(_obj);
        }

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="_id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public virtual object find(string _id)
        {
            // TODO: Сначала проверить в коллекции объектов кэша (Identity Map, 216).
            // Создаем фиктивный объект
            return CreateGhost(_id);
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public abstract bool checkExistance(IDomainObject _obj);

        /// <summary>
        /// Создает фиктивный объект домена
        /// </summary>
        /// <param name="_id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        private TDomEntity CreateGhost(string _id)
        {
            // Создаем объект
            TDomEntity _res = (TDomEntity)Activator.CreateInstance(typeof(TDomEntity));

            // Отмечаем его фиктивным
            _res.SetGhostStatus(_id);

            return _res;
        }

        #endregion

        #region IDataMapperService Attribute

        /// <summary>
        /// Атрибут сервиса доступа к базе данных
        /// </summary>
        public IDataMapperService DataMapperService { get; set; }

        #endregion

        /// <summary>Выполнить запрос к базе данных</summary>
        /// <param name="_query">Запрос</param>
        /// <returns>Таблица</returns>
        protected DataTable ExecuteQuery(string _query)
        {
            return ExecuteQuery(_query, DEFAULT_TIMEOUT);
        }

        /// <summary>
        /// Выполнить запрос к базе данных
        /// </summary>
        /// <param name="_query">Запрос</param>
        /// <param name="_timeout">timeout выполнения запроса в милисекундах</param>
        /// <returns>Таблица</returns>
        protected DataTable ExecuteQuery(string _queryString, int _timeout)
        {
            DataTable _table = new DataTable("QueryTable");
            _table.Columns.Add("ID", typeof(string));

            using (Entities _entities = new Entities())
            {
                var _query = _entities.CreateQuery<TDBEntity>(_queryString);

                foreach (TDBEntity _entity in _query)
                {
                    _table.Rows.Add(_entity.EntityKey);
                }
            }

            return _table;
        }

        /// <summary>
        /// Найти и создать шаблонный домен по запросу
        /// </summary>
        /// <param name="_query">Запрос, возвращающий таблицу с уникальной колонкой ID</param>
        /// <param name="_insureExist">
        /// Флаг генерации исключения при отсутствии домена с найденным по запросу идентификатором
        /// в базе данных
        /// </param>
        /// <returns>Домен объекта, реально существующего в базе данных</returns>
        protected TDomain ExecuteQueryAndFind<TDomain>(string _query, bool _insureExist)
        {
            TDomain _domain = default(TDomain);

            try
            {
                DataTable _table = ExecuteQuery(_query);
                if (_table.Rows.Count > 0)
                {
                    IDataMapper _dm = DataMapperService.get(typeof(TDomain));
                    _domain = (TDomain)_dm.find(_table.Rows[0]["ID"].ToString());
                }

                if (_insureExist && _domain == null)
                {
                    throw new Exception("Объекта с идентификатором из запроса нет в базе данных");
                }
            }
            catch (Exception _exception)
            {
                throw new Exception("Ошибка поиска объекта", _exception);
            }

            return _domain;
        }

        /// <summary>
        /// Найти и создать домен датамаппера по запросу
        /// </summary>
        /// <param name="_query">Запрос, возвращающий таблицу с уникальной колонкой ID</param>
        /// <param name="_insureExist">
        /// Флаг генерации исключения при отсутствии домена с найденным по запросу идентификатором
        /// в базе данных
        /// </param>
        /// <returns>Домен объекта, реально существующего в базе данных</returns>
        protected TDomEntity ExecuteQueryAndFind(string _query, bool _insureExist)
        {
            return ExecuteQueryAndFind<TDomEntity>(_query, _insureExist);
        }

        /// <summary>
        /// Проверка на возможность преобразования бизнес-сущности типа businessType.
        /// </summary>
        /// <param name="businessType">Тип бизнес-объекта.</param>
        /// <returns></returns>
        public bool CanTranslate(Type businessType)
        {
            return (businessType == typeof(TDomEntity));
        }

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected abstract TDBEntity BusinessToService(TDomEntity domObj);

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected abstract TDomEntity ServiceToBusiness(IDomainObject obj);
    }
}