using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Model;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries
{
    public static class GetCustomerInfoQuery
    {
        public static CustomerInfo GetCustomerInfo(this Entities db, string account)
        {
            return db.Customers
                .Where(x => x.Account == account)
                .Select(x =>
                    new CustomerInfo
                    {
                        Account = x.Account,
                        Apartment = x.Apartment,
                        Area = x.Square,
                        Building = x.Buildings.Number,
                        CustomerId = x.ID,
                        CustomerName = x.OwnerType == (int)OwnerType.JuridicalPerson
                            ? x.JuridicalPersonFullName
                            : x.PhysicalPersonShortName,
                        Street = x.Buildings.Streets.Name
                    })
                .FirstOrDefault();
        }

    }
}
