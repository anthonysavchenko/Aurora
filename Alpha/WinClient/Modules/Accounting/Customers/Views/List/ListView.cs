using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Вид списка элементов.
    /// </summary>
    [SmartPart]
    public partial class ListView : BaseMultipleListView
    {
        public ListView()
        {
            InitializeComponent();
            base.Initialize(gridControlOfListView, gridViewOfListView, "ID");
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            get
            {
                return (ListViewPresenter)base.Presenter;
            }
            set
            {
                base.Presenter = value;
            }
        }

        /// <summary>Получить составное наименование.</summary>
        /// <returns>Заголовок окна</returns>
        public override string GetCurrentItemShortName()
        {
            string name = "";

            if (gridViewOfListView.GetDataRow(gridViewOfListView.FocusedRowHandle) != null)
            {
                name = gridViewOfListView.GetDataRow(gridViewOfListView.FocusedRowHandle)["Owner"].ToString();
            }
            return name;
        }

        /// <summary>
        /// Возвращает ID элементов списка
        /// </summary>
        /// <returns>ID элементов списка</returns>
        public string[] GetIDs()
        {
            string[] _result = null;
            if(ElemList != null)
            {
                _result = new string[ElemList.Rows.Count];
                for(int i = 0; i < ElemList.Rows.Count; i++)
                {
                    _result[i] = ElemList.Rows[i][KeyField].ToString();
                }
            }
            return _result;
        }
    }
}

