using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ChargeSets;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.ChargeSet;
using DomUser = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    /// <summary>
    /// Преобразователь данных для наборов начислений
    /// </summary>
    public class ChargeSetDataMapper : BaseDataMapper<DomItem, DBItem>, IChargeSetDataMapper
    {
        #region Overrides of BaseDataMapper<ChargeSet,ChargeSets>

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.ChargeSets.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

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
                    _entities.AddToChargeSets(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ChargeSets.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Number = domObj.Number;
                _dbItem.Quantity = domObj.Quantity;
                _dbItem.ValueSum = domObj.ValueSum;
                _dbItem.Period = domObj.Period;

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
                    _entities.ChargeSets
                        .Include("Author")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Number = _dbItem.Number;
                _domItem.Quantity = _dbItem.Quantity;
                _domItem.ValueSum = _dbItem.ValueSum;
                _domItem.Period = _dbItem.Period;
                _domItem.Author =
                    (DomUser)DataMapperService.get(typeof(DomUser)).find(_dbItem.Author.ID.ToString());
            }

            return _domItem;
        }

        #endregion

        private DataTable CreateDataTableForChargesModule()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("TypeAka", typeof(string));
            _table.Columns.Add("CreationDateTime", typeof(DateTime));
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Quantity", typeof(int));
            _table.Columns.Add("ValueSum", typeof(decimal));
            _table.Columns.Add("Comment", typeof(string));
            _table.Columns.Add("Period", typeof(DateTime));
            _table.Columns.Add("AuthorAka", typeof(string));

            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            return _table;
        }

        #region Implementation IChargeSetDataMapper

        /// <summary>
        /// Возвращает таблицу наборов платежей за определенный период
        /// </summary>
        /// <param name="since">Начальная дата периода</param>
        /// <param name="till">Конечная дата перида</param>
        /// <returns>Таблица с данными наборо платежей за период</returns>
        public DataTable GetList(DateTime since, DateTime till)
        {
            DataTable _table = CreateDataTableForChargesModule();

            using (Entities _entities = new Entities())
            {
                var _result =
                    _entities.ChargeSets
                        .Where(c => c.CreationDateTime >= since && c.CreationDateTime <= till)
                        .Select(
                            c =>
                            new
                            {
                                c.ID,
                                IsRecharge = false,
                                c.CreationDateTime,
                                c.Number,
                                c.Quantity,
                                c.ValueSum,
                                Comment = "",
                                c.Period,
                                AuthorAka = c.Author.Aka
                            })
                        .Concat(
                            _entities.RechargeSets
                                .Where(c => c.CreationDateTime >= since && c.CreationDateTime <= till)
                                .Select(
                                    c =>
                                    new
                                    {
                                        c.ID,
                                        IsRecharge = true,
                                        c.CreationDateTime,
                                        c.Number,
                                        c.Quantity,
                                        c.ValueSum,
                                        c.Comment,
                                        c.Period,
                                        AuthorAka = c.Author.Aka
                                    }))
                        .OrderBy(c => c.CreationDateTime);

                foreach (var _set in _result)
                {
                    _table.Rows.Add(
                        string.Format("{0}_{1}", _set.IsRecharge ? "2" : "1", _set.ID),
                        _set.IsRecharge ? "Дополнительные" : "Ежемесячные",
                        _set.CreationDateTime,
                        _set.Number,
                        _set.Quantity,
                        Math.Abs(_set.ValueSum),
                        _set.Comment,
                        _set.Period,
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
                if (_entities.ChargeSets.Any())
                {
                    _res = _entities.ChargeSets.Max(p => p.Number) + 1;
                }
            }

            return _res;
        }

        #endregion
    }
}