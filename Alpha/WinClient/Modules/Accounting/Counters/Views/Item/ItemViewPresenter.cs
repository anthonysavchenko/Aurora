using System.Data;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.Alpha.Infrastructure.SQLAccessProvider.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;

using DomPrivateCounter = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.PrivateCounter;
using DomService = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, DomPrivateCounter>
    {
        protected override void FillDomainFromAllViews(DomPrivateCounter _domItem)
        {
            _domItem.Model = View.CounterModel.Trim();
            _domItem.Number = View.CounterNum.Trim();
            _domItem.Service = View.CounterService;
            _domItem.Archived = View.Archived;
        }

        protected override bool CheckPreSaveConditions(DomPrivateCounter _domItem, out string _errorMessage)
        {
            _errorMessage = string.Empty;

            if (string.IsNullOrEmpty(_domItem.Model))
            {
                _errorMessage += "Не указана модель\r\n";
            }

            if (string.IsNullOrEmpty(_domItem.Number))
            {
                _errorMessage += "Не указана номер\r\n";
            }

            if (_domItem.Service == null)
            {
                _errorMessage += "Не указана модель";
            }

            return string.IsNullOrEmpty(_errorMessage);
        }

        protected override void ShowDomainOnAllViews(DomPrivateCounter _domItem)
        {
            int _id = int.Parse(WorkItem.State[CommonStateNames.CurrentItemId].ToString());
            DataTable _values;
            CounterInfo _counterInfo;

            using (Entities _db = new Entities())
            {
                _counterInfo = _db.GetCounterInfo(_id);
                _values = _db.GetCounterValues(_id);
                // Только для УК ФР
                View.Services = _db.GetServicesForComboBox(x => x.ServiceTypes.Code == ServiceTypeConstants.PP_ELECTRICITY);

                //Для остальных
                /*View.Services = _db.GetServicesForComboBox(x => 
                    x.ChargeRule == (byte)ChargeRuleType.CounterRate 
                    || x.ChargeRule == (byte)ChargeRuleType.PublicPlaceAreaRate
                    || x.ChargeRule == (byte)ChargeRuleType.PublicPlaceVolumeAreaRate);*/
            }

            View.CounterNum = _counterInfo.Number;
            View.CounterService = GetItem<DomService>(_counterInfo.ServiceId.ToString());
            View.CounterModel = _counterInfo.Model;
            View.CustomerData = _counterInfo.CustomerData;
            View.CounterValueTable = _values;
            View.Archived = _domItem.Archived;
        }
    }
}
