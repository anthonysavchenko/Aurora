using System;
using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.Common.Services.ServerTimeService;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class BuildingValuesUploadHandler
    {
        static public bool CreateUpload(
            string filePath,
            DateTime month,
            string note,
            int userID,
            out BuildingValuesUploads upload)
        {
            upload = null;

            try
            {
                var newUpload =
                    new BuildingValuesUploads()
                    {
                        Created = ServerTimeServiceHolder.ServerTimeService.GetDateTimeInfo().Now,
                        Month = month,
                        FilePath = filePath.Length > 400 ? filePath.Substring(0, 400) : filePath,
                        Note = note.Length > 250 ? note.Substring(0, 250) : note,
                    };

                using (Entities db = new Entities())
                {
                    newUpload.Author = db.Users.First(u => u.ID == userID);

                    db.AddToBuildingValuesUploads(newUpload);

                    db.SaveChanges();
                }

                upload = newUpload;
            }
            catch (Exception e)
            {
                Logger.SimpleWrite($"BuildingValuesUploadHandler CreateUpload error: {e}");
                return false;
            }

            return true;
        }

        static public void UpdateUpload(
            BuildingValuesUploads upload,
            List<BuildingValuesUploadPoses> poses)
        {
            using (Entities db = new Entities())
            {
                db.BuildingValuesUploads.Attach(upload);

                foreach (BuildingValuesUploadPoses pos in poses)
                {
                    pos.BuildingValuesUploads = upload;
                    db.BuildingValuesUploadPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }

        static public void UpdateUploadWithError(
            int uploadID,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                var upload = db.BuildingValuesUploads.First(x => x.ID == uploadID);

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
