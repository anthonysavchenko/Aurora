using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Emails;
using DomDownload = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.DecFormsDownload;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Email;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class EmailDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToEmails(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Emails.First(x => x.ID == _id);
                }

                int _propId = int.Parse(domObj.DecFormsDownload.ID);
                _dbItem.DecFormsDownloads = _entities.DecFormsDownloads.First(p => p.ID == _propId);

                _dbItem.Subject = domObj.Subject;
                _dbItem.FromAddress = domObj.FromAddress;
                _dbItem.Received = domObj.Received;
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
                DBItem _dbItem = _entities.Emails
                    .Include("DecFormsDownloads")
                    .First(x => x.ID == _id);

                _domItem.DecFormsDownload =
                    (DomDownload)DataMapperService.get(typeof(DomDownload))
                        .find(_dbItem.DecFormsDownloads.ID.ToString());

                _domItem.Subject = _dbItem.Subject;
                _domItem.FromAddress = _dbItem.FromAddress;
                _domItem.Received = _dbItem.Received;
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
                _result = null != _entities.Emails.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
