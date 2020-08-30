using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser
{
    static public class FileParser
    {
        static public bool ParseFile(BuildingValuesUploads upload, Excel2007Worker worker, string file)
        {
            bool result = true;

            try
            {
                worker.OpenFile(file);
                Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);

                BuildingValuesFormParser.FileParser.ParseFile(
                    sheet,
                    upload,
                    out string message);

                if (upload.BuildingValuesUploadPoses
                    .Count(p => string.IsNullOrEmpty(p.ErrorDescription)) == 0)
                {
                    BuildingValuesUploadHandler.UpdateUploadWithError(
                        upload.ID,
                        "Ошибка при распознавании файла. " +
                            "Не было успешно распознано ни одной строки. " +
                            "Убедитесь, что файл составлен в правильном формате.");

                    result = false;
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"BuildingValuesParser.FileParser ParseFile error: {e}");
                BuildingValuesUploadHandler.UpdateUploadWithError(
                    upload.ID,
                    "Ошибка при распознавании файла. " +
                        "Убедитесь, что файл не открыт в другом окне или в другой программе. " +
                        "А также убедитесь, что файл составлен в правильном формате.",
                    e.ToString());

                result = false;
            }
            finally
            {
                worker.Close();
            }

            return result;
        }
    }
}
