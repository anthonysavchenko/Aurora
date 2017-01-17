using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Payments.Views.Payment
{
    [SmartPart]
    public partial class PaymentView : BaseView, IPaymentView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PaymentView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new PaymentViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (PaymentViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IPaymentView

        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public string Account
        {
            set
            {
                accountValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Посредник
        /// </summary>
        public string Intermediary
        {
            set
            {
                paymentOperIntermediaryLabel.Text = value;
            }
        }

        /// <summary>
        /// Period
        /// </summary>
        public DateTime Period
        {
            set
            {
                paymentOperPeriodlabel.Text = value.ToString(StringFormats.PERIOD_DATE);
            }
        }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Value
        {
            set
            {
                paymentOperValueLabel.Text = string.Format("{0:c2}", value);
            }
        }

        /// <summary>
        /// Собственник
        /// </summary>
        public string Owner
        {
            set
            {
                ownerValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street
        {
            set
            {
                streetValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Дом
        /// </summary>
        public string House
        {
            set
            {
                houseValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Квартира
        /// </summary>
        public string Apartment
        {
            set
            {
                apartmentValueLabel.Text = value;
            }
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Square
        {
            set
            {
                squareValuelabel.Text = value.ToString("0.00");
            }
        }

        /// <summary>
        /// Источник данных для таблицы позиций операции платежа
        /// </summary>
        public DataTable PaymentOperPoses
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

        private void groupByPeriodAndServiceTypeLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            paymentPosGridView.ClearGrouping();

            paymentPosGridView.BeginSort();
            try
            {

                paymentPosGridView.Columns["Period"].GroupIndex = 0;
                paymentPosGridView.Columns["ServiceTypeName"].GroupIndex = 1;
                paymentPosGridView.Columns["ServiceName"].VisibleIndex = 0;
                paymentPosGridView.Columns["Value"].VisibleIndex = 1;
            }
            finally
            {
                paymentPosGridView.EndSort();
            }
        }

        private void groupByServiceTypeAndServiceLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            paymentPosGridView.ClearGrouping();

            paymentPosGridView.BeginSort();
            try
            {
                paymentPosGridView.Columns["ServiceTypeName"].GroupIndex = 0;
                paymentPosGridView.Columns["ServiceName"].GroupIndex = 1;
                paymentPosGridView.Columns["Period"].VisibleIndex = 0;
                paymentPosGridView.Columns["Value"].VisibleIndex = 1;
            }
            finally
            {
                paymentPosGridView.EndSort();
            }
        }
    }
}