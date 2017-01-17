using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
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
        public Service.ChargeRuleType ChargeRule
        {
            get
            {
                if (fixedRuleRadioButton.Checked)
                {
                    return Service.ChargeRuleType.FixedRate;
                }

                if (ResidentsRateRadioButton.Checked)
                {
                    return Service.ChargeRuleType.ResidentsRate;
                }

                if (squareRuleRadioButton.Checked)
                {
                    return Service.ChargeRuleType.SquareRate;
                }

                if(counterRuleRadioButton.Checked)
                {
                    return Service.ChargeRuleType.CounterRate;
                }

                if (publicPlaceRadioButton.Checked)
                {
                    return Service.ChargeRuleType.PublicPlaceAreaRate;
                }

                throw new ApplicationException("Не выбрано правило начисления");
            }
            set
            {
                switch (value)
                {
                    case Service.ChargeRuleType.FixedRate:
                        fixedRuleRadioButton.Checked = true;
                        break;

                    case Service.ChargeRuleType.SquareRate:
                        squareRuleRadioButton.Checked = true;
                        break;

                    case Service.ChargeRuleType.ResidentsRate:
                        ResidentsRateRadioButton.Checked = true;
                        break;

                    case Service.ChargeRuleType.CounterRate:
                        counterRuleRadioButton.Checked = true;
                        break;

                    case Service.ChargeRuleType.PublicPlaceAreaRate:
                        publicPlaceRadioButton.Checked = true;
                        break;

                    default:
                        throw  new ApplicationException("Неизвестный тип правила начисления");
                }
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
        public string NormMeasure
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

        private void counterRuleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.CheckForCounters(counterRuleRadioButton.Checked);
        }

        private void publicPlaceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Presenter.CheckPublicPlacesOnSave(publicPlaceRadioButton.Checked);
        }
    }
}