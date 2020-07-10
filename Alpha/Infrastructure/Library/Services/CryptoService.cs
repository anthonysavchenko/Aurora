using System;
using System.Security.Cryptography;
using System.Text;

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

        #endregion
    }
}