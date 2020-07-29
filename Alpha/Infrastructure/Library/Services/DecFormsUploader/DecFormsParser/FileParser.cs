using System;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser
{
    static public class FileParser
    {
        static public void ParseFile(DecFormsUploads upload, Excel2007Worker worker, string file)
        {
            var uploadPos = DecFormsUploadPosHandler.CreateUploadPos(Path.GetFileName(file), upload);

            try
            {
                worker.OpenFile(file);
                Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);

                if (RouteFormParser.FileParser.IsRouteForm(
                    sheet,
                    out string isRouteFormMessage))
                {
                    RouteFormParser.FileParser.ParseFile(
                        sheet,
                        uploadPos,
                        out string message);
                }
                else if (FillFormParser.FileParser.IsFillForm(
                    sheet,
                    out string isFillFormMessage))
                {
                    FillFormParser.FileParser.ParseFile(
                        sheet,
                        uploadPos,
                        out string message);
                }
                else
                {
                    DecFormsUploadPosHandler.UpdateUploadPosWithError(
                        uploadPos,
                        $"Формат файла не определен. 1. {isRouteFormMessage} 2. {isFillFormMessage}");
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"FileParser ParseFile error (file: {file}): {e}");
                DecFormsUploadPosHandler.UpdateUploadPosWithError(
                    uploadPos,
                    "Ошибка при распознавании файла. " +
                        "Убедитесь, что  файл не открыт в другом окне или в другой программе. " +
                        "А также убедитесь, что файл составлен в правильном формате.",
                    e.ToString());
            }
            finally
            {
                worker.Close();
            }
        }
    }
}
