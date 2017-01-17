using System;
using System.Collections.Generic;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Infrastructure.Library.Services
{
    /// <summary>
    /// Сервис доступа к слою преобразователей данных из базы данных в домен и обратно.
    /// </summary>
    public class DataMapperService : IDataMapperService
    {
        /// <summary>
        /// Список зарегистрированных датамапперов
        /// </summary>
        private readonly List<IDataMapper> _dataMappers = new List<IDataMapper>();

        #region IDataMapperService Members

        /// <summary>
        /// Возвращает преобразователь данных для объекта домена заданного типа
        /// </summary>
        /// <param name="domainType">Тип объекта домена</param>
        /// <returns>Преобразователь данных</returns>
        public IDataMapper get(Type domainType)
        {
            return _dataMappers.Find(test => test.CanTranslate(domainType));
        }

        /// <summary>
        /// Регистрирует преобразователь данных
        /// </summary>
        /// <param name="dataMapper">Преобразователь данных</param>
        public void RegisterDataMapper(IDataMapper dataMapper)
        {
            if (dataMapper == null)
            {
                throw new ArgumentNullException("Преобразователь данных");
            }

            _dataMappers.Add(dataMapper);
            dataMapper.DataMapperService = this;
        }

        #endregion
    }
}