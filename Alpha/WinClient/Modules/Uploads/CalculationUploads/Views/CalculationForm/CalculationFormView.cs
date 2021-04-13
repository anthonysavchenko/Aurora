using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.CalculationUploads.Views.CalculationForm
{
    [SmartPart]
    public partial class CalculationFormView : BaseView, ICalculationFormView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CalculationFormView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new CalculationFormViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (CalculationFormViewPresenter)base.Presenter;
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
            var description = CalculationFormGridView.GetFocusedRowCellDisplayText("Description");
            RowDescriptionTextBox.Text = description;
        }
    }
}