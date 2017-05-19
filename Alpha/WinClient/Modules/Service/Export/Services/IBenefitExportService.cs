using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IBenefitExportService
    {
        ExportResult Export(string outputPath, string templatePath, DateTime startPeriod, Action<int> progressAction);
    }
}