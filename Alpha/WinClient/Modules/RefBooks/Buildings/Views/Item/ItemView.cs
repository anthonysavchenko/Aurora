using System;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

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
        public DataTable Streets
        {
            set
            {
                streetLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Улица
        /// </summary>
        public Street Street
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain<Street>(streetLookUpEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, streetLookUpEdit);
            }
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string Number
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(numberTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, numberTextBox);
            }
        }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string ZipCode
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(zipCodeTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, zipCodeTextBox);
            }
        }

        /// <summary>
        /// Количество этажей
        /// </summary>
        public short FloorCount
        {
            get
            {
                return Convert.ToInt16(floorCountSpinEdit.Value);
            }
            set
            {
                floorCountSpinEdit.Value = value;
            }
        }

        /// <summary>
        /// Количество подъездов
        /// </summary>
        public byte EntranceCount
        {
            get
            {
                return Convert.ToByte(entranceSpinEdit.Value);
            }
            set
            {
                entranceSpinEdit.Value = value;
            }
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Area
        {
            set
            {
                squareTextBox.Text = value.ToString("0.00");
            }
        }

        /// <summary>
        /// Площадь нежлых помещений
        /// </summary>
        public decimal NonResidentialPlaceArea
        {
            get
            {
                return nonResidentialPlaceAreaSpinEdit.Value;
            }
            set
            {
                nonResidentialPlaceAreaSpinEdit.Value = value;
            }
        }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        public int ResindentsCount
        {
            set
            {
                residentsCountTextBox.Text = value.ToString();
            }
        }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note
        {
            get
            {
                return noteTextBox.Text;
            }
            set
            {
                noteTextBox.Text = value;
            }
        }

        /// <summary>
        /// Код ФИАС
        /// </summary>
        public string FiasID
        {
            get
            {
                return fiasIdTextBox.Text;
            }
            set
            {
                fiasIdTextBox.Text = value;
            }
        }

        /// <summary>
        /// Список банковских реквизитов
        /// </summary>
        public DataTable BankDetailsSource
        {
            set
            {
                bankDetailsLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Банковские реквизиты
        /// </summary>
        public BankDetail BankDetail
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain<BankDetail>(bankDetailsLookUpEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, bankDetailsLookUpEdit);
            }
        }

        #endregion
    }
}