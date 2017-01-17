using System.Data;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IPaymentOperPosDataMapper : IDataMapper
    {
        DataTable GetList(string paymentOperId);
    }
}