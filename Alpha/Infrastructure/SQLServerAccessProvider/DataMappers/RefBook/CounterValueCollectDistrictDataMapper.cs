using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.CounterValueCollectDistricts;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks.CounterValueCollectDistrict;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class CounterValueCollectDistrictDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper

        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Name");

            using (var _db = new Entities())
            {
                var _list = _db.CounterValueCollectDistricts.ToList();

                foreach (var _item in _list)
                {
                    _table.Rows.Add(
                        _item.ID.ToString(),
                        _item.Name);
                }
            }

            return _table;
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (var _db = new Entities())
            {
                _result = null != _db.CounterValueCollectDistricts.FirstOrDefault(p => p.ID == _domainId);
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

            using (var _db = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _db.AddToCounterValueCollectDistricts(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _db.CounterValueCollectDistricts.First(p => p.ID == _id);
                }

                _dbItem.Name = domObj.Name;

                _db.SaveChanges();
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

            using (var _db = new Entities())
            {
                DBItem _dbItem = _db.CounterValueCollectDistricts.First(x => x.ID == _id);
                _domItem.Name = _dbItem.Name;
            }

            return _domItem;
        }

        #endregion
    }
}
