using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using System;
using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    public static class ImapDownloader
    {
        public static void Download(DecFormsDownloads download, Action<int> SetProgressPercents)
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
                Downloader.UpdateDownloadWithError(
                    download,
                    "Ошибка при подключении к почтовому ящику. " +
                        "Проверьте соединение с Интернет и правильность настроек подключения.",
                    e.ToString());
            }

            return result;
        }
    }
}
