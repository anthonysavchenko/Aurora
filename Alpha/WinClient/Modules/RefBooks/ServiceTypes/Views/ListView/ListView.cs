﻿using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

using Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.ServiceTypes
{ 
    /// <summary>
    /// Вид списка
    /// </summary>
    [SmartPart]
    public partial class ListView : BaseSimpleListView, IListView
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ListView()
        {
            InitializeComponent();
            Initialize(_listItems, _listView, "ID", true);
        }

        [CreateNew]
        public new ListViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        #region IListView Members

        /// <summary>
        /// Название
        /// </summary>
        public string ServiceName
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(_listView, "Name");
            }
        }

        /// <summary>
        /// Шифр
        /// </summary>
        public string Code
        {
            get
            {
                return GetBaseSimpleListViewMapper.ViewToDomain(_listView, "Code");
            }
        }

        #endregion
    }
}