using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Top
{
    [SmartPart]
    public partial class TopView : /*System.Windows.Forms.UserControl//*/BaseView, ITopView
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
        public bool ShowOnlyWoPeriodValue => showOnlyWoPeriodValuesCheckBox.Checked;

        /// <summary>
        /// Показать все
        /// </summary>
        public bool ShowAll => showAllCheckBox.Checked;

        /// <summary>
        /// Наименование улицы
        /// </summary>
        public string Street => StreetTextBox.Text;

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Building => HouseTextBox.Text;

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Apartment => ApartmentTextBox.Text;

        /// <summary>
        /// Номер аккаунта
        /// </summary>
        public string Account => AccountTextBox.Text;

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode => zipCodeTextBox.Text;

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
            string _id = districtLookUpEdit.ItemIndex != -1
                ? districtLookUpEdit.GetColumnValue("ID").ToString()
                : string.Empty;

            if (!string.IsNullOrEmpty(_id))
            {
                Presenter.PrintCollectForm(_id);
            }
        }
    }
}