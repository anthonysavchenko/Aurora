using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingValuesUploadHandler
    {
        static public int? CreateUpload(
            string directoryPath,
            DateTime month,
            string note,
            int userID)
        {
            int? uploadID = null;

            try
            {
                using (Entities db = new Entities())
                {
                    var upload =
                        new BuildingValuesUploads()
                        {
                            Created = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                            Author = db.Users.First(u => u.ID == userID),
                            Month = month,
                            DirectoryPath =
                                directoryPath.Length > 200 ? directoryPath.Substring(0, 200) : directoryPath,
                            Note = note.Length > 250 ? note.Substring(0, 250) : note,
                        };

                    db.BuildingValuesUploads.AddObject(upload);
                    db.SaveChanges();
                    uploadID = upload.ID;
                }
            }
            catch (Exception exception)
            {
                Logger.SimpleWrite(
                    "Программная ошибка во время загрузки показаний ОДПУ при подготовке к началу обработки данных. " +
                        $" {exception}");
            }

            return uploadID;
        }

        public static void UpdateProcessingResult(int uploadID)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    var upload = db.BuildingValuesUploads.First(u => u.ID == uploadID);

                    upload.ProcessingResult = (byte)UploadProcessingResult.OK;

                    db.SaveChanges();
                }
            }
            catch (Exception innerException)
            {
                Logger.SimpleWrite("Программная ошибка во время загрузки показаний ОДПУ " +
                    "при записи в БД данных об успешном завершении обработки. " +
                    $"uploadID: {uploadID}. {innerException}");
            }
        }

        public static void UpdateProcessingError(
            int uploadID,
            Exception exception)
        {
            UpdateError(
                uploadID,
                "Программная ошибка во время загрузки показаний ОДПУ при обработке данных.",
                exception);
        }

        public static void UpdateParsingError(
            int uploadID,
            string description,
            Exception exception)
        {
            UpdateError(
                uploadID,
                $"Программная ошибка во время загрузки показаний ОДПУ при распознавании файлов. {description}",
                exception);
        }

        public static void UpdateCheckingError(
            int uploadID,
            Exception exception)
        {
            UpdateError(
                uploadID,
                "Программная ошибка во время загрузки показаний ОДПУ при проверке распознанных файлов.",
                exception);
        }

        public static void UpdateErasingError(
            int uploadID,
            Exception exception)
        {
            UpdateError(
                uploadID,
                "Программная ошибка во время загрузки показаний ОДПУ при удалении неактуальных файлов, " +
                    "загруженных ранее.",
                exception);
        }

        public static void UpdateSavingError(
            int uploadID,
            Exception exception)
        {
            UpdateError(
                uploadID,
                "Программная ошибка во время загрузки показаний ОДПУ при сохранении распознанных файлов.",
                exception);
        }

        private static void UpdateError(
            int uploadID,
            string description,
            Exception exception)
        {
            Logger.SimpleWrite($"{description}. uploadID: {uploadID}. {exception}");

            try
            {
                using (Entities db = new Entities())
                {
                    var upload = db.BuildingValuesUploads.First(u => u.ID == uploadID);

                    upload.ErrorDescription = description;
                    upload.ExceptionMessage = exception.ToString();
                    upload.ProcessingResult = (byte)UploadProcessingResult.Exception;

                    db.SaveChanges();
                }
            }
            catch (Exception innerException)
            {
                Logger.SimpleWrite("Программная ошибка во время загрузки показаний ОДПУ " +
                    $"при записи в БД данных о другой ошибке. uploadID: {uploadID}. {innerException}");
            }
        }
    }
}
