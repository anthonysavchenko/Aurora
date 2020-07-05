using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Uploads.FillForms.Views.List
{
    [SmartPart]
    public partial class ListView : BaseMultipleListView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            Initialize(_gridControlOfListView, _gridViewOfListView, "ID");
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }

            get
            {
                return (ListViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Получить ID текущего элемента списка.
        /// </summary>
        public string GetCurrentItemId()
        {
            string _id = "";

            if (_gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle) != null)
                _id = _gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle)["ID"].ToString();
            return _id;
        }

        #region Implementation of IListView

        /// <summary>
        /// Начальная дата периода
        /// </summary>
        public DateTime Since
        {
            get
            {
                return sinceDateEdit.DateTime;
            }
            set
            {
                sinceDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Конечная дата периода
        /// </summary>
        public DateTime Till
        {
            get
            {
                return tillDateEdit.DateTime;
            }
            set
            {
                tillDateEdit.DateTime = value;
            }
        }

        #endregion

    }
}