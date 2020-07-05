using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RouteFormPoses;
using DomDoc = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RouteForm;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RouteFormPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RouteFormPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToRouteFormPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RouteFormPoses.First(x => x.ID == _id);
                }

                _dbItem.Apartment = domObj.Apartment;
                _dbItem.CounterType = (byte)domObj.CounterType;
                _dbItem.CounterNumber = domObj.CounterNumber;
                _dbItem.PrevDate = domObj.PrevDate;
                _dbItem.PrevValue = domObj.PrevValue;
                _dbItem.PrevDayValue = domObj.PrevDayValue;
                _dbItem.PrevNightValue = domObj.PrevNightValue;
                _dbItem.Account = domObj.Account;
                _dbItem.Owner = domObj.Owner;
                _dbItem.CounterCapacity = domObj.CounterCapacity;
                _dbItem.Debt = domObj.Debt;
                _dbItem.Payed = domObj.Payed;
                _dbItem.Phone = domObj.Phone;
                _dbItem.Note = domObj.Note;

                int _propId = int.Parse(domObj.RouteForm.ID);
                _dbItem.RouteForms = _entities.RouteForms.First(p => p.ID == _propId);

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
                    _entities.RouteFormPoses
                        .Include("RouteForms")
                        .First(x => x.ID == _id);

                _domItem.Apartment = _dbItem.Apartment;
                _domItem.CounterType = (RouteFormCounterType)_dbItem.CounterType;
                _domItem.CounterNumber = _dbItem.CounterNumber;
                _domItem.PrevDate = _dbItem.PrevDate.GetValueOrDefault();
                _domItem.PrevValue = _dbItem.PrevValue.GetValueOrDefault();
                _domItem.PrevDayValue = _dbItem.PrevDayValue.GetValueOrDefault();
                _domItem.PrevNightValue = _dbItem.PrevNightValue.GetValueOrDefault();
                _domItem.Account = _dbItem.Account;
                _domItem.Owner = _dbItem.Owner;
                _domItem.CounterCapacity = _dbItem.CounterCapacity;
                _domItem.Debt = _dbItem.Debt;
                _domItem.Payed = _dbItem.Payed;
                _domItem.Phone = _dbItem.Phone;
                _domItem.Note = _dbItem.Note;

                _domItem.RouteForm =
                    (DomDoc)DataMapperService.get(typeof(DomDoc))
                        .find(_dbItem.RouteForms.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RouteFormPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
