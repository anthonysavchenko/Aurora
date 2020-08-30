using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser.BuildingValuesFormParser
{
    static public class FileParser
    {
        static public void ParseFile(
            ExcelSheet source,
            BuildingValuesUploads form,
            out string message)
        {
            if (!RowParser.ParseRows(source,
                out List<BuildingValuesUploadPoses> poses,
                out message))
            {
                BuildingValuesUploadHandler.UpdateUploadWithError(
                    form.ID,
                    $"Ошибка при распознавании файла. {message}");
            }

            BuildingValuesUploadHandler.UpdateUpload(form, poses);
        }
    }
}
