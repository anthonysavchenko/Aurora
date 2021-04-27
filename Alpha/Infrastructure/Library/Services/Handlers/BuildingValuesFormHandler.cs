using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class BuildingValuesFormHandler
    {
        static public void CreateForm(
            int fileID,
            List<BuildingValuesRows> rows)
        {
            using (Entities db = new Entities())
            {
                var form = new BuildingValuesForms()
                {
                    BuildingValuesFiles = db.BuildingValuesFiles.First(f => f.ID == fileID),
                };

                db.BuildingValuesForms.AddObject(form);

                foreach (BuildingValuesRows row in rows)
                {
                    row.BuildingValuesForms = form;
                    db.BuildingValuesRows.AddObject(row);
                }

                db.SaveChanges();
            }
        }
    }
}
