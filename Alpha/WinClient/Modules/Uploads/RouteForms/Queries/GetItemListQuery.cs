using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.RouteForms.Queries
{
    public static class GetItemListQuery
    {
        public static DataTable GetItemList(this Entities db, int Id)
        {
            DataTable table = CreateDataTable();

            var items =
                db.RouteFormPoses
                    .Where(x => x.RouteForms.ID == Id)
                    .Select(x =>
                        new
                        {
                            x.ID,
                            x.Account,
                            x.Owner,
                            x.Apartment,
                            x.CounterNumber,
                            CounterType =
                                x.CounterType == (byte)RouteFormCounterType.Common
                                    ? "Однотарифный"
                                    : x.CounterType == (byte)RouteFormCounterType.DayAndNight
                                        ? "Двухтарифный"
                                        : "Норматив",
                            x.CounterCapacity,
                            x.Debt,
                            x.Payed,
                            x.PrevDate,
                            x.PrevValue,
                            x.PrevDayValue,
                            x.PrevNightValue,
                            x.Phone,
                            x.Note,
                        })
                    .ToList()
                    .OrderBy(x => x.Apartment, new StringWithNumbersComparer());

            foreach (var item in items)
            {
                table.Rows.Add(
                    item.ID.ToString(),
                    item.Account,
                    item.Owner,
                    item.Apartment,
                    item.CounterNumber,
                    item.CounterType,
                    item.CounterCapacity,
                    item.Debt,
                    item.Payed,
                    item.PrevDate,
                    item.PrevValue,
                    item.PrevDayValue,
                    item.PrevNightValue,
                    item.Phone,
                    item.Note);
            }

            return table;
        }

        private static DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Account");
            table.Columns.Add("Owner");
            table.Columns.Add("Apartment");
            table.Columns.Add("CounterNumber");
            table.Columns.Add("CounterType");
            table.Columns.Add("CounterCapacity");
            table.Columns.Add("Debt");
            table.Columns.Add("Payed");
            table.Columns.Add("PrevDate", typeof(DateTime));
            table.Columns.Add("PrevValue");
            table.Columns.Add("PrevDayValue");
            table.Columns.Add("PrevNightValue");
            table.Columns.Add("Phone");
            table.Columns.Add("Note");


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
