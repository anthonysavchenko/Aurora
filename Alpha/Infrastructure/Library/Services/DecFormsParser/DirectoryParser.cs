using System;
using System.ComponentModel;
using System.IO;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser
{
    static public class DirectoryParser
    {
        static public void ParseDirectory(
            string directory,
            DecFormsUploads upload,
            Action<int> SetProgressPercents)
        {
            Excel2007Worker worker = new Excel2007Worker();

            string[] files = Directory.GetFiles(directory, "*.xls", SearchOption.TopDirectoryOnly);
                
            for (int i = 0; i < files.Length; i++)
            {
                DecFormsUploadPoses uploadPos = FileParser.CreateUploadPos(Path.GetFileName(files[i]), upload);

                try
                {
                    worker.OpenFile(files[i]);
                    Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);

                    if (RouteFormParser.FileParser.IsRouteForm(sheet, out string isRouteFormMessage))
                    {
                        RouteFormParser.FileParser.ParseFile(
                            sheet,
                            uploadPos,
                            out string message);
                    }
                    else if (FillFormParser.FileParser.IsFillForm(sheet, out string isFillFormMessage))
                    {
                        FillFormParser.FileParser.ParseFile(
                            sheet,
                            uploadPos,
                            out string message);
                    }
                    else
                    {
                        FileParser.SaveError(
                            uploadPos,
                            $"Формат файла не определен. 1. {isRouteFormMessage} 2. {isFillFormMessage}");
                    }
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"DirectoryParser ParseDirectory error (file: {files[i]}): {e}");
                    FileParser.SaveError(
                        uploadPos,
                        "Внутренняя ошибка при распознавании файла. Убедитесь, что на компьютере " +
                            "установлен Excel, и файл не открыт в другом окне или в другой программе. " +
                            "А также убедитесь, что файл составлен в правильном формате.");
                }
                finally
                {
                    worker.Close();
                }

                SetProgressPercents((i + 1) * 100 / files.Length);
            }
        }
    }
}
