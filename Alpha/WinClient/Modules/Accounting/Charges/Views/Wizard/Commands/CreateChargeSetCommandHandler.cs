using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateChargeSetCommandHandler : ICommandHandler<CreateChargeSetCommand>
    {
        public void Execute(CreateChargeSetCommand command)
        {
            using (Entities _db = new Entities())
            {
                Users _user = new Users { ID = int.Parse(UserHolder.User.ID) };
                _db.Users.Attach(_user);

                var _chargeSet =
                    new ChargeSets
                    {
                        CreationDateTime = command.Now,
                        Period = command.Period,
                        Number = _db.ChargeSets.Any() ? _db.ChargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };

                _db.ChargeSets.AddObject(_chargeSet);
                _db.SaveChanges();
                command.Result = _chargeSet.ID;
            }
        }
    }
}
