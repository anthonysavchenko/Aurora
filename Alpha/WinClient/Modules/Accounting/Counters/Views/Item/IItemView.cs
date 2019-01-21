using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Models;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IItemView : IBaseItemView
    {
        /// <summary>
        /// Номер прибора учета
        /// </summary>
        string CounterNum { get; set; }

        /// <summary>
        /// Услуга связанная с прибором учета
        /// </summary>
        Service CounterService { get; set; }

        /// <summary>
        /// Услуги для выбора
        /// </summary>
        DataTable Services { set; }

        /// <summary>
        /// Модель прибор учета
        /// </summary>
        string CounterModel { get; set; }

        /// <summary>
        /// Актуальность счетчика
        /// </summary>
        bool Archived { get; set; }

        /// <summary>
        /// Данные абонента
        /// </summary>
        CustomerData CustomerData { set; }

        /// <summary>
        /// Данные дома
        /// </summary>
        BuildingData BuildingData { set; }

        /// <summary>
        /// Источник данных для таблицы с показаниями
        /// </summary>
        DataTable CounterValueTable { set; }
    }
}