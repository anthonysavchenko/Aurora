using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Charges.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportConponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ListViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IListView

        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        public DateTime Period
        {
            get
            {
                DateTime _temp = periodDateEdit.DateTime;
                return new DateTime(_temp.Year, _temp.Month, 1);
            }
            set
            {
                periodDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Количество квартир
        /// </summary>
        public int ApartmentTotalCount
        {
            set
            {
                apartmentTotalCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Количество муниципальных квартир
        /// </summary>
        public int ApartmentMunicipalCount
        {
            set
            {
                apartmentMunicipalCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Количество приватизированных квартир
        /// </summary>
        public int ApartmentPrivatizedCount
        {
            set
            {
                apartmentPrivatizedCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Количество домов
        /// </summary>
        public int BuildingCount
        {
            set
            {
                buildingCountLabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// Суммарная площадь квартир
        /// </summary>
        public decimal Square
        {
            set
            {
                squareLabel.Text = string.Format("{0:0.00} кв. м.", value);
            }
        }

        #endregion
    }
}