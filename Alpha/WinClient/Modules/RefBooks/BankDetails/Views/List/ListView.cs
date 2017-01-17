using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

//using BaseSimpleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.BankDetails.Views.List
{ 
    /// <summary>
    /// Вид списка
    /// </summary>
    [SmartPart]
    public partial class ListView : BaseSimpleListView, IListView
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ListView()
        {
            InitializeComponent();
            Initialize(_listItems, _listView, "ID", true);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        #region IListView Members

        /// <summary>
        /// Название
        /// </summary>
        public string BankName => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "Name");
        public string BIK => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "BIK");
        public string KPP => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "KPP");
        public string CorrAccount => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "CorrAccount");
        public string Account => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "Account");
        public string INN => GetBaseSimpleListViewMapper.ViewToDomain(_listView, "INN");

        #endregion
    }
}