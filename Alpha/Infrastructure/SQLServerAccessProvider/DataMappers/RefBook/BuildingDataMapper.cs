using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Buildings;
using DomCommonCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.CommonCounter;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Building;
using DomStreet = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Street;
using DomPublicPlace = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PublicPlace;
using DomBankDetail = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BankDetail;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class BuildingDataMapper : BaseDataMapper<DomItem, DBItem>, IBuildingDataMapper
    {
        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Street", typeof(string));
            _table.Columns.Add("BuildingNumber", typeof(string));
            _table.Columns.Add("ZipCode", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (var _building in _entities.Buildings.Include("Streets"))
                {
                    _table.Rows.Add(_building.ID.ToString(), _building.Streets.Name, _building.Number, _building.ZipCode);
                }
            }

            return _table;
        }

        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem =
                    _entities.Buildings
                        .Include("Streets")
                        .Include("CommonCounters")
                        .Include("PublicPlaces")
                        .First(x => x.ID == _id);

                _domItem.Number = _dbItem.Number;
                _domItem.Street = (DomStreet)DataMapperService.get(typeof(DomStreet)).find(_dbItem.Streets.ID.ToString());
                _domItem.ZipCode = _dbItem.ZipCode;
                _domItem.FloorCount = _dbItem.FloorCount;
                _domItem.EntranceCount = _dbItem.EntranceCount;
                _domItem.Note = _dbItem.Note;
                _domItem.FiasID = _dbItem.FiasID;
                _domItem.NonResidentialPlaceArea = _dbItem.NonResidentialPlaceArea;
                _domItem.BankDetail = _dbItem.BankDetailID.HasValue 
                    ? (DomBankDetail)DataMapperService.get(typeof (DomBankDetail)).find(_dbItem.BankDetailID.Value.ToString())
                    : null;

                IDataMapper _commonCounterDataMapper = DataMapperService.get(typeof(DomCommonCounter));

                _domItem.CommonCounters.Clear();

                foreach (CommonCounters _counter in _dbItem.CommonCounters)
                {
                    DomCommonCounter _domCounter =
                        (DomCommonCounter)_commonCounterDataMapper.find(_counter.ID.ToString());
                    _domCounter.Building = _domItem;
                    _domItem.CommonCounters.Add(_domCounter.ID, _domCounter);
                }

                IDataMapper _ppDataMapper = DataMapperService.get(typeof(DomPublicPlace));
                _domItem.PublicPlaces.Clear();

                foreach (PublicPlaces _pp in _dbItem.PublicPlaces)
                {
                    DomPublicPlace _domPublicPlace = (DomPublicPlace)_ppDataMapper.find(_pp.ID.ToString());
                    _domPublicPlace.Building = _domItem;
                    _domItem.PublicPlaces.Add(_domPublicPlace);
                }
            }

            return _domItem;
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
                    _entities.AddToBuildings(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Buildings.First(x => x.ID == _id);
                }

                _dbItem.Number = domObj.Number;
                _dbItem.ZipCode = domObj.ZipCode;
                _dbItem.FloorCount = domObj.FloorCount;
                _dbItem.EntranceCount = domObj.EntranceCount;
                _dbItem.Note = domObj.Note;
                _dbItem.FiasID = domObj.FiasID;
                _dbItem.NonResidentialPlaceArea = domObj.NonResidentialPlaceArea;

                int _tempId = Convert.ToInt32(domObj.Street.ID);
                _dbItem.Streets = _entities.Streets.First(s => s.ID == _tempId);

                _tempId = Convert.ToInt32(domObj.BankDetail.ID);
                _dbItem.BankDetails = _entities.BankDetails.First(b => b.ID == _tempId);

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.Buildings.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }

        #region IBuildingDataMapper Members

        /// <summary>
        /// Возвращает список зданий на улице
        /// </summary>
        /// <param name="street">Улица</param>
        /// <returns>Список зданий</returns>
        public DataTable GetBuildingsOnStreet(DomStreet street)
        {
            DataTable _result = new DataTable();
            _result.Columns.Add("ID", typeof(string));
            _result.Columns.Add("Number", typeof(string));

            int _streetId = Convert.ToInt32(street.ID);

            using (Entities _entities = new Entities())
            {
                var _buildingList = _entities.Buildings.Where(b => b.Streets.ID == _streetId);

                foreach (var _building in _buildingList)
                {
                    _result.Rows.Add(_building.ID.ToString(), _building.Number);
                }
            }

            return _result;
        }

        #endregion
    }
}
