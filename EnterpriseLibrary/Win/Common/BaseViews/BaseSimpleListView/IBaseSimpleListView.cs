using System.Data;

using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseSimpleListView
{
    /// <summary>
    /// Базовый интерфейс для простых списков типа справочников.
    /// </summary>
    public interface IBaseSimpleListView : IBaseView
    {
        /// <summary>
        /// Источник данных для элемента списка
        /// </summary>
        DataTable ElemList { get; set; }

        /// <summary>
        /// Идентификатор 
        /// </summary>
        string ID { set; }

        /// <summary>
        /// Получить ID текущего элемента списка.
        /// </summary>
        /// <returns>ID</returns>
        string GetCurrentItemId();
 
        /// <summary>
        /// Спозиционировать указатель на нужной строке по ID
        /// </summary>
        /// <param name="_id">ID элемента</param>
        void LocateToId(string _id);

        /// <summary>
        /// Процедура экспорта активного вида в MS Excel.
        /// </summary>
        void ExportToExcel(string _filename);

        /// <summary>
        /// Обновить список
        /// </summary>
        void RefreshList();

        /// <summary>
        /// Применить текущие изменения на списке
        /// </summary>
        void PostEditor();
    }
}
