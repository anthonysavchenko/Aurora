namespace Taumis.Infrastructure.Interface.Constants
{
    /// <summary>
    /// Имена событий
    /// </summary>
    public static class ApplicationEventNames
    {
        /// <summary>
        /// Событие запускается для сообщения оболочке об обновлении панели состояния.
        /// </summary>
        public const string StatusUpdate = "event://MainStatusStrip/StatusUpdate";

        /// <summary>
        /// Событие запускается для сообщения оболочке об обновлении панели состояния.
        /// </summary>
        public const string ConnectUpdate = "event://MainStatusStrip/ConnectUpdate";

        /// <summary>
        /// Отобразить прогрессбар в строке состояния. 
        /// </summary>
        public const string ShowStatusProgressBar = "event://MainStatusStrip/ShowProgressBar";

        /// <summary>
        /// Скрыть прогрессбар в строке состояния. 
        /// </summary>
        public const string HideStatusProgressBar = "event://MainStatusStrip/HideProgressBar";

        /// <summary>
        /// Изменить состояние прогрессбара в строке состояния. 
        /// </summary>
        public const string ChangeStatusProgressBar = "event://MainStatusStrip/ChangeProgressBar";
    }
}
