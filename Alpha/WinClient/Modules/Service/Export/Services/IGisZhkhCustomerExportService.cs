using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IGisZhkhCustomerExportService
    {
        ExportResult Export(string outputPath, bool exportOnlyNew, Action<int> progressAction);
    }
}