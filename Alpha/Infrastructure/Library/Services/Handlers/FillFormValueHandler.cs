﻿using System;
using System.Linq;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services.Handlers
{
    static public class FillFormValueHandler
    {
        static public void ClearExistedValues(int buildingID, DateTime month)
        {
            using (var db = new Entities())
            {
                var existedValues =
                    db.FillFormValues
                        .Where(v =>
                            v.PrivateCounters.Customers.Buildings.ID == buildingID
                            && v.Month == month)
                        .ToList();

                existedValues.ForEach(v => db.FillFormValues.DeleteObject(v));
                db.SaveChanges();
            }
        }
    }
}