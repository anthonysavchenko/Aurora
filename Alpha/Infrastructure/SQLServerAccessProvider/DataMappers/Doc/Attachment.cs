using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Attachments;
using DomEmail = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Email;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Attachment;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class AttachmentDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToAttachments(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Attachments.First(x => x.ID == _id);
                }

                int _propId = int.Parse(domObj.Email.ID);
                _dbItem.Emails = _entities.Emails.First(p => p.ID == _propId);

                _dbItem.FileName = domObj.FileName;
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
                DBItem _dbItem = _entities.Attachments
                    .Include("Emails")
                    .First(x => x.ID == _id);

                _domItem.Email =
                    (DomEmail)DataMapperService.get(typeof(DomEmail)).find(_dbItem.Emails.ID.ToString());

                _domItem.FileName = _dbItem.FileName;
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
                _result = null != _entities.Attachments.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
