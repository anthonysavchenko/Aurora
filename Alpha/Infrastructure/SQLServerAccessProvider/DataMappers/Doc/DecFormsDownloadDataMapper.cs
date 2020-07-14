using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.DecFormsDownloads;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.DecFormsDownload;
using DomUser = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class DecFormsDownloadDataMapper : BaseDataMapper<DomItem, DBItem>, IDataMapper
    {
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToDecFormsDownloads(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.DecFormsDownloads.First(x => x.ID == _id);
                }

                _dbItem.Created = domObj.Created;
                _dbItem.Directory = domObj.Directory;
                _dbItem.Note = domObj.Note;
                _dbItem.ErrorDescription = domObj.ErrorDescription;
                _dbItem.ExceptionMessage = domObj.ExceptionMessage;

                int _propId = int.Parse(domObj.Author.ID);
                _dbItem.Author = _entities.Users.First(u => u.ID == _propId);

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
                DBItem _dbItem = _entities.DecFormsDownloads
                    .Include("Author")
                    .First(x => x.ID == _id);

                _domItem.Created = _dbItem.Created;
                _domItem.Directory = _dbItem.Directory;
                _domItem.Note = _dbItem.Note;
                _domItem.ErrorDescription = _dbItem.ErrorDescription;
                _domItem.ExceptionMessage = _dbItem.ExceptionMessage;

                _domItem.Author =
                    (DomUser)DataMapperService.get(typeof(DomUser)).find(_dbItem.Author.ID.ToString());
            }

            return _domItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.DecFormsDownloads.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }
    }
}
