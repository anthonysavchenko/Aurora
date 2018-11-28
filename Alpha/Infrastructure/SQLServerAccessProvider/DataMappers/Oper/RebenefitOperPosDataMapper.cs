using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.RebenefitOperPoses;
using DomBenefitOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.RebenefitOper;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.RebenefitOperPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class RebenefitOperPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<BenefitOperPos,BenefitOperPoses>

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
                _result = null != _entities.RebenefitOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToRebenefitOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.RebenefitOperPoses.First(p => p.ID == _id);
                }

                _dbItem.BenefitRule = (byte)domObj.BenefitRule;
                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.RebenefitOper.ID);
                _dbItem.RebenefitOpers = _entities.RebenefitOpers.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.Contractor.ID);
                _dbItem.Contractors = _entities.Contractors.First(x => x.ID == _tempId);

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
                    _entities.RebenefitOperPoses
                        .Include("Services")
                        .Include("RebenefitOpers")
                        .First(x => x.ID == _id);

                _domItem.BenefitRule = (BenefitRuleType)_dbItem.BenefitRule;
                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.RebenefitOper = (DomBenefitOper)DataMapperService.get(typeof(DomBenefitOper)).find(_dbItem.RebenefitOpers.ID.ToString());
                _domItem.Contractor = (DomContractor)DataMapperService.get(typeof(DomContractor)).find(_dbItem.Contractors.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}