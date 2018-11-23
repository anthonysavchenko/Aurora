using System;
using System.Text;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Item
{
    public class ItemViewPresenter : BaseMainItemViewPresenter<IItemView, BenefitType>
    {
        #region Overrides of BaseItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Отображает домен на всех видах
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void ShowDomainOnAllViews(BenefitType _domItem)
        {
            View.BenefitCode = _domItem.Code;
            View.BenefitName = _domItem.Name;
            View.BenefitRule = _domItem.BenefitRule;
            View.BenefitFixedPercent = _domItem.FixedPercent ?? 0;
        }

        #endregion

        #region Overrides of BaseMainItemViewPresenter<IItemView,Service>

        /// <summary>
        /// Наполняет домен, собирая данные с видов
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        protected override void FillDomainFromAllViews(BenefitType _domItem)
        {
            _domItem.Name = View.BenefitName.Trim();
            _domItem.Code = View.BenefitCode.Trim();
            _domItem.BenefitRule = View.BenefitRule;
            _domItem.FixedPercent = _domItem.BenefitRule == BenefitRuleType.FixedPercent
                                        ? Convert.ToByte(View.BenefitFixedPercent)
                                        : (byte?)null;
        }

        /// <summary>
        /// Проверить предусловия перед операцией сохранения
        /// </summary>
        /// <param name="_domItem">Объект домена</param>
        /// <param name="_errorMessage">Сообщение об ошибке</param>
        /// <returns>true, если сохранение возможно; иначе - false</returns>
        protected override bool CheckPreSaveConditions(BenefitType _domItem, out string _errorMessage)
        {
            StringBuilder _error = new StringBuilder();

            if (string.IsNullOrEmpty(_domItem.Name))
            {
                _error.AppendLine("- Не указано полное название");
            }
            
            if (string.IsNullOrEmpty(_domItem.Code))
            {
                _error.AppendLine("- Не указан шифр");
            }
            
            _errorMessage = _error.ToString();

            return _error.Length == 0;
        }

        #endregion
    }
}