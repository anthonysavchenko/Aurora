using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IChargeOperDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает список операций начислений по набору
        /// </summary>
        /// <param name="chargeSet">Набор начислений</param>
        /// <returns>Таблица с данными</returns>
        DataTable GetList(ChargeSet chargeSet);
    }
}