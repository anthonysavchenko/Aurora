using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    /// <summary>
    /// Базовый класс вида для списков
    /// </summary>
    public class BaseListView : BaseView, IBaseListView
    {
        /// <summary>
        /// Презентер
        /// </summary>
        private new IBaseListViewPresenter Presenter
        {
            get
            {
                return (IBaseListViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Основная таблица списка (контрол)
        /// </summary>
        protected GridView GridListView
        {
            get;
            private set;
        }
        /// <summary>
        /// Основная таблица списка (вью)
        /// </summary>
        protected GridControl GridControl
        {
            get;
            private set;
        }

        /// <summary>
        /// Название ключевого поля в таблице 
        /// </summary>
        public string KeyField
        {
            get;
            private set;
        }

        /// <summary>
        /// Выполняет инициализацию вида
        /// </summary>
        /// <param name="_baseGridControl">Основная таблица списка (контрол)</param>
        /// <param name="_baseView">Основная таблица списка (вью)</param>
        /// <param name="_keyName">Название ключевого поля в таблице</param>
        protected void Initialize(GridControl _baseGridControl, GridView _baseView, string _keyName)
        {
            GridControl = _baseGridControl;
            GridListView = _baseView;
            KeyField = _keyName;
            GridListView.FocusedRowChanged += gridListView_FocusedRowChanged;
        }

        #region IBaseListView members

        /// <summary>
        /// Источник данных для элемента списка
        /// </summary>
        public virtual DataTable ElemList
        {
            set
            {
                if (value != null)
                {
                    value.PrimaryKey = new DataColumn[] { value.Columns[KeyField] };
                }

                GridControl.DataSource = value;
                GridListView.BestFitColumns();
            }

            get
            {
                if (GridControl != null)
                {
                    return (DataTable)GridControl.DataSource;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Получить наименование текущего элемента списка
        /// </summary>
        /// <returns>Наименование текущего элемента списка</returns>
        public virtual string GetCurrentItemShortName()
        {
            return "";
        }

        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID элемента в списке
        /// </summary>
        /// <param name="_id">ID элемента в списке</param>
        public void LocateToId(string _id)
        {
            LocateToId(_id, false);
        }

        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID элемента в списке
        /// </summary>
        /// <param name="_id">ID элемента в списке</param>
        /// <param name="_forceRowChanged">Признак необходимости вызова RowChanged</param>
        public void LocateToId(string _id, bool _forceRowChanged)
        {
            int _handle = GridListView.LocateByDisplayText(0, GridListView.Columns[KeyField], _id);

            GridListView.ClearSelection();
            GridListView.SelectRow(_handle < 0 ? 0 :_handle);
            GridListView.FocusedRowHandle = _handle;

            if (_forceRowChanged)
            {
                Presenter.OnRowChanged(_id);
            }
        }

        /// <summary>
        /// Обновить список
        /// </summary>
        public void RefreshList()
        {
            Presenter.RefreshList();
        }

        /// <summary>
        /// Обновляет состояние глобальных кнопок, исходя из текущего выбранного элемента
        /// </summary>
        public void UpdateGlobalButtonsForCurrentItem()
        {
            Presenter.UpdateGlobalButtonsForCurrentItem();
        }

        /// <summary>
        /// Экспорт в Excel
        /// </summary>
        /// <param name="_filename">Имя файла для экспорта</param>
        public void ExportToExcel(string _filename)
        {
            GridListView.ExportToExcelOld(_filename);
        }

        /// <summary>
        /// Очистить список
        /// </summary>
        public void ClearList()
        {
            ElemList = null;
        }

        #endregion

        /// <summary>
        /// На изменение текущей строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridListView_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Presenter.OnRowChanged(GridListView.GetFocusedRowCellDisplayText(KeyField));
        }
    }
}