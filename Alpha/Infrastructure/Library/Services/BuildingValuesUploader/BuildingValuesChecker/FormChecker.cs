using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesChecker
{
    public static class FormChecker
    {
        public static void CheckForm(int formID)
        {
            CheckRedundantBuildings(formID);
        }

        private static void CheckRedundantBuildings(int formID)
        {
            using (var db = new Entities())
            {
                var rowsWithErrors =
                    db.BuildingValuesRows
                        .Where(r =>
                            r.BuildingValuesForms.ID == formID
                                && r.ProcessingResult == (byte)RowProcessingResult.OK
                                && db.Buildings
                                    .Count(b =>
                                        b.Street.Equals(
                                            r.Street,
                                            StringComparison.OrdinalIgnoreCase)
                                            && b.Number.Equals(
                                                r.Building,
                                                StringComparison.OrdinalIgnoreCase)) == 0)
                        .ToList();

                rowsWithErrors
                    .ForEach(r =>
                        BuildingValuesRowHandler.SetCheckingError(
                            r,
                            "Распознанного адреса дома нет в справочнике обслуживаемых домов, " +
                                "поэтому данные по этому дому не могут быть сохранены."));

                db.SaveChanges();
            }
        }
    }
}
