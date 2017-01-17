using System;

using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win
{
    /// <summary>
    /// Интерфейс преобразователя данных. (Data Mapper, 187).
    /// </summary>
    public interface IDataMapper
    {
        /// <summary>
        /// Создать / сохранить элемент.
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        bool update(IDomainObject _obj);

        /// <summary>
        /// Удалить элемент.
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        bool delete(string _id);

        /// <summary>
        /// Загрузка списка без параметров.
        /// </summary>
        /// <returns></returns>
        object doLoad();

        /// <summary>
        /// Загрузка списка с параметрами.
        /// </summary>
        /// <param name="param">Объект - параметр.</param>
        /// <returns></returns>
        object doLoad(object param);

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        object find(string _id);

        /// <summary>
        /// Поиск и загрузка объекта (на основе фиктивного). Возвращает реальный объект
        /// </summary>
        /// <param name="_obj">Фиктивный объект домена</param>
        /// <returns>Найденный в БД объект, преобразованный в вид домена
        /// или null, если объект не найден.</returns>
        IDomainObject findReal(IDomainObject _obj);

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        bool checkExistance(IDomainObject _obj);

        /// <summary>
        /// Сервис возвращает преобразователи данных на заказ
        /// по типу объекта домена.
        /// </summary>
        IDataMapperService DataMapperService { get; set; }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        bool CanTranslate(Type domainType);
    }
}