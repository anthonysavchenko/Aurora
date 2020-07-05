using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

//using BaseTabbedView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Views.Tabbed
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

        public void RenewView()
        {
            bool _cancelAction = false;
            Presenter.OnNewItemRun(this, EventArgs.Empty);
            Presenter.OnEnterTabPage("tabDetail", out _cancelAction);
        }
    }
}

