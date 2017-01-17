using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.EnterpriseLibrary.Win;

namespace SQLServerAccessProviderTest.FakeDataMappers.Oper
{
    public class FakePaymentOperDataMapper : BaseFakeDataMapper, IPaymentOperDataMapper
    {
        #region Overrides of BaseFakeDataMapper

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public override object find(string _id)
        {
            return Db.PaymentOpers.ContainsKey(_id) ? Db.PaymentOpers[_id] : null;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            return Db.PaymentOpers.ContainsKey(_obj.ID);
        }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public override bool CanTranslate(Type domainType)
        {
            return domainType == typeof(PaymentOper);
        }

        #endregion

        public DataTable GetList(string paymentSetId)
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("AccountNumber", typeof(string));
            _table.Columns.Add("Value", typeof(decimal));
            _table.Columns.Add("IsCorrected", typeof(bool));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            var _data = Db.PaymentOpers.Values.Where(p => p.PaymentSet.ID == paymentSetId);

            foreach (PaymentOper _oper in _data)
            {
                _table.Rows.Add(
                    _oper.ID,
                    _oper.PaymentPeriod,
                    _oper.Customer.Account,
                    _oper.Value,
                    _oper.PaymentCorrection != null);
            }

            return _table;
        }
    }
}
