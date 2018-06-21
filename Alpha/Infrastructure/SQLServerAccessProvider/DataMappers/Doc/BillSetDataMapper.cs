using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BillSets;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BillSet;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class BillSetDataMapper : BaseDataMapper<DomItem, DBItem>, IBillSetDataMapper
    {
        #region Overrides of BaseDataMapper<BillSet,BillSets>

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
                    _entities.AddToBillSets(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BillSets.First(x => x.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Number = domObj.Number;
                _dbItem.BillType = (byte)domObj.BillType;
                _dbItem.Quantity = domObj.Quantity;
                _dbItem.ValueSum = domObj.ValueSum;

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
                DBItem _dbItem = _entities.BillSets.First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Number = _dbItem.Number;
                _domItem.BillType = (BillType)_dbItem.BillType;
                _domItem.Quantity = _dbItem.Quantity;
                _domItem.ValueSum = _dbItem.ValueSum;
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.BillSets.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        #endregion

        /// <summary>
        /// Возвращает список наборов квитанций за период
        /// </summary>
        /// <param name="since">Начало периода</param>
        /// <param name="till">Окончание периода</param>
        /// <returns>Таблица с наборами квитанций</returns>
        public DataTable GetList(DateTime since, DateTime till)
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("TypeAka", typeof(string));
            _table.Columns.Add("CreationDateTime", typeof(DateTime));
            _table.Columns.Add("Number", typeof(int));
            _table.Columns.Add("Quantity", typeof(short));
            _table.Columns.Add("ValueSum", typeof(decimal));
            DataSet _ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            _ds.Tables.Add(_table);

            using (Entities _entities = new Entities())
            {
                var _result =
                    _entities.BillSets
                        .Where(p => p.CreationDateTime >= since && p.CreationDateTime <= till);

                foreach (DBItem _billSet in _result)
                {
                    string _billType = null;

                    switch ((BillType)_billSet.BillType)
                    {
                        case BillType.Regular:
                            _billType = "Ежемесячные";
                            break;

                        case BillType.Debt:
                            _billType = "Долговые";
                            break;

                        case BillType.Total:
                            _billType = "На доплату";
                            break;

                        default:
                            _billType = String.Empty;
                            break;
                    }

                    _table.Rows.Add(
                        _billSet.ID.ToString(),
                        _billType,
                        _billSet.CreationDateTime,
                        _billSet.Number,
                        _billSet.Quantity,
                        _billSet.ValueSum);
                }
            }

            return _table;
        }
    }
}
