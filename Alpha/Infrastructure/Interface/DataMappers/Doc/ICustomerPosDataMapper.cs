using System.Collections.Generic;
using System.Data;
using Taumis.EnterpriseLibrary.Win;
using DomCustomer = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;
using DomServiceSinceTill = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.CustomerPos.ServiceSinceTill;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc
{
    public interface ICustomerPosDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает список одинаковых услуг абонента, которые есть у всех остальных абонентов из массива их индексов
        /// </summary>
        /// <param name="customer">Абонент</param>
        /// <param name="customerIDs">Массив индексов абонентов</param>
        /// <returns>Таблица услуг</returns>
        DataTable GetList(DomCustomer currentCustomer, string[] customerIDs, out List<DomServiceSinceTill> UniquePoses);

        /// <summary>
        /// Возвращает список услуг абонента
        /// </summary>
        /// <param name="customer">Абонент</param>
        /// <returns>Таблица услуг</returns>
        DataTable GetList(DomCustomer customer);
    }
}
