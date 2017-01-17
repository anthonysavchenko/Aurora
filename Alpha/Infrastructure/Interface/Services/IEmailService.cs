using System;
using System.IO;

namespace Taumis.Alpha.Infrastructure.Interface.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет учетные данные для доступа на веб сайт абоненту
        /// </summary>
        /// <param name="customerEmail">Email абонента</param>
        /// <param name="customerName">Имя абонента</param>
        /// <param name="password">Пароль</param>
        void SendCredentials(string customerEmail, string customerName, string password);

        void SendRegularBills(MemoryStream pdf, string customerEmail, string customerName, DateTime period);
    }
}