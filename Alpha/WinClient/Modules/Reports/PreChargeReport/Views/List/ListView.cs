using System;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportComponents(gridControlOfListView, gridViewOfListView);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            get => (ListViewPresenter)base.Presenter;
            set => base.Presenter = value;
        }

        #region Implementation of IListView

        /// <summary>
        /// Улицы
        /// </summary>
        public DataTable Streets
        {
            set
            {
                streetLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Дома
        /// </summary>
        public DataTable Buildings
        {
            set
            {
                buildingLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Улица
        /// </summary>
        public string StreetId
        {
            get
            {
                return (string)streetLookUpEdit.EditValue;
            }
        }


        /// <summary>
        /// Дом
        /// </summary>
        public string BuildingId
        {
            get
            {
                return (string)buildingLookUpEdit.EditValue;
            }
        }

        #endregion

        /// <summary>
        /// Обрабатывает очистку фильтров
        /// </summary>
        private void filterLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        /// <summary>
        /// Обработка выбора улицы
        /// </summary>
        private void streetLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }
    }
}