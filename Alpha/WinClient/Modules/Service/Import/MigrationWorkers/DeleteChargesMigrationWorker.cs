using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.MigrationWorkers
{
    public class DeleteChargesMigrationWorker : IMigrationWorker
    {
        private IMigrationView _view;

        public DeleteChargesMigrationWorker(IMigrationView view)
        {
            _view = view;
        }

        public int GetTotalCount()
        {
            int _result;
            using (Entities _db = new Entities())
            {
                _result = _db.ChargeOperPoses
                    .Where(p => p.Services.ServiceTypes.ID == 35)
                    .Select(p => p.ChargeOpers.ID)
                    .Distinct()
                    .Count();
            }

            return _result;
        }

        public void StartMigration()
        {
            using (Entities _db = new Entities())
            {
                _db.CommandTimeout = 3600;

                var _opers =
                    _db.ChargeOpers
                        .Include(p => p.ChargeSets)
                        .Include(p => p.RegularBillDocs)
                        .Include(p => p.RegularBillDocs.RegularBillDocSeviceTypePoses)
                        .Where(c => c.ChargeOperPoses.Any(p => p.Services.ServiceTypes.ID == 35))
                        .ToList();

                var _posByOper =
                    _db.ChargeOperPoses
                        .Where(p => p.Services.ServiceTypes.ID == 35)
                        .GroupBy(p => p.ChargeOpers.ID)
                        .ToDictionary(g => g.Key, g => g.Select(p => p).ToList());

                foreach (var _oper in _opers)
                {
                    List<ChargeOperPoses> _poses = _posByOper[_oper.ID];

                    foreach (ChargeOperPoses _pos in _poses)
                    {
                        _oper.Value -= _pos.Value;
                        _oper.ChargeSets.ValueSum -= _pos.Value;

                        _oper.RegularBillDocs.MonthChargeValue -= _pos.Value;
                        _oper.RegularBillDocs.Value -= _pos.Value;

                        _db.ChargeOperPoses.DeleteObject(_pos);
                    }

                    RegularBillDocSeviceTypePoses _billPos = 
                        _oper.RegularBillDocs.RegularBillDocSeviceTypePoses.FirstOrDefault(p => p.ServiceTypeName == "Содержание общедомового имущества");

                    _db.RegularBillDocSeviceTypePoses.DeleteObject(_billPos);

                    _db.SaveChanges();
                    _view.AddProgress(1);
                }
            }
        }
    }
}