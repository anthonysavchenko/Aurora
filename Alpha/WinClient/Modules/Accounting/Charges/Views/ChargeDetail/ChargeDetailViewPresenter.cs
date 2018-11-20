using System;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.List;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.ChargeDetail
{
    public class ChargeDetailViewPresenter : BaseDomainPresenter<IChargeDetailView>
    {
        /// <summary>
        /// ќтображает домен на view
        /// </summary>
        public void ShowDomainOnView()
        {
            BaseChargeOper _oper;
            string _id = (string)WorkItem.State[ModuleStateNames.CURRENT_CHARGE_OPER_ID];
            DataTable _poses;
            DateTime _periodCharged;

            if (WorkItem.State[CommonStateNames.CurrentItem] is RechargeSet)
            {
                _oper = GetItem<RechargeOper>(_id);
                _poses = DataMapper<RechargeOperPos, IRechargeOperPosDataMapper>().GetList((RechargeOper)_oper);
                _periodCharged = ((RechargeOper)_oper).ChargeSet.Period;
                RebenefitOper _benefitOper = ((RechargeOper)_oper).RebenefitOper;
                View.BenefitValue =
                     _benefitOper != null
                        ? Math.Abs(_benefitOper.Value)
                        : 0;
                WorkItem.State[ModuleStateNames.CURRENT_REGULAR_BILL_ID] = string.Empty;
                View.BillLinkEnabled = false;

                SetRechargeLink(int.Parse(_oper.ID), ((RechargeOper)_oper).ChargeCorrectionOper != null);
            }
            else
            {
                _oper = GetItem<ChargeOper>(_id);
                _poses = DataMapper<ChargeOperPos, IChargeOperPosDataMapper>().GetList((ChargeOper)_oper);
                _periodCharged = ((ChargeOper)_oper).ChargeSet.Period;
                BenefitOper _benefitOper = ((ChargeOper)_oper).BenefitOper;
                View.BenefitValue =
                     _benefitOper != null
                        ? Math.Abs(_benefitOper.Value)
                        : 0;

                WorkItem.State[ModuleStateNames.CURRENT_REGULAR_BILL_ID] = ((ChargeOper)_oper).RegularBillDoc.ID;
                View.BillLinkEnabled = true;

                SetRechargeLink(int.Parse(_oper.ID), ((ChargeOper)_oper).ChargeCorrectionOper != null);
            }

            View.Account = _oper.Customer.Account;
            View.Owner =
                _oper.Customer.OwnerType == OwnerType.PhysicalPerson
                    ? _oper.Customer.PhysicalPersonShortName
                    : _oper.Customer.JuridicalPersonFullName;
            View.Apartment = _oper.Customer.Apartment;
            View.House = _oper.Customer.Building.Number;
            View.Street = _oper.Customer.Building.Street.Name;
            View.Square = _oper.Customer.Square;
            View.PeriodCharged = _periodCharged;
            View.Value = Math.Abs(_oper.Value);
            View.ChargeOperPoses = _poses;
        }

        /// <summary>
        /// ”станавливает данные, необходимые дл¤ перехода по ссылке "ѕерерасчет"
        /// </summary>
        /// <param name="chargeOperId">ID операции начислени¤</param>
        /// <param name="isChargeCorrected">ќткорректированно ли начисление</param>
        private void SetRechargeLink(int chargeOperId, bool isChargeCorrected)
        {
            if (isChargeCorrected)
            {
                using (Entities _entities = new Entities())
                {
                    var _recharges =
                        _entities.RechargeOpers
                            .Where(
                                r =>
                                r.ChargeOpers.ID == chargeOperId &&
                                r.ChildChargeCorrectionOpers == null)
                            .Select(
                                r =>
                                new
                                {
                                    RechageOperID = r.ID,
                                    RechargeSetID = r.RechargeSets.ID,
                                    RechargeSetCreationDateTime = r.RechargeSets.CreationDateTime
                                }).ToList();

                    if (_recharges.Any())
                    {
                        var _recharge = _recharges.First();
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_ID] = _recharge.RechargeSetID.ToString();
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_OPER_ID] = _recharge.RechageOperID.ToString();
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_CREATION_DATETIME] = _recharge.RechargeSetCreationDateTime;
                        View.RechargeLinkEnabled = true;
                    }
                    else
                    {
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_ID] = string.Empty;
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_OPER_ID] = string.Empty;
                        WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_CREATION_DATETIME] = null;
                        View.RechargeLinkEnabled = false;
                    }
                }
            }
            else
            {
                WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_ID] = string.Empty;
                WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_OPER_ID] = string.Empty;
                WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_CREATION_DATETIME] = null;
                View.RechargeLinkEnabled = false;
            }
        }

        /// <summary>
        /// ќбрабатывает нажатие на ссылку в названии операции
        /// </summary>
        public void SelectLink()
        {
            WorkItem.Controller.RunUsecase(ApplicationUsecaseNames.REGULAR_BILL,
                new PrintItemsStartUpParams(new string[] { WorkItem.State[ModuleStateNames.CURRENT_REGULAR_BILL_ID].ToString() }));
        }

        public void ShowRecharge()
        {
            ITabbedView _tabbedView = WorkItem.SmartParts.Get<ITabbedView>(ModuleViewNames.TABBED_VIEW);
            IListView _listView = WorkItem.SmartParts.Get<IListView>(ModuleViewNames.LIST_VIEW);
            IBaseListView _itemView = WorkItem.SmartParts.Get<IBaseListView>(ModuleViewNames.ITEM_VIEW);

            string _chargeSetListId =
                string.Format(
                    "{0}_{1}",
                    ChargeSetTypes.RECHARGE_SET_TYPE,
                    WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_ID]);

            DateTime _chargeSetTime = (DateTime)WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_SET_CREATION_DATETIME];

            DateTime _since =
                new DateTime(
                    _chargeSetTime.Year,
                    _chargeSetTime.Month,
                    _chargeSetTime.Day,
                    _chargeSetTime.Hour,
                    0,
                    0);

            _listView.Since = _since;
            _listView.Till = _since.AddHours(1);
            _tabbedView.SelectTab(TabNames.LIST);
            _listView.RefreshList();
            _listView.LocateToId(_chargeSetListId);

            _tabbedView.SelectTab(TabNames.DETAIL);
            _itemView.LocateToId((string)WorkItem.State[ModuleStateNames.CURRENT_CHARGE_RECHARGE_OPER_ID]);

            _tabbedView.SelectTab(TabNames.CHARGE_DETAIL);
        }
    }
}