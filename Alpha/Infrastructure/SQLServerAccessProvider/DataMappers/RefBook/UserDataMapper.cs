using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Users;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;


namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    public class UserDataMapper : BaseDataMapper<DomItem, DBItem>, IUserDataMapper
    {
        #region Overrides of BaseDataMapper

        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Login");
            _table.Columns.Add("Aka");

            using (Entities _entities = new Entities())
            {
                foreach (var _user in _entities.Users)
                {
                    _table.Rows.Add(
                        _user.ID.ToString(),
                        _user.Login,
                        _user.Aka);
                }
            }

            return _table;
        }

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
                _result = null != _entities.Users.FirstOrDefault(p => p.ID == _domainId);
            }

            return _result;
        }

        /// <summary>
        /// Возвращает пользователя по логину и паролю в MD5
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="md5Password">Пароль в MD5</param>
        /// <returns>Пользователь</returns>
        public DomItem Get(string login, string md5Password)
        {
            DBItem _dbUser = null;

            using (Entities _entities = new Entities())
            {
                _dbUser = _entities.Users.FirstOrDefault(u => u.Login == login && u.Password == md5Password);
            }

            return
                _dbUser != null
                    ? (DomItem)find(_dbUser.ID.ToString())
                    : null;
        }

        public DataTable GetList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID");
            _table.Columns.Add("Login");
            _table.Columns.Add("Aka");

            using (Entities _entities = new Entities())
            {
                foreach (var _user in _entities.Users.Where(u => !u.Customers.Any()))
                {
                    _table.Rows.Add(
                        _user.ID.ToString(),
                        _user.Login,
                        _user.Aka);
                }
            }

            return _table;
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
                    _entities.AddToUsers(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Users.First(p => p.ID == _id);
                }

                _dbItem.Login = domObj.Login;
                _dbItem.Password = domObj.Password;
                _dbItem.Aka = domObj.Aka;
                _dbItem.SecurityStamp = domObj.SecurityStamp;
                _dbItem.LockoutEndDateUtc = domObj.LockoutEndDateUtc;
                _dbItem.LockoutEnabled = domObj.LockoutEnabled;
                _dbItem.AccessFailedCount = domObj.AccessFailedCount;

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
                    _entities.Users
                        .First(x => x.ID == _id);

                _domItem.Login = _dbItem.Login;
                _domItem.Password = _dbItem.Password;
                _domItem.Aka = _dbItem.Aka;
                _domItem.SecurityStamp = _dbItem.SecurityStamp;
                _domItem.LockoutEndDateUtc = _dbItem.LockoutEndDateUtc;
                _domItem.LockoutEnabled = _dbItem.LockoutEnabled;
                _domItem.AccessFailedCount = _dbItem.AccessFailedCount;
            }

            return _domItem;
        }

        #endregion
    }
}
