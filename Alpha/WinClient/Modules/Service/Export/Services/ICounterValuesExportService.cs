using System;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export.Services
{
    public interface ICounterValuesExportService
    {
        ExportResult Export(string collectFormFilePath, string form2FilePath, DateTime period, Action<int> progressAction);

        ExportResult Demo(string previousCollectFrom2Path, string currentCollectForm2Path, string calculationFormPath, Action<int> progressAction);
    }
}
