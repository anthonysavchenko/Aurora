using System;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Interface.Services
{
    public interface IBillService
    {
        /// <summary>
        /// Создает строку с данными об обслуживающей организации
        /// </summary>
        /// <param name="bankDetail">Банковские реквизиты</param>
        /// <param name="emergencyPhoneNumber">Телефон аварийных служб</param>
        string OrganizationDetails(BankDetails bankDetail, string emergencyPhoneNumber = null);

        /// <summary>
        /// Генерирует строку для штрих кода
        /// </summary>
        /// <param name="account">Лицевой счет абонента</param>
        /// <param name="period">Дата квитанции</param>
        /// <returns>Строка для штрих кода</returns>
        string GenerateBarCodeString(string account, DateTime period);

        /// <summary>
        /// Генерирует строку для QR-кода
        /// </summary>
        /// <param name="name">Наименование организации</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bic">БИК</param>
        /// <param name="corrAccount">Корр. счет</param>
        /// <param name="inn">ИНН</param>
        /// <param name="category">Категория платежа</param>
        /// <param name="account">Лицевой счет</param>
        /// <param name="fullName">ФИО</param>
        /// <param name="address">Адрес</param>
        /// <param name="period">Учетный период</param>
        /// <param name="sum">Сумма платежа</param>
        /// <returns>Строка для QR-кода</returns>
        string GenerateQrCodeString(
            string name,
            string bankAccount,
            string bankName,
            string bic,
            string corrAccount,
            string inn,
            string category,
            string account,
            string fullName,
            string address,
            DateTime period,
            decimal sum);

        /// <summary>
        /// Форматирует строку для штрих кода
        /// </summary>
        /// <param name="barcode">Строка штих кода</param>
        string FormatBarcodeString(string barcode);
    }
}