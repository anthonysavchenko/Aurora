using System.Collections.Generic;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Commands;
using Taumis.Alpha.Infrastructure.Interface.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Commands
{
    public class CreateBillSetsCommandHandler : ICommandHandler<CreateBillSetsCommand>
    {
        public void Execute(CreateBillSetsCommand command)
        {
            command.Result = new Dictionary<int, int>();

            using (Entities _db = new Entities())
            {
                var _zipCodes = _db.Buildings
                    .GroupBy(x => x.ZipCode)
                    .Select(g =>
                        new
                        {
                            ZipCode = g.Key,
                            BuildingIds = g.Select(x => x.ID)
                        })
                    .ToList();

                foreach (var _zc in _zipCodes)
                {
                    BillSets _billSet =
                        new BillSets()
                        {
                            CreationDateTime = command.CreationDateTime,
                            Number = _db.BillSets.Any() ? _db.BillSets.Max(c => c.Number) + 1 : 1,
                            BillType = (byte)BillTypes.Regular,
                        };
                    _db.AddToBillSets(_billSet);
                    _db.SaveChanges();

                    foreach (int _id in _zc.BuildingIds)
                    {
                        command.Result.Add(_id, _billSet.ID);
                    }
                }
            }
        }
    }
}
