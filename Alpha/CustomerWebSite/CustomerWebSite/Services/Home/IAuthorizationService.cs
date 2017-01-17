namespace CustomerWebSite.Services.Home
{
    /// <summary>
    /// Интерфейс сервиса авторизации действий пользователя
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Провряет принадлежит ли лицевой счет пользователю
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="customerId">ID лицевого счета, out параметр</param>
        /// <returns></returns>
        bool AuthAccount(string account, int userId, out int customerId);

        /// <summary>
        /// Проверяет уполномочен ли пользователь просматривать квитанцию
        /// </summary>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="billId">ID квитанции, out параметр</param>
        /// <returns></returns>
        bool AuthRegularBill(string account, int year, int month, int userId, out int billId);
    }
}