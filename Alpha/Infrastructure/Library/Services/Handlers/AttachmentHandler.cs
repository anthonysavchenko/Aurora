using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class AttachmentHandler
    {
        static public Attachments CreateAttachment(Emails email)
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

        static public void UpdateAttachment(
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

        static public void UpdateAttachmentWithError(
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
