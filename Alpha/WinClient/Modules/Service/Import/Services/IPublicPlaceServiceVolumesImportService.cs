using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public interface IPublicPlaceServiceVolumesImportService
    {
        bool GenerateTemplate(string filePath);
        string ProcessFile(string inputFileName, DateTime period, Action<int> reportProgressAction);
    }
}