using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.List
{
    public class ListViewPresenter : BaseListViewPresenter<ListView, BenefitType>
    {
        public override DataTable GetElemList()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("Code", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.BenefitTypes _benefitTypes in _entities.BenefitTypes)
                {
                    _table.Rows.Add(
                        _benefitTypes.ID,
                        _benefitTypes.Name,
                        _benefitTypes.Code);
                }
            }

            return _table;
        }
    }
}