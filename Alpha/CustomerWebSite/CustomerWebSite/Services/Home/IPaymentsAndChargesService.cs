using CustomerWebSite.Models.PaymentsAndChargesModels;

namespace CustomerWebSite.Services.Home
{
    /// <summary>
    /// Интефейс сервиса предоставления данных для страницы с платежами и начислениями
    /// </summary>
    public interface IPaymentsAndChargesService
    {
        /// <summary>
        /// Возвращает модель с данными для страницы с платежами и начислениями
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="customerId">ID лицевого счета</param>
        /// <param name="account">Номер лицевого счета</param>
        /// <param name="year">Год</param>
        /// <returns>Модель с данными</returns>
        PaymentsAndChargesViewModel GetPaymentsAndChargesViewModel(int userId, int customerId, string account, int? year);

        /// <summary>
        /// Возвращает один из номеров лицевых счетов пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <returns>Номер лицевого счета</returns>
        string GetFirstAccount(int userId);
    }
}