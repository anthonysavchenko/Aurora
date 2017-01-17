using System;
using System.Windows.Forms;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Интерфейс сервиса контроля изменений значений контролов
    /// </summary>
    public interface IChangeEventHandlerService
    {
        /// <summary>
        /// Добавить в элемент управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_control">Элемент управления</param>
        /// <param name="_handler">Обработчик</param>
        void Bind(Control _control, EventHandler _handler);

        /// <summary>
        /// Добавить в коллекцию элементов управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_collection">Коллекция элементов управления</param>
        /// <param name="_handler">Обработчик</param>
        void Bind(Control.ControlCollection _collection, EventHandler _handler);

        /// <summary>
        /// Убрать из элемента управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_control">Элемент управления</param>
        /// <param name="_handler">Обработчик</param>
        void UnBind(Control _control, EventHandler _handler);

        /// <summary>
        /// Убрать из коллекции элементов управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_collection">Коллекция элементов управления</param>
        /// <param name="_handler">Обработчик</param>
        void UnBind(Control.ControlCollection _collection, EventHandler _handler);
    }
}
