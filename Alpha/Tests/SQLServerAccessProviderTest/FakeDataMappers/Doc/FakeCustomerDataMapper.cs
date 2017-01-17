using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace SQLServerAccessProviderTest.FakeDataMappers.Doc
{
    public class FakeCustomerDataMapper : BaseFakeDataMapper, ICustomerDataMapper
    {
        #region Overrides of BaseFakeDataMapper

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public override object find(string _id)
        {
            return Db.Customers.ContainsKey(_id) ? Db.Customers[_id] : null;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            return Db.Customers.ContainsKey(_obj.ID);
        }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public override bool CanTranslate(Type domainType)
        {
            return domainType == typeof(Customer);
        }

        #endregion

        #region Implementation of ICustomerDataMapper

        public Customer GetItem(string accountNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает список абонентов по части номера аккаунта
        /// </summary>
        /// <param name="accountNumberPart">Часть номера аккаунта</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetListByAccount(string accountNumberPart)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает список абонентов по части названия улицы и части номера дома
        /// </summary>
        /// <param name="streetNamePart">Часть название улицы</param>
        /// <param name="housePart">Часть номера дома</param>
        /// <param name="ApartmentPart">Часть номера квартиры</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetList(string streetNamePart, string housePart, string ApartmentPart, bool WholeWord)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает список абонентов по части номера почтового индекса
        /// </summary>
        /// <param name="zipCodePart">Часть номера почтового индекса</param>
        /// <returns>Список абонентов</returns>
        public DataTable GetListByZipCode(string zipCodePart)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
