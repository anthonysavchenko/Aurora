using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BuildingValuesUploadPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BuildingValuesUploadPos;
using DomUpload = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.BuildingValuesUpload;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class BuildingValuesUploadPosDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToBuildingValuesUploadPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BuildingValuesUploadPoses.First(x => x.ID == _id);
                }

                int _propId = int.Parse(domObj.BuildingValuesUpload.ID);
                _dbItem.BuildingValuesUploads = _entities.BuildingValuesUploads.First(p => p.ID == _propId);

                _dbItem.Street = domObj.Street;
                _dbItem.Building = domObj.Building;
                _dbItem.CounterNumber = domObj.CounterNumber;
                _dbItem.Coefficient = domObj.Coefficient;
                _dbItem.CurrentValue = domObj.CurrentValue;
                _dbItem.PrevValue = domObj.PrevValue;
                _dbItem.CurrentDate = domObj.CurrentDate;
                _dbItem.ErrorDescription = domObj.ErrorDescription;
                _dbItem.ExceptionMessage = domObj.ExceptionMessage;

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(_domItem.ID);

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem = _entities.BuildingValuesUploadPoses
                    .Include("BuildingValuesUploads")
                    .First(x => x.ID == _id);

                _domItem.BuildingValuesUpload =
                    (DomUpload)DataMapperService.get(typeof(DomUpload))
                        .find(_dbItem.BuildingValuesUploads.ID.ToString());

                _domItem.Street = _dbItem.Street;
                _domItem.Building = _dbItem.Building;
                _domItem.CounterNumber = _dbItem.CounterNumber;
                _domItem.Coefficient = _dbItem.Coefficient;
                _domItem.CurrentValue = _dbItem.CurrentValue;
                _domItem.PrevValue = _dbItem.PrevValue;
                _domItem.CurrentDate = _dbItem.CurrentDate;
                _domItem.ErrorDescription = _dbItem.ErrorDescription;
                _domItem.ExceptionMessage = _dbItem.ExceptionMessage;
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.BuildingValuesUploadPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
