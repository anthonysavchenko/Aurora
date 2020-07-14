using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
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
                args.Result = Download(directory, userID, note, ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static private DecFormsDownloads Download(
            string directory,
            int userID,
            string note,
            Action<int> SetProgressPercents)
        {
            var download = CreateDownload(directory, note, userID);

            try
            {
                var settings = SettingsService.GetDecFormsDownloadSettings();

                using (var client = new ImapClient())
                {
                    client.Connect(settings.Server, settings.Port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(settings.Login, settings.Password);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);
                    var query = SearchQuery.NotSeen;
                    var uids = inbox.Search(query); 

                    for (int i = 0; i < uids.Count; i++)
                    {
                        EmailDownloader.DownloadEmail(
                            download,
                            inbox,
                            uids[i],
                            settings.Sender,
                            i,
                            uids.Count,
                            SetProgressPercents);

                        SetProgressPercents((i + 1) * 100 / uids.Count);
                    }

                    client.Disconnect(true);
                }
            }
            catch(Exception e)
            {
                Logger.SimpleWrite($"Downloader Download error: {e}");
                UpdateErrorDownload(
                    download,
                    "Ошибка при подключении к почтовому ящику или отключении от него. " +
                        "Проверьте соединение с Интернет и правильность настроек подключения.",
                    e.ToString());
            }

            return download;
        }

        static private DecFormsDownloads CreateDownload(string directory, string note, int userID)
        {
            DecFormsDownloads download = new DecFormsDownloads()
            {
                Created = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                Directory = directory.Length > 200 ? directory.Substring(0, 200) : directory,
                Note = note.Length > 250 ? note.Substring(0, 250) : note,
            };

            using (Entities db = new Entities())
            {
                download.Author = db.Users.First(u => u.ID == userID);

                db.AddToDecFormsDownloads(download);

                db.SaveChanges();
            }

            return download;
        }

        static private void UpdateErrorDownload(
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
