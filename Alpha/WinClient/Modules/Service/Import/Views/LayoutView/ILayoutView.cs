using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using RefBook = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Интерфейс вью формы
    /// </summary>
    public interface ILayoutView : IBaseLayoutView
    {
        /// <summary>
        /// Полное имя файла
        /// </summary>
        string FilePath { set; get; }

        /// <summary>
        /// Полное имя файла для импорта абонентов
        /// </summary>
        string ImportCustomersFilePath { set; get; }

        /// <summary>
        /// Услуга
        /// </summary>
        RefBook.Service Service { get; }

        /// <summary>
        /// Подрядчик
        /// </summary>
        Contractor Contractor { get; }

        /// <summary>
        /// Тариф
        /// </summary>
        decimal Rate { get; }

        /// <summary>
        /// Только для квартир в собственности
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Услуги
        /// </summary>
        DataTable Services
        {
            set;
        }

        /// <summary>
        /// Подрядчики
        /// </summary>
        DataTable Contractors
        {
            set;
        }

        /// <summary>
        /// Путь к файлу шаблона
        /// </summary>
        string GisZhkhInputFilePath { get; }

        void ShowGisZhkhProgressBar();
        void HideGisZhkhProgressBar();
    }
}