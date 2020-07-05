using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser
{
    static public class FileParser
    {
        static public DecFormsUploadPoses CreateUploadPos(
            string fileName,
            DecFormsUploads upload)
        {
            DecFormsUploadPoses uploadPos =
                new DecFormsUploadPoses()
                {
                    FileName = fileName,
                    FormType = (byte)DecFormsType.Unknown,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploads.Attach(upload);
                uploadPos.DecFormsUploads = upload;
                db.DecFormsUploadPoses.AddObject(uploadPos);
                db.SaveChanges();
            }

            return uploadPos;
        }

        static public void SaveError(
            DecFormsUploadPoses uploadPos,
            string message)
        {
            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);
                uploadPos.Error = message;
                db.SaveChanges();
            }
        }
    }
}
