using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using System;

//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.Views.Item
{
    [SmartPart]
    public partial class ItemView : BaseItemView, IItemView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            get => (ItemViewPresenter)base.Presenter;
            set => base.Presenter = value;
        }

        public ItemView()
        {
            InitializeComponent();
        }

        #region Implementation of IItemView

        public DateTime Period
        {
            get => periodDateEdit.DateTime;
            set => periodDateEdit.DateTime = value;
        }

        #endregion
    }
}