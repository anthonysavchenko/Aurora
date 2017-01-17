using Microsoft.Practices.CompositeUI.SmartParts;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutForListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Users.Views.Layout
{
    [SmartPart]
    public partial class LayoutView : BaseLayoutForTabbedView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public LayoutView()
        {
            InitializeComponent();
        }
    }
}