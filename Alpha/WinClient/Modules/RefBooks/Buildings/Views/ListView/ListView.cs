using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

//using BaseListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Views.List
{
    /// <summary>
    /// Вид списка
    /// </summary>
    [SmartPart]
    public partial class ListView : BaseListView
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ListView()
        {
            InitializeComponent();
            Initialize(_gridControl, _gridView, "ID");
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }
    }
}