using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Collections.Generic;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;

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
            get => (ItemViewPresenter)base.Presenter;
            set => base.Presenter = value;
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
                    { (byte)ChargeRuleType.PublicPlaceAreaRate, "Содержание общедового имущества" },
                    { (byte)ChargeRuleType.PublicPlaceVolumeAreaRate, "Содержание общедового имущества ОДПУ" },
                    { (byte)ChargeRuleType.PublicPlaceBankCommission, "Банковская комиссия расходов при СОД" }
                };

            chargeRuleComboBox.DataSource = new BindingSource(_chargeRuleDict, null);
            chargeRuleComboBox.DisplayMember = "Value";
            chargeRuleComboBox.ValueMember = "Key";

            serviceTypeComboBox.DisplayMember = "Value";
            serviceTypeComboBox.ValueMember = "Key";
        }

        #region Implementation of IItemView

        /// <summary>
        /// Список типов услуг
        /// </summary>
        public Dictionary<int, string> ServiceTypes
        {
            set => serviceTypeComboBox.DataSource = new BindingSource(value, null);
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string ServiceName
        {
            get => GetSimpleItemViewMapper.ViewToDomain(serviceNameTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, serviceNameTextBox);
        }

        /// <summary>
        /// Шифр
        /// </summary>
        public string ServiceCode
        {
            get => GetSimpleItemViewMapper.ViewToDomain(serviceCodeTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, serviceCodeTextBox);
        }

        /// <summary>
        /// Тип услуги
        /// </summary>
        public ServiceType ServiceType
        {
            get => GetSimpleItemViewMapper.ViewToDomain<ServiceType>(serviceTypeComboBox);
            set => GetSimpleItemViewMapper.DomainToView(value, serviceTypeComboBox);
        }

        /// <summary>
        /// Правило начисления
        /// </summary>
        public ChargeRuleType ChargeRule
        {
            get => (ChargeRuleType)chargeRuleComboBox.SelectedValue;
            set => chargeRuleComboBox.SelectedValue = (byte)value;
        }

        /// <summary>
        /// Норматив
        /// </summary>
        public decimal Norm
        {
            get => normNumericUpDown.Value;
            set => normNumericUpDown.Value = value;
        }

        /// <summary>
        /// Единица измерения норматива
        /// </summary>
        public string Measure
        {
            get => GetSimpleItemViewMapper.ViewToDomain(normMeasureTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, normMeasureTextBox);
        }

        #endregion
    }
}