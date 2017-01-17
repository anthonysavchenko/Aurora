using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Oper;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.Oper;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Constants;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Tabbed;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Item
{
    /// <summary>
    /// Презентер
    /// </summary>
    public class ItemViewPresenter : BaseListViewPresenter<ItemView, BaseChargeOper>
    {
        [InjectionConstructor]
        public ItemViewPresenter()
            : base(
                new BaseListViewParams
                {
                    CurrentItemIdStateName = ModuleStateNames.CURRENT_CHARGE_OPER_ID,
                    UpdateWindowTitleOnRowChanged = false
                })
        {
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
        }

        /// <summary>
        /// Загрузчик списка операций
        /// </summary>
        /// <returns>Таблица данных</returns>
        public override DataTable GetElemList()
        {
            object _item = WorkItem.State[CommonStateNames.CurrentItem];

            DataTable _table =
                _item != null
                    ? _item is RechargeSet
                          ? DataMapper<RechargeOper, IRechargeOperDataMapper>().GetList((RechargeSet)_item)
                          : DataMapper<ChargeOper, IChargeOperDataMapper>().GetList((ChargeSet)_item)
                    : new DataTable();

            return _table;
        }

        /// <summary>
        /// Подписчик на событие "Обновить список".
        /// </summary>
        public override void OnRefreshItemFired(object sender, EventArgs eventArgs)
        {
            ITabbedView _tabbedView = (ITabbedView)WorkItem.SmartParts[ModuleViewNames.TABBED_VIEW];

            if (WorkItem.Status == WorkItemStatus.Inactive || _tabbedView.CurrentTab != TabNames.DETAIL)
                return;

            RefreshList();
        }

        /// <summary>
        /// Подписка на глобальное событие - Удалить элемент.
        /// </summary>
        public override void OnDeleteItemFired(object sender, EventArgs eventArgs)
        {
            // Если текущий юзкейс не активен - 
            // глобальные команды обрабатывать не нужно.
            if (WorkItem.Status == WorkItemStatus.Inactive) return;

            if (MessageBox.Show(
                    "Вы уверены, что необходимо внести корректировку для выбранного начисления?",
                    "Подтверждение внесения корректировки",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question,
                     MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                DoDelete();
                RefreshList();
            }
        }

        /// <summary>
        /// Выполняет действия для удаления элемента
        /// </summary>
        protected override void DoDelete()
        {
            try
            {
                DataTable _elemList = View.ElemList;
                string _strId = (string)WorkItem.State[ModuleStateNames.CURRENT_CHARGE_OPER_ID];

                if ((bool)_elemList.Rows.Find(_strId)["IsCorrected"])
                {
                    View.ShowMessage("Данное начисление уже откорректировано", "Ошибка корректировки");
                }
                else
                {
                    DateTime _now = ServerTime.GetDateTimeInfo().Now;
                    DateTime _currentPeriod = ServerTime.GetPeriodInfo().FirstUncharged;

                    int _id = int.Parse(_strId);

                    object _item = WorkItem.State[CommonStateNames.CurrentItem];

                    using (Entities _entities = new Entities())
                    {
                        ChargeCorrectionOpers _chargeCorrectionOper =
                            new ChargeCorrectionOpers
                            {
                                CreationDateTime = _now,
                                Period = _currentPeriod
                            };
                        _entities.AddToChargeCorrectionOpers(_chargeCorrectionOper);

                        if (_item is RechargeSet)
                        {
                            RechargeOpers _rechargeOper =
                                _entities.RechargeOpers
                                    .Include("ChargeOpers")
                                    .Include("Customers")
                                    .Include("RechargeOperPoses")
                                    .Include("RechargeOperPoses.Contractors")
                                    .Include("RechargeOperPoses.Services")
                                    .First(r => r.ID == _id);

                            DeleteRechargeOper(_rechargeOper, _chargeCorrectionOper, _entities);
                        }
                        else
                        {
                            ChargeOpers _chargeOper =
                                _entities.ChargeOpers
                                    .Include("Customers")
                                    .Include("ChargeCorrectionOpers")
                                    .Include("ChargeOperPoses")
                                    .Include("ChargeOperPoses.Contractors")
                                    .Include("ChargeOperPoses.Services")
                                    .First(c => c.ID == _id);

                            DeleteChargeOper(_chargeOper, _chargeCorrectionOper, _entities);
                        }

                        _entities.SaveChanges();
                    }

                    View.ShowMessage("Корректировка успешно внесена", "Операция выполнена успешно");
                }
            }
            catch (Exception _ex)
            {
                View.ShowMessage(_ex.Message, "Ошибка удаления");
                Logger.SimpleWrite(_ex.ToString());
            }
        }

        /// <summary>
        /// Выполняет корректировку операции начисления
        /// </summary>
        /// <param name="chargeOper">Операция начисления</param>
        /// <param name="chargeCorrectionOper">Операция корректировки начисления</param>
        /// <param name="entities">Объект для работы с БД</param>
        private void DeleteChargeOper(ChargeOpers chargeOper, ChargeCorrectionOpers chargeCorrectionOper, Entities entities)
        {
            chargeCorrectionOper.Value = chargeOper.Value * (-1);
            chargeOper.ChargeCorrectionOpers = chargeCorrectionOper;

            foreach (ChargeOperPoses _chargePos in chargeOper.ChargeOperPoses)
            {
                ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses
                {
                    ChargeCorrectionOpers = chargeCorrectionOper,
                    Services = _chargePos.Services,
                    Contractors = _chargePos.Contractors,
                    Value = _chargePos.Value * (-1)
                };
                entities.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
            }

            BenefitOpers _benefitOper =
                entities.BenefitOpers
                    .Include("BenefitOperPoses")
                    .FirstOrDefault(b => b.ChargeOpers.ID == chargeOper.ID);

            if (_benefitOper != null)
            {
                BenefitCorrectionOpers _benefitCorrectionOper =
                    new BenefitCorrectionOpers
                    {
                        ChargeCorrectionOpers = chargeCorrectionOper,
                        Value = _benefitOper.Value * (-1)
                    };
                entities.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                _benefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                foreach (var _benefitPos in _benefitOper.BenefitOperPoses)
                {
                    BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                        new BenefitCorrectionOperPoses
                        {
                            BenefitCorrectionOpers = _benefitCorrectionOper,
                            Services = _benefitPos.Services,
                            Contractors = _benefitPos.Contractors,
                            Value = _benefitPos.Value * (-1)
                        };
                    entities.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                }
            }
        }

        /// <summary>
        /// Выполняет корректировку операции дополнительного начисления
        /// </summary>
        /// <param name="rechargeOper">Операция дополнительного начисления</param>
        /// <param name="chargeCorrectionOper">Операция корректировки начисления</param>
        /// <param name="entities">Объект для работы с БД</param>
        private void DeleteRechargeOper(RechargeOpers rechargeOper, ChargeCorrectionOpers chargeCorrectionOper, Entities entities)
        {
            chargeCorrectionOper.Value = rechargeOper.Value * (-1);
            rechargeOper.ChildChargeCorrectionOpers = chargeCorrectionOper;

            foreach (RechargeOperPoses _rechargeOperPos in rechargeOper.RechargeOperPoses)
            {
                ChargeCorrectionOperPoses _chargeCorrectionPos = new ChargeCorrectionOperPoses
                {
                    ChargeCorrectionOpers = chargeCorrectionOper,
                    Services = _rechargeOperPos.Services,
                    Contractors = _rechargeOperPos.Contractors,
                    Value = _rechargeOperPos.Value * (-1)
                };
                entities.AddToChargeCorrectionOperPoses(_chargeCorrectionPos);
            }

            RebenefitOpers _currentRebenefitOper =
                entities.RebenefitOpers
                    .Include("RebenefitOperPoses")
                    .FirstOrDefault(b => b.RechargeOpers.ID == rechargeOper.ID);

            if (_currentRebenefitOper != null)
            {
                BenefitCorrectionOpers _benefitCorrectionOper =
                    new BenefitCorrectionOpers
                    {
                        ChargeCorrectionOpers = chargeCorrectionOper,
                        Value = _currentRebenefitOper.Value * (-1)
                    };
                entities.AddToBenefitCorrectionOpers(_benefitCorrectionOper);
                _currentRebenefitOper.BenefitCorrectionOpers = _benefitCorrectionOper;

                foreach (var _benefitPos in _currentRebenefitOper.RebenefitOperPoses)
                {
                    BenefitCorrectionOperPoses _benefitCorrectionOperPos =
                        new BenefitCorrectionOperPoses
                        {
                            BenefitCorrectionOpers = _benefitCorrectionOper,
                            Services = _benefitPos.Services,
                            Contractors = _benefitPos.Contractors,
                            Value = _benefitPos.Value * (-1)
                        };
                    entities.AddToBenefitCorrectionOperPoses(_benefitCorrectionOperPos);
                }
            }
        }
    }
}