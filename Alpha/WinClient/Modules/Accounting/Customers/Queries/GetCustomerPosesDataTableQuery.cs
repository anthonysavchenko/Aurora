using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Queries
{
    public static class GetCustomerPosesDataTableQuery
    {
        public static DataTable GetCustomerPosesDataTable(this Entities db, int customerId, bool showAll)
        {
            DateTime _now = db.GetNowDateTime().Date;
            _now = new DateTime(_now.Year, _now.Month, 1);

            var _poses = db.CustomerPoses
                    .Where(p => p.Customers.ID == customerId && (showAll || p.Till >= _now))
                    .Select(p =>
                        new
                        {
                            p.ID,
                            ServiceID = p.Services.ID,
                            p.Since,
                            p.Till,
                            ContractorID = p.Contractors.ID,
                            p.Rate,
                            p.PrivateCounterID
                        })
                    .ToList();

            DataTable _table = GetTable();

            foreach (var _pos in _poses)
            {
                _table.Rows.Add(
                    _pos.ID.ToString(),
                    _pos.ServiceID,
                    _pos.Since,
                    _pos.Till,
                    _pos.ContractorID,
                    _pos.Rate,
                    _pos.PrivateCounterID.HasValue ? _pos.PrivateCounterID.Value.ToString() : string.Empty);
            }

            return _table;
        }

        /// <summary>
        /// Создает таблицу для вкладки "Услуги" модуля "Абонент"
        /// </summary>
        /// <returns>Таблица услуг</returns>
        private static DataTable GetTable()
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Service", typeof(int));
            _table.Columns.Add("Since", typeof(DateTime));
            _table.Columns.Add("Till", typeof(DateTime));
            _table.Columns.Add("Contractor", typeof(int));
            _table.Columns.Add("Rate", typeof(decimal));
            _table.Columns.Add("Counter", typeof(string));

            DataSet _ds = new DataSet { EnforceConstraints = false };
            _ds.Tables.Add(_table);

            return _table;
        }
    }
}
