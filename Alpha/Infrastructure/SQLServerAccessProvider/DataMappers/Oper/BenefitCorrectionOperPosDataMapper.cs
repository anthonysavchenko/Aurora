using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.BenefitCorrectionOperPoses;
using DomBenefitCorrectionOper = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.BenefitCorrectionOper;
using DomContractor = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Contractor;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper.BenefitCorrectionOperPos;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.Oper
{
    public class BenefitCorrectionOperPosDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<BenefitCorrectionOperPos,BenefitCorrectionOperPoses>

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
                _result = null != _entities.BenefitCorrectionOperPoses.FirstOrDefault(p => p.ID == _domainId);
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
                    _entities.AddToBenefitCorrectionOperPoses(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.BenefitCorrectionOperPoses.First(p => p.ID == _id);
                }

                _dbItem.BenefitRule = (byte)domObj.BenefitRule;
                _dbItem.Value = domObj.Value;

                int _tempId = int.Parse(domObj.Service.ID);
                _dbItem.Services = _entities.Services.First(x => x.ID == _tempId);

                _tempId = int.Parse(domObj.BenefitCorrectionOper.ID);
                _dbItem.BenefitCorrectionOpers = _entities.BenefitCorrectionOpers.First(x => x.ID == _tempId);

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
                    _entities.BenefitCorrectionOperPoses
                        .Include("Services")
                        .Include("BenefitCorrectionOpers")
                        .First(x => x.ID == _id);

                _domItem.BenefitRule = (BenefitRuleType)_dbItem.BenefitRule;
                _domItem.Value = _dbItem.Value;
                _domItem.Service = (Service)DataMapperService.get(typeof(Service)).find(_dbItem.Services.ID.ToString());
                _domItem.BenefitCorrectionOper = (DomBenefitCorrectionOper)DataMapperService.get(typeof(DomBenefitCorrectionOper)).find(_dbItem.BenefitCorrectionOpers.ID.ToString());
                _domItem.Contractor = (DomContractor)DataMapperService.get(typeof(DomContractor)).find(_dbItem.Contractors.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}