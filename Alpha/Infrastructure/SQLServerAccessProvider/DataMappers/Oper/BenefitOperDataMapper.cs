using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BenefitOpers;
using DomChargeOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeOper;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.BenefitOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class BenefitOperDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<BenefitOper,BenefitOpers>

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
                _result = null != _entities.BenefitOpers.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToBenefitOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BenefitOpers.First(p => p.ID == _id);
                }

                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.ChargeOper.ID);
                _dbItem.ChargeOpers = _entities.ChargeOpers.First(x => x.ID == _tempId);

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
                    _entities.BenefitOpers
                        .Include("ChargeOpers")
                        .First(x => x.ID == _id);

                _domItem.Value = _dbItem.Value;
                _domItem.ChargeOper =
                    (DomChargeOper)DataMapperService.get(typeof(DomChargeOper)).find(_dbItem.ChargeOpers.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}