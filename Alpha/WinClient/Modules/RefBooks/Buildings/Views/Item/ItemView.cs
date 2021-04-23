using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
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
        public string RouteFormLastMonth { set => RouteFormLastMonthTextBox.Text = value; }

        /// <summary>
        /// Количество абонентов
        /// </summary>
        public int CustomersCount { set => CustomersCountTextBox.Text = value.ToString(); }

        /// <summary>
        /// Количество ИПУ
        /// </summary>
        public int CountersCount { set => CountersCountTextBox.Text = value.ToString(); }

        /// <summary>
        /// Норматив
        /// </summary>
        public decimal NormCoefficient
        {
            get => NormCoefficientUpDown.Value;
            set => NormCoefficientUpDown.Value = value;
        }

        /// <summary>
        /// МОП
        /// </summary>
        public decimal CollectiveSquare
        {
            get => CollectiveSquareUpDown.Value;
            set => CollectiveSquareUpDown.Value = value;
        }

        /// <summary>
        /// Месяц последней расшифровки
        /// </summary>
        public string CalculationFormLastMonth { set => CalculationFormLastMonthTextBox.Text = value; }

        /// <summary>
        /// Договор
        /// </summary>
        public string BuildingContract { set => BuildingContractTextBox.Text = value; }

        /// <summary>
        /// Архивный
        /// </summary>
        public bool IsArchived
        {
            get => IsArchivedCheckBox.Checked;
            set => IsArchivedCheckBox.Checked = value;
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
