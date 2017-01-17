using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Базовый класс для простых списков типа справочник.
    /// </summary>
    public class BaseSimpleListView : BaseView, IBaseSimpleListView
    {
        // Основная таблица
        private GridView _gridListView;
        private GridControl _gridControl;
        private string _keyField;

        /// <summary>
        /// Обработчик вида.
        /// </summary>
        private new IBaseSimpleListViewPresenter Presenter
        {
            get
            {
                return (IBaseSimpleListViewPresenter)base.Presenter;
            }
        }

        private BaseSimpleListViewMapper _baseSimpleListViewMapper;
        /// <summary>
        /// Класс для преобразования элементов домена в DevExpress элементы вида
        /// </summary>
        protected BaseSimpleListViewMapper GetBaseSimpleListViewMapper
        {
            get
            {
                if (_baseSimpleListViewMapper == null)
                {
                    _baseSimpleListViewMapper = new BaseSimpleListViewMapper(Presenter);
                }

                return _baseSimpleListViewMapper;
            }
        }


        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseSimpleListView()
        {
            _gridListView = null;
            _gridControl = null;
            _keyField = "";
        }

        /// <summary>
        /// Инициализация соответствий ID контролов.
        /// </summary>
        /// <param name="_baseGridControl">ID контрола списка</param>
        /// <param name="_baseView">ID вида списка</param>
        /// <param name="_keyName">Идентифекатор списка</param>
        public void Initialize(GridControl _baseGridControl, GridView _baseView, string _keyName, bool _useNavigator)
        {
            _gridControl = _baseGridControl;
            _gridListView = _baseView;
            _keyField = _keyName;

            // Добавить/Удалить встроенный навигатор.
            AddOnGridControlEmbeddedNavigator(_useNavigator);

            // Для подкрашивания фокуса. 
            // Есть справочники у которых всего одна колонка и фокус не подкрашивается.
            _gridListView.Appearance.FocusedCell.BackColor = System.Drawing.Color.AliceBlue;
            _gridListView.Appearance.FocusedCell.Options.UseBackColor = true;
        }

        #region Методы интерфейса
        
        /// <summary
        /// Источник данных для элемента списка
        /// </summary>
        public virtual DataTable ElemList
        {
            get
            {
                if (_gridControl != null)
                {
                    return (DataTable)_gridControl.DataSource;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _gridControl.DataSource = value;
                _gridListView.BestFitColumns();
            }
        }

        /// <summary>
        /// Идентификатор 
        /// </summary>
        public string ID
        {
            set { GetBaseSimpleListViewMapper.DomainToView(value, _gridListView, _keyField); }
        }

        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID
        /// </summary>
        /// <param name="_id"></param>
        public void LocateToId(string _id)
        {
            int _handle = _gridListView.LocateByDisplayText(0, _gridListView.Columns[_keyField], _id);

            _gridListView.ClearSelection();
            _gridListView.SelectRow(_handle);
            _gridListView.FocusedRowHandle = _handle;
        }

        /// <summary>
        /// Взятие текущего ID
        /// </summary>
        /// <returns>ID элемента</returns>
        public string GetCurrentItemId()
        {
            string _id;

            object _obj =
                _gridListView.GetRowCellValue(_gridListView.FocusedRowHandle, _keyField);

            if (_obj != null)
            {
                _id = _obj.ToString();
            }
            else
            {
                _id = "";
            }

            return _id;
        }

        /// <summary>
        /// Экспортировать активный вид в MS Excel.
        /// </summary>
        public void ExportToExcel(string _filename)
        {
            _gridListView.ExportToExcelOld(_filename);
        }

        /// <summary>
        /// Обновить список
        /// </summary>
        public void RefreshList()
        {
            Presenter.RefreshList();
        }

        #endregion

        #region Встроенный навигатор
       
        /// <summary>
        /// Добавить/Удалить встроенный навигатор.
        /// </summary>
        /// <param name="_param">true-добавить, false-удалить</param>
        private void AddOnGridControlEmbeddedNavigator(bool _param)
        {
            // Добавляем/удаляем встроенный навигатор.
            _gridControl.UseEmbeddedNavigator = _param;
            if (_param)
            {
                // Обработчик кнопок навигатора.
                _gridControl.EmbeddedNavigator.ButtonClick += EmbeddedNavigatorButtonClick;
            }
        }

        #endregion
        #region Для наследников
        /// <summary>
        /// Обработа реакция на нажатие кнопок навигатора
        /// </summary>
        protected virtual void EmbeddedNavigatorButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                case DevExpress.XtraEditors.NavigatorButtonType.Append:
                    // Нажатие кнопки "Добавить"
                    e.Handled = Presenter.NavigatorBtnAppend();
                    break;
                case DevExpress.XtraEditors.NavigatorButtonType.Remove:
                    // Нажатие кнопки "Удалить"
                    e.Handled = Presenter.NavigatorBtnRemove();
                    break;
                case DevExpress.XtraEditors.NavigatorButtonType.EndEdit:
                    // По окончании редактирования строки.
                    e.Handled = Presenter.NavigatorBtnEndEdit();
                    break;
            }
        }

        /// <summary>
        /// Применить текущие изменения на списке
        /// </summary>
        public virtual void PostEditor()
        {
            _gridListView.PostEditor();
        }
        #endregion
    }
}