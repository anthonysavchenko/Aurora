using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RouteFormDataMapper : BaseDataMapper<RouteForm, RouteForms>, IDataMapper
    {
        protected override RouteForms BusinessToService(RouteForm domObj)
        {
            RouteForms _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new RouteForms();
                    _entities.AddToRouteForms(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RouteForms.First(p => p.ID == _id);
                }

                _dbItem.Street = domObj.Street;
                _dbItem.Building = domObj.Building;

                int _propId = int.Parse(domObj.DecFormsUploadPos.ID);
                _dbItem.DecFormsUploadPoses = _entities.DecFormsUploadPoses.First(p => p.ID == _propId);

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        protected override RouteForm ServiceToBusiness(IDomainObject obj)
        {
            RouteForm _domItem = (RouteForm)obj;
            int _id = int.Parse(_domItem.ID);

            using (Entities _entities = new Entities())
            {
                RouteForms _dbItem = _entities.RouteForms
                    .Include("DecFormsUploadPoses")
                    .First(p => p.ID == _id);

                _domItem.Street = _dbItem.Street;
                _domItem.Building = _dbItem.Building;

                _domItem.DecFormsUploadPos =
                    (DecFormsUploadPos)DataMapperService.get(typeof(DecFormsUploadPos))
                        .find(_dbItem.DecFormsUploadPoses.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RouteForms.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
