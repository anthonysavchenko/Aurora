using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateValuesUploadHandler
    {
        static public bool CreateUpload(
            string directory,
            DateTime month,
            string note,
            int userID,
            out PrivateValuesUploads upload)
        {
            upload = null;

            try
            {
                var newUpload =
                    new PrivateValuesUploads()
                    {
                        Created = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                        Month = month,
                        Directory = directory.Length > 200 ? directory.Substring(0, 200) : directory,
                        Note = note.Length > 250 ? note.Substring(0, 250) : note,
                    };

                using (Entities db = new Entities())
                {
                    newUpload.Author = db.Users.First(u => u.ID == userID);

                    db.AddToPrivateValuesUploads(newUpload);

                    db.SaveChanges();
                }

                upload = newUpload;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"PrivateValuesUploadHandler CreateUpload error: {e}");
                return false;
            }

            return true;
        }

        static public void UpdateUploadWithError(
            PrivateValuesUploads upload,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.PrivateValuesUploads.Attach(upload);

                upload.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    upload.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }
    }
}
