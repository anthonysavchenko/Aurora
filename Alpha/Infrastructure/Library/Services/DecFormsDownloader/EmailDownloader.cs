using MailKit;
using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    static public class EmailDownloader
    {
        static public void DownloadEmail(
            DecFormsDownloads download,
            IMailFolder inbox,
            UniqueId messageUid,
            string sender,
            int messageIndex,
            int messagesCount,
            Action<int> SetProgressPercents)
        {
            var email = CreateEmail(download);

            try
            {
                var message = inbox.GetMessage(messageUid);

                string fromAddress =
                    (message.From?.Count ?? 0) > 0
                        ? message.From.Any(m => m.ToString().ToLower() == sender.ToLower())
                            ? sender
                            : message.From[0].ToString()
                        : string.Empty;

                UpdateEmail(email, message.Subject, fromAddress, message.Date.DateTime);

                if (fromAddress == sender)
                {
                    var attachments = message.Attachments;
                    var attachmentsCount = attachments?.Count() ?? 0;

                    for (int j = 0; j < attachmentsCount; j++)
                    {
                        AttachmentDownloader.DownloadAttachment(
                            email,
                            messageUid,
                            message.Attachments,
                            j,
                            download.Directory);

                        SetProgressPercents((messageIndex * 100 + (j + 1) * 100 / attachmentsCount) / messagesCount);
                    }
                }

                inbox.AddFlags(messageUid, MessageFlags.Seen, true);
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"Downloader DownloadMessage error (message uid: {messageUid}): {e}");
                UpdateErrorEmail(
                    email,
                    "Ошибка при чтении письма.",
                    e.ToString());
            }
        }

        static private Emails CreateEmail(DecFormsDownloads download)
        {
            Emails email = new Emails();

            using (Entities db = new Entities())
            {
                db.DecFormsDownloads.Attach(download);
                email.DecFormsDownloads = download;
                db.AddToEmails(email);

                db.SaveChanges();
            }

            return email;
        }

        static private void UpdateEmail(
            Emails email,
            string subject,
            string fromAddress,
            DateTime received)
        {
            using (Entities db = new Entities())
            {
                db.Emails.Attach(email);

                email.Subject = subject.Length > 100 ? subject.Substring(0, 100) : subject;
                email.FromAddress = fromAddress.Length > 100 ? fromAddress.Substring(0, 100) : fromAddress;
                email.Received = received;

                db.SaveChanges();
            }
        }

        static private void UpdateErrorEmail(
            Emails email,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.Emails.Attach(email);

                email.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    email.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }
    }
}
