using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace SQLServerAccessProviderTest.FakeDataMappers.RefBook
{
    public class FakeStreetDataMapper : BaseFakeDataMapper
    {
        #region Overrides of BaseFakeDataMapper

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public override object find(string _id)
        {
            return Db.Streets.ContainsKey(_id) ? Db.Streets[_id] : null;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            return Db.Streets.ContainsKey(_obj.ID);
        }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public override bool CanTranslate(Type domainType)
        {
            return domainType == typeof(Street);
        }

        #endregion
    }
}
