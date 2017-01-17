using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IChargeOperPosDataMapper : IDataMapper
    {
        DataTable GetList(ChargeOper oper);
    }
}