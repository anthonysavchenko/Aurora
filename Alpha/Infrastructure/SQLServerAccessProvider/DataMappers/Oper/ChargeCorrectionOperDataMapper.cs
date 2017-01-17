using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.ChargeCorrectionOpers;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.ChargeCorrectionOper;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class ChargeCorrectionOperDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<ChargeCorrectionOper,ChargeCorrectionOpers>

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
                _result = null != _entities.ChargeCorrectionOpers.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToChargeCorrectionOpers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.ChargeCorrectionOpers.First(p => p.ID == _id);
                }

                _dbItem.CreationDateTime = domObj.CreationDateTime;
                _dbItem.Period = domObj.Period;
                _dbItem.Value = domObj.Value;

                if (domObj.RechargeOper != null)
                {
                    int _id = int.Parse(domObj.RechargeOper.ID);
                    _dbItem.ChildRechargeOpers = _entities.RechargeOpers.First(r => r.ID == _id);
                }
                else
                {
                    _dbItem.ChildRechargeOpers = null;
                }

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
                DBItem _dbItem = _entities.ChargeCorrectionOpers.First(x => x.ID == _id);

                _domItem.CreationDateTime = _dbItem.CreationDateTime;
                _domItem.Period = _dbItem.Period;
                _domItem.Value = _dbItem.Value;
                _domItem.RechargeOper =
                    _dbItem.ChildRechargeOpers != null
                        ? (RechargeOper)DataMapperService.get(typeof(RechargeOper)).find(_dbItem.ChildRechargeOpers.ID.ToString())
                        : null;
            }

            return _domItem;
        }

        #endregion
    }
}