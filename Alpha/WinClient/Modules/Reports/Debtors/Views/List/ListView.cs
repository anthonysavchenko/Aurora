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

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Debtors.Views.List
{
    [SmartPart]
    public partial class ListView : BaseReportForGridView, IListView
    {
        public ListView()
        {
            InitializeComponent();
            InitReportConponents(gridControlOfListView, gridViewOfListView);
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
        public void AddMoneyColumn(string fieldName, string caption)
        {
            GridColumn _column = AddColumn(fieldName, caption, FormatType.Numeric, "0.00");
            _column.SummaryItem.FieldName = fieldName;
            _column.SummaryItem.SummaryType = SummaryItemType.Sum;
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
        /// Нижняя граница суммы долга
        /// </summary>
        public decimal DebtMinSum
        {
            get => minDebtSum.Value;
            set => minDebtSum.Value = value;
        }

        /// <summary>
        /// Нижняя граница количества месяцев долга
        /// </summary>
        public int DebtMonthCount
        {
            get => (int)debtMonthCount.Value;
            set => debtMonthCount.Value = value;
        }

        /// <summary>
        /// Правая граница ограничения по периоду
        /// </summary>
        public DateTime TillDateTime
        {
            get => tillDateEdit.DateTime;
            set => tillDateEdit.DateTime = value;
        }

        /// <summary>
        /// Улицы
        /// </summary>
        public DataTable Streets { set => streetLookUpEdit.Properties.DataSource = value; }

        /// <summary>
        /// Дома
        /// </summary>
        public DataTable Buildings { set => buildingLookUpEdit.Properties.DataSource = value; }

        /// <summary>
        /// Улица
        /// </summary>
        public string StreetId => (string)streetLookUpEdit.EditValue;

        /// <summary>
        /// Дом
        /// </summary>
        public string BuildingId => (string)buildingLookUpEdit.EditValue;

        #endregion

        /// <summary>
        /// Обрабатывает очистку фильтров
        /// </summary>
        private void filterLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                ((LookUpEdit)sender).EditValue = null;
            }
        }

        private void streetLookUpEdit_EditValueChanged(object sender, System.EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }
    }
}