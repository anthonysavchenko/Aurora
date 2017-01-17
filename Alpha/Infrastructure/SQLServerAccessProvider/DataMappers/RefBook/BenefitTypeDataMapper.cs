using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BenefitTypes;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BenefitType;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class BenefitTypeDataMapper : BaseDataMapper<DomItem, DBItem>
    {
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
                BenefitTypes _dbItem = _entities.BenefitTypes.First(x => x.ID == _id);

                _domItem.Name = _dbItem.Name;
                _domItem.Code = _dbItem.Code;
                _domItem.BenefitRule = (DomItem.BenefitRuleType)_dbItem.BenefitRule;
                _domItem.FixedPercent = _dbItem.FixedPercent;
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
                    _entities.AddToBenefitTypes(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BenefitTypes.First(x => x.ID == _id);
                }

                _dbItem.Name = domObj.Name;
                _dbItem.Code = domObj.Code;
                _dbItem.BenefitRule = (byte)domObj.BenefitRule;
                _dbItem.FixedPercent = domObj.FixedPercent;

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
                _result = null != _entities.BenefitTypes.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }
    }
}