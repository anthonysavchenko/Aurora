using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.Alpha.Infrastructure.Interface.Enums;
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
        /// Улица
        /// </summary>
        public string Street
        {
            get => GetSimpleItemViewMapper.ViewToDomain(StreetTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, StreetTextBox);
        }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string BuildingNumber
        {
            get => GetSimpleItemViewMapper.ViewToDomain(BuildingNumberTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, BuildingNumberTextBox);
        }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        public string BuildingPartNumber
        {
            get => GetSimpleItemViewMapper.ViewToDomain(BuildingPartTextBox);
            set => GetSimpleItemViewMapper.DomainToView(value, BuildingPartTextBox);
        }

        /// <summary>
        /// Месяц последнего МЛ
        /// </summary>
        public string LastMonth { set => LastMonthTextBox.Text = value; }

        /// <summary>
        /// Количество абонентов
        /// </summary>
        public int CustomersCount { set => CustomersCountTextBox.Text = value.ToString(); }

        /// <summary>
        /// Количество ИПУ
        /// </summary>
        public int CountersCount { set => CountersCountTextBox.Text = value.ToString(); }

        /// <summary>
        /// Договор
        /// </summary>
        public BuildingContract BuildingContract
        {
            get => Contract15297RadioButton.Checked ? BuildingContract.Contract15297 : BuildingContract.Contract6784;
            set
            {
                Contract6784RadioButton.Checked =
                    value == BuildingContract.Contract6784
                    || value == BuildingContract.Unknown;
                Contract15297RadioButton.Checked = value == BuildingContract.Contract15297;
            }
        }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note
        {
            get => noteTextBox.Text;
            set => noteTextBox.Text = value;
        }

        #endregion
    }
}
