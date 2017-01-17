using Taumis.Alpha.Server.Core.Services.MutualSettlement.DataSets;

namespace Taumis.Alpha.Server.Core.Services.MutualSettlement
{
    public interface IMutualSettlementService
    {
        MutualSettlementDataSet GetDataForReport(int customerId);
    }
}