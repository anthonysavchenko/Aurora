using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public interface IChildrenOfWarBenefitImportService
    {
        string ProcessFile(string inputFileName, Action<int> reportProgressAction, int buildingId);
    }
}
