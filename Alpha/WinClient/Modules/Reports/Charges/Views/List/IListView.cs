using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Charges.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        DateTime Period { get; set; }

        /// <summary>
        /// Количество квартир
        /// </summary>
        int ApartmentTotalCount { set; }

        /// <summary>
        /// Количество муниципальных квартир
        /// </summary>
        int ApartmentMunicipalCount { set; }

        /// <summary>
        /// Количество приватизированных квартир
        /// </summary>
        int ApartmentPrivatizedCount { set; }

        /// <summary>
        /// Количество домов
        /// </summary>
        int BuildingCount { set; }

        /// <summary>
        /// Суммарная площадь квартир
        /// </summary>
        decimal Square { set; }
    }
}