using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
{
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Список типов услуг
        /// </summary>
        Dictionary<int, string> ServiceTypes { set; }

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
        ChargeRuleType ChargeRule { get; set; }

        /// <summary>
        /// Норматив
        /// </summary>
        decimal Norm { get; set; }

        /// <summary>
        /// Единица измерения норматива
        /// </summary>
        string Measure { get; set; }
    }
}