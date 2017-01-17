using System;
using System.Collections.Generic;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    /// <summary>
    /// Базовый класс единицы работы.
    /// </summary>
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        #region Локальные переменные.

        protected IDictionary<string, DomainObject> newObjects = new Dictionary<string, DomainObject>();
        protected IDictionary<string, DomainObject> dirtyObjects = new Dictionary<string, DomainObject>();
        protected IDictionary<string, DomainObject> removedObjects = new Dictionary<string, DomainObject>();

        protected IIdentityMap idMap = new IdentityMap();

        #endregion

        /// <summary>Получить преобразователь данных для типа домена с базовым интерфейсом</summary>
        /// <typeparam name="T">Тип домена</typeparam>
        /// <returns>Преобразователь данных типа интерфейса IDataMapper</returns>
        public IDataMapper DataMapper<TDomain>()
            where TDomain : IDomainObject
        {
            return DatMapServ.get(typeof(TDomain));
        }

        /// <summary>Получить преобразователь данных для типа домена</summary>
        /// <typeparam name="TDomain">Тип домена</typeparam>
        /// <typeparam name="TDataMapperInterface">Тип интерфейса преобразователя</typeparam>
        /// <returns>Преобразователь данных типа интерфейса TDataMapperInterface</returns>
        public TDataMapperInterface DataMapper<TDomain, TDataMapperInterface>()
            where TDomain : IDomainObject
            where TDataMapperInterface : IDataMapper
        {
            return (TDataMapperInterface)DataMapper<TDomain>();
        }

        /// <summary>
        /// Сервис доступа к слою преобразователей данных.
        /// </summary>
        virtual public IDataMapperService DatMapServ
        {
            set;
            protected get;
        }

        /// <summary>
        /// Сервис для работы с транзакциями БД
        /// </summary>
        public virtual IDbTransactionService DbTransactionService { protected get; set; }

        /// <summary>
        /// Регистрировать измененный.
        /// </summary>
        /// <param name="obj">Измененный объект.</param>
        virtual public string registerDirty(DomainObject obj)
        {
            string _res = "";

            // TODO: Переделать на: Guard.ArgumentNotNull(appraisalServiceProxy, "appraisalServiceProxy");
            if (obj.ID == null)
            {
                _res = "Ошибка регистрации измененного объекта.\n ID имеет пустое значение.";
                return _res;
            }

            if (removedObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                _res = "Ошибка регистрации измененного объекта.\n Объект зарегистрирован как удаленный.";
                return _res;
            }

            // Если есть среди новых, нужно обновить его в коллекции
            if (newObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                newObjects.Remove(GetObjectIdForUOW(obj));
                newObjects.Add(GetObjectIdForUOW(obj), obj);
            }
            else
            {
                // если нет в новых, регистрируем, удаляя если уже был в измененных
                if (dirtyObjects.ContainsKey(GetObjectIdForUOW(obj)))
                {
                    dirtyObjects.Remove(GetObjectIdForUOW(obj));
                }
                dirtyObjects.Add(GetObjectIdForUOW(obj), obj);
            }

            return _res;
        }

        /// <summary>
        /// Регистрировать удаленный;
        /// </summary>
        /// <param name="obj">Удаленный объект.</param>
        virtual public string registerRemoved(DomainObject obj)
        {
            string _res = "";

            // Объекту без ID тут делать нечего.
            if (obj.ID == null)
            {
                _res = "Ошибка регистрации удаленного объекта.\n ID имеет пустое значение.";
                return _res;
            }

            // Если новый, то выбросить на памяти из списка новых, и все.
            if (newObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                newObjects.Remove(GetObjectIdForUOW(obj));
            }
            else
            {
                // Если вертелся в измененных - выбросить оттуда.
                dirtyObjects.Remove(GetObjectIdForUOW(obj));

                // Если не числится среди удаленных - добавить.
                if (!removedObjects.ContainsKey(GetObjectIdForUOW(obj)))
                {
                    removedObjects.Add(GetObjectIdForUOW(obj), obj);
                }
                else
                {
                    _res = "Ошибка регистрации удаленного объекта.\n Объект зарегистрирован как удаленный.";
                }
            }

            return _res;
        }

        /// <summary>
        /// Регистрация загружаемого.
        /// </summary>
        /// <param name="obj"></param>
        public string registerClean(DomainObject obj)
        {
            string _res = "";

            if (obj.ID == null)
            {
                _res = "Ошибка регистрации  объекта загружаемого из базы данных.\n ID имеет пустое значение.";
                return _res;
            }

            // Добавляем в коллекцию объектов для кэширования, (IdentityMap, 216).
            idMap.Add(obj);

            return _res;
        }

        /// <summary>
        /// Регистрация нового.
        /// </summary>
        /// <param name="obj">Регистрируемый объект.</param>
        virtual public string registerNew(DomainObject obj)
        {
            string _res = "";

            if (obj.ID == null)
            {
                _res = "Ошибка регистрации нового объекта.\n ID имеет пустое значение.";
                return _res;
            }

            if (dirtyObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                _res = "Ошибка регистрации нового объекта.\n Объект зарегистрирован как измененный.";
                return _res;
            }

            if (removedObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                _res = "Ошибка регистрации нового объекта.\n Объект зарегистрирован как удаленный.";
                return _res;
            }

            if (newObjects.ContainsKey(GetObjectIdForUOW(obj)))
            {
                _res = "Ошибка регистрации нового объекта.\n Объект уже зарегистрирован.";
                return _res;
            }

            newObjects.Add(GetObjectIdForUOW(obj), obj);

            return _res;
        }

        /// <summary>
        /// Очистить все изменения единицы работы.
        /// </summary>
        public void Clear()
        {
            removedObjects.Clear();
            dirtyObjects.Clear();
            newObjects.Clear();
        }

        /// <summary>
        /// Удалить удалённые.
        /// </summary>
        virtual protected bool deleteRemoved()
        {
            bool _res = true;

            IDataMapper dm;

            foreach (KeyValuePair<string, DomainObject> domObj in removedObjects)
            {
                // TODO: Преобразователь данных находит объект базы данных,
                // стартует и коммитит транзакцию пакетного удаления данных.
                // Цикл нужно спрятать внутрь преобразователя стартующего
                // и откатывающего транзакцию пакетной обработки удаления объектов.

                // Получим тип объекта домена.
                Type _domType = domObj.Value.GetType();

                // Получим конкретный преобразователь данных для заданного типа.
                dm = DatMapServ.get(_domType);
                if (dm != null)
                {
                    if (!dm.delete(domObj.Value.ID))
                    {
                        _res = false;
                        break;
                    }
                }
            }

            return _res;
        }

        /// <summary>
        /// Обновить изменённые.
        /// </summary>
        virtual protected bool updateDirty()
        {
            bool _res = true;

            IDataMapper dm;

            foreach (KeyValuePair<string, DomainObject> domObj in dirtyObjects)
            {
                // TODO: Преобразователь данных находит объект базы данных,
                // Транслятор транслирует данные, Преобразователь данных 
                // стартует и коммитит транзакцию пакетного изменения данных.
                // Цикл нужно спрятать внутрь преобразователя стартующего
                // и откатывающего транзакцию пакетной обработки обновления объектов.
                // MapperListService.getMapper(domObj.GetType()).update(domObj);
                dm = DatMapServ.get(domObj.Value.GetType());
                if (dm != null)
                {
                    if (!dm.update(domObj.Value))
                    {
                        _res = false;
                        break;
                    }
                }
            }

            return _res;
        }

        /// <summary>
        /// Добавить новые.
        /// </summary>
        virtual protected bool insertNew()
        {
            bool _res = true;

            IDataMapper dm;

            foreach (KeyValuePair<string, DomainObject> domObj in newObjects)
            {
                dm = DatMapServ.get(domObj.Value.GetType());
                if (dm != null)
                {
                    if (!dm.update(domObj.Value))
                    {
                        _res = false;
                        break;
                    }
                }
            }

            return _res;
        }

        /// <summary>
        /// Получить объект домена из кэша.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DomainObject get(string id)
        {
            DomainObject resObj = null;
            resObj = (DomainObject)idMap.Get(id);
            return resObj;
        }


        /// <summary>
        /// Запуск транзакции изменения базы данных.
        /// </summary>
        virtual public bool commit()
        {
            bool _res = false;

            //DbTransactionService.BeginTransaction();

            try
            {
                // Добавить новые.
                if (!insertNew())
                {
                    throw new Exception("Ошибка в insertNew()");
                }

                // Обновить измененные.
                if (!updateDirty())
                {
                    throw new Exception("Ошибка в updateDirty()");
                }

                // Удалить удаленные.
                if (!deleteRemoved())
                {
                    throw new Exception("Ошибка в deleteRemoved()");
                }

                // Утверждаем транзакцию.
                //DbTransactionService.Commit();

                // Очищаем списки только по факту успешного завершения всей бизнес-транзакции.
                Clear();

                _res = true;
            }
            catch (Exception eInsert)
            {
                // Выводим в лог информацию об ошибке
                Logger.Write("Ошибка в BaseUnitOfWork::commit()");
                Logger.Write(eInsert.Message);
                Logger.Write(eInsert.StackTrace);

                //DbTransactionService.Rollback();
            }

            return _res;
        }

        /// <summary>
        /// Создает уникальный id, используемый в UOW, для объекта домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Уникальный id</returns>
        protected string GetObjectIdForUOW(DomainObject obj)
        {
            return obj.ID + "@" + obj.GetType().ToString();
        }

        #region ITestUnitOfWork Members

        public IDictionary<string, DomainObject> New
        {
            get { return newObjects; }
        }

        public IDictionary<string, DomainObject> Dirty
        {
            get { return dirtyObjects; }
        }

        public IDictionary<string, DomainObject> Removed
        {
            get { return removedObjects; }
        }

        public IIdentityMap Clean
        {
            get { return idMap; }
        }

        #endregion
    }
}
