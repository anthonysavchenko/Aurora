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
        static public void ParseDirectoryAsync(
            string directory,
            DecFormsUploads upload,
            Action OnProgress,
            Action<DirectoryParsingResult> OnCompleted)
        {
            BackgroundWorker worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };

            worker.ProgressChanged += (sender, args) =>
            {
                OnProgress();
            };

            worker.RunWorkerCompleted += (sender, args) =>
            {
                OnCompleted((DirectoryParsingResult)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result = ParseDirectory(directory, upload, ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static public DirectoryParsingResult ParseDirectory(
            string directory,
            DecFormsUploads upload,
            Action<int> setProgressPercents)
        {
            DirectoryParsingResult result = new DirectoryParsingResult();
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
                        result.RouteForms++;
                    }
                    else if (FillFormParser.FileParser.IsFillForm(sheet, out string isFillFormMessage))
                    {
                        FillFormParser.FileParser.ParseFile(
                            sheet,
                            uploadPos,
                            out string message);
                        result.FillForms++;
                    }
                    else
                    {
                        FileParser.SaveError(
                            uploadPos,
                            $"Формат файла не определен. 1. {isRouteFormMessage} 2. {isFillFormMessage}");
                        result.UnknownFiles++;
                    }
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"DirectoryParser TryParse error (file: {files[i]}): {e}");
                    FileParser.SaveError(
                        uploadPos,
                        "Внутренняя ошибка при распознавании файла. Убедитесь, что на компьютере " +
                            "установлен Excel, и файл не открыт в другом окне или в другой программе. " +
                            "А также убедитесь, что файл составлен в правильном формате.");
                    result.Exceptions++;
                }
                finally
                {
                    worker.Close();
                }

                setProgressPercents((i + 1) / files.Length * 100);
            }

            return result;
        }
    }
}
