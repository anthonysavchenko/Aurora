using System;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBooks;

//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseItemView, IItemView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        public ItemView()
        {
            InitializeComponent();
        }

        #region Implementation of IItemView

        /// <summary>
        /// Список улиц
        /// </summary>
        public DataTable Streets { set => streetLookUpEdit.Properties.DataSource = value; }

        /// <summary>
        /// Улица
        /// </summary>
        public Street Street
        {
            get => GetSimpleItemViewMapper.ViewToDomain<Street>(streetLookUpEdit);
            set => GetSimpleItemViewMapper.DomainToView(value, streetLookUpEdit);
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Number
        {
            get => GetSimpleItemViewMapper.ViewToDomain(numberTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, numberTextBox);
        }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode
        {
            get => GetSimpleItemViewMapper.ViewToDomain(zipCodeTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, zipCodeTextBox);
        }

        /// <summary>
        /// Количество этажей
        /// </summary>
        public short FloorCount
        {
            get => Convert.ToInt16(floorCountSpinEdit.Value);
            set => floorCountSpinEdit.Value = value;
        }

        /// <summary>
        /// Количество подъездов
        /// </summary>
        public byte EntranceCount
        {
            get => Convert.ToByte(entranceSpinEdit.Value);
            set => entranceSpinEdit.Value = value;
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area { set => squareTextBox.Text = value.ToString("0.00"); }

        /// <summary>
        /// Площадь нежлых помещений
        /// </summary>
        public decimal NonResidentialPlaceArea
        {
            get => nonResidentialPlaceAreaSpinEdit.Value;
            set => nonResidentialPlaceAreaSpinEdit.Value = value;
        }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        public int ResindentsCount { set => residentsCountTextBox.Text = value.ToString(); }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note
        {
            get => noteTextBox.Text;
            set => noteTextBox.Text = value;
        }

        /// <summary>
        /// Код ФИАС
        /// </summary>
        public string FiasID
        {
            get => fiasIdTextBox.Text;
            set => fiasIdTextBox.Text = value;
        }

        /// <summary>
        /// Список банковских реквизитов
        /// </summary>
        public DataTable BankDetailsSource { set => bankDetailsLookUpEdit.Properties.DataSource = value; }

        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public BankDetail BankDetail
        {
            get => GetSimpleItemViewMapper.ViewToDomain<BankDetail>(bankDetailsLookUpEdit);
            set => GetSimpleItemViewMapper.DomainToView(value, bankDetailsLookUpEdit);
        }

        /// <summary>
        /// Список участков сбора показаний приборов учета
        /// </summary>
        public DataTable CounterValueCollectDistrictSource { set => counterValueCollectDistrictLookUpEdit.Properties.DataSource = value; }

        /// <summary>
        /// Участок сбора показаний приборов учета
        /// </summary>
        public CounterValueCollectDistrict CounterValueCollectDistrict
        {
            get => GetSimpleItemViewMapper.ViewToDomain<CounterValueCollectDistrict>(counterValueCollectDistrictLookUpEdit);
            set => GetSimpleItemViewMapper.DomainToView(value, counterValueCollectDistrictLookUpEdit);
        }

        #endregion
    }
}