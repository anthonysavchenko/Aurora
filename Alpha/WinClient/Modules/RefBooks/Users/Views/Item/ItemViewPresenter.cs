using Microsoft.Practices.CompositeUI;
using System.Text;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, User>
    {
        [ServiceDependency]
        public ICryptoService CryptoService { get; set; }

        #region Overrides of BaseItemViewPresenter

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(User _domItem)
        {
            View.Login = _domItem.Login;
            View.Aka = _domItem.Aka;
            View.Password = string.Empty;
            View.PasswordCheck = string.Empty;
        }

        #endregion

        #region Overrides of BaseMainItemViewPresenter

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void FillDomainFromAllViews(User _domItem)
        {
            _domItem.Login = View.Login.Trim();
            _domItem.Aka = View.Aka.Trim();
            
            if (!string.IsNullOrEmpty(View.Password) && View.Password == View.PasswordCheck)
            {
                _domItem.Password = CryptoService.GetMD5Hash(View.Password);
            }
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(User _domItem, out string _errorMessage)
        {
            StringBuilder _error = new StringBuilder();

            if (string.IsNullOrEmpty(_domItem.Login))
            {
                _error.AppendLine("- Не указан логин");
            }
            
            if (string.IsNullOrEmpty(_domItem.Aka))
            {
                _error.AppendLine("- Не указаны фамилия, имя и отчество");
            }

            if (!string.IsNullOrEmpty(View.Password))
            {
                if (View.Password != View.PasswordCheck)
                {
                    _error.AppendLine("- Пароли не совпадают");
                }
            }
            else if (_domItem.IsNew)
            {
                _error.AppendLine("- Не указан пароль");
            }

            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion
    }
}