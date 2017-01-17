using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.WinClient.Aurora.Library.Views.AuthenticationView;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Library.Services
{
    /// <summary>
    /// CAB'овский сервис аутентификации
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Сервис доступа к данным
        /// </summary>
        private readonly IDomainWithDataMapperHelperService _domainDataMapperService;

        /// <summary>
        /// Сервис для работы с MD5
        /// </summary>
        private readonly ICryptoService _cryptoService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="cryptoService">Сервис для работы с MD5</param>
        /// <param name="domainDataMapperService">Сервис доступа к данным</param>
        [InjectionConstructor]
        public AuthenticationService(
            [ServiceDependency] ICryptoService cryptoService,
            [ServiceDependency]IDomainWithDataMapperHelperService domainDataMapperService)
        {
            _cryptoService = cryptoService;
            _domainDataMapperService = domainDataMapperService;
        }

        #region IAuthenticationService Members

        public void Authenticate()
        {
            AuthenticationView _view = new AuthenticationView(this);

            if (_view.ShowDialog() != DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        #endregion

        public bool Authenticate(string login, string password)
        {
            UserHolder.User =
                _domainDataMapperService.DataMapper<User, IUserDataMapper>()
                    .Get(login, _cryptoService.GetMD5Hash(password));
            
            return UserHolder.User != null;
        }
    }
}