using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Models;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseItemView, IItemView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ItemView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            get => (ItemViewPresenter)base.Presenter;
            set => base.Presenter = value;
        }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        public string CounterNum
        {
            get => counterNumTextBox.Text;
            set => counterNumTextBox.Text = value;
        }

        /// <summary>
        /// Данные абонента
        /// </summary>
        public Service CounterService
        {
            get => GetSimpleItemViewMapper.ViewToDomain<Service>(counterServicesLookUpEdit);
            set => GetSimpleItemViewMapper.DomainToView(value, counterServicesLookUpEdit);
        }

        /// <summary>
        /// Модель прибор учета
        /// </summary>
        public string CounterModel
        {
            get => counterModelTextBox.Text;
            set => counterModelTextBox.Text = value;
        }

        public DataTable Services
        {
            set => counterServicesLookUpEdit.Properties.DataSource = value;
        }

        /// <summary>
        /// Актуальность счетчика
        /// </summary>
        public bool Archived
        {
            get => counterArchivedCheckBox.Checked;
            set => counterArchivedCheckBox.Checked = value;
        }

        /// <summary>
        /// Данные абонента
        /// </summary>
        public CustomerData CustomerData
        {
            set
            {
                customerGroupBox.Visible = true;
                buildingGroupBox.Visible = false;

                customerAccount.Text = value.Account;
                customerApartment.Text = value.Apartment;
                customerArea.Text = value.Area.ToString("0.00 кв.м.");
                customerBuilding.Text = value.Building;
                customerOwner.Text = value.Owner;
                customerStreet.Text = value.Street;
            }
        }

        /// <summary>
        /// Данные дома
        /// </summary>
        public BuildingData BuildingData
        {
            set
            {
                customerGroupBox.Visible = false;
                buildingGroupBox.Visible = true;

                buildingCollectionSector.Text = value.CollectionSector;
                buildingDwellersNum.Text = value.DwellersNum.ToString();
                buildingNum.Text = value.Number;
                buildingStreet.Text = value.Street;
            }
        }

        /// <summary>
        /// Источник данных для таблицы с показаниями
        /// </summary>
        public DataTable CounterValueTable
        {
            set
            {
                counterValueGridControl.DataSource = value;
                counterValueGridControl.RefreshDataSource();
            }
        }
    }
}