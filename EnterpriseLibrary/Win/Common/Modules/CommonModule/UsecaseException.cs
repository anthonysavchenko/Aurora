using System;

using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
    /// <summary>
    /// Исключение уровня юзкейза
    /// </summary>
    public class UsecaseException : ApplicationException
    {
        /// <summary>
        /// Запись о неопределенном выражении
        /// </summary>
        private const string NULL_STRING = "null";

        /// <summary>
        /// Формат логирования
        /// </summary>
        private const string LOG_STRING_FORMAT = "UsecaseException, message: {0}, innerException.Message: {1}";

        /// <summary>
        /// Залогировать исключение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Исключение</param>
        private void LogException(string message, Exception innerException)
        {
            string customMessage = message ?? NULL_STRING;
            string innerMessage = (innerException == null) ? NULL_STRING : innerException.Message;

            Logger.Write(String.Format(LOG_STRING_FORMAT, customMessage, innerMessage));
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public UsecaseException()
        {
            LogException(null, null);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public UsecaseException(string message)
            : base(message)
        {
            LogException(message, null);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Исключение</param>
        public UsecaseException(string message, Exception innerException)
            : base(message, innerException)
        {
            LogException(message, innerException);
        }
    }
}
