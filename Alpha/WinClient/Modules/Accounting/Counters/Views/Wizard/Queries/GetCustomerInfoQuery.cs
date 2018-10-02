using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Wizard.Queries
{
    public static class GetCustomerInfoQuery
    {
        public static WizardItem GetCustomerInfo(this Entities db, string account)
        {
            return db.Customers
                .Where(x => x.Account == account)
                .Select(x =>
                    new WizardItem
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
                .First();
        }

    }
}
