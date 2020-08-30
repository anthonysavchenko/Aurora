using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsDownloader.DecFormsImapDownloader
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
            var attachment = AttachmentHandler.CreateAttachment(email);

            try
            {
                var mimeEntity = mimeEntities.ElementAt(attachmentIndex);
                var fileName = GetFileName(mimeEntity);

                if (string.IsNullOrEmpty(fileName))
                {
                    AttachmentHandler.UpdateAttachmentWithError(
                        attachment,
                        "Не удалось определить имя файла.");
                    return;
                }

                AttachmentHandler.UpdateAttachment(attachment, fileName);

                if (!fileName.EndsWith(".xls"))
                {
                    AttachmentHandler.UpdateAttachmentWithError(
                        attachment,
                        "Сохраняются файлы только в формате Excel 97-2003 (*.xls)");
                    return;
                }

                var filePath = Path.Combine(directory, fileName);

                if (File.Exists(filePath))
                {
                    AttachmentHandler.UpdateAttachmentWithError(
                        attachment,
                        "Файл с таким именем уже существует в выбранной папке.");
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
                Logger.SimpleWrite("AttachmentDownloader DownloadAttachment error " +
                    $"(message uid: {messageUid}, attachment: {attachmentIndex}): {e}");
                AttachmentHandler.UpdateAttachmentWithError(attachment, "Ошибка при скачивании файла.", e.ToString());
            }
        }

        static private string GetFileName(MimeEntity mimeEntity)
        {
            string fileName;

            fileName = GetFileNameEncodedToWin1251(mimeEntity, HeaderId.ContentDisposition, "filename")
                ?? GetFileNameEncodedToWin1251(mimeEntity, HeaderId.ContentType, "name");

            if (string.IsNullOrEmpty(fileName) || fileName.StartsWith("=?UTF-8?"))
            {
                fileName = mimeEntity.ContentDisposition?.FileName ?? mimeEntity.ContentType?.Name;
            }

            return fileName;
        }

        static private string GetFileNameEncodedToWin1251(
            MimeEntity mimeEntity,
            HeaderId headerId,
            string propertyName)
        {
            string fileName = null;

            int headerIndex = mimeEntity.Headers.IndexOf(headerId);

            if (headerIndex >= 0)
            {
                var header = mimeEntity.Headers[headerIndex];
                string headerContent = Encoding.GetEncoding(1251).GetString(header.RawValue);

                int fileNameIndex = headerContent.LastIndexOf(propertyName);

                if (fileNameIndex >= 0)
                {
                    fileName = headerContent
                        .Substring(fileNameIndex)
                        .Replace($"{propertyName}=", string.Empty)
                        .Replace("\"", string.Empty)
                        .Replace("\r\n", string.Empty);
                }
            }

            return fileName;
        }
    }
}
