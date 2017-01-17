using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BenefitTypes.Views.Item
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
        /// Наименование
        /// </summary>
        public string BenefitName
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(nameTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, nameTextBox);
            }
        }

        /// <summary>
        /// Шифр
        /// </summary>
        public string BenefitCode
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(codeTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, codeTextBox);
            }
        }

        /// <summary>
        /// Правило начисления льготы
        /// </summary>
        public BenefitType.BenefitRuleType BenefitRule
        {
            get
            {
                if (squareRuleRadioButton.Checked)
                {
                    return BenefitType.BenefitRuleType.FiftyPercentBySquare;
                }

                if (fixedRuleRadioButton.Checked)
                {
                    return BenefitType.BenefitRuleType.FixedPercent;
                }

                throw new ApplicationException("Не выбрано правило начисления");
            }
            set
            {
                switch (value)
                {
                    case BenefitType.BenefitRuleType.FiftyPercentBySquare:
                        squareRuleRadioButton.Checked = true;
                        break;

                    case BenefitType.BenefitRuleType.FixedPercent:
                        fixedRuleRadioButton.Checked = true;
                        break;

                    default:
                        throw new ApplicationException("Неизвестный тип правила начисления");
                }
            }
        }

        /// <summary>
        /// Процент льготы
        /// </summary>
        public int BenefitFixedPercent
        {
            get
            {
                return percentTrackBar.Value;
            }
            set
            {
                percentTrackBar.Value = value;
                beneitPercentValueLabel.Text = string.Format("{0}%", value);
            }
        }

        #endregion

        /// <summary>
        /// Обработчик события изменения значения чекбокса "Льгота в фиксированных процентах"
        /// </summary>
        private void fixedRuleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            providingRuleGroupBox.Enabled = fixedRuleRadioButton.Checked;
        }

        private void percentTrackBar_ValueChanged(object sender, EventArgs e)
        {
            beneitPercentValueLabel.Text = string.Format("{0}%", percentTrackBar.Value);
        }
    }
}