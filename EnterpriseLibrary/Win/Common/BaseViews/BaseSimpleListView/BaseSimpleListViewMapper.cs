using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;


namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Класс для преобразования элементов домена в DevExpress элементы вида
    /// </summary>
    public class BaseSimpleListViewMapper
    {
        /// <summary>
        /// Презентер
        /// </summary>
        private IBaseSimpleListViewPresenter Presenter { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_baseItemView">Владелец презентера (вид)</param>
        public BaseSimpleListViewMapper(IBaseSimpleListViewPresenter _baseSimpleListViewPresenter)
        {
            Presenter = _baseSimpleListViewPresenter;
        }

        /// <summary>
        /// Получить данные таблицы
        /// </summary>
        /// <param name="_gridListView">Таблица</param>
        /// <param name="_fieldName">Наименование поля</param>
        /// <returns>значение колонки</returns>
        public string ViewToDomain(GridView _gridListView, string _fieldName)
        {
            string res = string.Empty;

            if (_gridListView.State == GridState.Editing)
            { 
                _gridListView.UpdateCurrentRow(); 
            }

            res = _gridListView.GetFocusedRowCellDisplayText(_fieldName);

            return res;
        }

        /// <summary>
        /// Получить данные таблицы в виде простого типа
        /// </summary>
        /// <param name="_gridListView">Таблица</param>
        /// <param name="_fieldName">Наименование поля</param>
        /// <returns>Значение колонки или default(T), если значение не указано</returns>
        public T ViewToDomainSimpleType<T>(GridView _gridListView, string _fieldName)
        {
            T _res = default(T);

            if (_gridListView.State == GridState.Editing)
            {
                _gridListView.UpdateCurrentRow();
            }

            object _columnValue = _gridListView.GetFocusedRowCellValue(_fieldName);
            if (_columnValue != DBNull.Value)
            {
                _res = (T)_columnValue;
            }

            return _res;
        }

        /// <summary>
        /// Присвоить значение.
        /// </summary>
        /// <param name="_value">Значение</param>
        /// <param name="_gridListView">Таблица</param>
        /// <param name="_fieldName">Наименование поля</param>
        public void DomainToView(string _value, GridView _gridListView, string _fieldName)
        {
            _gridListView.SetFocusedRowCellValue(_gridListView.Columns[_fieldName], _value);
        }

        /// <summary>
        /// Получить домен из колонки грида типа справочник.
        /// </summary>
        /// <typeparam name="TDomain">Домен</typeparam>
        /// <param name="_gridListView">таблица</param>
        /// <param name="_fieldName">Наименование поля</param>
        /// <returns>Домен</returns>
        public TDomain ViewToDomain<TDomain>(GridView _gridListView, string _fieldName)
            where TDomain : DomainObject, new()
        {
            TDomain _res = null;

            if (_gridListView.State == GridState.Editing)
            {
                _gridListView.UpdateCurrentRow(); 
            }

            RepositoryItemLookUpEdit _column = (RepositoryItemLookUpEdit)_gridListView.Columns[_fieldName].ColumnEdit;
            if ( _column.ValueMember != null)
            {
                if (_column.GetDataSourceRowByDisplayValue(_gridListView.GetFocusedRowCellDisplayText(_fieldName)) != null)
                {
                    string _id = ((_column.GetDataSourceRowByDisplayValue(
                                       _gridListView.GetFocusedRowCellDisplayText(_fieldName)) as DataRowView)
                                                           .Row as DataRow)["ID"].ToString();
                    _res = Presenter.GetItem<TDomain>(_id);
                }
            }
            return _res;
        }

        /// <summary>
        /// Присвоить значения справочнику в гриде.
        /// </summary>
        /// <param name="_dataTable">Таблица значений</param>
        /// <param name="_gridListView">Грид</param>
        /// <param name="_fieldName">Наименование поля</param>
        public void DomainToView(DataTable _dataTable, GridView _gridListView, string _fieldName)
        {
            RepositoryItemLookUpEdit _column = (RepositoryItemLookUpEdit)_gridListView.Columns[_fieldName].ColumnEdit;
            _column.DataSource = _dataTable;
        }
    }
}
