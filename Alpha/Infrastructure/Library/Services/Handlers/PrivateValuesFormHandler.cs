using System.Collections.Generic;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class PrivateValuesFormHandler
    {
        static public PrivateValuesForms CreateForm(
            string fileName,
            PrivateValuesUploads upload)
        {
            var form =
                new PrivateValuesForms()
                {
                    FileName = fileName.Length > 200 ? fileName.Substring(fileName.Length - 200, 200) : fileName,
                };

            using (Entities db = new Entities())
            {
                db.PrivateValuesUploads.Attach(upload);
                form.PrivateValuesUploads = upload;
                db.AddToPrivateValuesForms(form);

                db.SaveChanges();
            }

            return form;
        }

        static public void UpdateForm(
            PrivateValuesForms form,
            string street,
            string building,
            List<PrivateValuesFormPoses> poses)
        {
            using (Entities db = new Entities())
            {
                db.PrivateValuesForms.Attach(form);

                form.Street = street;
                form.Building = building;

                foreach (PrivateValuesFormPoses pos in poses)
                {
                    pos.PrivateValuesForms = form;
                    db.PrivateValuesFormPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }

        static public void UpdateFormWithError(
            PrivateValuesForms form,
            string errorDescription,
            string exceptionMessage = null)
        {
            using (Entities db = new Entities())
            {
                db.PrivateValuesForms.Attach(form);

                form.ErrorDescription = errorDescription;

                if (!string.IsNullOrEmpty(exceptionMessage))
                {
                    form.ExceptionMessage = exceptionMessage;
                }

                db.SaveChanges();
            }
        }
    }
}
