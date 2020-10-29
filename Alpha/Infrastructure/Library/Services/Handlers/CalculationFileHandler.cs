using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class CalculationFileHandler
    {
        static public int CreateFile(
            string fileName,
            int uploadID)
        {
            using (Entities db = new Entities())
            {
                var file =
                    new CalculationFiles()
                    {
                        CalculationUploads = db.CalculationUploads.First(u => u.ID == uploadID),
                        FileName = fileName.Length > 200 ? fileName.Substring(fileName.Length - 200, 200) : fileName,
                    };

                db.CalculationFiles.AddObject(file);
                db.SaveChanges();

                return file.ID;
            }
        }

        public static void UpdateProcessingResult(int fileID)
        {
            using (Entities db = new Entities())
            {
                var upload = db.CalculationFiles.First(f => f.ID == fileID);

                upload.ProcessingResult = (byte)UploadProcessingResult.OK;

                db.SaveChanges();
            }
        }

        public static void UpdateParsingError(
            int fileID,
            string description,
            Exception exception)
        {
            UpdateError(
                fileID,
                $"Программная ошибка при распознавании файла. {description}",
                exception);
        }

        public static void UpdateCheckingError(
            int fileID,
            string description)
        {
            UpdateError(
                fileID,
                $"Ошибка при проверке распознанного файла. {description}");
        }

        public static void UpdateCheckingError(
            int fileID,
            Exception exception)
        {
            UpdateError(
                fileID,
                "Программная ошибка при проверке распознанного файла.",
                exception);
        }

        public static void UpdateErasingError(
            int fileID,
            Exception exception)
        {
            UpdateError(
                fileID,
                "Программная ошибка при удалении неактуального файла, загруженного ранее.",
                exception);
        }

        public static void UpdateSavingError(
            int fileID,
            Exception exception)
        {
            UpdateError(
                fileID,
                "Программная ошибка при сохранении распознанного файла.",
                exception);
        }

        private static void UpdateError(
            int fileID,
            string description,
            Exception exception = null)
        {
            if (exception != null)
            {
                Logger.SimpleWrite($"{description}, fileID: {fileID}, {exception}");
            }

            using (Entities db = new Entities())
            {
                var file = db.CalculationFiles.First(f => f.ID == fileID);

                file.ErrorDescription = description;

                if (exception != null)
                {
                    file.ExceptionMessage = exception.ToString();
                    file.ProcessingResult = (byte)FileProcessingResult.Exception;
                }
                else
                {
                    file.ProcessingResult = (byte)FileProcessingResult.Error;
                }

                db.SaveChanges();
            }
        }
    }
}
