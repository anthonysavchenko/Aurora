using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace SQLServerAccessProviderTest.FakeDataMappers.Doc
{
    public class FakePaymentSetDataMapper : BaseFakeDataMapper, IPaymentSetDataMapper
    {
        #region Overrides of BaseFakeDataMapper

        /// <summary>
        /// Поиск и загрузка объекта по заданному Id. Возвращает фиктивный объект
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Фиктивный объект домена</returns>
        public override object find(string _id)
        {
            return Db.PaymentSets.ContainsKey(_id) ? Db.PaymentSets[_id] : null;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="_obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject _obj)
        {
            return Db.PaymentSets.ContainsKey(_obj.ID);
        }

        /// <summary>
        /// Проверяет возможность работы датамапера с доменом переданного типа
        /// </summary>
        /// <param name="domainType">Тип домена</param>
        /// <returns>true - данный датамаппер может работать с доменом переданного типа, false в противном случае</returns>
        public override bool CanTranslate(Type domainType)
        {
            return domainType == typeof(PaymentSet);
        }

        #endregion

        public DataTable GetList(DateTime since, DateTime till)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("TypeAka", typeof(string));
            _table.Columns.Add("CreationDateTime", typeof(DateTime));
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Intermediary", typeof(string));
            _table.Columns.Add("Quantity", typeof(short));
            _table.Columns.Add("ValueSum", typeof(decimal));
            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            var _data = Db.PaymentSets.Values.Where(p => p.CreationDateTime >= since && p.CreationDateTime <= till);

            foreach (PaymentSet _paymentSet in _data)
            {
                _table.Rows.Add(
                    _paymentSet.ID,
                    _paymentSet.IsFile ? "Файл" : "Пачка",
                    _paymentSet.CreationDateTime,
                    _paymentSet.Number,
                    _paymentSet.Intermediary.Name,
                    _paymentSet.Quantity,
                    _paymentSet.ValueSum);
            }

            return _table;
        }

        /// <summary>
        /// Возвращает следующий номер документа
        /// </summary>
        /// <returns>Следующий номер документа</returns>
        public int GetNextNumber()
        {
            int _res = 1;

            if (Db.PaymentSets.Count() > 0)
            {
                _res = Db.PaymentSets.Last().Value.Number + 1;
            }

            return _res;
        }
    }
}
