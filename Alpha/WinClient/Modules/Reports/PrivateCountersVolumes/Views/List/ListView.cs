using System.Data;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;
using System;
//using BaseReportForGridView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PrivateCountersVolumes.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportComponents(gridControlOfListView, gridViewOfListView);
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

        #region Implementation of IListView

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        public void AddColumn(string fieldName, string caption)
        {
            AddColumn(fieldName, caption, FormatType.None, string.Empty);
        }

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        public void AddNumericColumn(string fieldName, string caption)
        {
            AddColumn(fieldName, caption, FormatType.Numeric, "0");
        }

        /// <summary>
        /// Добавляет колонку в таблицу
        /// </summary>
        /// <param name="fieldName">Наименование колонки в источнике данных</param>
        /// <param name="caption">Заголовок колонки</param>
        /// <param name="formatType">Тип форматирования</param>
        /// <param name="formatString">Строка форматирования</param>
        private GridColumn AddColumn(string fieldName, string caption, FormatType formatType, string formatString)
        {
            GridColumn _column = gridViewOfListView.Columns.AddVisible(fieldName, caption);
            _column.DisplayFormat.FormatType = formatType;
            _column.DisplayFormat.FormatString = formatString;
            return _column;
        }

        /// <summary>
        /// Удаляет все колонки
        /// </summary>
        public void ClearColumns()
        {
            gridViewOfListView.Columns.Clear();
        }

        /// <summary>
        /// Начальная дата периода
        /// </summary>
        public DateTime Since
        {
            get
            {
                return SinceDateEdit.DateTime;
            }
            set
            {
                SinceDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Конечная дата периода
        /// </summary>
        public DateTime Till
        {
            get
            {
                return TillDateEdit.DateTime;
            }
            set
            {
                TillDateEdit.DateTime = value;
            }
        }

        /// <summary>
        /// Дома
        /// </summary>
        public DataTable Buildings
        {
            set => BuildingLookUpEdit.Properties.DataSource = value;
        }

        /// <summary>
        /// Дом
        /// </summary>
        public string BuildingId => (string)BuildingLookUpEdit.EditValue;

        #endregion

    }
}