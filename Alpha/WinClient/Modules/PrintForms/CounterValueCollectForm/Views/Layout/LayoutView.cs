using Microsoft.Practices.CompositeUI.SmartParts;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

//using BaseLayoutForReportView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.PrintForms.CounterValueCollectForm.Views.Layout
{
    [SmartPart]
    public partial class LayoutView : BaseLayoutForReportView
    {
        public LayoutView()
        {
            InitializeComponent();
        }

        protected override SplitContainer LayoutSplitContainer
        {
            get
            {
                return splitContainer1;
            }
        }
    }
}