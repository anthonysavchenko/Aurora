using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    public static class BuildingCalculationValueHandler
    {
        public class BuildingInfo
        {
            public int BuildingID { get; set; }
            public decimal CollectiveVolume { get; set; }
        }

        static public void Delete(int formID, bool useDrafts, out List<BuildingInfo> buildingInfos)
        {
            using (var db = new Entities())
            {
                buildingInfos =
                    useDrafts
                        ? db.BuildingCalculationValues
                            .Where(v => v.CalculationRows.CalculationForms.ID == formID)
                            .Select(v => new BuildingInfo()
                            {
                                BuildingID = v.Buildings.ID,
                                CollectiveVolume = v.CollectiveVolume,
                            })
                            .ToList()
                        : null;

                var values =
                    db.BuildingCalculationValues
                        .Where(v => v.CalculationRows.CalculationForms.ID == formID)
                        .ToList();

                values.ForEach(v =>
                    db.BuildingCalculationValues.DeleteObject(v));

                db.SaveChanges();
            }
        }
    }
}
