using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Queries;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Wizard.Services
{
    public class WizardService : IWizardService
    {
        public DataTable GetServiceTable()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (var service in _entities.Services.OrderBy(s => s.Name))
                {
                    _table.Rows.Add(service.ID, service.Name);
                }
            }

            return _table;
        }
    }
}
