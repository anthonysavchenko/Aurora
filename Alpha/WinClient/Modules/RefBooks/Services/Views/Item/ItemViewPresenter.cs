using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.EnterpriseLibrary.Win.Services;
using ChargeRuleType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service.ChargeRuleType;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, Service>
    {
        private ChargeRuleType _oldChargeRule;

        #region Overrides of BaseMainItemViewPresenter<IItemView,Service>

        protected override void OnViewSet()
        {
            base.OnViewSet();
            View.ServiceTypes = GetServiceTypes();
        }

        private Dictionary<int, string> GetServiceTypes()
        {
            Dictionary<int, string> _result;

            try
            {
                using (Entities _db = new Entities())
                {
                    _result = _db.ServiceTypes
                        .OrderBy(st => st.Name)
                        .ToDictionary(st => st.ID, st => st.Name);
                }
            }
            catch(Exception _ex)
            {
                Logger.SimpleWrite($"Справочник \"Услуги\". Ошибка {_ex}");
                _result = new Dictionary<int, string>();
            }

            return _result;
        }

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(Service _domItem)
        {
            View.ServiceName = _domItem.Name;
            View.ServiceCode = _domItem.Code;
            View.ServiceType = _domItem.ServiceType;
            View.ChargeRule = _domItem.ChargeRule;
            View.Norm = _domItem.Norm ?? 0;
            View.Measure = _domItem.Measure;
        }

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void FillDomainFromAllViews(Service _domItem)
        {
            _domItem.Name = View.ServiceName;
            _domItem.Code = View.ServiceCode;
            _domItem.ServiceType = View.ServiceType;
            _oldChargeRule = _domItem.ChargeRule;
            _domItem.ChargeRule = View.ChargeRule;
            _domItem.Norm = View.Norm == 0 ? (decimal?)null : View.Norm;
            _domItem.Measure = View.Measure;
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(Service _domItem, out string _errorMessage)
        {
            StringBuilder _error = new StringBuilder();

            if (string.IsNullOrEmpty(_domItem.Name.Trim()))
            {
                _error.AppendLine("- Не указано полное название");
            }

            if (string.IsNullOrEmpty(_domItem.Code.Trim()))
            {
                _error.AppendLine("- Не указан шифр");
            }

            if (_domItem.ServiceType == null)
            {
                _error.AppendLine("- Не указан тип услуги");
            }

            if (_error.Length == 0 && _oldChargeRule == ChargeRuleType.CounterRate && _domItem.ChargeRule != _oldChargeRule)
            {
                Service _service = (Service)WorkItem.State[CommonStateNames.CurrentItem];
                int _id = int.Parse(_service.ID);
                bool _countersExist;

                using (Entities _entities = new Entities())
                {
                    _countersExist = _entities.CustomerPoses.Any(p => p.PrivateCounters != null && p.Services.ID == _id) ||
                        _entities.CommonCounters.Any(c => c.Services.ID == _id);
                }

                if (_countersExist)
                {
                    _error.Append("Нельзя изменить правило начисления, так как с данной услугой связаны приборы учета");
                }
            }

            if (_error.Length == 0 
                && (_oldChargeRule == ChargeRuleType.PublicPlaceAreaRate || _oldChargeRule == ChargeRuleType.PublicPlaceVolumeAreaRate)
                && _domItem.ChargeRule != _oldChargeRule)
            {
                Service _service = (Service)WorkItem.State[CommonStateNames.CurrentItem];
                int _id = int.Parse(_service.ID);
                bool _exist;
                if (_oldChargeRule == ChargeRuleType.PublicPlaceAreaRate)
                {
                    using (Entities _entities = new Entities())
                    {
                        _exist = _entities.PublicPlaces.Any(p => p.ServiceID == _id);
                    }

                    if (_exist)
                    {
                        _error.Append("Нельзя изменить правило начисления, так как с данной услугой связано одно или несколько мест общего пользования");
                    }
                }
                else
                {
                    using (Entities _entities = new Entities())
                    {
                        _exist = _entities.PublicPlaceServiceVolumes.Any(p => p.ServiceID == _id);
                    }

                    if (_exist)
                    {
                        _error.Append("Нельзя изменить правило начисления, так как с данной услугой связаны данные об объемах потребленного комм. ресурса при СОД");
                    }
                }
            }

            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion

        public void OnSelectedChargeRuleChanged()
        {
            ChargeRuleType _selectedChargeRule = View.ChargeRule;
            bool _enabled = _selectedChargeRule == ChargeRuleType.CounterRate
                || _selectedChargeRule == ChargeRuleType.PublicPlaceAreaRate
                || _selectedChargeRule == ChargeRuleType.PublicPlaceVolumeAreaRate;

            if(!_enabled)
            {
                View.Norm = 0;
            }

            View.NormEnabled = _enabled;
        }
    }
}