using System.Data;
using Taumis.EnterpriseLibrary.Win;
using User = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.User;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook
{
    public interface IUserDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает пользователя по логину и паролю в MD5
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="md5Password">Пароль в MD5</param>
        /// <returns>Пользователь</returns>
        User Get(string login, string md5Password);

        DataTable GetList();
    }
}