using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string BenefitName { get; set; }

        /// <summary>
        /// Шифр
        /// </summary>
        string BenefitCode { get; set; }

        /// <summary>
        /// Правило начисления льготы
        /// </summary>
        BenefitType.BenefitRuleType BenefitRule { get; set; }

        /// <summary>
        /// Процент льготы
        /// </summary>
        int BenefitFixedPercent { get; set; }
    }
}