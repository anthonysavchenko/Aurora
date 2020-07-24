using System;
using System.ComponentModel;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    static public class Downloader
    {
        static public void DownloadAsync(
            string directory,
            int userID,
            string note,
            Action<int> OnProgress,
            Action<DecFormsDownloads> OnCompleted)
        {
            BackgroundWorker worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true
            };

            worker.ProgressChanged += (sender, args) =>
            {
                OnProgress(args.ProgressPercentage);
            };

            worker.RunWorkerCompleted += (sender, args) =>
            {
                OnCompleted((DecFormsDownloads)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result =
                    Download(
                        directory,
                        userID,
                        note,
                        ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static private DecFormsDownloads Download(
            string directory,
            int userID,
            string note,
            Action<int> SetProgressPercents)
        {
            if (CreateDownload(
                directory,
                note,
                userID,
                out DecFormsDownloads download))
            {
                try
                {
                    ImapDownloader.Download(
                        download,
                        SetProgressPercents);
                }
                catch (Exception e)
                {
                    Logger.SimpleWrite($"Downloader Download error: {e}");
                    UpdateDownloadWithError(
                        download,
                        "Ошибка во время обработки данных.",
                        e.ToString());
                }
            }

            return download;
        }

        static private bool CreateDownload(
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
                Logger.SimpleWrite($"Downloader CreateDownload error: {e}");
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
