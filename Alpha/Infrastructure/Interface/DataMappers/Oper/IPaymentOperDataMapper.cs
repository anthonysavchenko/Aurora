using System.Data;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper
{
    public interface IPaymentOperDataMapper : IDataMapper
    {
        DataTable GetList(string paymentSetId);
    }
}