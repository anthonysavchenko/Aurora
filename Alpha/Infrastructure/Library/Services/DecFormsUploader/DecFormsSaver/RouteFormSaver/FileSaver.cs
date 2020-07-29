using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver.RouteFormSaver
{
    static public class FileSaver
    {
        static public void SaveFile(RouteForms form, int buildingID, DateTime month)
        {
            RouteFormValueHandler.ClearExistedValues(buildingID, month);
            PrivateCounterHandler.ClearExistedCounters(buildingID);
            CustomerHandler.ClearExistedCustomers(buildingID);

            foreach (var row in form.RouteFormPoses)
            {
                var customerID =
                    CustomerHandler.GetCustomer(
                        buildingID,
                        row.Apartment)
                    ?? CustomerHandler.CreateCustomer(
                        buildingID,
                        row.Apartment);

                var counterType = GetPrivateCounterType((RouteFormCounterType)row.CounterType);

                var counterID =
                    PrivateCounterHandler.GetCounter(
                        customerID,
                        counterType,
                        row.CounterNumber)
                    ?? PrivateCounterHandler.CreateCounter(
                        customerID,
                        counterType,
                        row.CounterNumber);

                CreateValues(month, counterID, row);
            }
        }

        static public int? GetBuilding(RouteForms form)
        {
            using (var db = new Entities())
            {
                var building = 
                    db.Buildings
                        .FirstOrDefault(b =>
                            b.Street.ToLower() == form.Street.ToLower()
                            && b.Number.ToLower() == form.Building.ToLower());

                return building?.ID;
            }
        }

        static private PrivateCounterType GetPrivateCounterType(
            RouteFormCounterType routeFormCounterType)
        {
            if (routeFormCounterType == RouteFormCounterType.Common)
            {
                return PrivateCounterType.Common;
            }
            else if (routeFormCounterType == RouteFormCounterType.DayAndNight)
            {
                return PrivateCounterType.DayAndNight;
            }
            else
            {
                return PrivateCounterType.Norm;
            }
        }

        static private void CreateValues(
            DateTime month,
            int counterID,
            RouteFormPoses pos)
        {
            if (pos.CounterType == (byte)RouteFormCounterType.Common)
            {
                RouteFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Common,
                    pos.PrevValue,
                    counterID,
                    pos);
            }
            else if (pos.CounterType == (byte)RouteFormCounterType.DayAndNight)
            {
                RouteFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Day,
                    pos.PrevDayValue,
                    counterID,
                    pos);
                RouteFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Night,
                    pos.PrevNightValue,
                    counterID,
                    pos);
            }
            else if (pos.CounterType == (byte)RouteFormCounterType.Norm)
            {
                RouteFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Norm,
                    null,
                    counterID,
                    pos);
            }
        }
    }
}
