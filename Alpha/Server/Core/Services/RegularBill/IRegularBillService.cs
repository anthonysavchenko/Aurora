using Taumis.Alpha.Server.Core.Services.RegularBill.DataSets;

namespace Taumis.Alpha.Server.Core.Services.RegularBill
{
    public interface IRegularBillService
    {
        RegularBillDataSet GetDataForReport(int billID);
        RegularBillDataSet GetDataForReport(int[] billIDs, bool removeEmptyBills);
    }
}