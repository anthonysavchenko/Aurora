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

        /// <summary>
        /// PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations
        /// </summary>
        /// <param name="password">Строка с паролем</param>
        /// <returns>Hashed password</returns>
        string HashPassword(string password);

        string GenerateWebPassword();
    }
}