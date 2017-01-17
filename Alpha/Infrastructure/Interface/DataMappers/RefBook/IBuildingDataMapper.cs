using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook
{
    public interface IBuildingDataMapper : IDataMapper
    {
        /// <summary>
        /// Возвращает список зданий на улице
        /// </summary>
        /// <param name="street">Улица</param>
        /// <returns>Список зданий</returns>
        DataTable GetBuildingsOnStreet(Street street);
    }
}