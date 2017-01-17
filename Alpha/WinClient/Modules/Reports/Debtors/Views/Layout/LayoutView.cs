using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutForReportView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.Layout
{
    [SmartPart]
    public partial class LayoutView : BaseLayoutForReportView
    {
        public LayoutView()
        {
            InitializeComponent();
        }

        [CreateNew]
        public new LayoutViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        /// <summary>
        /// Ссылка на SplitContainer, который должен реализовать наследник
        /// </summary>
        protected override SplitContainer LayoutSplitContainer
        {
            get
            {
                return splitContainer1;
            }
        }
    }
}