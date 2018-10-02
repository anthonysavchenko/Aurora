using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item.Model;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

//using BaseView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseView, IItemView
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
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ItemViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        public string CounterNum { set => counterNum.Text = value; }

        /// <summary>
        /// Данные абонента
        /// </summary>
        public string CounterService { set => counterService.Text = value; }

        /// <summary>
        /// Модель прибор учета
        /// </summary>
        public string CounterModel { set => counterModel.Text = value; }

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

        /// <summary>
        /// Отображает домен на виде
        /// </summary>
        public void ShowDomainOnView()
        {
            Presenter.ShowDomainOnView();
        }
    }
}