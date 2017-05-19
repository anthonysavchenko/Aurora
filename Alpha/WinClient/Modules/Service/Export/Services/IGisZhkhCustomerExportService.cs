using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IGisZhkhCustomerExportService
    {
        ExportResult Export(string outputPath, string templatePath, bool exportOnlyNew, Action<int> progressAction);
    }
}