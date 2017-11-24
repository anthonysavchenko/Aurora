using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

//using BaseTabbedView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.Tabbed
{
    [SmartPart]
    public partial class TabbedView : BaseTabbedView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TabbedView()
        {
            InitializeComponent();
            Initialize(_tabWorkspace);
        }

        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new TabbedViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }
    }
}