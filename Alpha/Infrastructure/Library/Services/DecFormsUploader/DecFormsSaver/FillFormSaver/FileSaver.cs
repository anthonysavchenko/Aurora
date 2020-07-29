using System;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsSaver.FillFormSaver
{
    static public class FileSaver
    {
        static public void SaveFile(FillForms form, int buildingID, DateTime month)
        {
            FillFormValueHandler.ClearExistedValues(buildingID, month);
            PrivateCounterHandler.ClearExistedCounters(buildingID);
            CustomerHandler.ClearExistedCustomers(buildingID);

            foreach (var row in form.FillFormPoses)
            {
                var customerID =
                    CustomerHandler.GetCustomer(
                        buildingID,
                        row.Apartment)
                    ?? CustomerHandler.CreateCustomer(
                        buildingID,
                        row.Apartment);

                var counterType = GetPrivateCounterType((FillFormCounterType)row.CounterType);

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

        static public int? GetBuilding(FillForms form)
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
            FillFormCounterType fillFormCounterType)
        {
            if (fillFormCounterType == FillFormCounterType.Common)
            {
                return PrivateCounterType.Common;
            }
            else if (fillFormCounterType == FillFormCounterType.Day
                || fillFormCounterType == FillFormCounterType.Night)
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
            FillFormPoses pos)
        {
            if ((FillFormCounterType)pos.CounterType == FillFormCounterType.Common)
            {
                FillFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Common,
                    pos.PrevValue,
                    counterID,
                    pos);
            }
            else if ((FillFormCounterType)pos.CounterType == FillFormCounterType.Day)
            {
                FillFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Day,
                    pos.PrevDayValue,
                    counterID,
                    pos);
            }
            else if ((FillFormCounterType)pos.CounterType == FillFormCounterType.Night)
            {
                FillFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Night,
                    pos.PrevNightValue,
                    counterID,
                    pos);
            }
            else if ((FillFormCounterType)pos.CounterType == FillFormCounterType.Norm)
            {
                FillFormValueHandler.CreateValue(
                    month,
                    PrivateCounterValueType.Norm,
                    null,
                    counterID,
                    pos);
            }
        }
    }
}
