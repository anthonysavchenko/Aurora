using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using ChargeRuleType = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Service.ChargeRuleType;

//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Services.Views.Item
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
            get
            {
                return (ItemViewPresenter)base.Presenter;
            }
            set
            {
                base.Presenter = value;
            }
        }

        public ItemView()
        {
            InitializeComponent();
            Dictionary<byte, string> _chargeRuleDict =
                new Dictionary<byte, string>
                {
                    { (byte)ChargeRuleType.FixedRate, "Фиксированное" },
                    { (byte)ChargeRuleType.SquareRate, "По тарифу за кв. м" },
                    { (byte)ChargeRuleType.ResidentsRate, "По тарифу на количество жильцов" },
                    { (byte)ChargeRuleType.CounterRate, "По счетчику" },
                    { (byte)ChargeRuleType.CommonCounterByAreaRate, "По общему счетчику пропорционально общей площади" },
                    { (byte)ChargeRuleType.CommonCounterByHeatedAreaRate, "По общему счетчику пропорционально отапливаемой площади" },
                    { (byte)ChargeRuleType.CommonCounterByAssignedCustomerAreaRate, "По общему счетчику пропорционально сумме площадей абонентов, которым назначена услуга" },
                    { (byte)ChargeRuleType.PublicPlaceAreaRate, "Содержание общедового имущества (СОД)" },
                    { (byte)ChargeRuleType.PublicPlaceBankCommission, "Банковская комиссия расходов при СОД" }
                };

            chargeRuleComboBox.DataSource = new BindingSource(_chargeRuleDict, null);
            chargeRuleComboBox.DisplayMember = "Value";
            chargeRuleComboBox.ValueMember = "Key";
        }

        #region Implementation of IItemView

        /// <summary>
        /// Список типов услуг
        /// </summary>
        public DataTable ServiceTypes
        {
            set
            {
                serviceTypeLookUpEdit.Properties.DataSource = value;
            }
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string ServiceName
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(serviceNameTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, serviceNameTextBox);
            }
        }

        /// <summary>
        /// Шифр
        /// </summary>
        public string ServiceCode
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(serviceCodeTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, serviceCodeTextBox);
            }
        }

        /// <summary>
        /// Тип услуги
        /// </summary>
        public ServiceType ServiceType
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain<ServiceType>(serviceTypeLookUpEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, serviceTypeLookUpEdit);
            }
        }

        /// <summary>
        /// Правило начисления
        /// </summary>
        public ChargeRuleType ChargeRule
        {
            get
            {
                return (ChargeRuleType)chargeRuleComboBox.SelectedValue;
            }
            set
            {
                chargeRuleComboBox.SelectedValue = (byte)value;
            }
        }

        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Norm
        {
            get
            {
                return normNumericUpDown.Value;
            }
            set
            {
                normNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Единица измерения норматива
        /// </summary>
        public string Measure
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(normMeasureTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, normMeasureTextBox);
            }
        }

        #endregion
    }
}