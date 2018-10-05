using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    [SmartPart]
    public partial class TopView : BaseView, ITopView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TopView()
        {
            InitializeComponent();
        }

        [CreateNew]
        public new TopViewPresenter Presenter
        {
            set => base.Presenter = value;
            get => (TopViewPresenter)base.Presenter;
        }

        /// <summary>
        /// Строка целиком
        /// </summary>
        public bool ShowOnlyWoPeriodValue
        {
            get
            {
                return showOnlyWoPeriodValuesCheckBox.Checked;
            }
        }

        /// <summary>
        /// Наименование улицы
        /// </summary>
        public string Street
        {
            get
            {
                return StreetTextBox.Text;
            }
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Building
        {
            get
            {
                return HouseTextBox.Text;
            }
        }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment
        {
            get
            {
                return ApartmentTextBox.Text;
            }
        }

        /// <summary>
        /// Номер аккаунта
        /// </summary>
        public string Account
        {
            get
            {
                return AccountTextBox.Text;
            }
        }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode
        {
            get
            {
                return zipCodeTextBox.Text;
            }
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        public FilterType Filter
        {
            get
            {
                if (addressRadioButton.Checked)
                {
                    return FilterType.Address;
                }
                if (accountRadioButton.Checked)
                {
                    return FilterType.Account;
                }
                if (zipCodeRadioButton.Checked)
                {
                    return FilterType.ZipCode;
                }

                throw new ApplicationException("Не выбран фильтр");
            }
        }

        public DataTable Districts
        {
            set
            {
                districtLookUpEdit.Properties.DataSource = value;
                districtLookUpEdit.Properties.ForceInitialize();
            }
        }

        private void StreetTextBox_Enter(object sender, EventArgs e)
        {
            addressRadioButton.Checked = true;
        }

        private void AccountTextBox_Enter(object sender, EventArgs e)
        {
            accountRadioButton.Checked = true;
        }

        private void zipCodeTextBox_Enter(object sender, EventArgs e)
        {
            zipCodeRadioButton.Checked = true;
        }

        private void HouseTextBox_Enter(object sender, EventArgs e)
        {
            addressRadioButton.Checked = true;
        }

        private void printCollectFormButton_Click(object sender, EventArgs e)
        {
            int _id = districtLookUpEdit.ItemIndex != -1
                ? (int)districtLookUpEdit.GetColumnValue("ID")
                : -1;

            if (_id > 0)
            {
                Presenter.PrintCollectForm(_id);
            }
        }
    }
}