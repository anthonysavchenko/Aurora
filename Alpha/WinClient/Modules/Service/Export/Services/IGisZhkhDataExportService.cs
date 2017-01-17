using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IGisZhkhDataExportService
    {
        string ProcessFile(string inputFileName, bool onlyNew);
    }
}
