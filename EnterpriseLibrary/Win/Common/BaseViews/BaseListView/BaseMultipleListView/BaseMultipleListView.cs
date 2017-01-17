using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView.BaseMultipleListView
{
    /// <summary>
    /// Базовый класс для многострочных списков
    /// </summary>
    public class BaseMultipleListView : BaseListView, IBaseMultipleListView
    {
        /// <summary>
        /// Выполняет инициализацию вида
        /// </summary>
        /// <param name="_baseGridControl">Основная таблица списка (контрол)</param>
        /// <param name="_baseView">Основная таблица списка (вью)</param>
        /// <param name="_keyName">Название ключевого поля в таблице</param>
        public new void Initialize(GridControl _baseGridControl, GridView _baseView, string _keyName)
        {
            base.Initialize(_baseGridControl, _baseView, _keyName);

            GridListView.OptionsSelection.MultiSelect = true;
        }

        /// <summary>
        /// Возвращает массив номеров выбранных строк
        /// </summary>
        public int[] SelectedRows
        {
            get
            {
                return GridListView.GetSelectedRows();
            }
        }

        /// <summary>
        /// Возвращает массив ID элементов в выбранных строках
        /// </summary>
        public string[] SelectedIds
        {
            get
            {
                string[] _ids = new string[SelectedRows.Length];

                for (int i = 0; i < SelectedRows.Length; i++)
                {
                    _ids[i] = GridListView.GetDataRow(SelectedRows[i])[KeyField].ToString();
                }

                return _ids;
            }
        }
    }
}