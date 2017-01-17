using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Doc;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    public class ListViewPresenter : BaseMultipleListViewPresenter<ListView, DomItem>
    {
        /// <summary>
        /// Получить таблицу данных
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            ITopView _topView = ((ITopView)WorkItem.SmartParts.Get("TopView"));

            ICustomerDataMapper _dm = DataMapper<DomItem, ICustomerDataMapper>();

            DataTable _result;

            try
            {
                switch (_topView.Filter)
                {
                    case FilterType.Address:
                        _result = _dm.GetList(_topView.Street, _topView.House, _topView.Apartment, _topView.WholeWord);
                        break;
                    case FilterType.Account:
                        _result = _dm.GetListByAccount(_topView.Account);
                        break;
                    case FilterType.ZipCode:
                        _result = _dm.GetListByZipCode(_topView.ZipCode);
                        break;
                    default:
                        return new DataTable();
                }
            }
            catch (Exception _ex)
            {
                _result = new DataTable();
                View.ShowMessage(_ex.ToString(), "Ошибка");
            }

            return _result;
        }
    }
}