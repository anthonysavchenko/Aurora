using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc
{
    public interface IChargeSetDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает таблицу наборов платежей за определенный период
        /// </summary>
        /// <param name="since">Начальная дата периода</param>
        /// <param name="till">Конечная дата перида</param>
        /// <returns>Таблица с данными наборо платежей за период</returns>
        DataTable GetList(DateTime since, DateTime till);

        /// <summary>
        /// Возвращает следующий номер документа
        /// </summary>
        /// <returns>Следующий номер документа</returns>
        int GetNextNumber();
    }
}