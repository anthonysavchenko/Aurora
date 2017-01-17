using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

//using BaseListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Charges.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseListView
    {
        public ItemView()
        {
            InitializeComponent();
            Initialize(_gridControlOfListView, _gridViewOfListView, "ID");
        }

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
        /// Получить ID текущего элемента списка.
        /// </summary>
        public string GetCurrentItemId()
        {
            string _id = "";

            if (_gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle) != null)
                _id = _gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle)["ID"].ToString();
            return _id;
        }
    }
}