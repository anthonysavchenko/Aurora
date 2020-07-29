using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class FillFormHandler
    {
        static public void CreateForm(
            DecFormsUploadPoses uploadPos,
            string street,
            string building,
            List<FillFormPoses> poses)
        {
            FillForms form =
                new FillForms()
                {
                    Street = street,
                    Building = building,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);
                db.FillForms.AddObject(form);

                uploadPos.FormType = (byte)DecFormsType.FillForm;
                uploadPos.FillForm = form;

                form.DecFormsUploadPoses = uploadPos;

                foreach (FillFormPoses pos in poses)
                {
                    pos.FillForms = form;
                    db.FillFormPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }
    }
}
