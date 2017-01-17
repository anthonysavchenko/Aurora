using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView
{
    public abstract class BaseReportObject : XtraReport
    {
        /// <summary>
        /// Графический объект диаграммы
        /// </summary>
        protected virtual XRChart ChartReport
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Данные для диаграммы
        /// </summary>
        public object SeriesSourceForDiagramm
        {
            set
            {
                if (null != ChartReport)
                {
                    ChartReport.Series.Clear();
                    if (value != null)
                    {
                        List<DevExpress.XtraCharts.Series> _series = (List<DevExpress.XtraCharts.Series>)value;
                        ChartReport.Series.AddRange(_series.ToArray());
                    }
                }
            }
        }
    }
}