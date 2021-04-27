using System;
using System.ComponentModel;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesChecker;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesEraser;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesParser;
using Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesSaver;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader
{
    public static class Uploader
    {
        public static void UploadAsync(
            string directoryPath,
            int userID,
            DateTime month,
            string note,
            Action<int, string> OnProgress,
            Action<int?> OnCompleted)
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
                OnCompleted((int?)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                OnProgress(0, "Подготовка к началу обработки данных...");

                args.Result =
                    Upload(
                        directoryPath,
                        userID,
                        month,
                        note,
                        ((BackgroundWorker)sender).ReportProgress);

                OnProgress(100, "Обработка данных завершена...");
            };

            worker.RunWorkerAsync();
        }

        private static int? Upload(
            string directoryPath,
            int userID,
            DateTime month,
            string note,
            Action<int, string> SetProgress)
        {
            int? uploadID = BuildingValuesUploadHandler.CreateUpload(
                directoryPath,
                month,
                note,
                userID);

            if (!uploadID.HasValue)
            {
                return uploadID;
            }

            try
            {
                if (!Parser.TryParse(
                    uploadID.Value,
                    directoryPath,
                    0,
                    30,
                    SetProgress))
                {
                    return uploadID;
                }

                if (!Checker.TryCheck(
                    uploadID.Value,
                    30,
                    40,
                    SetProgress))
                {
                    return uploadID;
                }

                if (!Eraser.TryErase(
                    uploadID.Value,
                    month,
                    40,
                    70,
                    SetProgress))
                {
                    return uploadID;
                }

                if (!Saver.TrySave(
                    uploadID.Value,
                    month,
                    70,
                    100,
                    SetProgress))
                {
                    return uploadID;
                }
            }
            catch (Exception exception)
            {
                BuildingValuesUploadHandler.UpdateProcessingError(uploadID.Value, exception);
                return uploadID;
            }

            BuildingValuesUploadHandler.UpdateProcessingResult(uploadID.Value);
            return uploadID;
        }
    }
}
