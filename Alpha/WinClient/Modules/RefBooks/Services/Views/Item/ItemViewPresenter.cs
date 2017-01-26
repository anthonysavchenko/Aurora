using System.Linq;
using System.Text;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.EnterpriseLibrary.Win.Constants;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, Service>
    {
        private bool _checkForCounters;
        private bool _checkPublicPlaces;

        #region Overrides of BaseMainItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Обработка загрузки вида.
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            View.ServiceTypes = GetList<ServiceType>();
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

            if (_error.Length == 0 && _checkForCounters)
            {
                Service _service = (Service)WorkItem.State[CommonStateNames.CurrentItem];
                int _id = int.Parse(_service.ID);
                bool _countersExist;

                using (Entities _entities = new Entities())
                {
                    _countersExist =
                        _entities.PrivateCounters.Any(p => p.CustomerPoses.Services.ID == _id) ||
                        _entities.CommonCounters.Any(c => c.Services.ID == _id);
                }

                if (_countersExist)
                {
                    _error.Append("Нельзя изменить правило начисления, так как с данной услугой связаны приборы учета");
                }
            }

            if (_error.Length == 0 && _checkPublicPlaces)
            {
                Service _service = (Service)WorkItem.State[CommonStateNames.CurrentItem];
                int _id = int.Parse(_service.ID);
                bool _exist;

                using (Entities _entities = new Entities())
                {
                    _exist = _entities.PublicPlaces.Any(p => p.ServiceID == _id);
                }

                if (_exist)
                {
                    _error.Append("Нельзя изменить правило начисления, так как с данной услугой связано одно или несколько мест общего пользования");
                }
            }

            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion

        /// <summary>
        /// Проверяет привязаны ли счетчики к редактируемой услуге
        /// </summary>
        /// <returns></returns>
        public void CheckForCounters(bool radioBtnChecked)
        {
            _checkForCounters = !radioBtnChecked && (string)WorkItem.State[CommonStateNames.EditItemState] == CommonEditItemStates.Edit;
        }

        /// <summary>
        /// Проверяет привязаны ли МОП к редактируемой услуге
        /// </summary>
        /// <param name="radioBtnChecked"></param>
        public void CheckPublicPlacesOnSave(bool radioBtnChecked)
        {
            _checkPublicPlaces = !radioBtnChecked && (string)WorkItem.State[CommonStateNames.EditItemState] == CommonEditItemStates.Edit;
        }
    }
}