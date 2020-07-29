using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class RouteFormHandler
    {
        static public void CreateForm(
            DecFormsUploadPoses uploadPos,
            string street,
            string building,
            List<RouteFormPoses> poses)
        {
            RouteForms form =
                new RouteForms()
                {
                    Street = street,
                    Building = building,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);
                db.RouteForms.AddObject(form);

                uploadPos.FormType = (byte)DecFormsType.RouteForm;
                uploadPos.RouteForm = form;

                form.DecFormsUploadPoses = uploadPos;

                foreach (RouteFormPoses pos in poses)
                {
                    pos.RouteForms = form;
                    db.RouteFormPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }
    }
}
