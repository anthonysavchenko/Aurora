using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class DecFormsUploadPosHandler
    {
        static public DecFormsUploadPoses CreateUploadPos(
            string fileName,
            DecFormsUploads upload)
        {
            var uploadPos =
                new DecFormsUploadPoses()
                {
                    FileName = fileName.Length > 200 ? fileName.Substring(fileName.Length - 200, 200) : fileName,
                    FormType = (byte)DecFormsType.Unknown,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploads.Attach(upload);
                uploadPos.DecFormsUploads = upload;
                db.AddToDecFormsUploadPoses(uploadPos);

                db.SaveChanges();
            }

            return uploadPos;
        }

        static public void UpdateUploadPosWithError(
            DecFormsUploadPoses uploadPos,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);

                uploadPos.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    uploadPos.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }
    }
}
