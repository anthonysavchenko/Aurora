using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IRechargeOperPosDataMapper : IDataMapper
    {
        DataTable GetList(RechargeOper oper);
    }
}