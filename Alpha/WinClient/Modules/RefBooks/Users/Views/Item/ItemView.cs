using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseItemView, IItemView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        public ItemView()
        {
            InitializeComponent();
        }

        #region Implementation of IItemView

        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(loginTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, loginTextBox);
            }
        }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(passwordTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, passwordTextBox);
            }
        }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        public string PasswordCheck
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(passwordCheckTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, passwordCheckTextBox);
            }
        }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Aka
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(akaTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, akaTextBox);
            }
        }

        #endregion
    }
}