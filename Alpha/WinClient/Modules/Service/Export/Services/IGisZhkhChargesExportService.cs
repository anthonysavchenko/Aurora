using System;
using System.Collections.Generic;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IGisZhkhChargesExportService
    {
        ExportResult Export(string outputPath, string templatePath, DateTime period, Dictionary<int, string> serviceMatchingDict, Action<int> progressAction);
        List<string> GetGisZhkhServices(string templatePath);
        Dictionary<int, string> GetServices(DateTime period);
    }
}