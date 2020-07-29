using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class DownloadHandler
    {
        static public bool CreateDownload(
            string directory,
            string note,
            int userID,
            out DecFormsDownloads download)
        {
            download = null;

            try
            {
                var newDownload = new DecFormsDownloads()
                {
                    Created = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                    Directory = directory.Length > 200 ? directory.Substring(0, 200) : directory,
                    Note = note.Length > 250 ? note.Substring(0, 250) : note,
                };

                using (Entities db = new Entities())
                {
                    newDownload.Author = db.Users.First(u => u.ID == userID);

                    db.AddToDecFormsDownloads(newDownload);

                    db.SaveChanges();
                }

                download = newDownload;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"DownloadHandler CreateDownload error: {e}");
                return false;
            }

            return true;
        }

        static public void UpdateDownloadWithError(
            DecFormsDownloads download,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.DecFormsDownloads.Attach(download);

                download.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    download.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }
    }
}
