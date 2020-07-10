using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    static public class Downloader
    {
        const string EMAIL_SERVER_URL = "imap.mail.ru";
        const int EMAIL_SERVER_PORT = 993;
        const string EMAIL_LOGIN = "ukfr_dek_forms@mail.ru";
        const string EMAIL_PASSWORD = "$fkafdbn_789";
        const string EMAIL_FROM_FILTER = "anton.savchenko@mail.ru";

        static public void DownloadAsync(
             string directory,
             Action<int> OnProgress,
             Action<int> OnCompleted)
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
                OnCompleted((int)args.Result);
            };

            worker.DoWork += (sender, args) =>
            {
                args.Result = Download(directory, ((BackgroundWorker)sender).ReportProgress);
            };

            worker.RunWorkerAsync();
        }

        static public int Download(
            string directory,
            Action<int> SetProgressPercents)
        {
            int messagesCount = 0;

            using (var client = new ImapClient())
            {
                client.Connect(EMAIL_SERVER_URL, EMAIL_SERVER_PORT, SecureSocketOptions.SslOnConnect);
                client.Authenticate(EMAIL_LOGIN, EMAIL_PASSWORD);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                var query = SearchQuery.NotSeen;

                var uids = inbox.Search(query);
                messagesCount = uids.Count;

                for (int i = 0; i < uids.Count; i++)
                {
                    try
                    {
                        var message = inbox.GetMessage(uids[i]);

                        if ((message.From?.Count ?? 0) > 0
                            && message.From.Any(m => m.ToString().ToLower() == EMAIL_FROM_FILTER.ToLower()))
                        {
                            var attachments = message.Attachments;
                            var attachmentsCount = attachments?.Count() ?? 0;

                            for (int j = 0; j < attachmentsCount; j++)
                            {
                                var attachment = attachments.ElementAt(j);
                                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                                var filePath = Path.Combine(directory, fileName);

                                using (var stream = File.Create(filePath))
                                {
                                    if (attachment is MessagePart)
                                    {
                                        var rfc822 = (MessagePart)attachment;

                                        rfc822.Message.WriteTo(stream);
                                    }
                                    else
                                    {
                                        var part = (MimePart)attachment;

                                        part.Content.DecodeTo(stream);
                                    }
                                }

                                SetProgressPercents((i * 100 + (j + 1) * 100 / attachmentsCount) / uids.Count);
                            }
                        }

                        inbox.AddFlags(uids[i], MessageFlags.Seen, true);
                    }
                    catch (Exception e)
                    {

                    }

                    SetProgressPercents((i + 1) * 100 / uids.Count);
                }

                client.Disconnect(true);
            }

            return messagesCount;
        }
    }
}
