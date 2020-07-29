using System;
using System.ComponentModel;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser;
using Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader
{
    static public class Uploader
    {
        static public void UploadAsync(
            string directory,
            int userID,
            DateTime month,
            string note,
            Action<int, string> OnProgress,
            Action<DecFormsUploads> OnCompleted)
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
                OnCompleted((DecFormsUploads)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result =
                    Upload(
                        directory,
                        userID,
                        month,
                        note,
                        ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static private DecFormsUploads Upload(
            string directory,
            int userID,
            DateTime month,
            string note,
            Action<int, string> SetProgress)
        {
            SetProgress(0, "Подготовка к началу обработки данных...");

            if (UploadHandler.CreateUpload(
                directory,
                month,
                note,
                userID,
                out DecFormsUploads upload))
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
                    Logger.SimpleWrite($"Uploader Upload error: {e}");
                    UploadHandler.UpdateUploadWithError(
                        upload,
                        "Ошибка во время обработки данных.",
                        e.ToString());
                }
            }

            return upload;
        }
    }
}
