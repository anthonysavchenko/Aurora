using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Логин
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        string PasswordCheck { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        string Aka { get; set; }
    }
}