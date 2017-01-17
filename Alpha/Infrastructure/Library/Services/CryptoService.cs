using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace Taumis.Infrastructure.Interface.Services
{
    /// <summary>
    /// Сервис работы с MD5
    /// </summary>
    public class CryptoService : ICryptoService
    {
        private const int WEB_PASSWORD_LENGHT = 8;

        #region ICryptoService Members

        /// <summary>
        /// Возвращает MD5 хэш от переданной строки
        /// </summary>
        /// <param name="_string">Строка</param>
        /// <returns>MD5 хэш</returns>
        public string GetMD5Hash(string _string)
        {
            MD5 _md5Hasher = MD5.Create();

            byte[] _data = _md5Hasher.ComputeHash(Encoding.Default.GetBytes(_string));

            StringBuilder _sBuilder = new StringBuilder();

            for (int i = 0; i < _data.Length; i++)
            {
                _sBuilder.Append(_data[i].ToString("x2"));
            }

            return _sBuilder.ToString();
        }

        /// <summary>
        /// PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations
        /// </summary>
        /// <param name="password">Строка с паролем</param>
        /// <returns>Hashed password</returns>
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public string GenerateWebPassword()
        {
            const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();
            byte[] _randomBytes = new byte[WEB_PASSWORD_LENGHT * sizeof(int)];
            _random.GetBytes(_randomBytes);

            StringBuilder _result = new StringBuilder(WEB_PASSWORD_LENGHT);

            for (int i = 0; i < WEB_PASSWORD_LENGHT; ++i)
            {
                int _val = Math.Abs(BitConverter.ToInt32(_randomBytes, i * 4) % CHARS.Length);
                _result.Append(CHARS[_val]);
            }

            return _result.ToString();
        }

        #endregion
    }
}