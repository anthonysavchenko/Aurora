using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Win;

namespace SQLServerAccessProviderTest.FakeDataMappers.Oper
{
    public class FakePaymentOperPosDataMapper : BaseFakeDataMapper, IPaymentOperPosDataMapper
    {
        #region Overrides of BaseFakeDataMapper

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public override object find(string _id)
        {
            return Db.PaymentOperPoses.ContainsKey(_id) ? Db.PaymentOperPoses[_id] : null;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            return Db.PaymentOperPoses.ContainsKey(_obj.ID);
        }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public override bool CanTranslate(Type domainType)
        {
            return domainType == typeof(PaymentOperPos);
        }

        #endregion

        public DataTable GetList(string paymentOperId)
        {
            DataTable _table = new DataTable("PaymentOperPoses");
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("Value", typeof(Decimal));
            _table.Columns.Add("ServiceName", typeof(string));
            _table.Columns.Add("ServiceTypeName", typeof(string));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            var _data = Db.PaymentOperPoses.Values.Where(pos => pos.PaymentOper.ID == paymentOperId);

            foreach (PaymentOperPos _pos in _data)
            {
                _table.Rows.Add(
                        _pos.ID,
                        _pos.Period,
                        _pos.Value,
                        _pos.Service.Name,
                        _pos.Service.ServiceType.Name);
            }

            return _table;
        }
    }
}
