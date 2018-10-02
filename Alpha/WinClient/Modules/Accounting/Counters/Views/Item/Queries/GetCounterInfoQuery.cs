using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Queries
{
    public static class GetCounterInfoQuery
    {
        public static CounterInfo GetCounterInfo(this Entities db, int counterId)
        {
            return db.PrivateCounters
                .Where(x => x.ID == counterId)
                .Select(x =>
                    new CounterInfo
                    {
                        Model = x.Model,
                        Number = x.Number,
                        Service = x.Services.Name,
                        CustomerData =
                            new CustomerData
                            {
                                Account = x.Customers.Account,
                                Apartment = x.Customers.Apartment,
                                Area = x.Customers.Square,
                                Building = x.Customers.Buildings.Number,
                                Street = x.Customers.Buildings.Streets.Name,
                                Owner = x.Customers.OwnerType == (int)OwnerType.PhysicalPerson
                                    ? x.Customers.PhysicalPersonShortName
                                    : x.Customers.JuridicalPersonFullName
                            }
                    })
                .First();
        }
    }
}
