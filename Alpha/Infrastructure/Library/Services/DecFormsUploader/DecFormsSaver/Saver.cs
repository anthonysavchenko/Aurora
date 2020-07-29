using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver
{
    static public class Saver
    {
        public static bool Save(
            DecFormsUploads upload,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к началу сохранения распознанных файлов...");

            if (!GetPoses(upload, out DateTime month, out List<DecFormsUploadPoses> poses))
            {
                return false;
            }

            SetProgress(progressFrom, "Сохранение распознанных файлов...");

            for (int i = 0; i < poses.Count; i++)
            {
                FileSaver.SaveFile(
                    poses[i],
                    month);

                SetProgress(
                    progressFrom + (i + 1) * (progressTill - progressFrom) / upload.DecFormsUploadPoses.Count,
                    "Сохранение распознанных файлов...");
            }

            SetProgress(progressTill, "Сохранение распознанных файлов...");

            return true;
        }

        public static bool GetPoses(
            DecFormsUploads upload,
            out DateTime month,
            out List<DecFormsUploadPoses> poses)
        {
            poses = null;
            month = DateTime.MinValue;

            try
            {
                using (var db = new Entities())
                {
                    db.DecFormsUploads.Attach(upload);

                    month = upload.Month;
                    poses =
                        upload.DecFormsUploadPoses
                            .Where(p =>
                                string.IsNullOrEmpty(p.ErrorDescription)
                                && (DecFormsType)p.FormType != DecFormsType.Unknown)
                            .ToList();
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Saver GetPoses error: {e}");
                UploadHandler.UpdateUploadWithError(
                    upload,
                    "Ошибка при подготовке к сохранению распознанных данных. " +
                        "Проверьте подключение к локальной сети УК ФР и серверу БД.",
                    e.ToString());
                return false;
            }
            
            return true;
        }
    }
}
