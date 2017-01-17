using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView;

//using BaseMultipleListView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Bills.Views.List
{
    [SmartPart]
    public partial class ListView : BaseListView, IListView
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

        /// <summary>Получить составное наименование.</summary>
        /// <returns>Заголовок окна</returns>
        public override string GetCurrentItemShortName()
        {
            string name = "";

            if (_gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle) != null)
            {
                name = String.Format("Пачка № {0}", _gridViewOfListView.GetDataRow(_gridViewOfListView.FocusedRowHandle)["Number"].ToString());
            }
            return name;
        }

        #region Implementation of IListView

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