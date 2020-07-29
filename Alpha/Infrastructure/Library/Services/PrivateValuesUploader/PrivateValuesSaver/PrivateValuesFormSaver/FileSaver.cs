using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesSaver.PrivateValuesFormSaver
{
    static public class FileSaver
    {
        static public void SaveFile(PrivateValuesForms form, int buildingID, DateTime month)
        {
            PrivateCounterValueHandler.ClearExistedValues(buildingID, month);
            PrivateCounterHandler.ClearExistedCounters(buildingID);
            CustomerHandler.ClearExistedCustomers(buildingID);

            foreach (var row in form.PrivateValuesFormPoses)
            {
                var customerID =
                    CustomerHandler.GetCustomer(
                        buildingID,
                        row.Apartment)
                    ?? CustomerHandler.CreateCustomer(
                        buildingID,
                        row.Apartment);

                var counterType = GetPrivateCounterType((PrivateValuesFormCounterType)row.CounterType);

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

        static public int? GetBuilding(PrivateValuesForms form)
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
            PrivateValuesFormCounterType privateValuesFormCounterType)
        {
            if (privateValuesFormCounterType == PrivateValuesFormCounterType.Common)
            {
                return PrivateCounterType.Common;
            }
            else if (privateValuesFormCounterType == PrivateValuesFormCounterType.Day
                || privateValuesFormCounterType == PrivateValuesFormCounterType.Night)
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
            PrivateValuesFormPoses pos)
        {
            if ((PrivateValuesFormCounterType)pos.CounterType == PrivateValuesFormCounterType.Common)
            {
                PrivateCounterValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Common,
                    pos.CurrentValue,
                    counterID,
                    pos);
            }
            else if ((PrivateValuesFormCounterType)pos.CounterType == PrivateValuesFormCounterType.Day)
            {
                PrivateCounterValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Day,
                    pos.CurrentDayValue,
                    counterID,
                    pos);
            }
            else if ((PrivateValuesFormCounterType)pos.CounterType == PrivateValuesFormCounterType.Night)
            {
                PrivateCounterValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Night,
                    pos.CurrentNightValue,
                    counterID,
                    pos);
            }
            else if ((PrivateValuesFormCounterType)pos.CounterType == PrivateValuesFormCounterType.Norm)
            {
                PrivateCounterValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Norm,
                    null,
                    counterID,
                    pos);
            }
        }
    }
}
