using System;
using System.Collections.Generic;
using System.ComponentModel;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationChecker;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationEraser;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser;
using Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationSaver;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader
{
    public static class Uploader
    {
        public static void UploadAsync(
            string directoryPath,
            int userID,
            DateTime month,
            bool useDrafts,
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
                        useDrafts,
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
            bool useDrafts,
            string note,
            Action<int, string> SetProgress)
        {
            int? uploadID = CalculationUploadHandler.CreateUpload(
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
                    month,
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
                    useDrafts,
                    40,
                    70,
                    out List<BuildingCalculationValueHandler.BuildingInfo> buildingInfos,
                    SetProgress))
                {
                    return uploadID;
                }

                if (!Saver.TrySave(
                    uploadID.Value,
                    month,
                    70,
                    100,
                    useDrafts ? buildingInfos : null,
                    SetProgress))
                {
                    return uploadID;
                }
            }
            catch (Exception exception)
            {
                CalculationUploadHandler.UpdateProcessingError(uploadID.Value, exception);
                return uploadID;
            }

            CalculationUploadHandler.UpdateProcessingResult(uploadID.Value);
            return uploadID;
        }
    }
}
