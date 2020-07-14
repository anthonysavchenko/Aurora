using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader
{
    static public class AttachmentDownloader
    {
        static public void DownloadAttachment(
            Emails email,
            UniqueId messageUid,
            IEnumerable<MimeEntity> mimeEntities,
            int attachmentIndex,
            string directory)
        {
            var attachment = CreateAttachment(email);

            try
            {
                var mimeEntity = mimeEntities.ElementAt(attachmentIndex);
                var fileName = mimeEntity.ContentDisposition?.FileName ?? mimeEntity.ContentType.Name;

                UpdateAttachment(attachment, fileName);

                var filePath = Path.Combine(directory, fileName);

                if (File.Exists(filePath))
                {
                    UpdateErrorAttachment(attachment, "Файл с таким именем уже существует в выбранной папке.");
                    return;
                }

                using (var stream = File.Create(filePath))
                {
                    if (mimeEntity is MessagePart)
                    {
                        var rfc822 = (MessagePart)mimeEntity;

                        rfc822.Message.WriteTo(stream);
                    }
                    else
                    {
                        var part = (MimePart)mimeEntity;

                        part.Content.DecodeTo(stream);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.SimpleWrite("Downloader DownloadFile error " +
                    $"(message uid: {messageUid}, attachment: {attachmentIndex}): {e}");
                UpdateErrorAttachment(attachment, "Ошибка при скачивании файла.", e.ToString());
            }
        }

        static private Attachments CreateAttachment(Emails email)
        {
            Attachments attachment = new Attachments();

            using (Entities db = new Entities())
            {
                db.Emails.Attach(email);
                attachment.Emails = email;
                db.AddToAttachments(attachment);

                db.SaveChanges();
            }

            return attachment;
        }

        static private void UpdateAttachment(
            Attachments attachment,
            string fileName)
        {
            using (Entities db = new Entities())
            {
                db.Attachments.Attach(attachment);

                attachment.FileName = fileName.Length > 200 ? fileName.Substring(0, 200) : fileName;

                db.SaveChanges();
            }
        }

        static private void UpdateErrorAttachment(
            Attachments attachment,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.Attachments.Attach(attachment);

                attachment.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    attachment.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }

    }
}
