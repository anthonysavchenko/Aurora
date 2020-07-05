using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.DecFormsUploadPoses;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.DecFormsUploadPos;
using DomUpload = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.DecFormsUpload;
using DomRouteForm = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RouteForm;
using DomFillForm = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.FillForm;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class DecFormsUploadPosDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToDecFormsUploadPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.DecFormsUploadPoses.First(x => x.ID == _id);
                }

                int _propId = int.Parse(domObj.DecFormsUpload.ID);
                _dbItem.DecFormsUploads = _entities.DecFormsUploads.First(p => p.ID == _propId);

                _dbItem.FileName = domObj.FileName;
                _dbItem.FormType = (byte)domObj.FormType;

                if (domObj.RouteForm != null)
                {
                    _propId = int.Parse(domObj.RouteForm.ID);
                    _dbItem.RouteForm = _entities.RouteForms.First(p => p.ID == _propId);
                }
                else
                {
                    _dbItem.RouteForm = null;
                }

                if (domObj.FillForm != null)
                {
                    _propId = int.Parse(domObj.FillForm.ID);
                    _dbItem.FillForm = _entities.FillForms.First(p => p.ID == _propId);
                }
                else
                {
                    _dbItem.FillForm = null;
                }

                _dbItem.Error = domObj.Error;

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
                DBItem _dbItem = _entities.DecFormsUploadPoses
                    .Include("DecFormsUploads")
                    .Include("RouteForms")
                    .Include("FillForms")
                    .First(x => x.ID == _id);

                _domItem.DecFormsUpload =
                    (DomUpload)DataMapperService.get(typeof(DomUpload)).find(_dbItem.DecFormsUploads.ID.ToString());

                _domItem.FileName = _dbItem.FileName;
                _domItem.FormType = (DecFormsType)_dbItem.FormType;

                _domItem.RouteForm =
                    _dbItem.RouteForm != null
                        ? (DomRouteForm)DataMapperService.get(typeof(DomRouteForm))
                            .find(_dbItem.RouteForm.ID.ToString())
                        : null;

                _domItem.FillForm =
                    _dbItem.FillForm != null
                        ? (DomFillForm)DataMapperService.get(typeof(DomFillForm))
                            .find(_dbItem.FillForm.ID.ToString())
                        : null;

                _domItem.Error = _dbItem.Error;
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.DecFormsUploadPoses.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
