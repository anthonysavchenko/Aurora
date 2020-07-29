using System;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesParser
{
    static public class FileParser
    {
        static public void ParseFile(PrivateValuesUploads upload, Excel2007Worker worker, string file)
        {
            var form = PrivateValuesFormHandler.CreateForm(Path.GetFileName(file), upload);

            try
            {
                worker.OpenFile(file);
                Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);

                if (PrivateValuesFormParser.FileParser.IsPrivateValuesForm(
                    sheet,
                    out string isPrivateValuesFormMessage))
                {
                    PrivateValuesFormParser.FileParser.ParseFile(
                        sheet,
                        form,
                        out string message);
                }
                else
                {
                    PrivateValuesFormHandler.UpdateFormWithError(
                        form,
                        isPrivateValuesFormMessage);
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"PrivateValuesParser.FileParser ParseFile error (file: {file}): {e}");
                PrivateValuesFormHandler.UpdateFormWithError(
                    form,
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
