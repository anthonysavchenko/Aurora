using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface IItemView : IBaseView
    {
        /// <summary>
        /// Номер прибора учета
        /// </summary>
        string CounterNum { set; }

        /// <summary>
        /// Услуга связанная с прибором учета
        /// </summary>
        string CounterService { set; }

        /// <summary>
        /// Модель прибор учета
        /// </summary>
        string CounterModel { set; }

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

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        void ShowDomainOnView();
    }
}