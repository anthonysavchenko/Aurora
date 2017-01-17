using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Residents;
using DomBenefitType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.BenefitType;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Resident;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class ResidentDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        #region Overrides of BaseDataMapper<Resident,Residents>

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
                _result = null != _entities.Residents.FirstOrDefault(x => x.ID == _domainId);
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
                    _entities.AddToResidents(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Residents.First(x => x.ID == _id);
                }

                _dbItem.FirstName = domObj.FirstName;
                _dbItem.Patronymic = domObj.Patronymic;
                _dbItem.Surname = domObj.Surname;
                _dbItem.ResidentDocument = domObj.ResidentDocument;
                _dbItem.OwnerRelationship = (byte)domObj.OwnerRelationship;

                int _tempId;

                if (domObj.BenefitType != null)
                {
                    _tempId = int.Parse(domObj.BenefitType.ID);
                    _dbItem.BenefitTypes = _entities.BenefitTypes.First(benefitType => benefitType.ID == _tempId);
                }
                else
                {
                    _dbItem.BenefitTypes = null;
                }

                _tempId = int.Parse(domObj.Customer.ID);
                _dbItem.Customers = _entities.Customers.First(customer => customer.ID == _tempId);

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
                Residents _dbItem =
                    _entities.Residents
                        .Include("Customers")
                        .Include("BenefitTypes")
                        .First(x => x.ID == _id);

                _domItem.FirstName = _dbItem.FirstName;
                _domItem.Patronymic = _dbItem.Patronymic;
                _domItem.Surname = _dbItem.Surname;
                _domItem.ResidentDocument = _dbItem.ResidentDocument;
                _domItem.OwnerRelationship = (DomItem.Relationship)_dbItem.OwnerRelationship;
                _domItem.BenefitType =
                    _dbItem.BenefitTypes != null
                        ? (DomBenefitType)DataMapperService.get(typeof(DomBenefitType)).find(_dbItem.BenefitTypes.ID.ToString())
                        : null;
                _domItem.Customer = (DomCustomer)DataMapperService.get(typeof(DomCustomer)).find(_dbItem.Customers.ID.ToString());
            }

            return _domItem;
        }

        #endregion
    }
}
