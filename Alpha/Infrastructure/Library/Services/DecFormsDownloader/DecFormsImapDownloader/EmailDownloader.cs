using MailKit;
using MimeKit;
using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader.DecFormsImapDownloader
{
    static public class EmailDownloader
    {
        static public void DownloadEmail(
            DecFormsDownloads download,
            IMailFolder inbox,
            UniqueId messageUid,
            string sender,
            int progressFrom,
            int progressTill,
            Action<int, string> SetProgress)
        {
            var email = EmailHandler.CreateEmail(download);

            try
            {
                var message = inbox.GetMessage(messageUid);

                string fromAddress =
                    (message.From?.Count ?? 0) > 0
                        ? message.From.Any(m =>
                            m is MailboxAddress
                            && ((MailboxAddress)message.From.First()).Address.ToLower() == sender.ToLower())
                                ? sender
                                : ((MailboxAddress)message.From.First()).Address
                        : string.Empty;

                EmailHandler.UpdateEmail(email, message.Subject, fromAddress, message.Date.DateTime);

                if (fromAddress != sender)
                {
                    EmailHandler.UpdateEmailWithError(email, $"Читаются письма полученные, только от {sender}.");
                    return;
                }

                var attachments = message.Attachments;
                var attachmentsCount = attachments?.Count() ?? 0;

                if (attachmentsCount <= 0)
                {
                    EmailHandler.UpdateEmailWithError(email, "Читаются письма только с приложенными файлами.");
                    return;
                }

                for (int i = 0; i < attachmentsCount; i++)
                {
                    AttachmentDownloader.DownloadAttachment(
                        email,
                        messageUid,
                        message.Attachments,
                        i,
                        download.Directory);

                    SetProgress(
                        progressFrom + (i + 1) * (progressTill - progressFrom) / attachmentsCount,
                        "Скачивание файлов из почтового ящика...");
                }

                inbox.AddFlags(messageUid, MessageFlags.Seen, true);
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"EmailDownloader DownloadEmail error (message uid: {messageUid}): {e}");
                EmailHandler.UpdateEmailWithError(
                    email,
                    "Ошибка при чтении письма.",
                    e.ToString());
            }
        }
    }
}
