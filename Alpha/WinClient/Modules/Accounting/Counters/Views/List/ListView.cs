using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Views.List
{
    [SmartPart]
    public partial class ListView : BaseMultipleListView
    {
        public ListView()
        {
            InitializeComponent();
            Initialize(_gridControlOfListView, _gridViewOfListView, "ID");
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ListViewPresenter)base.Presenter;
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