namespace Taumis.Infrastructure.Interface.Services
{
    /// <summary>
    /// Интерфейс сервиса работы с MD5
    /// </summary>
    public interface ICryptoService
    {
        /// <summary>
        /// Возвращает MD5 хэш от переданной строки
        /// </summary>
        /// <param name="_string">Строка</param>
        /// <returns>MD5 хэш</returns>
        string GetMD5Hash(string _string);
    }
}