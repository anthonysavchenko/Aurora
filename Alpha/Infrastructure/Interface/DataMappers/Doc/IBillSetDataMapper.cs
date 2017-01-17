using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc
{
    public interface IBillSetDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает список наборов квитанций за период
        /// </summary>
        /// <param name="since">Начало периода</param>
        /// <param name="till">Окончание периода</param>
        /// <returns>Таблица с наборами квитанций</returns>
        DataTable GetList(DateTime since, DateTime till);
    }
}