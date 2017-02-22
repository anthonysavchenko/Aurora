using System.Data;

namespace Taumis.Alpha.Server.Core.Services
{
    public interface IMutualSettlementService
    {
        DataSet GetDataForReport(int customerId);
    }
}