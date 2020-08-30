using System;
using System.ComponentModel;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader
{
    static public class Uploader
    {
        static public void UploadAsync(
            string filePath,
            int userID,
            DateTime month,
            string note,
            Action<int, string> OnProgress,
            Action<BuildingValuesUploads> OnCompleted)
        {
            BackgroundWorker worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };

            worker.ProgressChanged += (sender, args) =>
            {
                OnProgress(args.ProgressPercentage, (string)args.UserState);
            };

            worker.RunWorkerCompleted += (sender, args) =>
            {
                OnCompleted((BuildingValuesUploads)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result =
                    Upload(
                        filePath,
                        userID,
                        month,
                        note,
                        ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static private BuildingValuesUploads Upload(
            string filePath,
            int userID,
            DateTime month,
            string note,
            Action<int, string> SetProgress)
        {
            SetProgress(0, "Подготовка к началу обработки данных...");

            if (BuildingValuesUploadHandler.CreateUpload(
                filePath,
                month,
                note,
                userID,
                out BuildingValuesUploads upload))
            {
                try
                {
                    if (!Parser.Parse(
                        upload,
                        0,
                        50,
                        SetProgress))
                    {
                        return upload;
                    }

                    if (!Saver.Save(
                        upload,
                        50,
                        100,
                        SetProgress))
                    {
                        return upload;
                    }
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"BuildingValuesUploader.Uploader Upload error: {e}");
                    BuildingValuesUploadHandler.UpdateUploadWithError(
                        upload.ID,
                        "Ошибка во время обработки данных.",
                        e.ToString());
                }
            }

            return upload;
        }
    }
}
