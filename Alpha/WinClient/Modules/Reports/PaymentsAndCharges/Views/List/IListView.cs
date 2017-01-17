using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PaymentsAndCharges.Views.List
{
    public interface IListView : IBaseReportForGridView
    {
        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        DateTime SincePeriod { get; set; }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        DateTime TillPeriod { get; set; }

        /// <summary>
        /// Виды услуг
        /// </summary>
        DataTable ServiceTypes { set; }

        /// <summary>
        /// Подуслуги
        /// </summary>
        DataTable Services { set; }

        /// <summary>
        /// Улицы
        /// </summary>
        DataTable Streets { set; }

        /// <summary>
        /// Дома
        /// </summary>
        DataTable Buildings { set; }

        /// <summary>
        /// Вид услуг
        /// </summary>
        string ServiceTypeId { get; }

        /// <summary>
        /// Услуга
        /// </summary>
        string ServiceId { get; }

        /// <summary>
        /// Улица
        /// </summary>
        string StreetId { get; }

        /// <summary>
        /// Дом
        /// </summary>
        string BuildingId { get; }

        /// <summary>
        /// Признак разбивки по подуслугам
        /// </summary>
        bool SplitByServices { get; }

        /// <summary>
        /// Флаг группировки по абоненту (по услугам, в противном случае)
        /// </summary>
        bool GroupByCustomer { get; }

        /// <summary>
        /// Устанавливает колонки грида в соответствии с выбранным вариантом отчета
        /// </summary>
        void SetGridColumns();
    }
}