using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
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

        /// <summary>
        /// Строка целиком
        /// </summary>
        public bool WholeWord
        {
            get
            {
                return WholeWordCheckBox.Checked;
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
        public string House
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
    }
}