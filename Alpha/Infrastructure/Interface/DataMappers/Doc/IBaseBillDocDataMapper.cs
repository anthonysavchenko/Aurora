using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc
{
    public interface IBaseBillDocDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает список квитанций в наборе
        /// </summary>
        /// <param name="BillSet">Набор квитанций</param>
        /// <returns>Таблица с квитанциями</returns>
        DataTable GetList(BillSet BillSet);

        /// <summary>
        /// Возвращает массив ID квитанций в наборе
        /// </summary>
        /// <param name="BillSet">Набор квитанций</param>
        /// <returns>Таблица с квитанциями</returns>
        string[] GetIdsByBillSet(BillSet BillSet);
    }
}