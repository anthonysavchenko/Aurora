using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    /// <summary>
    /// Тип фильтра
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        /// По адресу
        /// </summary>
        Address,

        /// <summary>
        /// По номеру аккаута
        /// </summary>
        Account,

        /// <summary>
        /// По почтовому индексу
        /// </summary>
        ZipCode
    }

    public interface ITopView : IBaseView
    {
        /// <summary>
        /// Показать только без показаний за текущий период
        /// </summary>
        bool ShowOnlyWoPeriodValue { get; }

        /// <summary>
        /// Показать все
        /// </summary>
        bool ShowAll { get; }

        /// <summary>
        /// Наименование улицы
        /// </summary>
        string Street { get; }

        /// <summary>
        /// Номер дома
        /// </summary>
        string Building { get; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        string Apartment { get; }

        /// <summary>
        /// Номер аккаунта
        /// </summary>
        string Account { get; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        string ZipCode { get; }

        /// <summary>
        /// Фильтр
        /// </summary>
        FilterType Filter { get; }

        /// <summary>
        /// Участки сбора показаний приборов учета
        /// </summary>
        DataTable Districts { set; }
    }
}