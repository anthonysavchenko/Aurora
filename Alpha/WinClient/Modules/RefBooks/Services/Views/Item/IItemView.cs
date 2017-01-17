using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Список типов услуг
        /// </summary>
        DataTable ServiceTypes { set; }

        /// <summary>
        /// Наименование
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Шифр
        /// </summary>
        string ServiceCode { get; set; }

        /// <summary>
        /// Тип услуги
        /// </summary>
        ServiceType ServiceType { get; set; }

        /// <summary>
        /// Правило начисления
        /// </summary>
        Service.ChargeRuleType ChargeRule { get; set; }

        /// <summary>
        /// Норматив
        /// </summary>
        decimal Norm { get; set; }

        /// <summary>
        /// Единица измерения норматива
        /// </summary>
        string NormMeasure { get; set; }
    }
}