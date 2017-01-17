using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

//using BaseTabbedView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.Tabbed
{
    [SmartPart]
    public partial class TabbedView : BaseTabbedView, ITabbedView
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
            get
            {
                return (TabbedViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Показывает вкладку с мастером
        /// </summary>
        public void ShowWizardTab()
        {
            if (!_tabWorkspace.Controls.Contains(tabWizard))
            {
                _tabWorkspace.TabPages.Add(tabWizard);
            }
        }

        /// <summary>
        /// Скрывает вкладку с мастером
        /// </summary>
        public void HideWizardTab()
        {
            if (_tabWorkspace.Controls.Contains(tabWizard))
            {
                _tabWorkspace.Controls.Remove(tabWizard);
            }
        }
    }
}

