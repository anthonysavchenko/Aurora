using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int Id)
        {
            DataTable table = CreateDataTable();

            var items =
                db.FillFormPoses
                    .Where(x => x.FillForms.ID == Id)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Account,
                            x.Apartment,
                            x.CounterModel,
                            x.CounterNumber,
                            CounterType =
                                x.CounterType == (byte)FillFormCounterType.Common
                                    ? "Однотарифный"
                                    : x.CounterType == (byte)FillFormCounterType.Day
                                        || x.CounterType == (byte)FillFormCounterType.Night
                                            ? "Двухтарифный"
                                            : "Норматив",
                            x.CounterCapacity,
                            x.PrevDate,
                            x.PrevValue,
                            x.PrevDayValue,
                            x.PrevNightValue,
                        })
                    .ToList()
                    .OrderBy(x => x.Apartment, new StringWithNumbersComparer());

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Account,
                    item.Apartment,
                    item.CounterModel,
                    item.CounterNumber,
                    item.CounterType,
                    item.CounterCapacity,
                    item.PrevDate,
                    item.PrevValue,
                    item.PrevDayValue,
                    item.PrevNightValue);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Account");
            table.Columns.Add("Apartment");
            table.Columns.Add("CounterModel");
            table.Columns.Add("CounterNumber");
            table.Columns.Add("CounterType");
            table.Columns.Add("CounterCapacity");
            table.Columns.Add("PrevDate", typeof(DateTime));
            table.Columns.Add("PrevValue");
            table.Columns.Add("PrevDayValue");
            table.Columns.Add("PrevNightValue");

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
