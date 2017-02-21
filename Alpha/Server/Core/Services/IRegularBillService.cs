using System.Data;

namespace Taumis.Alpha.Server.Core.Services
{
    public interface IRegularBillService
    {
        DataSet GetDataForReport(int billID);
        DataSet GetDataForReport(int[] billIDs, bool removeEmptyBills);
    }
}