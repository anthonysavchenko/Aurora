using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.FillFormPoses;
using DomDoc = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FillForm;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FillFormPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class FillFormPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToFillFormPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.FillFormPoses.First(x => x.ID == _id);
                }

                _dbItem.Apartment = domObj.Apartment;
                _dbItem.CounterType = (byte)domObj.CounterType;
                _dbItem.CounterNumber = domObj.CounterNumber;
                _dbItem.PrevDate = domObj.PrevDate;
                _dbItem.PrevValue = domObj.PrevValue;
                _dbItem.PrevDayValue = domObj.PrevDayValue;
                _dbItem.PrevNightValue = domObj.PrevNightValue;
                _dbItem.Account = domObj.Account;
                _dbItem.CounterCapacity = domObj.CounterCapacity;

                int _propId = int.Parse(domObj.FillForm.ID);
                _dbItem.FillForms = _entities.FillForms.First(p => p.ID == _propId);

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
                DBItem _dbItem =
                    _entities.FillFormPoses
                        .Include("FillForms")
                        .First(x => x.ID == _id);

                _domItem.Apartment = _dbItem.Apartment;
                _domItem.CounterType = (FillFormCounterType)_dbItem.CounterType;
                _domItem.CounterNumber = _dbItem.CounterNumber;
                _domItem.PrevDate = _dbItem.PrevDate;
                _domItem.PrevValue = _dbItem.PrevValue;
                _domItem.PrevDayValue = _dbItem.PrevDayValue;
                _domItem.PrevNightValue = _dbItem.PrevNightValue;
                _domItem.Account = _dbItem.Account;
                _domItem.CounterCapacity = _dbItem.CounterCapacity;

                _domItem.FillForm =
                    (DomDoc)DataMapperService.get(typeof(DomDoc))
                        .find(_dbItem.FillForms.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.FillFormPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
