using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.Constants;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.ChargeDetail
{
    [SmartPart]
    public partial class ChargeDetailView : BaseView, IChargeDetailView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ChargeDetailView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new ChargeDetailViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ChargeDetailViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IChargeDetailView

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

        public DateTime PeriodCharged
        {
            set
            {
                periodChargedValueLabel.Text = value.ToString(StringFormats.PERIOD_DATE);
            }
        }

        /// <summary>
        /// Сумма начисления
        /// </summary>
        public decimal Value
        {
            set
            {
                chargeOperValueLabel.Text = string.Format("{0:c2}", value);
            }
        }

        /// <summary>
        /// Сумма льготы
        /// </summary>
        public decimal BenefitValue
        {
            set
            {
                benefitSumValueLabel.Text = string.Format("{0:c2}", value);
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
        /// Показывать ли ссылку на квитанцию
        /// </summary>
        public bool BillLinkEnabled
        {
            set
            {
                BillLinkLabel.Enabled = value;
            }
        }

        /// <summary>
        /// Показывать ли ссылку на перерасчет
        /// </summary>
        public bool RechargeLinkEnabled
        {
            set
            {
                rechargeLinkLabel.Enabled = value;
            }
        }

        /// <summary>
        /// Источник данных для таблицы позиций операции платежа
        /// </summary>
        public DataTable ChargeOperPoses
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
            chargePosGridView.ClearGrouping();

            chargePosGridView.BeginSort();
            try
            {

                chargePosGridView.Columns["Type"].GroupIndex = 0;
                chargePosGridView.Columns["ServiceTypeName"].GroupIndex = 1;
                chargePosGridView.Columns["ServiceName"].VisibleIndex = 0;
                chargePosGridView.Columns["Value"].VisibleIndex = 1;
            }
            finally
            {
                chargePosGridView.EndSort();
            }
        }

        private void BillLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Presenter.SelectLink();
        }

        private void rechargeLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Presenter.ShowRecharge();
        }
    }
}