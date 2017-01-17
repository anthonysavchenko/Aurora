using System.Data;

using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseListView
{
    public interface IBaseListView : IBaseView
    {
        /// <summary>
        /// Источник данных для элемента списка
        /// </summary>
        DataTable ElemList { get; set; }

        /// <summary>
        /// Получить наименование текущего элемента списка
        /// </summary>
        /// <returns>Наименование текущего элемента списка</returns>
        string GetCurrentItemShortName();

        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID элемента в списке
        /// </summary>
        /// <param name="_id">ID элемента в списке</param>
        void LocateToId(string _id);
        
        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID элемента в списке
        /// </summary>
        /// <param name="_id">ID элемента в списке</param>
        /// <param name="_forceRowChanged">Признак необходимости вызова RowChanged</param>
        void LocateToId(string _id, bool _forceRowChanged);

        /// <summary>
        /// Обновить список
        /// </summary>
        void RefreshList();

        /// <summary>
        /// Обновляет состояние глобальных кнопок, исходя из текущего выбранного элемента
        /// </summary>
        void UpdateGlobalButtonsForCurrentItem();

        /// <summary>
        /// Экспорт в Excel данных из таблицы
        /// </summary>
        /// <param name="_filename">Имя файла для экспорта</param>
        void ExportToExcel(string _filename);

        /// <summary>
        /// Очистить список
        /// </summary>
        void ClearList();
    }
}