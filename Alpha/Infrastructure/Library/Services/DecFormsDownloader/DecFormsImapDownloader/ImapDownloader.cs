using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader.DecFormsImapDownloader
{
    public static class ImapDownloader
    {
        public static bool Download(
            DecFormsDownloads download,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            SetProgress(progressFrom, "Подготовка к скачиванию файлов из почтового ящика...");

            using (var client = new ImapClient())
            {
                if (!Connect(download, client, out IMailFolder inbox, out IList<UniqueId> uids, out string sender))
                {
                    return false;
                }

                SetProgress(progressFrom, "Скачивание файлов из почтового ящика...");

                for (int i = 0; i < uids.Count; i++)
                {
                    EmailDownloader.DownloadEmail(
                        download,
                        inbox,
                        uids[i],
                        sender,
                        progressFrom + i * (progressTill - progressFrom) / uids.Count,
                        progressFrom + (i + 1) * (progressTill - progressFrom) / uids.Count,
                        SetProgress);

                    SetProgress(
                        progressFrom + (i + 1) * (progressTill - progressFrom) / uids.Count,
                        "Скачивание файлов из почтового ящика...");
                }

                SetProgress(progressTill, "Скачивание файлов из почтового ящика...");

                client.Disconnect(true);
            }

            return true;
        }

        static private bool Connect(
            DecFormsDownloads download,
            ImapClient client,
            out IMailFolder inbox,
            out IList<UniqueId> uids,
            out string sender)
        {
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
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Downloader Connect error: {e}");
                DownloadHandler.UpdateDownloadWithError(
                    download,
                    "Ошибка при подключении к почтовому ящику. " +
                        "Проверьте соединение с Интернет и правильность настроек подключения.",
                    e.ToString());
                return false;
            }

            return true;
        }
    }
}
