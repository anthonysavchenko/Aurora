using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class CalculationFormHandler
    {
        static public void CreateForm(
            int fileID,
            List<CalculationRows> rows)
        {
            using (Entities db = new Entities())
            {
                var form = new CalculationForms()
                {
                    CalculationFiles = db.CalculationFiles.First(f => f.ID == fileID),
                };

                db.CalculationForms.AddObject(form);

                foreach (CalculationRows row in rows)
                {
                    row.CalculationForms = form;

                    if (row.RowType == (byte)CalculationRowType.BuildingInfo
                        && row.BuildingInfo != null)
                    {
                        db.CalculationBuildingInfos.AddObject(row.BuildingInfo);
                    }
                    else if (row.RowType == (byte)CalculationRowType.BuildingCounter
                        && row.BuildingCounter != null)
                    {
                        db.CalculationBuildingCounters.AddObject(row.BuildingCounter);
                    }
                    else if (row.RowType == (byte)CalculationRowType.Customer
                        && row.Customer != null)
                    {
                        db.CalculationCustomers.AddObject(row.Customer);
                    }
                    else if (row.RowType == (byte)CalculationRowType.LegalEntity
                        && row.LegalEntity != null)
                    {
                        db.CalculationLegalEntities.AddObject(row.LegalEntity);
                    }

                    db.CalculationRows.AddObject(row);
                }

                db.SaveChanges();
            }
        }
    }
}
