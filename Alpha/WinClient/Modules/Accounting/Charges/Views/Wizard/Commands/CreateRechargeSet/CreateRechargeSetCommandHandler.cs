using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateRechargeSetCommandHandler : ICommandHandler<CreateRechargeSetCommand>
    {
        public void Execute(CreateRechargeSetCommand cmd)
        {
            using (var _db = new Entities())
            {
                Users _user = new Users { ID = cmd.AuthorId };
                _db.Users.Attach(_user);

                RechargeSets _rechargeSet =
                    new RechargeSets
                    {
                        CreationDateTime = cmd.Now,
                        Period = cmd.Period,
                        Number = _db.RechargeSets.Any() ? _db.RechargeSets.Max(c => c.Number) + 1 : 1,
                        Author = _user
                    };
                _db.AddToRechargeSets(_rechargeSet);
                _db.SaveChanges();
                cmd.Result = _rechargeSet.ID;
            }
        }
    }
}
