using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services
{
    public interface IImportService
    {
        string ProcessFile(string inputFileName, Action<int> reportProgressAction, DateTime? period = null);
    }
}