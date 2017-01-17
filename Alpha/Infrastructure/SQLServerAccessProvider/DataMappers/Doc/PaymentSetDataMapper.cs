using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.PaymentSets;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.PaymentSet;
using DomItermediary = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Intermediary;
using DomUser = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class PaymentSetDataMapper : BaseDataMapper<DomItem, DBItem>, IPaymentSetDataMapper
    {
        #region Overrides of BaseDataMapper<PaymentSet,PaymentSets>

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToPaymentSets(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.PaymentSets.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.PaymentDate = domObj.PaymentDate;
                _dbItem.Number = domObj.Number;
                _dbItem.IsFile = domObj.IsFile;
                _dbItem.Quantity = domObj.Quantity;
                _dbItem.ValueSum = domObj.ValueSum;
                _dbItem.Comment = domObj.Comment;

                if (domObj.Intermediary != null)
                {
                    int _intermediaryId = Convert.ToInt32(domObj.Intermediary.ID);
                    _dbItem.Intermediaries = _entities.Intermediaries.FirstOrDefault(i => i.ID == _intermediaryId);
                }
                else
                {
                    _dbItem.Intermediaries = null;
                }

                int _authorId = int.Parse(domObj.Author.ID);
                _dbItem.Author = _entities.Users.First(u => u.ID == _authorId);

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(_domItem.ID);
            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.PaymentSets
                        .Include("Intermediaries")
                        .Include("Author")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.PaymentDate = _dbItem.PaymentDate;
                _domItem.Number = _dbItem.Number;
                _domItem.IsFile = _dbItem.IsFile;
                _domItem.Quantity = _dbItem.Quantity;
                _domItem.ValueSum = _dbItem.ValueSum;
                _domItem.Comment = _dbItem.Comment;
                _domItem.Intermediary =
                    _dbItem.Intermediaries != null
                        ? (DomItermediary)DataMapperService.get(typeof(DomItermediary)).find(_dbItem.Intermediaries.ID.ToString())
                        : null;
                _domItem.Author =
                    (DomUser)DataMapperService.get(typeof(DomUser)).find(_dbItem.Author.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.PaymentSets.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

        /// <summary>
        /// Возвращает таблицу наборов платежей за определенный период
        /// </summary>
        /// <param name="since">Начальная дата периода</param>
        /// <param name="till">Конечная дата перида</param>
        /// <returns>Таблица с данными наборо платежей за период</returns>
        public DataTable GetList(DateTime since, DateTime till)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("TypeAka", typeof(string));
            _table.Columns.Add("CreationDateTime", typeof(DateTime));
            _table.Columns.Add("PaymentDate", typeof(DateTime));
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Intermediary", typeof(string));
            _table.Columns.Add("QuantityWoCorrection", typeof(short));
            _table.Columns.Add("ValueSumWoCorrection", typeof(decimal));
            _table.Columns.Add("Quantity", typeof(short));
            _table.Columns.Add("ValueSum", typeof(decimal));
            _table.Columns.Add("Comment", typeof(string));
            _table.Columns.Add("AuthorAka", typeof(string));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            using (Entities _db = new Entities())
            {
                var _paymentSets =
                    _db.PaymentSets
                        .Where(p => p.CreationDateTime >= since && p.CreationDateTime <= till)
                        .Select(
                            ps =>
                            new
                            {
                                ps.ID,
                                ps.IsFile,
                                ps.CreationDateTime,
                                ps.PaymentDate,
                                ps.Number,
                                IntermediaryName = ps.Intermediaries.Name,
                                ps.Quantity,
                                ps.ValueSum,
                                ps.Comment,
                                AuthorAka = ps.Author.Aka
                            })
                        .ToList();

                List<int> _ids = _paymentSets.Select(p => p.ID).ToList();

                var _operInfoDictionary =
                    _db.PaymentOpers
                        .Where(p => _ids.Contains(p.PaymentSets.ID) && p.PaymentCorrectionOper == null)
                        .GroupBy(p => p.PaymentSets.ID)
                        .Select(g =>
                            new
                            {
                                PaymentSetID = g.Key,
                                woCorrQuantity = g.Count(),
                                woCorrValueSum = g.Sum(p => p.Value)
                            })
                        .ToDictionary(r => r.PaymentSetID);

                int _woCorrQuantity;
                decimal _woCorrValueSum;

                foreach (var _set in _paymentSets)
                {
                    if (_operInfoDictionary.ContainsKey(_set.ID))
                    {
                        var _operInfo = _operInfoDictionary[_set.ID];
                        _woCorrQuantity = _operInfo.woCorrQuantity;
                        _woCorrValueSum = _operInfo.woCorrValueSum;
                    }
                    else
                    {
                        _woCorrQuantity = 0;
                        _woCorrValueSum = 0;
                    }

                    _table.Rows.Add(
                        _set.ID.ToString(),
                        _set.IsFile ? "Файл" : "Пачка",
                        _set.CreationDateTime,
                        _set.PaymentDate,
                        _set.Number,
                        _set.IntermediaryName,
                        _woCorrQuantity,
                        Math.Abs(_woCorrValueSum),
                        _set.Quantity,
                        Math.Abs(_set.ValueSum),
                        _set.Comment,
                        _set.AuthorAka);
                }
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
            using (Entities _entities = new Entities())
            {
                if (_entities.PaymentSets.Any())
                {
                    _res = _entities.PaymentSets.Max(p => p.Number) + 1;
                }
            }

            return _res;
        }
    }
}