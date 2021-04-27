using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.BuildingValuesUploads.Views.Form
{
    [SmartPart]
    public partial class FormView : BaseView, IFormView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public FormView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new FormViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (FormViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IPaymentView

        /// <summary>
        /// Источник данных для таблицы 
        /// </summary>
        public DataTable Rows
        {
            set
            {
                paymentPosGridControl.DataSource = value;
                paymentPosGridControl.RefreshDataSource();
            }
        }

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        public void ShowDomainOnView()
        {
            Presenter.ShowDomainOnView();
        }

        #endregion

        private void paymentPosGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var description = FormGridView.GetFocusedRowCellDisplayText("Description");
            RowDescriptionTextBox.Text = description;
        }
    }
}