using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int uploadId)
        {
            DataTable table = CreateDataTable();

            var uploadPoses =
                db.BuildingValuesUploadPoses
                    .Where(x => x.BuildingValuesUploads.ID == uploadId)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Street,
                            Building = 
                                !string.IsNullOrEmpty(x.Street) && !string.IsNullOrEmpty(x.Building)
                                    ? x.Street + ", д. " + x.Building
                                    : null,
                            x.CounterNumber,
                            x.Coefficient,
                            x.CurrentValue,
                            x.PrevValue,
                            x.CurrentDate,
                            x.ErrorDescription,
                        })
                    .ToList()
                    .OrderBy(x => x.ID);

            foreach (var item in uploadPoses)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Building,
                    item.CounterNumber,
                    item.Coefficient,
                    item.CurrentValue,
                    item.PrevValue,
                    item.CurrentDate.HasValue ? item.CurrentDate.Value.ToString("dd.MM.yyyy") : null,
                    !string.IsNullOrEmpty(item.ErrorDescription) ? "Ошибка" : "ОК",
                    item.ErrorDescription);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Building");
            table.Columns.Add("CounterNumber");
            table.Columns.Add("Coefficient");
            table.Columns.Add("CurrentValue");
            table.Columns.Add("PrevValue");
            table.Columns.Add("CurrentDate");
            table.Columns.Add("Result");
            table.Columns.Add("Description");

            DataSet ds =
                new DataSet
                {
                    EnforceConstraints = false
                };
            ds.Tables.Add(table);

            return table;
        }
    }
}
