using System;
using System.Collections.Generic;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface IChargeExportService
    {
        ExportResult Export(string outputPath, DateTime period, IEnumerable<ChargeExportFormatType> formats, Action<int> progressAction);
    }
}
