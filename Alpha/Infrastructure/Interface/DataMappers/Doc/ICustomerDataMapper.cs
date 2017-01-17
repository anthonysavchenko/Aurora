using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc
{
    public interface ICustomerDataMapper : IDataMapper
    {
        Customer GetItem(string accountNumber);

        /// <summary>
        /// Возвращает список абонентов по части номера аккаунта
        /// </summary>
        /// <param name="accountNumberPart">Часть номера аккаунта</param>
        /// <returns>Список абонентов</returns>
        DataTable GetListByAccount(string accountNumberPart);

        /// <summary>
        /// Возвращает список абонентов по части названия улицы и части номера дома
        /// </summary>
        /// <param name="streetNamePart">Часть название улицы</param>
        /// <param name="housePart">Часть номера дома</param>
        /// <param name="ApartmentPart">Часть номера квартиры</param>
        /// <returns>Список абонентов</returns>
        DataTable GetList(string streetNamePart, string housePart, string ApartmentPart, bool WholeWord);

        /// <summary>
        /// Возвращает список абонентов по части номера почтового индекса
        /// </summary>
        /// <param name="zipCodePart">Часть номера почтового индекса</param>
        /// <returns>Список абонентов</returns>
        DataTable GetListByZipCode(string zipCodePart);

        /// <summary>
        /// Возвращает список всех абонентов
        /// </summary>
        /// <returns>Список абонентов</returns>
        DataTable GetList();
    }
}