using System;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace SQLServerAccessProviderTest.FakeDataMappers
{
    public abstract class BaseFakeDataMapper : IDataMapper
    {
        protected InMemoryDatabase Db { get; private set; }

        protected BaseFakeDataMapper()
        {
            Db = new InMemoryDatabase();
        }

        #region Implementation of IDataMapper

        /// <summary>
        /// Создать / сохранить элемент.
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        public bool update(IDomainObject _obj)
        {
            return true;
        }

        /// <summary>
        /// Удалить элемент.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public bool delete(string _id)
        {
            return true;
        }

        /// <summary>
        /// Загрузка списка без параметров.
        /// </summary>
        /// <returns></returns>
        public object doLoad()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Загрузка списка с параметрами.
        /// </summary>
        /// <param name="param">Объект - параметр.</param>
        /// <returns></returns>
        public object doLoad(object param)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public abstract object find(string _id);

        /// <summary>
        /// Поиск и загрузка объекта (на основе фиктивного). Возвращает реальный объект
        /// </summary>
        /// <param name="_obj">Фиктивный объект домена</param>
        /// <returns>Найденный в БД объект, преобразованный в вид домена
        /// или null, если объект не найден.</returns>
        public IDomainObject findReal(IDomainObject _obj)
        {
            return _obj;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public abstract bool checkExistance(IDomainObject _obj);

        /// <summary>
        /// Сервис возвращает преобразователи данных на заказ
        /// по типу объекта домена.
        /// </summary>
        public IDataMapperService DataMapperService { get; set; }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public abstract bool CanTranslate(Type domainType);

        #endregion
    }
}
