using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
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
            base.Initialize(_tabWorkspace);
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