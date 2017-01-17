using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RechargeSets;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.RechargeSet;
using DomUser = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Doc
{
    public class RechargeSetDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<ChargeSet,ChargeSets>

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>true если объект найден в БД, иначе - false</returns>
        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.RechargeSets.FirstOrDefault(p => p.ID == _domainId);
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

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToRechargeSets(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RechargeSets.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Number = domObj.Number;
                _dbItem.Quantity = domObj.Quantity;
                _dbItem.ValueSum = domObj.ValueSum;
                _dbItem.Comment = domObj.Comment;
                _dbItem.Period = domObj.Period;
                
                int _authorId = int.Parse(domObj.Author.ID);
                _dbItem.Author = _entities.Users.First(u => u.ID == _authorId);

                _entities.SaveChanges();
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

            using (Entities _entities = new Entities())
            {
                DBItem _dbItem = 
                    _entities.RechargeSets
                        .Include("Author")
                        .First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Number = _dbItem.Number;
                _domItem.Quantity = _dbItem.Quantity;
                _domItem.ValueSum = _dbItem.ValueSum;
                _domItem.Comment = _dbItem.Comment;
                _domItem.Period = _dbItem.Period;
                _domItem.Author =
                    (DomUser)DataMapperService.get(typeof(DomUser)).find(_dbItem.Author.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}