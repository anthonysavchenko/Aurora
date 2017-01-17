using System.Drawing;

namespace Taumis.EnterpriseLibrary.Win.Modules.CommonModule
{
    /// <summary>
    /// Интерфейс контроллера юзкейза
    /// </summary>
    public interface ICommonModuleController
    {
        /// <summary>
        /// Заголовок основного окна по умолчанию
        /// </summary>
        string DefaultMainViewTitle
        {
            get;
        }

        /// <summary>
        /// Заголовок основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает заголовок в зависимости от того, запущен ли юзкейз,
        /// сохраняя текущие координаты и размер окна.
        /// </remarks>
        string MainViewTitle
        {
            set;
        }

        /// <summary>
        /// Координаты основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает координаты основного окна в зависимости от того,
        /// запущен ли юзкейз, сохраняя текущий заголовок и размер окна.
        /// get возвращет текущие или заданные координаты окна в зависимости от того,
        /// запущен ли юзкейз.
        /// </remarks>
        Point MainViewLocation
        {
            set;
            get;
        }

        /// <summary>
        /// Размер основного окна
        /// </summary>
        /// <remarks>
        /// set обновляет или задает размер основного окна в зависимости от того,
        /// запущен ли юзкейз, сохраняя текущий заголовок и координаты окна.
        /// get возвращет текущий или заданный размер окна в зависимости от того,
        /// запущен ли юзкейз.
        /// </remarks>
        Size MainViewSize
        {
            set;
            get;
        }

        /// <summary>
        /// Загрузить юзкейз
        /// </summary>
        void Load();

        /// <summary>
        /// Запустить юзкейз, найденный в загруженных юзкейзах по имени
        /// </summary>
        /// <param name="usecaseName">Имя юзкейза</param>
        /// <param name="parameters">Параметры запуска юзкейза</param>
        void RunUsecase(string usecaseName, object parameters);
    }
}
