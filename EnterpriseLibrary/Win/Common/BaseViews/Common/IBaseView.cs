using System.Windows.Forms;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.Common
{
    /// <summary>
    /// Интерфейс базового вида
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Возвращает родительский контрол для вью
        /// </summary>
        Control Parent { get; }

        /// <summary>
        /// Показывает сообщение пользователю
        /// </summary>
        /// <param name="_text">Текст сообщения</param>
        /// <param name="_caption">Заголовок окна</param>
        void ShowMessage(string _text, string _caption);
    }
}