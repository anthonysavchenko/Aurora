using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System;
using System.Collections.Generic;
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
            DecFormsDownloads download;

            try
            {
                download = CreateDownload(directory, note, userID);
            }
            catch(Exception e)
            {
                Logger.SimpleWrite($"Downloader Download error: {e}");
                return null;
            }

            try
            {
                using (var client = new ImapClient())
                {
                    if (Connect(download, client, out IMailFolder inbox, out IList<UniqueId> uids, out string sender))
                    {
                        for (int i = 0; i < uids.Count; i++)
                        {
                            EmailDownloader.DownloadEmail(
                                download,
                                inbox,
                                uids[i],
                                sender,
                                i,
                                uids.Count,
                                SetProgressPercents);

                            SetProgressPercents((i + 1) * 100 / uids.Count);
                        }

                        client.Disconnect(true);
                    }
                }
            }
            catch(Exception e)
            {
                Logger.SimpleWrite($"Downloader Download error: {e}");
                UpdateErrorDownload(
                    download,
                    "Ошибка во время обработки данных.",
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

        static private bool Connect(
            DecFormsDownloads download,
            ImapClient client,
            out IMailFolder inbox,
            out IList<UniqueId> uids,
            out string sender)
        {
            bool result = false;
            inbox = null;
            uids = null;
            sender = null;

            try
            {
                var settings = SettingsService.GetDecFormsDownloadSettings();

                sender = settings.Sender;

                client.Connect(settings.Server, settings.Port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(settings.Login, settings.Password);

                inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);
                var query = SearchQuery.NotSeen;
                uids = inbox.Search(query);

                result = true;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Downloader Download error: {e}");
                UpdateErrorDownload(
                    download,
                    "Ошибка при подключении к почтовому ящику. " +
                        "Проверьте соединение с Интернет и правильность настроек подключения.",
                    e.ToString());
            }

            return result;
        }
    }
}
