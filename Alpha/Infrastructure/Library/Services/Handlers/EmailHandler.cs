using System;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class EmailHandler
    {
        static public Emails CreateEmail(DecFormsDownloads download)
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

        static public void UpdateEmail(
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

        static public void UpdateEmailWithError(
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
