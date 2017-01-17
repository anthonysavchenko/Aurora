using System;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Интерфейс доступа к сервису слоя преобразователей данных.
    /// </summary>
    public interface IDataMapperService
    {
        /// <summary>
        /// Возвращает преобразователь данных для типа домена.
        /// </summary>
        /// <param name="_domainType">Тип объекта домена.</param>
        /// <returns>Преобразователь данных.</returns>
        IDataMapper get(Type _domainType);

        /// <summary>
        /// Регистрирует преобразователь данных
        /// </summary>
        /// <param name="dataMapper">Преобразователь данных</param>
        void RegisterDataMapper(IDataMapper dataMapper);
    }
}